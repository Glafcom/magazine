using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagazineApp.Web.Helpers {
    public static class FileHelper {
        public static byte[] SetUploadedFileToBytes(HttpPostedFileBase file) {
            var fileLength = file.ContentLength;
            byte[] bFile = new byte[fileLength];
            file.InputStream.Read(bFile, 0, fileLength);
            return bFile;
        }
    }
}