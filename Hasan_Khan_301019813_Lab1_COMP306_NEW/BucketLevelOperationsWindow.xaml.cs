using System;
using System.Collections.Generic;
using System.Windows;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace Hasan_Khan_301019813_Lab1_COMP306_NEW
{
    /// <summary>
    /// Interaction logic for BucketLevelOperationsWindow.xaml
    /// </summary>
    public partial class BucketLevelOperationsWindow : Window
    {
        static Helper connect = new Helper();
        AmazonS3Client client = connect.Connection();

        public BucketLevelOperationsWindow()
        {
            InitializeComponent();
            LoadBucketsAsync(); // Automatically load buckets when the window opens
        }

        // Method to load buckets into the DataGrid
        private async Task LoadBucketsAsync()
        {
            try
            {
                List<Bucket> bucketList = new List<Bucket>();
                ListBucketsResponse response = await client.ListBucketsAsync();

                foreach (S3Bucket bucket in response.Buckets)
                {
                    bucketList.Add(new Bucket
                    {
                        Buckets = bucket.BucketName,
                        CreationDate = bucket.CreationDate.ToShortDateString() + " " + bucket.CreationDate.ToShortTimeString()
                    });
                }

                BucketListDataGrid.ItemsSource = bucketList;
            }
            catch (AmazonS3Exception s3Ex)
            {
                MessageBox.Show($"AWS S3 Error: {s3Ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading buckets: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void CreateBucketButton_Click(object sender, RoutedEventArgs e)
        {
            string bucketName = CreateBucketTextBox.Text;

            if (string.IsNullOrWhiteSpace(bucketName))
            {
                MessageBox.Show("Please enter a valid bucket name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Create the new bucket
                var response = await client.PutBucketAsync(new PutBucketRequest
                {
                    BucketName = bucketName
                });

                Message.Content = "Bucket successfully created!";
                MessageBox.Show("Bucket created successfully!");

                // Refresh the bucket list
                await LoadBucketsAsync();
            }
            catch (AmazonS3Exception s3Ex)
            {
                MessageBox.Show($"AWS S3 Error: {s3Ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating bucket: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteBucketButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedBucket = (Bucket)BucketListDataGrid.SelectedItem;

            if (selectedBucket == null)
            {
                MessageBox.Show("Please select a bucket to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Check if the bucket is empty before deletion
                ListObjectsV2Request listRequest = new ListObjectsV2Request
                {
                    BucketName = selectedBucket.Buckets
                };

                ListObjectsV2Response listResponse = await client.ListObjectsV2Async(listRequest);

                if (listResponse.S3Objects.Count > 0)
                {
                    MessageBox.Show("Bucket is not empty. Please empty it first before deleting.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Delete the bucket if it's empty
                await client.DeleteBucketAsync(new DeleteBucketRequest
                {
                    BucketName = selectedBucket.Buckets
                });

                Message.Content = "Bucket successfully deleted!";
                MessageBox.Show("Bucket deleted successfully!");

                // Refresh the bucket list after deletion
                await LoadBucketsAsync();
            }
            catch (AmazonS3Exception s3Ex)
            {
                MessageBox.Show($"AWS S3 Error: {s3Ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting bucket: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackToMainWindowButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close(); // Close the current window
        }
    }
}
