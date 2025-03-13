using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Globalization;
using System.Security.Claims;

namespace Authentication.Client.Common
{

    public enum ErrorList
    {
        NotAuthenticated,
        FailedToLoadCustomerAccounts,
        ServerError,
        FailedToLoadCustomerTransactions,
        FailedToLoadCustomerBalance,
        FailedToLoadCurrencies
    }


    public static class Helper
    {

        public static readonly string BackgroundColor = "#243048";

        public static readonly Dictionary<string, string> ErrorsDictionary = new Dictionary<string, string>
    {
        { nameof(ErrorList.NotAuthenticated), "شما مجاز به این کار نیستید" },
        { nameof(ErrorList.FailedToLoadCustomerAccounts), "بارگیری اطلاعات مشتریان با مشکل مواجه شد." },
        { nameof(ErrorList.ServerError), "خطای در سرور رخ داده است." },
        { nameof(ErrorList.FailedToLoadCustomerTransactions), "بارگیری تراکنش مشتری با مشکل مواجه شد." },
        { nameof(ErrorList.FailedToLoadCustomerBalance), "بارگیری بلانس مشتری با مشکل مواجه شد." },
        { nameof(ErrorList.FailedToLoadCurrencies), "بارگیری ارزها با مشکل مواجه شد." },

    };

        /// <summary>
        /// Set the culture in cookie and inform the server section of the blazor webassembly that
        /// culture is changed
        /// </summary>
        /// <param name="Navigation"></param>
        /// <param name="culture"></param>
        public static void SetCultureInCookie(NavigationManager Navigation, CultureInfo culture)
        {
            var uri = new Uri(Navigation.Uri).GetComponents(UriComponents.PathAndQuery, UriFormat.Unescaped);
            // Redirect to update the culture on the server side
            var cultureEscaped = Uri.EscapeDataString(culture.Name);
            var uriEscaped = Uri.EscapeDataString(uri);

            Navigation.NavigateTo(
                $"Culture/Set?culture={cultureEscaped}&redirectUri={uriEscaped}",
                forceLoad: true);
        }

        public static string ExtractJWTFromToken(this AuthenticationState state)
        {
            var token = state.User.FindFirst("token")?.Value;
            if (token != null) return token;
            return string.Empty;
        }
        public static string ExtractUserIdFromToken(this AuthenticationState state)
        {
            var userId = state.User.Claims.
        FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return string.Empty;
            }
            return userId;
        }

        /// <summary>
        /// Convert numbers into persian words
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ConvertToPersianWords(decimal number)
        {
            try
            {

                if (number == 0) return "صفر";

                string[] units = { "", "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه" };
                string[] teens = { "ده", "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" };
                string[] tens = { "", "ده", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" };
                string[] thousands = { "", "هزار", "میلیون", "میلیارد", "تریلیون", "کوارترین", "کینتلیون", "سکستیلیون", "سپتولیون", "اکتولیون", "نونوچلیون" };

                var words = new List<string>();
                long num = (long)number;
                int group = 0;

                while (num > 0)
                {
                    int chunk = (int)(num % 1000);
                    num /= 1000;

                    if (chunk > 0)
                    {
                        var chunkWords = new List<string>();

                        if (chunk >= 100)
                        {
                            chunkWords.Add(units[chunk / 100] + " صد");
                            chunk %= 100;
                        }
                        if (chunk >= 10 && chunk < 20)
                        {
                            chunkWords.Add(teens[chunk - 10]);
                        }
                        else
                        {
                            if (chunk >= 20)
                            {
                                chunkWords.Add(tens[chunk / 10]);
                                chunk %= 10;
                            }
                            if (chunk > 0)
                            {
                                chunkWords.Add(units[chunk]);
                            }
                        }

                        words.Insert(0, string.Join(" و ", chunkWords) + (group > 0 ? " " + thousands[group] : ""));
                    }
                    group++;
                }

                return string.Join(" و ", words);

            }
            catch (Exception ex)
            {
                return "عدد مورد نظر بیش از حد بزرگ است";
            }
        }

    }
}
