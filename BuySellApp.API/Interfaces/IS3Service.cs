using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;

namespace BuySellApp.API.Interfaces {
    public interface IS3Service {
        Task<S3Response> CreateBucketAsync (string name);

        Task UploadImagesAsync (string name);

        Task<bool> BucketExists (string name);

        Task<List<S3Object>> GetImagesAsync (string name);
    }
}