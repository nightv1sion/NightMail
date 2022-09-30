using Entities.Models;
using Microsoft.AspNetCore.Http;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helpers
{
    public static class ImageConverter
    {
        /*public static UserProfileImage ToUserProfileImage(this IFormFile formFile, UserForUpdateDTO userDto)
        {
            UserProfileImage image = new UserProfileImage() { ImageName = formFile.FileName, UserId = userDto.Id};
            using var memoryStream = new MemoryStream();
            formFile.CopyTo(memoryStream);

            image.ImageData = memoryStream.ToArray();

            return image;
        }
*/
        public static IFormFile ToIFormFile(this UserProfileImage image)
        {
            var memoryStream = new MemoryStream(image.ImageData);
            IFormFile file = new FormFile(memoryStream, 0, memoryStream.Length, "ProfileImage", image.ImageName);
            return file;
        }
    }
}
