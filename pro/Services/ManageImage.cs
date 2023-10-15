

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using pro.Helper;
using System;
using System.IO;
using System.Threading.Tasks;


namespace pro.Services
{
    public class ManageImage : IManageImage
    {
        public async Task<string> UploadFile(IFormFile formFile)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(formFile.FileName);
                string fileName = $"{formFile.FileName}_{DateTime.Now.Ticks}{fileInfo.Extension}";
                var filePath = Common.GetFilePath(fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }

                return fileName;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it more gracefully
                throw new InvalidOperationException("Error during file upload", ex);
            }
        }

        public async Task<(byte[], string, string)> DownloadFile(string fileName)
        {
            try
            {
                var filePath = Common.GetFilePath(fileName);
                var provider = new FileExtensionContentTypeProvider();

                if (!provider.TryGetContentType(filePath, out var contentType))
                {
                    contentType = "application/octet-stream";
                }

                var fileBytes = await File.ReadAllBytesAsync(filePath);

                return (fileBytes, contentType, Path.GetFileName(filePath));
            }
            catch (Exception ex)
            {
                // Log the exception or handle it more gracefully
                throw new InvalidOperationException("Error during file download", ex);
            }
        }

        public async Task<bool> DeleteFile(string fileName)
        {
            try
            {
                var filePath = Common.GetFilePath(fileName);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it more gracefully
                throw new InvalidOperationException("Error during file deletion", ex);
            }
        }
    }



}
    

