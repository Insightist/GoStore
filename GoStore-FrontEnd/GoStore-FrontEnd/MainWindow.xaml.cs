using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Xps.Packaging;


using LibGit2Sharp;

using Microsoft.Win32;
using Microsoft.Office.Interop.Word;

namespace GoStore_FrontEnd
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        #region Properties
        // Properties
        CommitsMap_Manager  _cmapManager;
        List<string>        _unstagedFiles;
        List<string>        _stagedFiles;
        List<string>        _historyFiles;
        List<string>        _historyFilesShas;
        string              _repoPath;

        public string       installedPath;
        

        #endregion Properties

        #region Constructors
        // Constructors
        public MainWindow()
        {
            InitializeComponent();

            _unstagedFiles = new List<string>();
            _stagedFiles = new List<string>();
            _historyFiles = new List<string>();
            _historyFilesShas = new List<string>();

            // !!! For Test Only
            installedPath = "F:\\FireField\\3";
        }

        #endregion Constructors

        #region Window events
        private void winMain_Loaded(object sender, RoutedEventArgs e)
        {
            cm1.CommitSelected += SelectedCommit;

            lblCommit.Content = String.Empty;
            lblAuthorName.Content = String.Empty;
            lblDate.Content = String.Empty;
            lblEmail.Content = String.Empty;
        }

        #endregion Window events

        #region Graphical Interaction

        private void btn_newRepo_MouseEnter(object sender, MouseEventArgs e)
        {
            Color clr = new Color();
            clr.A = 255;
            clr.R = 218;
            clr.G = 114;
            clr.B = 90;

            btn_newRepo.Background = new SolidColorBrush(clr);
        }

        private void btn_newRepo_MouseLeave(object sender, MouseEventArgs e)
        {
            Color clr = new Color();
            clr.A = 255;
            clr.R = 210;
            clr.G = 70;
            clr.B = 37;

            btn_newRepo.Background = new SolidColorBrush(clr);
        }

        private void btn_openRepo_MouseEnter(object sender, MouseEventArgs e)
        {
            Color clr = new Color();
            clr.A = 255;
            clr.R = 218;
            clr.G = 114;
            clr.B = 90;

            btn_openRepo.Background = new SolidColorBrush(clr);
        }

        private void btn_openRepo_MouseLeave(object sender, MouseEventArgs e)
        {
            Color clr = new Color();
            clr.A = 255;
            clr.R = 210;
            clr.G = 70;
            clr.B = 37;

            btn_openRepo.Background = new SolidColorBrush(clr);
        }

        private void btn_setting_MouseEnter(object sender, MouseEventArgs e)
        {
            Color clr = new Color();
            clr.A = 255;
            clr.R = 218;
            clr.G = 114;
            clr.B = 90;

            btn_setting.Background = new SolidColorBrush(clr);
        }

        private void btn_setting_MouseLeave(object sender, MouseEventArgs e)
        {
            Color clr = new Color();
            clr.A = 255;
            clr.R = 210;
            clr.G = 70;
            clr.B = 37;

            btn_setting.Background = new SolidColorBrush(clr);
        }

        private void btn_max_MouseEnter(object sender, MouseEventArgs e)
        {
            Color clr = new Color();
            clr.A = 255;
            clr.R = 37;
            clr.G = 116;
            clr.B = 210;

            btn_max.Background = new SolidColorBrush(clr);
        }

        private void btn_max_MouseLeave(object sender, MouseEventArgs e)
        {
            Color clr = new Color();
            clr.A = 255;
            clr.R = 210;
            clr.G = 70;
            clr.B = 37;

            btn_max.Background = new SolidColorBrush(clr);
        }

        private void btn_close_MouseEnter(object sender, MouseEventArgs e)
        {
            Color clr = new Color();
            clr.A = 255;
            clr.R = 255;
            clr.G = 100;
            clr.B = 50;

            btn_close.Background = new SolidColorBrush(clr);
        }

        private void btn_close_MouseLeave(object sender, MouseEventArgs e)
        {
            Color clr = new Color();
            clr.A = 255;
            clr.R = 210;
            clr.G = 70;
            clr.B = 37;

            btn_close.Background = new SolidColorBrush(clr);
        }

        private void btn_min_MouseEnter(object sender, MouseEventArgs e)
        {
            Color clr = new Color();
            clr.A = 255;
            clr.R = 37;
            clr.G = 116;
            clr.B = 210;

            btn_min.Background = new SolidColorBrush(clr);
        }

        private void btn_min_MouseLeave(object sender, MouseEventArgs e)
        {
            Color clr = new Color();
            clr.A = 255;
            clr.R = 210;
            clr.G = 70;
            clr.B = 37;

            btn_min.Background = new SolidColorBrush(clr);
        }

        private void btn_notes_more_MouseEnter(object sender, MouseEventArgs e)
        {
            Color clr = new Color();
            clr.A = 255;
            clr.R = 252;
            clr.G = 205;
            clr.B = 182;

            btn_notes_more.Background = new SolidColorBrush(clr);
        }

        private void btn_notes_more_MouseLeave(object sender, MouseEventArgs e)
        {
            Color clr = new Color();
            clr.A = 0;
            clr.R = 0;
            clr.G = 0;
            clr.B = 0;

            btn_notes_more.Background = new SolidColorBrush(clr);
        }

        #endregion Graphical Interaction

        #region Left-Button-Down events

        private void lbl_caption_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AboutUs aboutWin = new AboutUs();

            aboutWin.ShowDialog();
        }

        private void frm_TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btn_close_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void btn_max_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(this.WindowState == System.Windows.WindowState.Normal)
            {
                this.WindowState = System.Windows.WindowState.Maximized;
                btn_max.Content = "↓";
            }
            else if(this.WindowState == System.Windows.WindowState.Maximized)
            {
                this.WindowState = System.Windows.WindowState.Normal;
                btn_max.Content = "↑";
            }
        }

        private void btn_min_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void btn_newRepo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fd = new System.Windows.Forms.FolderBrowserDialog();
            fd.ShowNewFolderButton = true;
            fd.Description = "Select a folder that you want to make it as a repository.";            
            
            string path;

            try
            {
                if (System.Windows.Forms.DialogResult.OK == fd.ShowDialog())
                {
                    path = fd.SelectedPath;

                    Repository.Init(path);

                    _cmapManager = new CommitsMap_Manager(cm1, path);
                    _cmapManager.Load();

                    cm1.Draw(500);

                    SuccessWindow_CreateRepo succWin = new SuccessWindow_CreateRepo();
                    succWin.ShowDialog();
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Error occurs. Cannot create a new repoistory", "Warning");
            }

        }

        private void btn_openRepo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fd = new System.Windows.Forms.FolderBrowserDialog();
            fd.ShowNewFolderButton = false;
            fd.Description = "Select repoistory's directory";

            string branch;
            int counter = 0;

            try
            {
                if (System.Windows.Forms.DialogResult.OK == fd.ShowDialog())
                {
                    // Clean up first.
                    _stagedFiles.Clear();
                    _unstagedFiles.Clear();
                    lst_historyFiles.Items.Clear();
                    lst_staged.Items.Clear();
                    lst_unstaged.Items.Clear();


                    // Set path.
                    _repoPath = fd.SelectedPath;

                    // Create new manager
                    _cmapManager = new CommitsMap_Manager(cm1, _repoPath);
                    _cmapManager.Load();

                    // Draw the commits map
                    cm1.Draw(500);

                    // Open Repository
                    Repository repo = new Repository(_repoPath);

                    if(null != repo.Head.Tip)
                    {
                        lblEmail.Content = repo.Head.Tip.Author.Email;
                        lblAuthorName.Content = repo.Head.Tip.Author.Name;
                        lblDate.Content = repo.Head.Tip.Author.When.DateTime.ToString();
                        lblCommit.Content = repo.Head.Tip.Sha;
                        txt_notes.Text = repo.Head.Tip.Message;
                    }

                    RefreshStatus(repo);

                    counter = 0;
                    foreach(var entry in repo.Branches)
                    {
                        if (entry.IsRemote == true)
                            continue;

                        branch = entry.FriendlyName;

                        cbx_branch.Items.Add(branch);

                        if(entry.IsCurrentRepositoryHead)
                        {
                            cbx_branch.SelectedIndex = counter;
                        }

                        ++counter;
                    }

                    
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurs. Cannot open the repository", "Warning");
            }
        }

        private void btn_setting_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Setting winSetting = new Setting();

            winSetting.ShowDialog();
        }
        
        private void btn_notes_more_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NoteWin noteWin = new NoteWin();

            noteWin.notes = txt_notes.Text;
            noteWin.Show();
        }

        #endregion Left-Button-Down events

        #region Click events

        private void btn_preview_Click(object sender, RoutedEventArgs e)
        {
            if (_repoPath == null)
                return;

            if (_historyFiles.Count() <= 0)
                return;

            if (lst_historyFiles.SelectedIndex < 0)
                return;


            Repository repo;
            Blob data;
            Stream dataSource = null;
            BinaryWriter dataDest = null;
            FileStream fs = null;
            XpsDocument doc = null;
            byte[] buffer = new byte[2048];
            int bytesRead = 0;
            
            string file;

            try
            {
                this.Cursor = Cursors.Wait;

                // Open the repository
                repo = new Repository(_repoPath);

                // get file path
                file = _historyFiles[lst_historyFiles.SelectedIndex];

                // find out the file from repository
                data = repo.Lookup<Blob>(_historyFilesShas[lst_historyFiles.SelectedIndex]);


                // !!! Test code ===============================================
                string postfix = file.Substring(file.LastIndexOf('.') + 1);
                if (postfix != "doc" && postfix != "docx")
                    return;
                // =============================================================

                // output
                dataSource = data.GetContentStream();

                fs = new FileStream(installedPath + "\\tempData\\origin\\checkout-file-preview.ckf", FileMode.Create);
                dataDest = new BinaryWriter(fs);

                bytesRead = dataSource.Read(buffer, 0, 2048);
                while (bytesRead > 0)
                {
                    dataDest.Write(buffer, 0, bytesRead);
                    bytesRead = dataSource.Read(buffer, 0, 2048);
                }

                dataDest.Close();
                dataDest = null;
           
                fs.Close();
                fs = null;
              
                dataSource.Close();
                dataSource = null;

                doc = ConvertWordDocToXPSDoc(installedPath + "\\tempData\\origin\\checkout-file-preview.ckf",
                    installedPath + "\\tempData\\converted\\checkout-file-preview.cvt");

                //dvr_docView
                dvr_docView.Document = doc.GetFixedDocumentSequence();
                
            }
            catch(Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (dataDest != null)
                {
                    dataDest.Close();
                    dataDest = null;
                }

                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }

                if (dataSource != null)
                {
                    dataSource.Close();
                    dataSource = null;
                }

                if (doc != null)
                {
                    doc.Close();
                    doc = null;
                }

                this.Cursor = Cursors.Arrow;
            }
        }


        private void btn_checkoutAs_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fd = new System.Windows.Forms.FolderBrowserDialog();
            fd.ShowNewFolderButton = true;
            fd.Description = "Select a folder that you want to checkout into.";

            string path;
            string selectedline;
            string sha;
            Repository repo;
            Blob data;
            Stream dataSource = null;
            BinaryWriter dataDest = null;
            FileStream fs = null;
            Exception thisEx;

            byte[] buffer = new byte[2048];
            int bytesRead = 0;
 
            int i = 0;

            try
            {
                if (System.Windows.Forms.DialogResult.OK == fd.ShowDialog())
                {
                    this.Cursor = Cursors.Wait;

                    repo = new Repository(_repoPath);


                    foreach (var item in lst_historyFiles.SelectedItems)
                    {
                        path = fd.SelectedPath;

                        selectedline = item as string;

                        i = 0;
                        foreach(var line in _historyFiles)
                        {
                            if (selectedline == line)
                                break;
                            ++i;
                        }

                        // Exception: can not find out the commit matched.
                        if (i >= _historyFiles.Count())
                        {
                            thisEx = new Exception("can not find out the commit matched");
                            throw thisEx;
                        }

                        sha = _historyFilesShas[i];
                        data = repo.Lookup<Blob>(_historyFilesShas[i]);
                        dataSource = data.GetContentStream();

                        path += "\\" + selectedline;

                        ConfirmDirectory(path);

                        fs = new FileStream(path, FileMode.CreateNew);
                        dataDest = new BinaryWriter(fs);

                        bytesRead = dataSource.Read(buffer, 0, 2048);
                        while (bytesRead > 0)
                        {
                            dataDest.Write(buffer, 0, bytesRead);
                            bytesRead = dataSource.Read(buffer, 0, 2048);
                        }

                        dataDest.Close();
                        dataDest = null;

                        fs.Close();
                        fs = null;

                        dataSource.Close();
                        dataSource = null;
                    }
                }
            }
            catch(Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (dataDest != null)
                {
                    dataDest.Close();
                    dataDest = null;
                }

                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }

                if (dataSource != null)
                {
                    dataSource.Close();
                    dataSource = null;
                }

                this.Cursor = Cursors.Arrow;
            }

        }

        private void btn_checkout_Click(object sender, RoutedEventArgs e)
        {
            string path;
            string selectedline;
            string sha;
            Repository repo;
            Blob data;
            Stream dataSource = null;
            BinaryWriter dataDest = null;
            FileStream fs = null;
            Exception thisEx;

            byte[] buffer = new byte[2048];
            int bytesRead = 0;

            int i = 0;

            try
            {
                this.Cursor = Cursors.Wait;

                repo = new Repository(_repoPath);


                foreach (var item in lst_historyFiles.SelectedItems)
                {
                    path = _repoPath;

                    selectedline = item as string;

                    i = 0;
                    foreach (var line in _historyFiles)
                    {
                        if (selectedline == line)
                            break;
                        ++i;
                    }

                    // Exception: can not find out the commit matched.
                    if (i >= _historyFiles.Count())
                    {
                        thisEx = new Exception("can not find out the commit matched");
                        throw thisEx;
                    }

                    sha = _historyFilesShas[i];
                    data = repo.Lookup<Blob>(_historyFilesShas[i]);
                    dataSource = data.GetContentStream();

                    path += "\\" + selectedline;

                    ConfirmDirectory(path);

                    fs = new FileStream(path, FileMode.CreateNew);
                    dataDest = new BinaryWriter(fs);

                    bytesRead = dataSource.Read(buffer, 0, 2048);
                    while (bytesRead > 0)
                    {
                        dataDest.Write(buffer, 0, bytesRead);
                        bytesRead = dataSource.Read(buffer, 0, 2048);
                    }

                    dataDest.Close();
                    dataDest = null;

                    fs.Close();
                    fs = null;

                    dataSource.Close();
                    dataSource = null;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (dataDest != null)
                {
                    dataDest.Close();
                    dataDest = null;
                }

                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }

                if (dataSource != null)
                {
                    dataSource.Close();
                    dataSource = null;
                }

                this.Cursor = Cursors.Arrow;
            }
        }

        #endregion

        #region Double-Click events

        private void frm_TitleBar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (WindowState.Normal == this.WindowState)
            {
                this.WindowState = WindowState.Maximized;
            }
            else if (WindowState.Maximized == this.WindowState)
            {
                this.WindowState = WindowState.Normal;
            }
        }


        #endregion Double-Click events

        private void SelectedCommit(object sender, RoutedEventArgs e)
        {
            RoutedPropertyChangedEventArgs<string> arg = e as RoutedPropertyChangedEventArgs<string>;

            tbc_cards.SelectedIndex = 1;

            string sha;           
            TimeSpan timeLapsed;

            try
            {
                Repository repo = new Repository(_repoPath);

                sha = arg.NewValue;
                Commit cmt = repo.Lookup<Commit>(sha);

                lblAuthorName.Content = cmt.Author.Name;
                lblEmail.Content = cmt.Author.Email;
                lblDate.Content = cmt.Committer.When.DateTime.ToString();
                lblCommit.Content = cmt.Sha;
                txt_notes.Text = cmt.Message;

                // Clean up list
                lst_historyFiles.Items.Clear(); 
                _historyFiles.Clear();
                _historyFilesShas.Clear();

                // Find out all the files in the commit
                CommitTreeTraversal(_historyFiles, _historyFilesShas, cmt.Tree);

                foreach (var item in _historyFiles)
                {
                    lst_historyFiles.Items.Add(item);
                }


                // Show time interval
                timeLapsed = System.DateTimeOffset.Now - cmt.Committer.When;

                if (timeLapsed.TotalSeconds < 60)
                {
                    lbl_dayspassed.Content = timeLapsed.TotalSeconds.ToString("0.0") + " seconds ago";
                }
                else if (timeLapsed.TotalMinutes < 60)
                {
                    lbl_dayspassed.Content = timeLapsed.TotalMinutes.ToString("0.0") + " minutes ago";
                }
                else if (timeLapsed.TotalHours < 24)
                {
                    lbl_dayspassed.Content = timeLapsed.TotalHours.ToString("0.0") + " hours ago";
                }
                else
                {
                    lbl_dayspassed.Content = timeLapsed.TotalDays.ToString("0.0")  + " days ago";
                }
            }
            catch(Exception)
            {

            }
            finally
            {

            }

        }

        private void CommitTreeTraversal(List<string> filePaths, List<string> shas, GitObject entry)
        {
            Tree tree = entry as Tree;

            foreach (var treeEntry in tree)
            {
                if (treeEntry.TargetType == TreeEntryTargetType.Blob)
                {
                    filePaths.Add(treeEntry.Path);
                    shas.Add(treeEntry.Target.Sha);
                    continue;
                }
                else if (treeEntry.TargetType == TreeEntryTargetType.Tree)
                {
                    CommitTreeTraversal(filePaths, shas, treeEntry.Target);
                }                
            }
        }

        private void RefreshStatus(Repository repo)
        {
            RepositoryStatus status = repo.RetrieveStatus();
            int chrIdx = 0;

            foreach (var entry in status)
            {
                chrIdx = entry.FilePath.LastIndexOf('\\');
                if (chrIdx != 0)
                    chrIdx += 1;

                // Unstaged
                if (entry.State == FileStatus.ModifiedInWorkdir)
                {
                    lst_unstaged.Items.Add("[ modified ]  【 " + entry.FilePath.Substring(chrIdx) + " 】  @@ " + entry.FilePath);
                    _unstagedFiles.Add(entry.FilePath);
                }
                else if (entry.State == FileStatus.NewInWorkdir)
                {
                    lst_unstaged.Items.Add("[ new ]  【 " + entry.FilePath.Substring(chrIdx) + " 】  @@ " + entry.FilePath);
                    _unstagedFiles.Add(entry.FilePath);
                }
                else if (entry.State == FileStatus.RenamedInWorkdir)
                {
                    lst_unstaged.Items.Add("[ removed ]  【 " + entry.FilePath.Substring(chrIdx) + " 】  @@ " + entry.FilePath);
                    _unstagedFiles.Add(entry.FilePath);
                }
                else if (entry.State == FileStatus.DeletedFromWorkdir)
                {
                    lst_unstaged.Items.Add("[ deleted ]  【 " + entry.FilePath.Substring(chrIdx) + " 】  @@ " + entry.FilePath);
                    _unstagedFiles.Add(entry.FilePath);
                }
                else if (entry.State == FileStatus.RenamedInWorkdir)
                {
                    lst_unstaged.Items.Add("[ renamed ]  【 " + entry.FilePath.Substring(chrIdx) + " 】  @@ " + entry.FilePath);
                    _unstagedFiles.Add(entry.FilePath);
                }

                    // Staged
                else if (entry.State == FileStatus.ModifiedInIndex)
                {
                    lst_staged.Items.Add("[ modified ]  【 " + entry.FilePath.Substring(chrIdx) + " 】  @@ " + entry.FilePath);
                    _stagedFiles.Add(entry.FilePath);
                }
                else if (entry.State == FileStatus.NewInIndex)
                {
                    lst_staged.Items.Add("[ new ]  【 " + entry.FilePath.Substring(chrIdx) + " 】  @@ " + entry.FilePath);
                    _stagedFiles.Add(entry.FilePath);
                }
                else if (entry.State == FileStatus.RenamedInIndex)
                {
                    lst_staged.Items.Add("[ removed ]  【 " + entry.FilePath.Substring(chrIdx) + " 】  @@ " + entry.FilePath);
                    _stagedFiles.Add(entry.FilePath);
                }
                else if (entry.State == FileStatus.DeletedFromIndex)
                {
                    lst_staged.Items.Add("[ deleted ]  【 " + entry.FilePath.Substring(chrIdx) + " 】  @@ " + entry.FilePath);
                    _stagedFiles.Add(entry.FilePath);
                }
                else if (entry.State == FileStatus.RenamedInIndex)
                {
                    lst_staged.Items.Add("[ renamed ]  【 " + entry.FilePath.Substring(chrIdx) + " 】  @@ " + entry.FilePath);
                    _stagedFiles.Add(entry.FilePath);
                }

            }
        }

        private void ConfirmDirectory(string path)
        {
            try
            {
                string dirpath = path;
                int idx = dirpath.LastIndexOf('\\');
                dirpath = dirpath.Substring(0, idx);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(dirpath);
                }
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// This method takes a Word document full path and new XPS document full path and name
        /// and returns the new XpsDocument
        /// </summary>
        /// <param name="wordDocName"></param>
        /// <param name="xpsDocName"></param>
        /// <returns></returns>
        private XpsDocument ConvertWordDocToXPSDoc(string wordDocName, string xpsDocName)
        {
            // Create a WordApplication and add Document to it
            Microsoft.Office.Interop.Word.Application
                wordApplication = new Microsoft.Office.Interop.Word.Application();
            wordApplication.Documents.Add(wordDocName);


            Document doc = wordApplication.ActiveDocument;
            // You must ensure you have Microsoft.Office.Interop.Word.Dll version 12.
            // Version 11 or previous versions do not have WdSaveFormat.wdFormatXPS option
            try
            {
                doc.SaveAs(xpsDocName, WdSaveFormat.wdFormatXPS);
                wordApplication.Quit();

                XpsDocument xpsDoc = new XpsDocument(xpsDocName, System.IO.FileAccess.Read);
                return xpsDoc;
            }
            catch (Exception exp)
            {
                string str = exp.Message;
            }
            return null;
        }



    }
}
