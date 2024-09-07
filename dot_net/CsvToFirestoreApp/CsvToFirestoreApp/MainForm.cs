using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CsvHelper;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using System.Globalization;
using System.Formats.Asn1;
using Newtonsoft.Json;
using System.Dynamic;

namespace CsvToFirestoreApp
{
    public partial class MainForm : Form
    {
        private FirestoreDb db;

        public MainForm()
        {
            InitializeComponent();
        }

        // Form Load Event - Populate dropdown with Firestore collection names
        private async void MainForm_Load(object sender, EventArgs e)
        {
            //要去系統變數設置GOOGLE_APPLICATION_CREDENTIALS，指向firestore.json的位置
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string credentialPath = Path.Combine(appPath, "firestore.json");
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(credentialPath),
            });

            db = FirestoreDb.Create("hackathon2024-1b87f");

            // Fetch collections and add them to the ComboBox
            comboBoxCollections.Items.Clear();

            // 使用 await foreach 來非同步枚舉集合
            await foreach (var collection in db.ListRootCollectionsAsync())
            {
                comboBoxCollections.Items.Add(collection.Id);
            }

        }

        private async void btnUpload_Click(object sender, EventArgs e)
        {
            if (comboBoxCollections.SelectedItem == null)
            {
                MessageBox.Show("Please select a Firestore collection.");
                return;
            }

            // Open file dialog to select CSV or JSON file
            openFileDialog.Filter = "CSV files (*.csv)|*.csv|JSON files (*.json)|*.json";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;
                string fileExtension = Path.GetExtension(selectedFilePath).ToLower();

                // Get selected Firestore collection
                var collectionName = comboBoxCollections.SelectedItem.ToString();
                CollectionReference collection = db.Collection(collectionName);

                try
                {
                    if (fileExtension == ".csv")
                    {
                        // Handle CSV file parsing and uploading
                        await UploadCsvToFirestore(selectedFilePath, collection);
                    }
                    else if (fileExtension == ".json")
                    {
                        // Handle JSON file parsing and uploading
                        await UploadJsonToFirestore(selectedFilePath, collection);
                    }

                    MessageBox.Show("Data successfully imported into Firestore.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error importing data: {ex.Message}");
                }
            }
        }

        private async Task UploadCsvToFirestore(string filePath, CollectionReference collection)
        {
            // Parse the CSV file
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<dynamic>().ToList();

                // Add each record to the Firestore collection
                foreach (var record in records)
                {
                    await collection.AddAsync(record);
                }
            }
        }

        private async Task UploadJsonToFirestore(string filePath, CollectionReference collection)
        {
            // 读取 JSON 文件内容
            string jsonContent = File.ReadAllText(filePath);

            // 使用 JsonConvert 解析 JSON 为动态对象
            var data = JsonConvert.DeserializeObject<ExpandoObject>(jsonContent);

            // 将解析的 JSON 上传到 Firestore
            await collection.AddAsync(data);
        }



    }
}

