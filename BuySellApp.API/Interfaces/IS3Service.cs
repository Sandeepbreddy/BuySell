using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BuySellApp.API.Interfaces {
    public interface IS3Service {
        Task<S3Response> CreateBucketAsync (string name);

        Task<bool> BucketExists (string name);
    }
}