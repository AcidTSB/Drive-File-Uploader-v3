using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using System;
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
            InitializeDriveService();
            progressBar.Maximum = 100;

            btnChooseFile.Click += BtnChooseFile_Click;
            btnUpload.Click += BtnUpload_Click;
        }

        private void InitializeDriveService()
        {
            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { DriveService.Scope.DriveFile },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Drive API .NET Quickstart",
            });
        }

        private void BtnChooseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Chọn tệp để upload lên Google Drive";
            openFileDialog.Filter = "All files (*.*)|*.*";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Xóa danh sách cũ trước khi thêm mới
                fileListView.Items.Clear();

                foreach (string fileName in openFileDialog.FileNames)
                {
                    // Thêm từng file vào ListView
                    ListViewItem item = new ListViewItem(Path.GetFileName(fileName));
                    item.SubItems.Add("Chưa upload");
                    item.Tag = fileName; // Lưu đường dẫn file trong Tag
                    fileListView.Items.Add(item);
                }
            }
        }
        private async void BtnUpload_Click(object sender, EventArgs e)
        {
            if (fileListView.Items.Count == 0)
            {
                MessageBox.Show("Hãy chọn các tệp trước khi upload.");
                return;
            }

            int totalFiles = fileListView.Items.Count;
            int filesUploaded = 0;

            foreach (ListViewItem item in fileListView.Items)
            {
                string filePath = item.Tag.ToString();
                item.SubItems[1].Text = "Đang upload...";

                // Upload file mà không hiển thị MessageBox
                await UploadFileAsync(filePath);

                // Cập nhật trạng thái sau khi upload xong
                item.SubItems[1].Text = "Đã hoàn thành";
                filesUploaded++;

                // Cập nhật progress tổng thể
                int percentage = (filesUploaded * 100) / totalFiles;
                progressBar.Value = percentage;

                // Cập nhật trạng thái trên lblUploadStatus
                lblUploadStatus.Text = $"{filesUploaded}/{totalFiles} file đã được upload.";
            }

            // Chỉ hiển thị MessageBox khi tất cả các file đã được upload
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

        private async Task UploadFileAsync(string filePath)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = Path.GetFileName(filePath)
            };

            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                var request = service.Files.Create(fileMetadata, stream, "application/octet-stream");
                request.Fields = "id";
                request.ProgressChanged += Request_ProgressChanged;

                await request.UploadAsync(); // Sử dụng async để không chặn UI
            }
        }


        private void Request_ProgressChanged(IUploadProgress progress)
        {
            if (progress.Status == UploadStatus.Uploading)
            {
                long bytesSent = progress.BytesSent;
                long totalSize = new FileInfo(txtFileName.Text).Length;
                int percentage = (int)((bytesSent * 100) / totalSize);

                // Sử dụng Invoke để cập nhật UI từ thread khác
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


    }
}
