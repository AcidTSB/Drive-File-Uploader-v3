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
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = openFileDialog.FileName;
            }
        }

        private void BtnUpload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFileName.Text))
            {
                MessageBox.Show("Hãy chọn tệp trước khi upload.");
                return;
            }

            string filePath = txtFileName.Text;
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = Path.GetFileName(filePath)
            };

            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                FilesResource.CreateMediaUpload request = service.Files.Create(fileMetadata, stream, "application/octet-stream");
                request.Fields = "id";
                request.ProgressChanged += Request_ProgressChanged;
                request.Upload();
                var file = request.ResponseBody;
                MessageBox.Show("File đã upload, ID: " + file.Id);
            }
        }

        private void Request_ProgressChanged(IUploadProgress progress)
        {
            if (progress.Status == UploadStatus.Uploading)
            {
                progressBar.Value = (int)(progress.BytesSent / 1024);
            }
        }

        private void logoPictureBox_Click(object sender, EventArgs e)
        {

        }
    }
}
