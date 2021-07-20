using Azure.Storage.Blobs;
using mattazeblog2021_site.Models;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mattazeblog2021_site.CMServices {
    public class AzureStorage {

        string StorageConnectionString;
        private static readonly string BlobContainerName = "basicblogimages";
        private readonly string BlobContainerNameRAW = BlobContainerName + "raw";
        private readonly string PostItemsTableName = "basicblogposts";

        public AzureStorage(string storageConnectionString) {
            StorageConnectionString = storageConnectionString;

        }

        private void CreateLocationsIfNotExists() {
            var blobClient = GetBlobContainerClient();
            blobClient.CreateIfNotExists();

            var tableClient = GetTableClient();
            tableClient.CreateIfNotExists();
        }

        private CloudTable GetTableClient() {
            var storageAccount = CloudStorageAccount.Parse(StorageConnectionString);
            var tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());
            return tableClient.GetTableReference(PostItemsTableName);
        }

        private BlobContainerClient GetBlobContainerClient() {
            return new BlobContainerClient(StorageConnectionString, BlobContainerNameRAW);
        }

        public PostItem GetPost(string partitionKey, string rowKey) {
            var tableClient = GetTableClient();
            var query = TableOperation.Retrieve<PostItem>(partitionKey, rowKey);
            var results = tableClient.Execute(query);
            var item = results.Result as PostItem;
            return item;
        }

        public PostItem Latest() {
            return null;
        }

        public Models.PostImage GetImage(string blobName) {
            var containerClient = GetBlobContainerClient();
            var blobClient = containerClient.GetBlobClient(blobName);

            if(blobClient.Exists() == false) {
                return null;
            }

            var value = blobClient.DownloadContent().Value;

            var postImage = new PostImage() {
                ContentType = value.Details.ContentType,
                Data = value.Content,
                Name = blobClient.Name
            };

            return postImage;
        }
    }
}
