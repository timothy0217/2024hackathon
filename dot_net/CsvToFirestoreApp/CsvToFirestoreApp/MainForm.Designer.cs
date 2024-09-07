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
            this.comboBoxCollections = new System.Windows.Forms.ComboBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.lblSelectCollection = new System.Windows.Forms.Label();
            this.lblSelectFile = new System.Windows.Forms.Label();

            // ComboBox (DropdownList for Firestore collections)
            this.comboBoxCollections.FormattingEnabled = true;
            this.comboBoxCollections.Location = new System.Drawing.Point(30, 50);
            this.comboBoxCollections.Name = "comboBoxCollections";
            this.comboBoxCollections.Size = new System.Drawing.Size(250, 24);
            this.comboBoxCollections.TabIndex = 0;

            // Button to upload CSV and import data
            this.btnUpload.Location = new System.Drawing.Point(30, 150);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(250, 30);
            this.btnUpload.TabIndex = 1;
            this.btnUpload.Text = "Upload CSV and Import to Firestore";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);

            // Label for ComboBox
            this.lblSelectCollection.AutoSize = true;
            this.lblSelectCollection.Location = new System.Drawing.Point(30, 30);
            this.lblSelectCollection.Name = "lblSelectCollection";
            this.lblSelectCollection.Size = new System.Drawing.Size(115, 17);
            this.lblSelectCollection.TabIndex = 2;
            this.lblSelectCollection.Text = "Select Collection";

            // Label for File Upload
            this.lblSelectFile.AutoSize = true;
            this.lblSelectFile.Location = new System.Drawing.Point(30, 100);
            this.lblSelectFile.Name = "lblSelectFile";
            this.lblSelectFile.Size = new System.Drawing.Size(130, 17);
            this.lblSelectFile.TabIndex = 3;
            this.lblSelectFile.Text = "Select CSV File";

            // Form configuration
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 200);
            this.Controls.Add(this.lblSelectFile);
            this.Controls.Add(this.lblSelectCollection);
            this.Controls.Add(this.comboBoxCollections);
            this.Controls.Add(this.btnUpload);
            this.Name = "MainForm";
            this.Text = "Firestore CSV Uploader";
            this.Load += new System.EventHandler(this.MainForm_Load);
        }
    }
}
