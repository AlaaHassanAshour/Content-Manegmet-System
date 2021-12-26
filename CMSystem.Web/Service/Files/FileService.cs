﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CMSystem.Web.Service.Files
{
    public class FileService : IFileService
    {
            private readonly IWebHostEnvironment _env;

            public FileService(IWebHostEnvironment env)
            {
                _env = env;
            }

            public async Task<byte[]> GetFile(string folderName, string fileName)
            {
                var filePath = Path.Combine(_env.WebRootPath, folderName, fileName);
                return await File.ReadAllBytesAsync(filePath);
            }

            public async Task<string> SaveFile(IFormFile file, string folderName)
            {
                string fileName = null;
                if (file != null && file.Length > 0)
                {
                    var uploads = Path.Combine(_env.WebRootPath, folderName);
                    fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                    await using var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create);
                    await file.CopyToAsync(fileStream);
                }

                return fileName;
            }

            public async Task<string> SaveFile(byte[] file, string folderName, string extension)
            {
                string fileName = null;
                if (file != null && file.Length > 0)
                {
                    var uploads = Path.Combine(_env.WebRootPath, folderName);
                    fileName = Guid.NewGuid().ToString().Replace("-", "") + extension;
                    await File.WriteAllBytesAsync(Path.Combine(uploads, fileName), file);
                }

                return fileName;
            }

            public async Task<string> SaveFile(string file, string folderName, string extension)
            {
                string fileName = null;
                if (!string.IsNullOrWhiteSpace(file))
                {
                    var bytes = Convert.FromBase64String(file);
                    var uploads = Path.Combine(_env.WebRootPath, folderName);
                    fileName = Guid.NewGuid().ToString().Replace("-", "") + extension;
                    await File.WriteAllBytesAsync(Path.Combine(uploads, fileName), bytes);
                }

                return fileName;
            }
        }
    }

 
