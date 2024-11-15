using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Drive_FIle_Uploader_v3
{
    public partial class Form1 : Form
    {
        private UserCredential credential;
        private DriveService service;

        public Form1()
        {
            InitializeComponent();
            progressBar.Maximum = 100;

            btnChooseFile.Click += BtnChooseFile_Click;
            btnUpload.Click += BtnUpload_Click;

            fileListView.AllowDrop = true;
            fileListView.DragEnter += new DragEventHandler(fileListView_DragEnter);
            fileListView.DragDrop += new DragEventHandler(fileListView_DragDrop);

            btnLogin.Click += BtnLogin_Click;
            btnLogout.Click += BtnLogout_Click;
            btnRemoveFile.Click += BtnRemoveFile_Click;
            btnSelectAll.Click += BtnSelectAll_Click;
            btnRemoveSelected.Click += BtnRemoveSelected_Click;

            btnChooseFile.Enabled = false;
            btnUpload.Enabled = false;
            btnLogout.Enabled = false;
        }

        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            await InitializeDriveService();

            btnChooseFile.Enabled = true;
            btnUpload.Enabled = true;
            btnLogout.Enabled = true;
            btnLogin.Enabled = false;

            MessageBox.Show("Đăng nhập thành công!. Bây giờ bạn đã có thể upload file(s).");
        }


        private void BtnLogout_Click(object sender, EventArgs e)
        {
            // Xóa token.json để logout
            string credPath = "token.json";
            if (Directory.Exists(credPath))
            {
                Directory.Delete(credPath, true);
            }

            btnChooseFile.Enabled = false;
            btnUpload.Enabled = false;
            btnLogin.Enabled = true;
            btnLogout.Enabled = false;

            // Xóa tất cả các file trong ListView
            ClearUploadState();

            MessageBox.Show("Đăng xuất thành công. Hãy đăng nhập lại để có thể sử dụng.");
        }

        private async Task InitializeDriveService()
        {
            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { DriveService.Scope.DriveFile },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true));
            }

            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Drive API .NET Quickstart",
            });
        }

        // Hàm xử lý sự kiện DragEnter cho ListView
        void fileListView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }


        // Hàm xử lý sự kiện DragDrop cho ListView
        void fileListView_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                // Kiểm tra nếu file đã có trong danh sách thì bỏ qua
                if (!IsFileAlreadyAdded(file))
                {
                    // Thêm file vào ListView
                    ListViewItem item = new ListViewItem(Path.GetFileName(file))
                    {
                        Tag = file
                    };
                    item.SubItems.Add("Chưa upload");
                    fileListView.Items.Add(item);
                }
            }
        }

        private string PrepareFileName(string fileName)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(c, '_'); // Thay thế ký tự đặc biệt bằng '_'
            }
            return fileName;
        }

        private void BtnChooseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Chọn các tệp để upload lên Google Drive",
                Filter = "All files (*.*)|*.*",
                Multiselect = true // Cho phép chọn nhiều file
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Lặp qua tất cả các file đã chọn và thêm vào ListView
                foreach (string filePath in openFileDialog.FileNames)
                {
                    // Kiểm tra nếu file đã có trong danh sách thì bỏ qua
                    string safeFileName = PrepareFileName(Path.GetFileName(filePath));
                    if (!IsFileAlreadyAdded(filePath))
                    {
                        // Thêm file vào ListView
                        ListViewItem item = new ListViewItem(Path.GetFileName(filePath))
                        {
                            Tag = filePath
                        };
                        item.SubItems.Add("Chưa upload");
                        fileListView.Items.Add(item);
                    }
                }
            }
        }

        // Hàm để kiểm tra xem file đã có trong ListView hay chưa
        private bool IsFileAlreadyAdded(string filePath)
        {
            foreach (ListViewItem item in fileListView.Items)
            {
                if (item.Tag != null && item.Tag.ToString() == filePath)
                {
                    return true;
                }
            }
            return false;
        }


        private void BtnRemoveFile_Click(object sender, EventArgs e)
        {
            // Xóa mục được chọn trong fileListView
            if (fileListView.SelectedItems.Count > 0)
            {
                foreach (ListViewItem selectedItem in fileListView.SelectedItems)
                {
                    fileListView.Items.Remove(selectedItem);
                }
            }
            else
            {
                MessageBox.Show("Hãy chọn một file để xóa.");
            }
        }

        // Hàm xử lý sự kiện khi nhấn nút "Remove Selected"
        private void BtnRemoveSelected_Click(object sender, EventArgs e)
        {
            if (fileListView.SelectedItems.Count > 0)
            {
                foreach (ListViewItem selectedItem in fileListView.SelectedItems)
                {
                    fileListView.Items.Remove(selectedItem);
                }
            }
            else
            {
                MessageBox.Show("Hãy chọn một hoặc nhiều file để xóa.");
            }
        }

        // Hàm xử lý sự kiện khi nhấn nút "Select All"
        private void BtnSelectAll_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem tất cả các mục đã được chọn hay chưa
            bool allSelected = fileListView.SelectedItems.Count == fileListView.Items.Count;

            // Nếu tất cả đã được chọn, bỏ chọn tất cả
            if (allSelected)
            {
                foreach (ListViewItem item in fileListView.Items)
                {
                    item.Selected = false;
                }
            }
            // Nếu chưa chọn hết, chọn tất cả
            else
            {
                foreach (ListViewItem item in fileListView.Items)
                {
                    item.Selected = true;
                }
            }
        }

        // Hàm xử lý sự kiện khi nhấn nút "Upload"
        private async void BtnUpload_Click(object sender, EventArgs e)
        {
            if (fileListView.Items.Count == 0)
            {
                MessageBox.Show("Hãy chọn các tệp trước khi upload.");
                return;
            }

            int totalFiles = fileListView.Items.Count;
            int filesUploaded = 0;

            var uploadTasks = new List<Task>();

            foreach (ListViewItem item in fileListView.Items)
            {
                string filePath = item.Tag.ToString();
                item.SubItems[1].Text = "Đang upload...";

                // Create a task for each file upload
                var uploadTask = Task.Run(async () =>
                {
                    await UploadFileAsync(filePath);
                    fileListView.Invoke((MethodInvoker)(() => item.SubItems[1].Text = "Đã hoàn thành"));
                    Interlocked.Increment(ref filesUploaded);

                    // Update overall progress
                    int percentage = (filesUploaded * 100) / totalFiles;
                    progressBar.Invoke((MethodInvoker)(() => progressBar.Value = percentage));

                    // Update status on lblUploadStatus
                    lblUploadStatus.Invoke((MethodInvoker)(() => lblUploadStatus.Text = $"{filesUploaded}/{totalFiles} file đã được upload."));
                }).ContinueWith(t =>
                {
                    if (t.IsFaulted)
                    {
                        fileListView.Invoke((MethodInvoker)(() => item.SubItems[1].Text = "Lỗi upload"));
                    }
                });

                uploadTasks.Add(uploadTask);
            }

            // Wait for all upload tasks to complete
            await Task.WhenAll(uploadTasks);

            // Show message box when all files are uploaded
            MessageBox.Show("Tất cả các file đã được upload thành công.");
            ClearUploadState();
        }



        private void ClearUploadState()
        {
            // Xóa tất cả các file trong ListView
            fileListView.Items.Clear();

            // Reset thanh progress bar
            progressBar.Value = 0;

            // Reset trạng thái hiển thị
            lblUploadStatus.Text = "Sẵn sàng để upload.";
        }

        // Hàm upload file lên Google Drive
        private async Task UploadFileAsync(string filePath)
        {
            string safeFileName = PrepareFileName(Path.GetFileName(filePath));
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = safeFileName
            };

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    var request = service.Files.Create(fileMetadata, stream, "application/octet-stream");
                    request.Fields = "id";
                    request.Upload();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi trong quá trình upload: " + ex.Message);
            }
        }


        // Hàm xử lý sự kiện khi upload file
        private void Request_ProgressChanged(IUploadProgress progress)
        {
            if (progress.Status == UploadStatus.Uploading)
            {
                // Kiểm tra xem txtFilePath.Text có hợp lệ không
                string filePath = txtFileName.Text;

                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                {
                    MessageBox.Show("Đường dẫn file không hợp lệ.");
                    return;
                }

                long bytesSent = progress.BytesSent;
                long totalSize = new FileInfo(filePath).Length;
                int percentage = (int)((bytesSent * 100) / totalSize);

                // Cập nhật progress bar trên UI
                progressBar.Invoke((MethodInvoker)(() =>
                {
                    if (percentage >= 0 && percentage <= 100)
                    {
                        progressBar.Value = percentage;
                    }
                }));
            }
            else if (progress.Status == UploadStatus.Completed)
            {
                progressBar.Invoke((MethodInvoker)(() => { progressBar.Value = 100; }));
            }
            else if (progress.Status == UploadStatus.Failed)
            {
                progressBar.Invoke((MethodInvoker)(() => { progressBar.Value = 0; }));
                MessageBox.Show("Upload thất bại. Vui lòng thử lại.");
            }
        }


        private void lblUploadStatus_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
