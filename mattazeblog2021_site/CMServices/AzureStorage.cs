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

        private double ImageSizeSmall = 100;
        private double ImageSizeNormal = 300;
        private string ImageRawFolder = "raw";
        private string ImageFolderSmall = "small";

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
        public PostItem LatestPost() {
            return null;
        }

        public PostItem GetPost(string partitionKey, string rowKey) {
            var tableClient = GetTableClient();
            var query = TableOperation.Retrieve<PostItem>(partitionKey, rowKey);
            var results = tableClient.Execute(query);
            var item = results.Result as PostItem;
            return item;
        }

        public PostItem PutPost(PostItem post) {
            var tableClient = GetTableClient();
            var insertOrMerge = TableOperation.InsertOrReplace(post);
            var result = tableClient.Execute(insertOrMerge);
            var updatedPost = result.Result as PostItem;
            return updatedPost;
        }

        public Models.PostImage GetImage(string blobName) {
            var containerClient = GetBlobContainerClient();
            var blobClient = containerClient.GetBlobClient(blobName);

            if (blobClient.Exists() == false) {
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

        public bool PutImage(PostImage postImage) {
            var containerClient = GetBlobContainerClient();
            var blobClient = containerClient.GetBlobClient(postImage.Name);

            var response = blobClient.Upload(postImage.Data, false);

            var version = response.Value.VersionId;

            return true;
        }
    }
}
