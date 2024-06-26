using Ocean_Home.Models.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ocean_Home.Helper
{
    public static class MediaControl
    {
        public static async Task<string> Upload(FilePath filePath , IFormFile file , IWebHostEnvironment _hostEnvironment)
        {
            var folderName = "";
            switch (filePath)
            {
                case FilePath.Project:
                    folderName = Path.Combine(_hostEnvironment.WebRootPath, "Images", "Project");
                    break;
        case FilePath.Slider:
                    folderName = Path.Combine(_hostEnvironment.WebRootPath, "Images", "Slider");
                    break;
        case FilePath.Manager:
                    folderName = Path.Combine(_hostEnvironment.WebRootPath, "Images", "Manager");
                    break;
      case FilePath.System:
                    folderName = Path.Combine(_hostEnvironment.WebRootPath, "Images", "System");
                    break;
     
            }
            string extension = Path.GetExtension(file.FileName);
            var UinqueName = Guid.NewGuid().ToString();
            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);
            using (var filestream = new FileStream(Path.Combine(folderName, UinqueName + extension), FileMode.Create))
            {
                await file.CopyToAsync(filestream);
            }
            return UinqueName + extension;
        }
        public static string Upload(FilePath filePath ,byte[] image , MediaType mediaType, IWebHostEnvironment _hostEnvironment)
        {
            string folderPath = string.Empty;
            string fileName = string.Empty;
            if(image!=null && image.Length > 0)
            {
                switch (mediaType)
                {
                    case MediaType.Image:
                        fileName = Guid.NewGuid().ToString() + ".jpg";
                        break;
                }
                switch (filePath)
                {
                    case FilePath.Project:
                        folderPath = Path.Combine(_hostEnvironment.WebRootPath, "Images", "Project");
                        break;
                     case FilePath.Slider:
                        folderPath = Path.Combine(_hostEnvironment.WebRootPath, "Images", "Slider");
                        break;
                      case FilePath.Manager:
                        folderPath = Path.Combine(_hostEnvironment.WebRootPath, "Images", "Manager");
                        break;
                    case FilePath.System:
                        folderPath = Path.Combine(_hostEnvironment.WebRootPath, "Images", "System");
                        break;
                   
                }
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                File.WriteAllBytes(Path.Combine(folderPath,fileName), image);
                return fileName;
            }
            return null;
        }
        public static string GetPath(FilePath filePath)
        {
            switch (filePath)
            {
                case FilePath.Project:
                    return "/Images/Project/";
                case FilePath.Slider:
                    return "/Images/Slider/";
                 case FilePath.Manager:
                    return "/Images/Manager/";
                 case FilePath.System:
                    return "/Images/System/";
               
                default:
                    return null;
            }
        }
    }
}
