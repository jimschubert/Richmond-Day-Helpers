using System;
using System.IO;
using System.Web;

namespace RichmondDay.Helpers {
    public static class FileHelpers {
        public static string BuildFullImagePath(string pathToFolder, string imageName) {
            string fullImagePath = string.Format("{0}{1}", pathToFolder, imageName);

            return fullImagePath;
        }

        public static void DeleteFile(string pathToFile) {
            if (string.IsNullOrEmpty(pathToFile))
                throw new ArgumentNullException(pathToFile);

            if (!FileExists(pathToFile))
                return;

            string fullPath = HttpContext.Current.Server.MapPath(pathToFile.Insert(0, "~"));

            File.Delete(fullPath);

        }
        
        public static bool FileExists(string fullPathAndFileName) {
            string modifiedPathAndFileName = fullPathAndFileName;

            if (!modifiedPathAndFileName.StartsWith("~"))
                modifiedPathAndFileName = modifiedPathAndFileName.Insert(0, "~");

            modifiedPathAndFileName = HttpContext.Current.Server.MapPath(modifiedPathAndFileName);

            if (!File.Exists(modifiedPathAndFileName))
                return false;

            return true;
        }

        public static void SaveFile(HttpPostedFileBase file, string pathToFolder, string fileName) {
            if (file == null || file.ContentLength == 0)
                return;

            string fullFilePath = FileHelpers.BuildFullImagePath(pathToFolder, fileName);
            
            // create the directory if it doesn't exist
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(pathToFolder)))
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(pathToFolder));

            file.SaveAs(HttpContext.Current.Server.MapPath(fullFilePath));
        }
    }
}
