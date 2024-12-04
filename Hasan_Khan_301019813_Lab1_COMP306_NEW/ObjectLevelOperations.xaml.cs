using Amazon.S3.Model;
using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Windows;
using Amazon.S3.Transfer;
using System.Windows.Controls;

namespace Hasan_Khan_301019813_Lab1_COMP306_NEW
{
    /// <summary>
    /// Interaction logic for ObjectLevelOperations.xaml
    /// </summary>
    public partial class ObjectLevelOperations : Window
    {
        static Helper connection = new Helper();
        AmazonS3Client client = connection.Connection();
        List<BucketItem> item = new List<BucketItem>();

        public ObjectLevelOperations()
        {
            InitializeComponent();
        }

        private class BucketItem
        {
            public string Object { get; set; }
            public long Size { get; set; }
        }

        // Helper method to fetch and display objects in the selected bucket
        private async Task LoadObjectsAsync(string bucketName)
        {
            item.Clear();
            try
            {
                ListObjectsV2Request request = new ListObjectsV2Request
                {
                    BucketName = bucketName
                };

                ListObjectsV2Response response = await client.ListObjectsV2Async(request);

                foreach (S3Object obj in response.S3Objects)
                {
                    item.Add(new BucketItem { Object = obj.Key, Size = obj.Size });
                }

                ObjectDataGrid.ItemsSource = item;
                ObjectDataGrid.Items.Refresh();
            }
            catch (AmazonS3Exception s3Ex)
            {
                MessageBox.Show($"AWS S3 Error: {s3Ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading objects: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            bool? response = openFileDialog.ShowDialog();
            if (response == true)
            {
                string filePath = openFileDialog.FileName;
                FileTextBox.Text = filePath;
            }
        }

        private async void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedBucket = BucketSelectorComboBox.SelectedItem.ToString();

            if (!string.IsNullOrEmpty(selectedBucket))
            {
                await LoadObjectsAsync(selectedBucket);
            }
        }

        private async void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedBucket = BucketSelectorComboBox.SelectedItem?.ToString();
            string filePath = FileTextBox.Text;

            if (string.IsNullOrEmpty(selectedBucket))
            {
                MessageBox.Show("Please select a bucket.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
            {
                MessageBox.Show("Please select a valid file to upload.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var putRequest = new PutObjectRequest
                {
                    BucketName = selectedBucket,
                    FilePath = filePath
                };

                await client.PutObjectAsync(putRequest);
                MessageBox.Show("File uploaded successfully!");

                // Refresh object list after upload
                await LoadObjectsAsync(selectedBucket);
            }
            catch (AmazonS3Exception s3Ex)
            {
                MessageBox.Show($"AWS S3 Error: {s3Ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error uploading file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedBucket = BucketSelectorComboBox.SelectedItem as string;
            BucketItem selectedFile = (BucketItem)ObjectDataGrid.SelectedItem;

            if (string.IsNullOrEmpty(selectedBucket) || selectedFile == null)
            {
                MessageBox.Show("Please select a bucket and a file to download.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var pathAndFileName = $"C:\\temp\\{selectedFile.Object}";

            try
            {
                var downloadRequest = new TransferUtilityDownloadRequest
                {
                    BucketName = selectedBucket,
                    Key = selectedFile.Object,
                    FilePath = pathAndFileName
                };

                using (var transferUtility = new TransferUtility(client))
                {
                    await transferUtility.DownloadAsync(downloadRequest);
                }

                MessageBox.Show($"File downloaded successfully to {pathAndFileName}.");
            }
            catch (AmazonS3Exception s3Ex)
            {
                MessageBox.Show($"AWS S3 Error: {s3Ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error downloading file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close(); //Close the current Window
        }
    }
}
