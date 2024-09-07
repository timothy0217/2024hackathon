namespace CsvToFirestoreApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox comboBoxCollections;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label lblSelectCollection;
        private System.Windows.Forms.Label lblSelectFile;

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
            comboBoxCollections = new ComboBox();
            btnUpload = new Button();
            openFileDialog = new OpenFileDialog();
            lblSelectCollection = new Label();
            lblSelectFile = new Label();
            SuspendLayout();
            // 
            // comboBoxCollections
            // 
            comboBoxCollections.FormattingEnabled = true;
            comboBoxCollections.Location = new Point(34, 59);
            comboBoxCollections.Margin = new Padding(3, 4, 3, 4);
            comboBoxCollections.Name = "comboBoxCollections";
            comboBoxCollections.Size = new Size(281, 27);
            comboBoxCollections.TabIndex = 0;
            // 
            // btnUpload
            // 
            btnUpload.Location = new Point(34, 178);
            btnUpload.Margin = new Padding(3, 4, 3, 4);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new Size(314, 36);
            btnUpload.TabIndex = 1;
            btnUpload.Text = "Upload CSV,JSON and Import to Firestore";
            btnUpload.UseVisualStyleBackColor = true;
            btnUpload.Click += btnUpload_Click;
            // 
            // lblSelectCollection
            // 
            lblSelectCollection.AutoSize = true;
            lblSelectCollection.Location = new Point(34, 36);
            lblSelectCollection.Name = "lblSelectCollection";
            lblSelectCollection.Size = new Size(123, 19);
            lblSelectCollection.TabIndex = 2;
            lblSelectCollection.Text = "Select Collection";
            // 
            // lblSelectFile
            // 
            lblSelectFile.AutoSize = true;
            lblSelectFile.Location = new Point(34, 119);
            lblSelectFile.Name = "lblSelectFile";
            lblSelectFile.Size = new Size(111, 19);
            lblSelectFile.TabIndex = 3;
            lblSelectFile.Text = "Select CSV File";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(360, 238);
            Controls.Add(lblSelectFile);
            Controls.Add(lblSelectCollection);
            Controls.Add(comboBoxCollections);
            Controls.Add(btnUpload);
            Margin = new Padding(3, 4, 3, 4);
            Name = "MainForm";
            Text = "Firestore CSV Uploader";
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
