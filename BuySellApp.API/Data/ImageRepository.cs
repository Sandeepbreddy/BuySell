using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;
using BuySellApp.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BuySellApp.API.Data {
    public class ImageRepository : IS3Service {
        private IAmazonS3 _amazonS3 { get; }
        public ImageRepository (IAmazonS3 amazonS3) {
            _amazonS3 = amazonS3;
        }
        public async Task<S3Response> CreateBucketAsync (string name) {
            try {
                var putBuckerRequest = new PutBucketRequest {
                    BucketName = name,
                    UseClientRegion = true
                };
                var response = await _amazonS3.PutBucketAsync (putBuckerRequest);

                return new S3Response {
                    Message = response.ResponseMetadata.RequestId,
                        Status = response.HttpStatusCode
                };
            } catch (AmazonS3Exception e) {
                return new S3Response {
                    Message = e.Message,
                        Status = e.StatusCode
                };
            }
        }

        public async Task UploadImagesAsync (string name) {
            var fileTransferUtility = new TransferUtility (_amazonS3);
            var imagesUploadRequest = new TransferUtilityUploadRequest {
                BucketName = name,
                FilePath = "C:\\Users\\sande\\Downloads\\car1.jpg",
                Key = name + "/" + DateTime.Now.ToString ("mm-dd-yyyy"),
                StorageClass = S3StorageClass.Standard,
                PartSize = 6291456,
                CannedACL = S3CannedACL.NoACL
            };
            imagesUploadRequest.Metadata.Add (name + "key", name);

            await fileTransferUtility.UploadAsync (imagesUploadRequest);
        }

        public async Task<List<S3Object>> GetImagesAsync (string name) {
            var request = new ListObjectsRequest {
                BucketName = name
            };
            do {
                ListObjectsResponse response = await _amazonS3.ListObjectsAsync (request);

                if (response.IsTruncated) {
                    request.Marker = response.NextMarker;
                } else {
                    request = null;
                }

                var responseStream = response.S3Objects;

                return responseStream;
            } while (request != null);

        }

        public async Task<bool> BucketExists (string name) {
            if (await AmazonS3Util.DoesS3BucketExistV2Async (_amazonS3, name) == true) {
                return true;
            }
            return false;
        }
    }
}