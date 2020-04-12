using System;

namespace NCR.EG.Remittance.BulkUploader.Model
{
    public class FileTransaction
    {
        public string MobileNumber { get; internal set; }
        public int Amount { get; internal set; }
        public string NationalID { get; internal set; }
        public string Name { get; internal set; }
        public string ReferenceNumber { get; internal set; }
        public int RemitterId { get; internal set; }



        public FileTransaction(string _mob, string _nationalId, string _name, int remitterId, int _amount)
        {
            MobileNumber = _mob;
            Amount = _amount;
            NationalID = _nationalId;
            Name = _name;
            RemitterId = remitterId;
        }

        internal int GenerateReference()
        {
            string refNum = string.Empty;
            int retVal = GenerateReference2();
            if (retVal == ConstantsClass.OK)
            {
                ReferenceNumber = refNum;
                return ConstantsClass.OK;
            }
            else
            {
                return ConstantsClass.ERROR_GENERATE_REF_NO;
            }
        }

        internal int GenerateReference2()
        {
            // 'This function generate Id depending on the Date / Time up to seconds
            // ' to be sure that no doublicate Id's are generated .... a sleep for one second
            // ' is invoked and all threads asking for iD's are  locked .
            int yy;
            int mon;
            int dy;
            int hh;
            int mm;
            int ss;
            int ff;
            int datecode;
            string datecodestr;
            int timecode;
            string timecodestr;
            DateTime ts = new DateTime();
            string k11str;
            string k12str = "";

            try
            {
                System.Threading.Thread.Sleep(1);
                var days = DateTime.Now.ToString("yyMMdd");
                var ttimes = DateTime.Now.ToString("HHmmssffffff");
                long vOut = Convert.ToInt64(ttimes);
                var total = vOut + System.Convert.ToInt64(days);
                var thref = "";
                AddCheckDigit(total.ToString(), ref thref);
                if (thref != "")
                {
                    ReferenceNumber = thref;
                    return ConstantsClass.OK;
                }
                else
                {
                    ReferenceNumber = "";
                    return ConstantsClass.ERROR_GENERATE_REF_NO;
                }


                yy = ts.Year - 2000;
                yy = yy & 0x7F;
                mon = ts.Month;
                dy = ts.Day;
                yy = yy << 9;
                mon = mon & 0xF;
                mon = mon << 5;
                dy = dy & 0x1F;
                hh = ts.Hour;
                mm = ts.Minute;
                ss = ts.Second;
                ff = ts.Millisecond;
                hh = hh & 0x1F;
                hh = hh << 12;
                mm = mm & 0x3F;
                mm = mm << 6;
                ss = ss & 0x3F;
                ss = ss << 3;
                ff = ff & 0x3F;
                ff = ff << 1;
                datecode = yy + mon + dy;
                timecode = hh + mm + ss + ff;
                datecodestr = datecode.ToString("00000");
                timecodestr = timecode.ToString("000000");
                k11str = datecodestr + timecodestr;
                AddCheckDigit(k11str, ref k12str);
                ReferenceNumber = k12str;
                return ConstantsClass.OK;
            }
            catch (Exception ex)
            {
                LogClass.LogError(ex, "HelperFunctionsClass.GenerateReferenceNo", "GenerateReferenceNo, Excption ");
                return ConstantsClass.ERROR_GENERATE_REF_NO;
            }
        }
        private static void AddCheckDigit(string idNoCD, ref string idCD)
        {
            string sid, sidpure, c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11;
            int v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, vt, cdg = -1;
            if (idNoCD.Length < 11)
            {
                idCD = "";
                return;
            }
            sidpure = idNoCD;
            c1 = sidpure.Substring(10, 1);
            c2 = sidpure.Substring(9, 1);
            c3 = sidpure.Substring(8, 1);
            c4 = sidpure.Substring(7, 1);
            c5 = sidpure.Substring(6, 1);
            c6 = sidpure.Substring(5, 1);
            c7 = sidpure.Substring(4, 1);
            c8 = sidpure.Substring(3, 1);
            c9 = sidpure.Substring(2, 1);
            c10 = sidpure.Substring(1, 1);
            c11 = sidpure.Substring(0, 1);

            v2 = System.Convert.ToInt32(c2);
            v4 = System.Convert.ToInt32(c4);
            v6 = System.Convert.ToInt32(c6);
            v8 = System.Convert.ToInt32(c8);
            v10 = System.Convert.ToInt32(c10);
            v1 = 2 * System.Convert.ToInt32(c1);
            if ((v1 >= 10))
                v1 = v1 - 9;
            v3 = 2 * System.Convert.ToInt32(c3);
            if ((v3 >= 10))
                v3 = v3 - 9;
            v5 = 2 * System.Convert.ToInt32(c5);
            if ((v5 >= 10))
                v5 = v5 - 9;
            v7 = 2 * System.Convert.ToInt32(c7);
            if ((v7 >= 10))
                v7 = v7 - 9;
            v9 = 2 * System.Convert.ToInt32(c9);
            if ((v9 >= 10))
                v9 = v9 - 9;
            v11 = 2 * System.Convert.ToInt32(c11);
            if ((v11 >= 10))
                v11 = v11 - 9;
            vt = v1 + v2 + v3 + v4 + v5 + v6 + v7 + v8 + v9 + v10 + v11;
            cdg = (10 - vt % 10) % 10;
            sid = sidpure.Substring(0, 11) + cdg;
            idCD = sid;
        }



    }
}
