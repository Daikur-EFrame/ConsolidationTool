using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsolidationTool
{
    public partial class MainForm : Form
    {
        public Watcher watcher;

        public MainForm()
        {
            InitializeComponent();
            SetPathSavedToTextBox();
        }

        private void BrowseFolderbtn_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            if(folderBrowser.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default["WatchedFolder"] = folderBrowser.SelectedPath;
                watchedFoldertxtBox.Text = folderBrowser.SelectedPath;
                statuslbl.Text = "Watching folder";
                Properties.Settings.Default.Save();
                InitializeWatcher(folderBrowser.SelectedPath);
            }
        }

        private void SetPathSavedToTextBox()
        {
            if(Properties.Settings.Default["WatchedFolder"] != null && Properties.Settings.Default["WatchedFolder"].ToString() != "")
            {
                watchedFoldertxtBox.Text = Properties.Settings.Default["WatchedFolder"].ToString();
                statuslbl.Text = "Watching folder";
                InitializeWatcher(Properties.Settings.Default["WatchedFolder"].ToString());

            }
        }

        public void InitializeWatcher(string folder)
        {
            watcher = new Watcher(folder, "*.xlsx");
        }
    }
}
