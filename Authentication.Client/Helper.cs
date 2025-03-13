namespace Authentication
{
    public static class NavItemsHelper
    {
        public static List<NavbarItem> RegisterDepositeItems = new List<NavbarItem>()
        {
            new NavbarItem("برداشت / واریز وجه","/RecieptsRegistration/CashReceiptAndWithdrawal"),
            new NavbarItem("انتقال وجه","/RecieptsRegistration/TransferBetweenAccount"),
            new NavbarItem("خرید و فروش وجه","/RecieptsRegistration/CashBuyAndSell"),
            //new NavbarItem("خرید و فروش وجه(از حساب)","/RecieptsRegistration/BuyAndSellFromAccount"),
        };
        
        //دفتر کل
        public static List<NavbarItem> GeneralLedger = new List<NavbarItem>()
        {
            new NavbarItem("مشتریان","/GeneralLedger/Customers"),
            new NavbarItem("لیست کل","/GeneralLedger/GeneralList"),
            new NavbarItem("لیست طلبکاران","/GeneralLedger/CreditorsList"),
            new NavbarItem("لیست بدهکاران","/GeneralLedger/DeptorsList"),


        };

        public static List<NavbarItem> Magazine = new List<NavbarItem>()
        {
            new NavbarItem("عملیات روز","/journal/dailyOperations"),
        };


        public static List<NavbarItem> CurrencySectionItems = new List<NavbarItem>()
        {
            new("نرخ تبادله ارزها","currency/exchangeRate"),
            new("افزودن/ویرایش ارزها","currency/addOrEdit"),
        };

        public static List<NavbarItem> AccountManagement = new List<NavbarItem>()
        {
            new("حسابات مشتریان","accounts/customers"),
            new("حسابات شخصی","accounts/personal"),
        };
    }


    public class NavbarItem
    {
        public NavbarItem(string name, string href)
        {
            Name = name;
            Href = href;
        }

        public string Name { get; set; }

        public string Href { get; set; }


    }
}
