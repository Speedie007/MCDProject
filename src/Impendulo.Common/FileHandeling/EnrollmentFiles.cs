using System;
using System.Collections.Generic;
using System.Linq;

using Impendulo.Data.Models;
using System.Windows.Forms;
using System.IO;

namespace Impendulo.Common.FileHandeling
{
    public class EnrollmentFiles
    {
        public int FileID { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string EnrollentDocumentType { get; set; }
        public int EnrollmentDocumentTypeID { get; set; }
    }

}
