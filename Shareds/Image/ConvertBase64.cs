using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Shareds.Image
{
    public class ConvertBase64
    {
        public string SavsImage(string Base64, string file)
        {
            if (string.IsNullOrEmpty(Base64) || Base64.Length % 4 != 0
           || Base64.Contains(" ") || Base64.Contains("\t") || Base64.Contains("\r") || Base64.Contains("\n"))
            {
                return null;
            }
            else
            {
                var myfilename = string.Format(@"{0}", Guid.NewGuid());

                //Generate unique filename
                Directory.CreateDirectory(file);
                string filepath = file + myfilename + ".jpeg";
                var bytess = Convert.FromBase64String(Base64);
                using (var imageFile = new FileStream(filepath, FileMode.Create))
                {
                    imageFile.Write(bytess, 0, bytess.Length);
                    imageFile.Flush();
                }
                return filepath;
            }
        }
    }


}
