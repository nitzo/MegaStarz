using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace MegaStar.MVC.Lib.RestServices
{
    public sealed class BlobStorageManager
    {

        #region Static Methods
        private static CloudStorageAccount Account
        {
            get
            {
#if DEBUG
                return CloudStorageAccount.DevelopmentStorageAccount;
#else
                return CloudStorageAccount.FromConfigurationSetting("DataConnectionString");
#endif
            }
        }

        /// <summary>
        /// Init the container with the specified name.
        /// The container is initialized with Blog Public Access permissions
        /// </summary>
        /// <param name="containerName">Name of container to INIT</param>
        public static void InitContainer(string containerName)
        {
            var _blobClient = Account.CreateCloudBlobClient();
            var _blobContainer = _blobClient.GetContainerReference(containerName);

            var permissions = new BlobContainerPermissions();

            permissions.PublicAccess = BlobContainerPublicAccessType.Blob;
            
            _blobContainer.CreateIfNotExist();

            _blobContainer.SetPermissions(permissions);
        }

#endregion


        private CloudBlobClient _blobClient;
        private CloudBlobContainer _blobContainer;
       
        public BlobStorageManager(string containerName)
        {
            _blobClient = Account.CreateCloudBlobClient();
            _blobContainer = _blobClient.GetContainerReference(containerName);
        }


        /// <summary>
        /// Upload a stream to the current container.
        /// </summary>
        /// <param name="file">A stream object to upload</param>
        /// <param name="contentType">The content type of the stream</param>
        /// <returns></returns>
        public string UploadStream(Stream file, string contentType)
        {
            var fileGuid = System.Guid.NewGuid().ToString();
            var blob = _blobContainer.GetBlobReference(fileGuid);

            blob.UploadFromStream(file);

            blob.Metadata["FileName"] = fileGuid;
            blob.SetMetadata();

            blob.Properties.ContentType = contentType;
            blob.SetProperties();


            return fileGuid;
        }

        public Uri GetBlobUri(string blobId)
        {
            var blob = _blobContainer.GetBlobReference(blobId);

            return blob.Uri;
        }

        public void DeleteBlob(string blobId)
        {
            var blob = _blobContainer.GetBlobReference(blobId);

            blob.DeleteIfExists();
        }
    }
}
