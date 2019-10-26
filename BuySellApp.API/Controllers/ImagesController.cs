using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3.Model;
using BuySellApp.API.DTOs;
using BuySellApp.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BuySellApp.API.Controllers {

    [Route ("api/images")]
    [ApiController]
    public class ImagesController : ControllerBase {
        private IS3Service _s3Service { get; }
        public ImagesController (IS3Service s3Service) {
            _s3Service = s3Service;
        }

        [HttpPost ("createBucket")]
        public async Task<IActionResult> CreateBucket (CreateBucketDTO createBucketDTO) {
            if (await _s3Service.BucketExists (createBucketDTO.Name)) {
                return BadRequest ("Bucket already exists");
            }
            var response = await _s3Service.CreateBucketAsync (createBucketDTO.Name);
            return Ok (response);
        }

        [HttpPost ("upload")]
        public async Task<IActionResult> UploadImages (CreateBucketDTO createBucketDTO) {
            await _s3Service.UploadImagesAsync (createBucketDTO.Name);
            return Ok ();
        }

        [HttpGet ("loadimages")]
        public async Task<IActionResult> GetImagesFromS3 (string Name) {
            List<S3Object> response = await _s3Service.GetImagesAsync (Name);
            return Ok (response);
        }
    }
}