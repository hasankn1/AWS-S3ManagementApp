using Amazon.S3.Model;
using Amazon.S3;
using System.Windows;

namespace Hasan_Khan_301019813_Lab1_COMP306_NEW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Helper connection = new Helper();
        AmazonS3Client client = connection.Connection();

        public MainWindow()
        {
            InitializeComponent();
        }

        // Reusable method for fetching and listing all buckets
        private async Task<List<Bucket>> GetAllBucketsAsync()
        {
            List<Bucket> bucketList = new List<Bucket>();

            try
            {
                ListBucketsResponse response = await client.ListBucketsAsync();
                foreach (S3Bucket bucket in response.Buckets)
                {
                    bucketList.Add(new Bucket
                    {
                        Buckets = bucket.BucketName,
                        CreationDate = bucket.CreationDate.ToShortDateString() + " " + bucket.CreationDate.ToShortTimeString()
                    });
                }
            }
            catch (AmazonS3Exception s3Ex)
            {
                MessageBox.Show($"AWS S3 Error: {s3Ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching the buckets: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return bucketList;
        }

        // Event handler for Bucket Level Operations button click
        private async void BucketLevelOperationsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create the Bucket Level Operations Window
                BucketLevelOperationsWindow bucketWindow = new BucketLevelOperationsWindow();

                // Fetch buckets and bind to the DataGrid
                List<Bucket> buckets = await GetAllBucketsAsync();
                if (buckets.Count > 0)
                {
                    bucketWindow.BucketListDataGrid.ItemsSource = buckets;
                }
                else
                {
                    MessageBox.Show("No buckets available.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                bucketWindow.Show();
                this.Close(); // Hide the main window, only 1 window open at a time.
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Event handler for Object Level Operations button click
        private async void ObjectLevelOperationsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create the Object Level Operations Window
                ObjectLevelOperations objectOperationsForm = new ObjectLevelOperations();

                // Fetch buckets and populate the ComboBox
                ListBucketsResponse response = await client.ListBucketsAsync();
                foreach (S3Bucket bucket in response.Buckets)
                {
                    objectOperationsForm.BucketSelectorComboBox.Items.Add(bucket.BucketName);
                }

                objectOperationsForm.Show();
                this.Close(); // Hide the main window, only 1 window open at a time.
            }
            catch (AmazonS3Exception s3Ex)
            {
                MessageBox.Show($"AWS S3 Error: {s3Ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Event handler for Exit button click
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while exiting: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
