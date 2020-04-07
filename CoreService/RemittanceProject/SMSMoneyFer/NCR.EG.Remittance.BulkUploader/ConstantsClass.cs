namespace NCR.EG.Remittance.BulkUploader
{
    public static class ConstantsClass
    {
        public const int OK = 0;

        public const int ERROR_LOADING_CONFIG_FILE = 100;
        public const int ERROR_READING_LICENSE_PATH = 101;
        public const int ERROR_READING_LICENSE_NAME = 102;
        public const int ERROR_READING_LICENSE = 103;
        public const int ERROR_DECRYPT_STRING = 104;

        public const int DB_ERROR_INSERT = 200;
        public const int DB_ERROR_FIND = 201;
        public const int DB_ERROR_UPDATE = 202;
        public const int DB_NO_RECORD = 203;
        public const int DB_EXCEPTION = 204;

        public const int ATM_DISABLED = 300;
        public const int IP_CONFLICT = 301;

        public const int SYSTE_ERROR = 400;
        public const int PARSING_ERROR = 401;
        public const int ERROR_FORMING_ATM_REPLY = 402;
        public const int LICENSE_ERROR = 403;
        public const int UNKNOWN_REQUEST = 404;
        public const int ERROR_ZERO_AMOUNT = 405;
        public const int ERROR_GENERATE_REF_NO = 406;

        public const int ERROR_PARSE_CASSETTES = 500;
        public const int ERROR_ARRANGE_CASSETTES_VALUES = 501;
        public const int ERROR_CALCULATE_DISPENSED_NOTES = 502;
        public const int ERROR_CALCULATE_DISPENSED_NOTES_2 = 503;
        public const int ERROR_EXCEED_MAXIMUM_ALLOWED_DISPENSED_NOTES = 504;
        public const int ERROR_UNABLE_TO_DISPENSE = 505;
        public const int ERROR_GET_DISPENSER_FROM_AVAILABLE_CASSETTES = 506;
        //-----------------------------------------
        public const int ERROR_GENERATE_RANDOM_NUMBER = 700;
        public const int ERROR_FILE_NOT_FOUND = 701;
        public const int PROCESS_FILE_EXCEPTION = 702;
        public const int WRITE_FILE_EXCEPTION = 703;
    }
}
