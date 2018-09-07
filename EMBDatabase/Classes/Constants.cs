using System;
using System.Linq;
using System.Web;

namespace EMBDatabase.Classes
{
    public struct Constants
    {
        public const string FILE_TYPE_IMAGE = "Images";
        public const string FILE_TYPE_DOC = "Documents";


        public const string FILE_SENDER_PART = "Parts";
        public const string FILE_SENDER_MAN = "Manufacturers";
        public const string FILE_SENDER_LOC = "Locations";
        public const string FILE_SENDER_PCK = "Packages";

        public const char FILE_DELIMITER = '\t';
    }
}