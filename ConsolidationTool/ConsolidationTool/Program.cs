using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace ConsolidationTool
{
    public class Watcher
    {
        public static string mainFolder;
        private static string processedFolder;
        private static string notApplicableFolder;
        private static string openFile;
        private static string[] ExcelTypes = new string[4] {".xlsx", ".xls", ".xlsm", ".xlsb"};

        public Watcher(string path, string filterFile)
        {
            FileSystemWatcher systemWatcher = new FileSystemWatcher(path);
            systemWatcher.NotifyFilter = NotifyFilters.Attributes |
                NotifyFilters.CreationTime |
                NotifyFilters.DirectoryName |
                NotifyFilters.FileName |
                NotifyFilters.LastAccess |
                NotifyFilters.LastWrite |
                NotifyFilters.Security |
                NotifyFilters.Size;
            systemWatcher.EnableRaisingEvents = true;
            systemWatcher.Created += OnCreated;
            systemWatcher.Deleted += OnDeleted;
            systemWatcher.Renamed += OnChanged;

            processedFolder = $@"{path}\Processed Files";
            notApplicableFolder = $@"{path}\Not Applicable Files";
            mainFolder = path;

            Directory.CreateDirectory(processedFolder);
            Directory.CreateDirectory(notApplicableFolder);
        }

        #region Excel Methods

        private static void CopyExcelSheetToMaster(FileSystemEventArgs e)
        {
            Excel.Application application = new Excel.Application();
            string sourceFile = e.FullPath;
            string targetFile = $@"{mainFolder}\MasterSpreadsheet.xlsx";
            Excel.Workbook sourceWorkbook = application.Workbooks.Open(sourceFile);
            Excel.Workbook masterWorkbook;

            //Gets the reference to the master file, if not found, creates it.
            if (File.Exists(targetFile))
                masterWorkbook = application.Workbooks.Open(targetFile);
            else
            {
                masterWorkbook = application.Workbooks.Add(Type.Missing);
                masterWorkbook.SaveAs(targetFile);
            }

            //When the reference is ready, copy the sheets from the source file to the master file.
            int sheetCount = masterWorkbook.Worksheets.Count;
            foreach (Excel.Worksheet sheet in sourceWorkbook.Worksheets)
            {
                sheet.Copy(After: masterWorkbook.Worksheets[sheetCount]);
                sheetCount++;
            }
            masterWorkbook.Save();
            
            //Cleaning process
            CloseExcelWorkbook(sourceWorkbook);
            sourceWorkbook = null;
            CloseExcelWorkbook(masterWorkbook);
            masterWorkbook = null;
            QuitExcelApplication(application);
            application = null;
            GC.Collect();
        }

        private static void CopyExcelSheetToMaster(string fullpath)
        {
            Excel.Application application = new Excel.Application();
            string sourceFile = fullpath;
            string targetFile = $@"{mainFolder}\MasterSpreadsheet.xlsx";
            Excel.Workbook sourceWorkbook = application.Workbooks.Open(sourceFile);
            Excel.Workbook masterWorkbook;

            //Gets the reference to the master file, if not found, creates it.
            if (File.Exists(targetFile))
                masterWorkbook = application.Workbooks.Open(targetFile);
            else
            {
                masterWorkbook = application.Workbooks.Add(Type.Missing);
                masterWorkbook.SaveAs(targetFile);
            }

            //When the reference is ready, copy the sheets from the source file to the master file.
            int sheetCount = masterWorkbook.Worksheets.Count;
            foreach (Excel.Worksheet sheet in sourceWorkbook.Worksheets)
            {
                sheet.Copy(After: masterWorkbook.Worksheets[sheetCount]);
                sheetCount++;
            }
            masterWorkbook.Save();

            //Cleaning process
            CloseExcelWorkbook(sourceWorkbook);
            sourceWorkbook = null;
            CloseExcelWorkbook(masterWorkbook);
            masterWorkbook = null;
            QuitExcelApplication(application);
            application = null;
            GC.Collect();
        }

        private static void CloseExcelWorkbook(Excel.Workbook workbook)
        {
            workbook.Close();
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(workbook);
        }

        private static void QuitExcelApplication(Excel.Application app)
        {
            app.Quit();
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(app);
        }

        #endregion

        #region Listeners

        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            string type = Path.GetExtension(e.FullPath).ToLower();
            //If the file is a temporary excel file it skips it.
            if (type == ".tmp" || e.Name.Substring(0, 2) == "~$" || type == "")
                return;

            if (ExcelTypes.Contains(type))
            {
                CopyExcelSheetToMaster(e);
                //Check if the file already exists in the location to take some action.
                if (!File.Exists($@"{processedFolder}\{e.Name}"))
                {
                    File.Move(e.FullPath, $@"{processedFolder}\{e.Name}");
                }
                else
                {
                    DialogResult overwriteResult;
                    overwriteResult = MessageBox.Show($"File '{e.Name}' already exist in the processed folder, do you want to overwrite it?",
                        "Duplicate file notification", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (overwriteResult == DialogResult.Yes)
                    {
                        File.Delete($@"{processedFolder}\{e.Name}");
                        File.Move(e.FullPath, $@"{processedFolder}\{e.Name}");
                    }
                    else
                    {
                        //Creates a copy of an existing file by appending a copy count to the file name.
                        string fileName = e.Name.Split('.')[0];
                        int fileCount = Directory.EnumerateFiles(processedFolder, fileName, SearchOption.AllDirectories).Count();
                        File.Move(e.FullPath, $@"{processedFolder}\{fileName}({fileCount + 1}){type}");
                    }
                }
            }
            else
            {
                try
                {
                    File.Move(e.FullPath, $@"{notApplicableFolder}\{e.Name}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void OnDeleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine(e.FullPath);
            string type = Path.GetExtension(e.FullPath).ToLower();
            //Check if it is a temporary file and is not the master file.
            if (e.Name != "MasterSpreadsheet.xlsx" && ExcelTypes.Contains(type) && e.Name.Substring(0, 2) == "~$")
            {
                //If the deleted temporary file is the same as the open file, it indicates that the file has been closed and can be processed.
                if (openFile != null && openFile == e.Name.Split('$')[1])
                {
                    //Check if file exist in the main folder
                    if (!File.Exists(($@"{mainFolder}\{openFile}")))
                        return;
                    CopyExcelSheetToMaster($@"{mainFolder}\{openFile}");
                    //Check if the file already exists in the location to take some action.
                    if (!File.Exists($@"{processedFolder}\{openFile}"))
                        File.Move($@"{mainFolder}\{openFile}", $@"{processedFolder}\{openFile}");
                    else
                    {
                        DialogResult overwriteResult;
                        overwriteResult = MessageBox.Show($"File '{openFile}' already exist in the processed folder, do you want to overwrite it?",
                            "Duplicate file notification", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (overwriteResult == DialogResult.Yes)
                        {
                            File.Delete($@"{processedFolder}\{openFile}");
                            File.Move($@"{mainFolder}\{openFile}", $@"{processedFolder}\{openFile}");
                        }
                        else
                        {
                            //Creates a copy of an existing file by appending a copy count to the file name.
                            string fileName = openFile.Split('.')[0];
                            int fileCount = Directory.EnumerateFiles(processedFolder, fileName, SearchOption.AllDirectories).Count();
                            File.Move($@"{mainFolder}\{openFile}", $@"{processedFolder}\{fileName}({fileCount + 1}){type}");
                        }
                    }
                }
            }
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine(e.FullPath);
            string type = Path.GetExtension(e.FullPath).ToLower();
            if (e.Name != "MasterSpreadsheet.xlsx" && ExcelTypes.Contains(type))
            {
                openFile = e.Name;
            }
        }

        #endregion
    }

    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!CheckIfWatchedFolderExist())
            {
                MessageBox.Show("To start, browse and select the folder path to monitor.",
                    "Initial indication", MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);
            }
            Application.Run(new MainForm());
        }

        static bool CheckIfWatchedFolderExist()
        {
            if (Properties.Settings.Default["WatchedFolder"] == null || Properties.Settings.Default["WatchedFolder"].ToString() == "")
                return false;
            return true;
        }
    }
}
