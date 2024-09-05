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
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string credentialPath = Path.Combine(appPath, "firestore.json");
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(credentialPath),
            });

            db = FirestoreDb.Create("1:87705706406:web:8e227985bb1196c2b63967");

            // Fetch collections and add them to the ComboBox
            comboBoxCollections.Items.Clear();

            // 使用 await foreach 來非同步枚舉集合
            await foreach (var collection in db.ListRootCollectionsAsync())
            {
                comboBoxCollections.Items.Add(collection.Id);
            }

        }

        // Button click event - Upload CSV and import to selected Firestore collection
        private async void btnUpload_Click(object sender, EventArgs e)
        {
            if (comboBoxCollections.SelectedItem == null)
            {
                MessageBox.Show("Please select a Firestore collection.");
                return;
            }

            // Open file dialog to select CSV file
            openFileDialog.Filter = "CSV files (*.csv)|*.csv";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;

                // Parse the CSV file
                using (var reader = new StreamReader(selectedFilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<dynamic>().ToList();

                    // Get selected Firestore collection
                    var collectionName = comboBoxCollections.SelectedItem.ToString();
                    CollectionReference collection = db.Collection(collectionName);

                    // Add each record to the Firestore collection
                    foreach (var record in records)
                    {
                        await collection.AddAsync(record);
                    }

                    MessageBox.Show("CSV data successfully imported into Firestore.");
                }
            }
        }
    }
}

