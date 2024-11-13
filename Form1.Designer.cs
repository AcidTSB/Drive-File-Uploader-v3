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
        private System.Windows.Forms.Button btnRemoveFile;
        private System.Windows.Forms.Button btnRemoveSelected;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnLogout;
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
            btnLogin = new Button();
            btnLogout = new Button();
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
            btnRemoveSelected = new Button();
            btnSelectAll = new Button();
            btnRemoveFile = new Button();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
            SuspendLayout();
            // 
            // btnLogin
            // 
            btnLogin.BackColor = SystemColors.ButtonHighlight;
            btnLogin.Font = new System.Drawing.Font("Arial", 10F);
            btnLogin.ForeColor = Color.Black;
            btnLogin.Location = new Point(370, 153);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(157, 31);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "Đăng nhập";
            btnLogin.UseVisualStyleBackColor = false;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = SystemColors.ButtonHighlight;
            btnLogout.Font = new System.Drawing.Font("Arial", 10F);
            btnLogout.ForeColor = Color.Black;
            btnLogout.Location = new Point(539, 153);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(157, 31);
            btnLogout.TabIndex = 6;
            btnLogout.Text = "Đăng xuất";
            btnLogout.UseVisualStyleBackColor = false;
            // 
            // btnChooseFile
            // 
            btnChooseFile.BackColor = SystemColors.ButtonHighlight;
            btnChooseFile.Font = new System.Drawing.Font("Arial", 12F);
            btnChooseFile.ForeColor = Color.Black;
            btnChooseFile.Location = new Point(24, 280);
            btnChooseFile.Name = "btnChooseFile";
            btnChooseFile.Size = new Size(336, 42);
            btnChooseFile.TabIndex = 0;
            btnChooseFile.Text = "Chọn tệp tin";
            btnChooseFile.UseVisualStyleBackColor = false;
            // 
            // btnUpload
            // 
            btnUpload.BackColor = SystemColors.ButtonHighlight;
            btnUpload.Font = new System.Drawing.Font("Arial", 12F);
            btnUpload.ForeColor = Color.Black;
            btnUpload.Location = new Point(370, 280);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new Size(336, 42);
            btnUpload.TabIndex = 1;
            btnUpload.Text = "Upload";
            btnUpload.UseVisualStyleBackColor = false;
            // 
            // txtFileName
            // 
            txtFileName.Font = new System.Drawing.Font("Arial", 12F);
            txtFileName.Location = new Point(24, 328);
            txtFileName.Name = "txtFileName";
            txtFileName.Size = new Size(682, 26);
            txtFileName.TabIndex = 2;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(24, 389);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(682, 28);
            progressBar.TabIndex = 3;
            // 
            // logoPictureBox
            // 
            logoPictureBox.Location = new Point(24, 24);
            logoPictureBox.Name = "logoPictureBox";
            logoPictureBox.Size = new Size(160, 160);
            logoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            logoPictureBox.Image = System.Drawing.Image.FromFile("logo.jpg");
            logoPictureBox.TabIndex = 3;
            logoPictureBox.TabStop = false;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Arial", 36F, FontStyle.Bold);
            lblTitle.ForeColor = Color.DarkBlue;
            lblTitle.Location = new Point(200, 40);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(461, 56);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Drive File Uploader";
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new System.Drawing.Font("Arial", 24F);
            lblWelcome.ForeColor = Color.DarkSlateGray;
            lblWelcome.Location = new Point(200, 100);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(496, 36);
            lblWelcome.TabIndex = 1;
            lblWelcome.Text = "Welcome! Upload your files easily.";
            // 
            // lblInstructions
            // 
            lblInstructions.AutoSize = true;
            lblInstructions.Font = new System.Drawing.Font("Arial", 20F);
            lblInstructions.ForeColor = Color.Gray;
            lblInstructions.Location = new Point(24, 200);
            lblInstructions.Name = "lblInstructions";
            lblInstructions.Size = new Size(348, 64);
            lblInstructions.TabIndex = 2;
            lblInstructions.Text = "1. Chọn tệp tin.\n2. Bấm 'Upload' để bắt đầu.";
            // 
            // fileListView
            // 
            fileListView.Columns.AddRange(new ColumnHeader[] { columnFileName, columnStatus });
            fileListView.Font = new System.Drawing.Font("Arial", 12F);
            fileListView.FullRowSelect = true;
            fileListView.GridLines = true;
            fileListView.Location = new Point(24, 460);
            fileListView.Name = "fileListView";
            fileListView.Size = new Size(682, 240);
            fileListView.TabIndex = 4;
            fileListView.UseCompatibleStateImageBehavior = false;
            fileListView.View = View.Details;
            // 
            // columnFileName
            // 
            columnFileName.Text = "File Name";
            columnFileName.Width = 440;
            // 
            // columnStatus
            // 
            columnStatus.Text = "Status";
            columnStatus.Width = 200;
            // 
            // lblUploadStatus
            // 
            lblUploadStatus.Font = new System.Drawing.Font("Arial", 12F);
            lblUploadStatus.ForeColor = Color.DarkSlateGray;
            lblUploadStatus.Location = new Point(24, 359);
            lblUploadStatus.Name = "lblUploadStatus";
            lblUploadStatus.Size = new Size(682, 20);
            lblUploadStatus.TabIndex = 0;
            lblUploadStatus.Text = "Sẵn sàng để upload.";
            lblUploadStatus.Click += lblUploadStatus_Click;
            // 
            // btnRemoveSelected
            // 
            btnRemoveSelected.BackColor = SystemColors.ButtonHighlight;
            btnRemoveSelected.Font = new System.Drawing.Font("Arial", 10F);
            btnRemoveSelected.ForeColor = Color.Black;
            btnRemoveSelected.Location = new Point(187, 423);
            btnRemoveSelected.Name = "btnRemoveSelected";
            btnRemoveSelected.Size = new Size(157, 31);
            btnRemoveSelected.TabIndex = 5;
            btnRemoveSelected.Text = "Xoá tệp đã chọn";
            btnRemoveSelected.UseVisualStyleBackColor = false;
            // 
            // btnSelectAll
            // 
            btnSelectAll.BackColor = SystemColors.ButtonHighlight;
            btnSelectAll.Font = new System.Drawing.Font("Arial", 10F);
            btnSelectAll.ForeColor = Color.Black;
            btnSelectAll.Location = new Point(24, 423);
            btnSelectAll.Name = "btnSelectAll";
            btnSelectAll.Size = new Size(157, 31);
            btnSelectAll.TabIndex = 6;
            btnSelectAll.Text = "Chọn tất cả";
            btnSelectAll.UseVisualStyleBackColor = false;
            // 
            // btnRemoveFile
            // 
            btnRemoveFile.Location = new Point(0, 0);
            btnRemoveFile.Name = "btnRemoveFile";
            btnRemoveFile.Size = new Size(75, 23);
            btnRemoveFile.TabIndex = 0;
            // 
            // Form1
            // 
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(731, 724);
            Controls.Add(btnLogin);
            Controls.Add(btnLogout);
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
            Controls.Add(btnRemoveSelected);
            Controls.Add(btnSelectAll);
            Name = "Form1";
            Text = "Drive File Uploader";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
