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
        public const string FILE_SENDER_TYP = "Types";

        // API
        public const string API_RES_TYP_SUC = "Success";
        public const string API_RES_TYP_WAR = "Warning";
        public const string API_RES_TYP_ERR = "Error";
        public const int API_RES_COD_SUC = 0;
        public const int API_RES_COD_WAR = 2;
        public const int API_RES_COD_ERR = 1;


        public const string API_CRE_ITM_NOTES = "Item created through API";

        public const char FILE_DELIMITER = '\t';
    }
}