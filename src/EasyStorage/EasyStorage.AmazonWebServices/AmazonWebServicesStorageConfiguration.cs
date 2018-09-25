using Amazon;
using Amazon.S3;

namespace EasyStorage.AmazonWebServices
{
    public class AmazonWebServicesStorageConfiguration
    {
        private AmazonS3Config _configuration { get; set; }
        private readonly string _accesKey;
        private readonly string _secretKey;

        public AmazonWebServicesStorageConfiguration(string accesKey, string secretKey)
        {
            _accesKey = accesKey;
            _secretKey = secretKey;
            this.SetConfiguration();
        }

        private void SetConfiguration()
        {
            _configuration = new AmazonS3Config { RegionEndpoint = RegionEndpoint.EUWest1 };
        }

        public AmazonS3Client Client()
        {
            return new AmazonS3Client(_accesKey, _secretKey, _configuration);
        }
    }
}