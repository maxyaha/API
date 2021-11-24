using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Shareds.Setting;


namespace Shareds.Utilizing
{
    public static class FileTransfer
    {
        public static object UploadFileAzure(string base64, string providerid, string name, string filetype)
        {
            string fileName;
            if (name.Contains(".pdf"))
            {
                fileName = name;
            }
            else
            {
                fileName = name + "." + filetype;
            }

            var result = base64.Split(',');
            if (result.Length > 1)
            {
                base64 = result[1];
            }
            else
            {
                base64 = result[0];

            }

            var pathfile = Path.Combine(providerid);
            var folderName = Guid.NewGuid().ToString();

            var current = NesPathService.Config.FolderName;

            var pathStringdirectory = Path.Combine(current, pathfile, folderName, fileName);

            var pathString = Path.Combine(current, pathfile, folderName);

            var bytess = Convert.FromBase64String(base64);
            var maxFileSize = 10 * 1024 * 1024;
            if (bytess.Length <= maxFileSize)
            {
                Directory.CreateDirectory(pathString);
                pathString = Path.GetFullPath(pathStringdirectory);
                pathfile = Path.Combine(pathfile, folderName, fileName);
                var regex = new System.Text.RegularExpressions.Regex(@"^[\w-_. ]+\.pdf$");
                if (regex.IsMatch(fileName))
                {
                    if (System.IO.File.Exists(pathString))
                    {
                        pathfile = Path.Combine(NesPathService.Config.Cifs, NesPathService.Config.Provider, pathfile);
                    }
                    else
                    {
                        using (var imageFile = File.Open(pathString, FileMode.Append))
                        {
                            imageFile.Write(bytess, 0, bytess.Length);
                            imageFile.Flush();
                        }
                        pathfile = Path.Combine(NesPathService.Config.Cifs, NesPathService.Config.Provider, pathfile);
                    }
                }
            }
            else
            {
                pathfile = null;
            }

            return new { pathfile = pathfile, directory = pathString };
        }

    }
}
