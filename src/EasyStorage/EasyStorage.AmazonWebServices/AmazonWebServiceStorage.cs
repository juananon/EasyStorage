using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using EasyStorage.Core;

namespace EasyStorage.AmazonWebServices
{
    public class AmazonWebServiceStorage : IEasyStorage
    {
        private readonly AmazonWebServicesStorageConfiguration _amazonWebServicesStorageConfiguration;

        public AmazonWebServiceStorage(AmazonWebServicesStorageConfiguration amazonWebServicesStorageConfiguration)
        {
            if (amazonWebServicesStorageConfiguration == null)
            {
                throw new Exception("Configuration should be initialized.");
            }
            _amazonWebServicesStorageConfiguration = amazonWebServicesStorageConfiguration;
        }

        public async Task<Stream> Get(string service, string folder, string key)
        {
            using (var client = _amazonWebServicesStorageConfiguration.Client())
            {
                var request = new GetObjectRequest
                {
                    BucketName = service,
                    Key = folder + "/" + key
                };
                using (var response = await client.GetObjectAsync(request))
                {
                    return response.ResponseStream;
                }
            }
        }

        public async Task Put(Stream file, string service, string folder, string key)
        {
            using (var client = _amazonWebServicesStorageConfiguration.Client())
            {
                var request = new PutObjectRequest
                {
                    InputStream = file,
                    BucketName = service,
                    CannedACL = S3CannedACL.PublicRead,
                    Key = folder + "/" + key
                };
                var response = await client.PutObjectAsync(request);
            }
        }
        
        public async Task Delete(string service, string folder, string key)
        {
            using (var client = _amazonWebServicesStorageConfiguration.Client())
            {
                var request = new DeleteObjectRequest
                {
                    BucketName = service,
                    Key = folder + "/" + key
                };
                var response = await client.DeleteObjectAsync(request);
            }
        }
        
        public async Task<IEnumerable<string>> List(string service, string folder)
        {
            using (var client = _amazonWebServicesStorageConfiguration.Client())
            {
                var request = new ListObjectsRequest { BucketName = service };
                var response = await client.ListObjectsAsync(request);
                return response.S3Objects.Select(o => o.Key);
            }
        }
    }
}