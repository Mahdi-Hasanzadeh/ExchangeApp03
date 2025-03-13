using System;
using System.Globalization;
using System.Text;
namespace Authentication.Client.Common
{
    public class AfghanDate
    {
        public static string strCurDate;

        public static bool ChekTime(string Time)
        {
            try
            {
                double num = 0.0;
                string value = Time.Substring(0, 2);
                string value2 = Time.Substring(3, 2);
                num = (double)Convert.ToInt32(value) + Convert.ToDouble(value2) / 60.0;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GetTimenow(bool withSecond = false)
        {
            if (withSecond)
            {
                return DateTime.Now.TimeOfDay.ToString().Substring(0, 5);
            }

            return DateTime.Now.TimeOfDay.ToString().Substring(0, 8);
        }

        public static bool CheckDate(string dateShow)
        {
            bool flag = false;
            try
            {
                DateTime dateTime = default(DateTime);
                dateTime = Shamsi2Miladi(dateShow);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string PersianLongDate(string test)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            string text = "";
            string text2;
            string text3;
            string text4;
            string text5;
            if (string.IsNullOrEmpty(test))
            {
                text2 = persianCalendar.GetYear(DateTime.Now).ToString();
                text3 = persianCalendar.GetMonth(DateTime.Now).ToString();
                text4 = persianCalendar.GetDayOfMonth(DateTime.Now).ToString();
                text5 = persianCalendar.GetDayOfWeek(DateTime.Now).ToString();
            }
            else
            {
                DateTime time = Convert.ToDateTime(test);
                text2 = persianCalendar.GetYear(time).ToString();
                text3 = persianCalendar.GetMonth(time).ToString();
                text4 = persianCalendar.GetDayOfMonth(time).ToString();
                text5 = persianCalendar.GetDayOfWeek(time).ToString();
            }

            switch (text3)
            {
                case "1":
                    text3 = "حمل";
                    break;
                case "2":
                    text3 = "ثور";
                    break;
                case "3":
                    text3 = "جوزا";
                    break;
                case "4":
                    text3 = "سرطان";
                    break;
                case "5":
                    text3 = "اسد";
                    break;
                case "6":
                    text3 = "سنبله";
                    break;
                case "7":
                    text3 = "میزان";
                    break;
                case "8":
                    text3 = "عقرب";
                    break;
                case "9":
                    text3 = "قوس";
                    break;
                case "10":
                    text3 = "جدی";
                    break;
                case "11":
                    text3 = "دلو";
                    break;
                case "12":
                    text3 = "حوت";
                    break;
            }

            switch (text5.ToLower())
            {
                case "saturday":
                    text5 = "شنبه";
                    break;
                case "sunday":
                    text5 = "یکشنبه";
                    break;
                case "monday":
                    text5 = "دوشنبه";
                    break;
                case "tuesday":
                    text5 = "سه شنبه";
                    break;
                case "wednesday":
                    text5 = "چهارشنبه";
                    break;
                case "thursday":
                    text5 = "پنجشنبه";
                    break;
                case "friday":
                    text5 = "جمعه";
                    break;
            }

            return text5 + " " + text4 + " " + text3 + " " + text2;
        }

        public static string GetIntRozeHafte()
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            string text = persianCalendar.GetYear(DateTime.Now).ToString();
            string text2 = persianCalendar.GetMonth(DateTime.Now).ToString();
            string text3 = persianCalendar.GetDayOfMonth(DateTime.Now).ToString();
            string text4 = persianCalendar.GetDayOfWeek(DateTime.Now).ToString();
            switch (text4.ToLower())
            {
                case "saturday":
                    text4 = "0";
                    break;
                case "sunday":
                    text4 = "1";
                    break;
                case "monday":
                    text4 = "2";
                    break;
                case "tuesday":
                    text4 = "3";
                    break;
                case "wednesday":
                    text4 = "4";
                    break;
                case "thursday":
                    text4 = "5";
                    break;
                case "friday":
                    text4 = "6";
                    break;
            }

            return text4;
        }

        public static string GetMahStr(string test)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            string text = "";
            string text2;
            string text3;
            string text4;
            string text5;
            if (string.IsNullOrEmpty(test))
            {
                text2 = persianCalendar.GetYear(DateTime.Now).ToString();
                text3 = persianCalendar.GetMonth(DateTime.Now).ToString();
                text4 = persianCalendar.GetDayOfMonth(DateTime.Now).ToString();
                text5 = persianCalendar.GetDayOfWeek(DateTime.Now).ToString();
            }
            else
            {
                DateTime time = Convert.ToDateTime(test);
                text2 = persianCalendar.GetYear(time).ToString();
                text3 = persianCalendar.GetMonth(time).ToString();
                text4 = persianCalendar.GetDayOfMonth(time).ToString();
                text5 = persianCalendar.GetDayOfWeek(time).ToString();
            }

            switch (text3)
            {
                case "1":
                    text3 = "حمل";
                    break;
                case "2":
                    text3 = "ثور";
                    break;
                case "3":
                    text3 = "جوزا";
                    break;
                case "4":
                    text3 = "سرطان";
                    break;
                case "5":
                    text3 = "اسد";
                    break;
                case "6":
                    text3 = "سنبله";
                    break;
                case "7":
                    text3 = "میزان";
                    break;
                case "8":
                    text3 = "عقرب";
                    break;
                case "9":
                    text3 = "قوس";
                    break;
                case "10":
                    text3 = "جدی";
                    break;
                case "11":
                    text3 = "دلو";
                    break;
                case "12":
                    text3 = "حوت";
                    break;
            }

            switch (text5.ToLower())
            {
                case "saturday":
                    text5 = "شنبه";
                    break;
                case "sunday":
                    text5 = "یکشنبه";
                    break;
                case "monday":
                    text5 = "دوشنبه";
                    break;
                case "tuesday":
                    text5 = "سه شنبه";
                    break;
                case "wednesday":
                    text5 = "چهارشنبه";
                    break;
                case "thursday":
                    text5 = "پنجشنبه";
                    break;
                case "friday":
                    text5 = "جمعه";
                    break;
            }

            text = text5 + " " + text4 + " " + text3 + " " + text2;
            return text3;
        }

        public static string Miladi2Shamsi(DateTime date1)
        {
            try
            {
                bool flag = false;
                PersianCalendar persianCalendar = new PersianCalendar();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(persianCalendar.GetYear(date1).ToString("0000"));
                stringBuilder.Append("/");
                stringBuilder.Append(persianCalendar.GetMonth(date1).ToString("00"));
                stringBuilder.Append("/");
                stringBuilder.Append(persianCalendar.GetDayOfMonth(date1).ToString("00"));
                return stringBuilder.ToString() + " " + date1.Hour + ":" + date1.Minute;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string Miladi2ShamsiWithOutTime(DateTime date1)
        {
            try
            {
                bool flag = false;
                PersianCalendar persianCalendar = new PersianCalendar();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(persianCalendar.GetYear(date1).ToString("0000"));
                stringBuilder.Append("/");
                stringBuilder.Append(persianCalendar.GetMonth(date1).ToString("00"));
                stringBuilder.Append("/");
                stringBuilder.Append(persianCalendar.GetDayOfMonth(date1).ToString("00"));
                return stringBuilder.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string GetMah(string test)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            string text = "";
            test = Shamsi2Miladi(test).ToString();
            string text2;
            string text3;
            if (string.IsNullOrEmpty(test))
            {
                text2 = persianCalendar.GetYear(DateTime.Now).ToString();
                text3 = persianCalendar.GetMonth(DateTime.Now).ToString();
                string text4 = persianCalendar.GetDayOfMonth(DateTime.Now).ToString();
                string text5 = persianCalendar.GetDayOfWeek(DateTime.Now).ToString();
            }
            else
            {
                DateTime time = Convert.ToDateTime(test);
                text2 = persianCalendar.GetYear(time).ToString();
                text3 = persianCalendar.GetMonth(time).ToString();
                string text4 = persianCalendar.GetDayOfMonth(time).ToString();
                string text5 = persianCalendar.GetDayOfWeek(time).ToString();
            }

            switch (text3)
            {
                case "1":
                    text3 = "حمل";
                    break;
                case "2":
                    text3 = "ثور";
                    break;
                case "3":
                    text3 = "جوزا";
                    break;
                case "4":
                    text3 = "سرطان";
                    break;
                case "5":
                    text3 = "اسد";
                    break;
                case "6":
                    text3 = "سنبله";
                    break;
                case "7":
                    text3 = "میزان";
                    break;
                case "8":
                    text3 = "عقرب";
                    break;
                case "9":
                    text3 = "قوس";
                    break;
                case "10":
                    text3 = "جدی";
                    break;
                case "11":
                    text3 = "دلو";
                    break;
                case "12":
                    text3 = "حوت";
                    break;
            }

            return text3 + " " + text2;
        }

        public static DateTime Shamsi2Miladi(string date1)
        {
            int year = int.Parse(date1.Substring(0, 4));
            int month = int.Parse(date1.Substring(5, 2));
            int day = int.Parse(date1.Substring(8, 2));
            PersianCalendar persianCalendar = new PersianCalendar();
            return persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
        }

        public static DateTime Shamsi2Miladi22(string date1)
        {
            try
            {
                string s = date1.Substring(0, 4);
                int year = int.Parse(s);
                int month = int.Parse(date1.Substring(5, 2));
                int day = int.Parse(date1.Substring(8, 2));
                PersianCalendar persianCalendar = new PersianCalendar();
                return persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
            }
            catch
            {
                return DateTime.Now;
            }
        }

        public static double TimeToInt(string time)
        {
            try
            {
                string value = time.Substring(0, 2);
                string value2 = time.Substring(3, 2);
                double num = Convert.ToDouble(value);
                return num + Convert.ToDouble(value2) / 60.0;
            }
            catch
            {
                return 0.0;
            }
        }

        public static DateTime Shamsi2MiladiForDif(string date1, int Saat, int Daghighe)
        {
            try
            {
                int year = int.Parse(date1.Substring(0, 4));
                int month = int.Parse(date1.Substring(5, 2));
                int day = int.Parse(date1.Substring(8, 2));
                PersianCalendar persianCalendar = new PersianCalendar();
                return persianCalendar.ToDateTime(year, month, day, Saat, Daghighe, 0, 0);
            }
            catch
            {
                return DateTime.Now;
            }
        }

        public static string IntToTimeLong(double x)
        {
            string text = "00:00";
            double num = x % 1.0;
            x -= num;
            text = x.ToString();
            double num2 = Math.Round(60.0 * num);
            if (num2 < 10.0)
            {
                return text + ":0" + num2;
            }

            return text + ":" + num2;
        }

        public static string IntToTime(double Time)
        {
            try
            {
                string text = "";
                string text2 = "";
                double num = 0.0;
                text = ((!(Time < 10.0)) ? Time.ToString().Substring(0, 2) : ("0" + Time.ToString().Substring(0, 1)));
                num = Time - Convert.ToDouble(text);
                num = Math.Round(60.0 * num);
                text2 = ((!(num < 10.0)) ? num.ToString() : ("0" + num));
                return text + ":" + text2;
            }
            catch
            {
                return "00:00";
            }
        }

        public static string ToolShift(string Date1, string Date2)
        {
            try
            {
                double num = TimeToInt(Date1);
                double num2 = TimeToInt(Date2);
                if (num > num2)
                {
                    num2 += 24.0;
                }

                return IntToTime(num2 - num);
            }
            catch
            {
                return "00:00";
            }
        }

        public static string ToolShift(double D1, double D2)
        {
            try
            {
                if (D1 > D2)
                {
                    D2 += 24.0;
                }

                return IntToTime(D2 - D1);
            }
            catch
            {
                return "0";
            }
        }
    }
}
