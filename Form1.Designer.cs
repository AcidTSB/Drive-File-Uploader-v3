using System.Windows.Forms;
using System.Xml.Linq;

namespace Drive_FIle_Uploader_v3
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        // Button, TextBox, and ProgressBar declarations
        private System.Windows.Forms.Button btnChooseFile;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.ProgressBar progressBar;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnChooseFile = new Button();
            btnUpload = new Button();
            txtFileName = new TextBox();
            progressBar = new ProgressBar();
            SuspendLayout();
            // 
            // btnChooseFile
            // 
            btnChooseFile.Location = new Point(12, 12);
            btnChooseFile.Name = "btnChooseFile";
            btnChooseFile.Size = new Size(75, 23);
            btnChooseFile.TabIndex = 0;
            btnChooseFile.Text = "Choose File";
            btnChooseFile.UseVisualStyleBackColor = true;
            // 
            // btnUpload
            // 
            btnUpload.Location = new Point(12, 41);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new Size(75, 23);
            btnUpload.TabIndex = 1;
            btnUpload.Text = "Upload";
            btnUpload.UseVisualStyleBackColor = true;
            // 
            // txtFileName
            // 
            txtFileName.Location = new Point(93, 14);
            txtFileName.Name = "txtFileName";
            txtFileName.Size = new Size(179, 23);
            txtFileName.TabIndex = 2;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(12, 70);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(260, 23);
            progressBar.TabIndex = 3;
            // 
            // Form1
            // 
            ClientSize = new Size(284, 101);
            Controls.Add(progressBar);
            Controls.Add(txtFileName);
            Controls.Add(btnUpload);
            Controls.Add(btnChooseFile);
            Name = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}