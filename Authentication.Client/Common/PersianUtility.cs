using System.Globalization;

namespace Authentication.Client.Common
{
    internal class PersianUtility
    {
        public static string GetMonthName(int number)
        {
            return number switch
            {
                1 => "حمل",
                2 => "ثور",
                3 => "جوزا",
                4 => "سرطان",
                5 => "اسد",
                6 => "سنبله",
                7 => "میزان",
                8 => "عقرب",
                9 => "قوس",
                10 => "جدی",
                11 => "دلو",
                12 => "حوت",
                _ => "",
            };
        }

        public static string ConvertToPersianCalender(DateTime date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            return persianCalendar.GetYear(date).ToString("0000") + "/" + persianCalendar.GetMonth(date).ToString("00") + "/" + persianCalendar.GetDayOfMonth(date).ToString("00");
        }

        public static T GetMonth<T>(DateTime date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            int month = persianCalendar.GetMonth(date);
            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(GetMonthName(month), typeof(T));
            }

            if (typeof(T) == typeof(int))
            {
                return (T)Convert.ChangeType(month, typeof(T));
            }

            return default(T);
        }

        public static T GetYear<T>(DateTime date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(persianCalendar.GetYear(date).ToString("0000"), typeof(T));
            }

            if (typeof(T) == typeof(int))
            {
                return (T)Convert.ChangeType(persianCalendar.GetYear(date), typeof(T));
            }

            return default(T);
        }

        public static int GetDaysOfMonth(DateTime date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            return persianCalendar.GetDayOfMonth(date);
        }

        public static int GetDaysInMonth(DateTime date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            int year = persianCalendar.GetYear(date);
            int month = persianCalendar.GetMonth(date);
            return persianCalendar.GetDaysInMonth(year, month);
        }

        public static bool IsToday(DateTime selectedDate, DateTime currentDate, int item, string mode)
        {
            try
            {
                int year = GetYear<int>(currentDate);
                int month = GetMonth<int>(currentDate);
                int daysOfMonth = GetDaysOfMonth(currentDate);
                if (mode == "day")
                {
                    int year2 = GetYear<int>(selectedDate);
                    int month2 = GetMonth<int>(selectedDate);
                    return year == year2 && month == month2 && daysOfMonth == item;
                }

                if (mode == "month")
                {
                    int year3 = GetYear<int>(selectedDate);
                    return year3 == year && month == item;
                }

                return year == item;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsDateTimeInRange(DateTime date, DateTime min, DateTime max, int day)
        {
            try
            {
                DateTime dateTime = new DateTime(date.Year, date.Month, day);
                return min <= dateTime && dateTime <= max;
            }
            catch
            {
                int num = DateTime.DaysInMonth(date.Year, date.Month);
                DateTime dateTime2 = new DateTime(date.Year, date.Month + 1, day - num);
                return min <= dateTime2 && dateTime2 <= max;
            }
        }
    }
}
