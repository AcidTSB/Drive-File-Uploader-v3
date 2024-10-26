using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Drive_FIle_Uploader_v3
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Button btnChooseFile;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.ListView fileListView;
        private System.Windows.Forms.ColumnHeader columnFileName;
        private System.Windows.Forms.ColumnHeader columnStatus;
        private Label lblUploadStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            btnChooseFile = new Button();
            btnUpload = new Button();
            txtFileName = new TextBox();
            progressBar = new ProgressBar();
            logoPictureBox = new PictureBox();
            lblTitle = new Label();
            lblWelcome = new Label();
            lblInstructions = new Label();
            fileListView = new ListView();
            columnFileName = new ColumnHeader();
            columnStatus = new ColumnHeader();
            lblUploadStatus = new Label();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
            SuspendLayout();
            // 
            // btnChooseFile
            // 
            btnChooseFile.Location = new Point(12, 140);
            btnChooseFile.Name = "btnChooseFile";
            btnChooseFile.Size = new Size(100, 30);
            btnChooseFile.TabIndex = 0;
            btnChooseFile.Text = "Choose File";
            btnChooseFile.UseVisualStyleBackColor = true;
            // 
            // btnUpload
            // 
            btnUpload.Location = new Point(120, 140);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new Size(100, 30);
            btnUpload.TabIndex = 1;
            btnUpload.Text = "Upload";
            btnUpload.UseVisualStyleBackColor = true;
            // 
            // txtFileName
            // 
            txtFileName.Location = new Point(12, 180);
            txtFileName.Name = "txtFileName";
            txtFileName.Size = new Size(341, 23);
            txtFileName.TabIndex = 2;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(12, 239);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(341, 23);
            progressBar.TabIndex = 3;
            // 
            // logoPictureBox
            // 
            logoPictureBox.Location = new Point(12, 12);
            logoPictureBox.Name = "logoPictureBox";
            logoPictureBox.Size = new Size(80, 80);
            logoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            logoPictureBox.Image = System.Drawing.Image.FromFile("logo.jpg");
            logoPictureBox.TabIndex = 3;
            logoPictureBox.TabStop = false;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Arial", 18F, FontStyle.Bold);
            lblTitle.Location = new Point(100, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(232, 29);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Drive File Uploader";
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new System.Drawing.Font("Arial", 12F);
            lblWelcome.Location = new Point(100, 50);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(245, 18);
            lblWelcome.TabIndex = 1;
            lblWelcome.Text = "Welcome! Upload your files easily.";
            // 
            // lblInstructions
            // 
            lblInstructions.AutoSize = true;
            lblInstructions.Font = new System.Drawing.Font("Arial", 10F);
            lblInstructions.Location = new Point(12, 100);
            lblInstructions.Name = "lblInstructions";
            lblInstructions.Size = new Size(159, 32);
            lblInstructions.TabIndex = 2;
            lblInstructions.Text = "1. Choose a file.\n2. Click 'Upload' to start.";
            // 
            // fileListView
            // 
            fileListView.Columns.AddRange(new ColumnHeader[] { columnFileName, columnStatus });
            fileListView.FullRowSelect = true;
            fileListView.GridLines = true;
            fileListView.Location = new Point(12, 268);
            fileListView.MultiSelect = false;
            fileListView.Name = "fileListView";
            fileListView.Size = new Size(341, 120);
            fileListView.TabIndex = 4;
            fileListView.UseCompatibleStateImageBehavior = false;
            fileListView.View = View.Details;
            // 
            // columnFileName
            // 
            columnFileName.Text = "Tên File";
            columnFileName.Width = 220;
            // 
            // columnStatus
            // 
            columnStatus.Text = "Trạng Thái";
            columnStatus.Width = 100;
            // 
            // lblUploadStatus
            // 
            lblUploadStatus.Location = new Point(12, 206);
            lblUploadStatus.Name = "lblUploadStatus";
            lblUploadStatus.Size = new Size(341, 23);
            lblUploadStatus.TabIndex = 0;
            lblUploadStatus.Text = "Sẵn sàng để upload.";
            // 
            // Form1
            // 
            ClientSize = new Size(365, 400);
            Controls.Add(lblUploadStatus);
            Controls.Add(progressBar);
            Controls.Add(fileListView);
            Controls.Add(lblTitle);
            Controls.Add(lblWelcome);
            Controls.Add(lblInstructions);
            Controls.Add(logoPictureBox);
            Controls.Add(txtFileName);
            Controls.Add(btnUpload);
            Controls.Add(btnChooseFile);
            Name = "Form1";
            Text = "Drive File Uploader";
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
