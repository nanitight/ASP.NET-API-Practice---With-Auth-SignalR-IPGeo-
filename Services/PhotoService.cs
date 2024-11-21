using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using RunGroupTUT.Helpers;
using RunGroupTUT.Interfaces;

namespace RunGroupTUT.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary cloudianary;

        public PhotoService(IOptions<CloudianarySettings> config)
        {

            var acc = new Account(
                config.Value.CloudName, 
                config.Value.ApiKey,
                config.Value.ApiSecret
                );
            cloudianary = new Cloudinary(acc);

        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                ImageUploadParams uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                uploadResult = await cloudianary.UploadAsync(uploadParams);
            }
            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParamas = new DeletionParams(publicId);
            var result = await cloudianary.DestroyAsync(deleteParamas);
            return result;
        }
    }
}
