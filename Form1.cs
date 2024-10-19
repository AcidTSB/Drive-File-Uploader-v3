using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace Drive_FIle_Uploader_v3
{
    public partial class Form1 : Form
    {
        private DriveService service;
        private string[] Scopes = { DriveService.Scope.DriveFile };
        private string ApplicationName = "Drive API .NET Quickstart";

        public Form1()
        {
            InitializeComponent();
            InitializeDriveService();
        }

        private void InitializeDriveService()
        {
            UserCredential credential;

            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Chọn tệp để upload lên Google Drive";
            openFileDialog.Filter = "All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = openFileDialog.FileName;
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFileName.Text) && File.Exists(txtFileName.Text))
            {
                UploadFileToDrive(txtFileName.Text);
            }
            else
            {
                MessageBox.Show("Please choose a valid file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UploadFileToDrive(string filePath)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = Path.GetFileName(filePath)
            };

            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                var request = service.Files.Create(fileMetadata, stream, "application/octet-stream");
                request.Fields = "id";
                request.Upload();
                var file = request.ResponseBody;
                MessageBox.Show("File uploaded successfully. ID: " + file.Id, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
