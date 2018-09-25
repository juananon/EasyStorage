using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EasyStorage.Core
{
    public interface IEasyStorage
    {
        Task<Stream> Get(string service, string folder, string key);
        Task Put(Stream file, string service, string folder, string key);
        Task Delete(string service, string folder, string key);
        Task<IEnumerable<string>> List(string service, string folder);
    }
}