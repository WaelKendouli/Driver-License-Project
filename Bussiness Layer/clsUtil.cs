using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace UtilityClass
{
    public class clsUtil
    {

        public static string CreateGUID()
        {
            string GUID = Guid.NewGuid().ToString();
            return GUID;
        }

        public static string ReplaceFileNameWithGUID(ref string FileSource)
        {
            string Filename = FileSource;
            FileInfo fi = new FileInfo(Filename);
            string extn = fi.Extension;
            return CreateGUID() + extn;
        }
        public static bool CreateFolderIfNotExist(string folderPath)
        {

            // Check if the folder exists
            if (!Directory.Exists(folderPath))
            {
                // Create the folder if it doesn't exist
                Directory.CreateDirectory(folderPath);
               
                return true;
            }
            else
            {
                
                return false;
            }

        }
        public static bool CopyImageToProjectImagesFolder(ref string sourceFile)
        {
            

            if (!CreateFolderIfNotExist(sourceFile))
            {
                return false;
            }
            string DestinationFile = sourceFile + ReplaceFileNameWithGUID(ref sourceFile);

            try
            {
                File.Copy(sourceFile, DestinationFile , true);
            }
            catch (IOException ex)
            {
                
                return false;
            }
            sourceFile = DestinationFile;
            return true;
        }

    }
}
