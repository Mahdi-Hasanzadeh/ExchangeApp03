using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using ModernBlazorDatePicker;
using System.Globalization;

namespace Authentication.Client.Common
{
    public class AfghanModernPersianCalendar : ComponentBase
    {
        public string SelectedPersianDate = clsDate.Miladi2ShamsiWithOutTime(DateTime.Now);

        private DateTime _now;

        private string _datePickerClass = "";

        private string _currentShortYear = "";

        private string _currentShortMonth = "";

        private int _currentYear = 0;

        private int _monthDays = 0;

        private bool _isClickInside = false;

        private bool _showYearSelect = false;

        private bool _showMonthSelect = false;

        public string SelectedPersianDateProperty
        {
            get
            {
                return SelectedPersianDate;
            }
            set
            {
                SelectedPersianDate = value;
                txtChanged();
            }
        }

        [Parameter]
        public DateTime MinDateTime { get; set; } = DateTime.Now.AddYears(-100);


        [Parameter]
        public DateTime MaxDateTime { get; set; } = DateTime.Now.AddYears(100);


        [Parameter]
        public DateTime SelectedDateTime { get; set; } = default(DateTime);


        [Parameter]
        public string CssClass { get; set; } = "flex flex-row items-center gap-2";


        [Parameter]
        public string InputCssClass { get; set; } = "w-[190px] border rounded-lg p-2 focus:ring-2 focus:ring-blue-500 focus:outline-none";


        [Parameter]
        public EventCallback<DateTime> SelectedDateTimeValueChanged { get; set; }

        [Parameter]
        public bool CloseAfterSelectDay { get; set; } = true;

        //background-image: url( '/_content/ModernPersianBlazorDatePicker/icons/icon.svg');\r\n
        protected override void BuildRenderTree(RenderTreeBuilder __builder)

        {
            __builder.AddMarkupContent(0, "<style>\r\n    .Gtnbutton {\r\n  background-size: auto;\r\n  width:25px;\r\n height:25px;\r\n       border: hidden;\r\n    }\r\n</style>\r\n");
            
            __builder.OpenElement(1, "div");
            __builder.AddAttribute(2, "class", CssClass);
            __builder.OpenElement(3, "input");
            __builder.AddAttribute(4, "type", "text");
            __builder.AddAttribute(5, "class", InputCssClass);
            __builder.AddAttribute(6, "placeholder", "تاریخ");
            __builder.AddAttribute(7, "aria-label", "Example text with button addon");
            __builder.AddAttribute(8, "aria-describedby", "button-addon1");
            __builder.AddAttribute(9, "value", BindConverter.FormatValue(SelectedPersianDateProperty));
            __builder.AddAttribute(10, "onchange", EventCallback.Factory.CreateBinder(this, delegate (string? __value)
            {
                SelectedPersianDateProperty = __value;
            }, SelectedPersianDateProperty));


            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.AddMarkupContent(11, "\r\n    ");
            __builder.OpenElement(12, "button");
            __builder.AddAttribute(13, "onclick", EventCallback.Factory.Create<MouseEventArgs>((object)this, (Action)ToggleActiveDatePicker));
            __builder.AddAttribute(14, "class", "btn Gtnbutton");
            __builder.AddAttribute(15, "type", "button");
            __builder.AddAttribute(16, "id", "button-addon1");
            __builder.AddMarkupContent(17, "<img src=\"_content/ModernPersianBlazorDatePicker/icons/icons8-calendar-48.png\" alt=\"Show\" class=\"ShowBtn\">");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(18, "\r\n \r\n");
            __builder.OpenElement(19, "div");
            __builder.AddAttribute(20, "class", "modalbox " + _datePickerClass);
            __builder.AddAttribute(21, "onclick", EventCallback.Factory.Create<MouseEventArgs>((object)this, (Action)ToggleActiveDatePicker));
            __builder.OpenElement(22, "div");
            __builder.AddAttribute(23, "class", "dates");
            __builder.AddAttribute(24, "onclick", EventCallback.Factory.Create<MouseEventArgs>((object)this, (Action)SetClickInsideElement));
            __builder.OpenElement(25, "div");
            __builder.AddAttribute(26, "class", "month");
            __builder.OpenElement(27, "div");
            __builder.AddAttribute(28, "onclick", EventCallback.Factory.Create<MouseEventArgs>((object)this, (Action)GoToNextMonth));
            __builder.AddAttribute(29, "class", "arrows prev-mth");
            __builder.AddMarkupContent(30, "<img src=\"_content/ModernPersianBlazorDatePicker/icons/left.svg\" alt class=\"arrow\">");
            __builder.CloseElement();
            __builder.AddMarkupContent(31, "\r\n            ");
            __builder.OpenElement(32, "div");
            __builder.AddAttribute(33, "class", "mth");
            __builder.OpenElement(34, "span");
            __builder.AddAttribute(35, "onclick", EventCallback.Factory.Create<MouseEventArgs>((object)this, (Action)ToggleMonthSelect));
            __builder.AddAttribute(36, "class", "ym");
            __builder.AddContent(37, _currentShortMonth);
            __builder.CloseElement();
            __builder.AddContent(38, " ");
            __builder.OpenElement(39, "span");
            __builder.AddAttribute(40, "onclick", EventCallback.Factory.Create<MouseEventArgs>((object)this, (Action)ToggleYearSelect));
            __builder.AddAttribute(41, "class", "ym");
            __builder.AddContent(42, _currentShortYear);
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(43, "\r\n            ");
            __builder.OpenElement(44, "div");
            __builder.AddAttribute(45, "onclick", EventCallback.Factory.Create<MouseEventArgs>((object)this, (Action)GoToPreviousMonth));
            __builder.AddAttribute(46, "class", "arrows next-mth");
            __builder.AddMarkupContent(47, "<img src=\"_content/ModernPersianBlazorDatePicker/icons/right.svg\" alt class=\"arrow\">");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(48, "\r\n        ");
            __builder.OpenElement(49, "div");
            __builder.AddAttribute(50, "class", "days");
            for (int j = 0; j < _monthDays; j++)
            {
                int day = j + 1;
                bool isActive = IsDayActive(day);
                __builder.OpenElement(51, "div");
                __builder.AddAttribute(52, "class", "day " + (IsToday(day, "day") ? "selected-day" : "") + "\r\n                " + ((!isActive) ? "disable-day" : ""));
                __builder.AddAttribute(53, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, () => SetNewDate(day, isActive)));
                __builder.AddContent(54, day);
                __builder.AddMarkupContent(55, "<a href=\"Javascript:void(0)\"></a>");
                __builder.CloseElement();
            }

            __builder.CloseElement();
            __builder.AddMarkupContent(56, "\r\n        ");
            __builder.OpenElement(57, "div");
            __builder.AddAttribute(58, "class", "buttons");
            __builder.OpenElement(59, "div");
            __builder.AddAttribute(60, "class", "btns");
            __builder.AddAttribute(61, "onclick", EventCallback.Factory.Create<MouseEventArgs>((object)this, (Func<Task>)SetToday));
            __builder.AddMarkupContent(62, "امروز");
            __builder.CloseElement();
            __builder.AddMarkupContent(63, "\r\n            ");
            __builder.OpenElement(64, "div");
            __builder.AddAttribute(65, "class", "btns");
            __builder.AddAttribute(66, "onclick", EventCallback.Factory.Create<MouseEventArgs>((object)this, (Action)DatebeSelect));
            __builder.AddMarkupContent(67, "تایید");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(68, "\r\n        ");
            __builder.OpenElement(69, "div");
            __builder.AddAttribute(70, "class", "years-cont " + (_showYearSelect ? "years-select-active" : ""));
            __builder.AddAttribute(71, "onclick", EventCallback.Factory.Create<MouseEventArgs>((object)this, (Action)SetClickInsideElement));
            __builder.OpenElement(72, "div");
            __builder.AddAttribute(73, "class", "y-m-btns-cont");
            __builder.OpenElement(74, "div");
            __builder.AddAttribute(75, "class", "cancel-cont");
            __builder.OpenElement(76, "div");
            __builder.AddAttribute(77, "onclick", EventCallback.Factory.Create<MouseEventArgs>((object)this, (Action)ToggleYearSelect));
            __builder.AddAttribute(78, "class", "cancel-btn");
            __builder.AddMarkupContent(79, "تایید");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(80, "\r\n                ");
            __builder.OpenElement(81, "div");
            __builder.AddAttribute(82, "onclick", EventCallback.Factory.Create<MouseEventArgs>((object)this, (Action)delegate
            {
                ChangeYearRange(-25);
            }));
            __builder.AddAttribute(83, "class", "lr-btns pull-right");
            __builder.AddMarkupContent(84, "<img src=\"_content/ModernPersianBlazorDatePicker/icons/right.svg\" alt class=\"year-arrow\">");
            __builder.CloseElement();
            __builder.AddMarkupContent(85, "\r\n                ");
            __builder.OpenElement(86, "div");
            __builder.AddAttribute(87, "onclick", EventCallback.Factory.Create<MouseEventArgs>((object)this, (Action)delegate
            {
                ChangeYearRange(25);
            }));
            __builder.AddAttribute(88, "class", "lr-btns pull-left");
            __builder.AddMarkupContent(89, "<img src=\"_content/ModernPersianBlazorDatePicker/icons/left.svg\" alt class=\"year-arrow\">");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(90, "\r\n            ");
            __builder.OpenElement(91, "div");
            __builder.AddAttribute(92, "class", "y-holder");
            for (int k = 0; k < 24; k++)
            {
                int year = _currentYear + k;
                __builder.OpenElement(93, "div");
                __builder.AddAttribute(94, "onclick", EventCallback.Factory.Create<MouseEventArgs>((object)this, (Action)delegate
                {
                    SetNowDate(year, 0);
                }));
                __builder.AddAttribute(95, "class", "the-m " + (IsToday(year, "year") ? "selected-other" : ""));
                __builder.AddContent(96, year);
                __builder.CloseElement();
            }

            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(97, "\r\n        ");
            __builder.OpenElement(98, "div");
            __builder.AddAttribute(99, "class", "months-cont " + (_showMonthSelect ? "months-select-active" : ""));
            __builder.AddAttribute(100, "onclick", EventCallback.Factory.Create<MouseEventArgs>((object)this, (Action)SetClickInsideElement));
            __builder.OpenElement(101, "div");
            __builder.AddAttribute(102, "class", "y-m-btns-cont");
            __builder.OpenElement(103, "div");
            __builder.AddAttribute(104, "class", "cancel-cont");
            __builder.OpenElement(105, "div");
            __builder.AddAttribute(106, "onclick", EventCallback.Factory.Create<MouseEventArgs>((object)this, (Action)ToggleMonthSelect));
            __builder.AddAttribute(107, "class", "cancel-btn");
            __builder.AddMarkupContent(108, "تایید");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(109, "\r\n            ");
            __builder.OpenElement(110, "div");
            __builder.AddAttribute(111, "class", "m-holder");
            for (int l = 1; l <= 12; l++)
            {
                int i = l;
                __builder.OpenElement(112, "div");
                __builder.AddAttribute(113, "onclick", EventCallback.Factory.Create<MouseEventArgs>((object)this, (Action)delegate
                {
                    SetNowDate(0, i);
                }));
                __builder.AddAttribute(114, "class", "the-m " + (IsToday(i, "month") ? "selected-other" : ""));
                __builder.AddContent(115, PersianUtility.GetMonthName(l));
                __builder.CloseElement();
            }

            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
        }

        private void ClickHandler(DateTime date)
        {
            SelectedPersianDate = clsDate.Miladi2ShamsiWithOutTime(date);
            SelectedDateTime = date;
            ToggleActiveDatePicker();
            CloseDatePicker();
            StateHasChanged();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                SelectedDateTime = DateTime.Now;
                SelectedPersianDate = clsDate.Miladi2ShamsiWithOutTime(SelectedDateTime);
            }

            base.OnAfterRender(firstRender);
        }

        private void OpenDatePicker()
        {
            ToggleActiveDatePicker();
        }

        private void txtChanged()
        {
            try
            {
                if (SelectedPersianDate.Length != 10)
                {
                    return;
                }

                SelectedDateTime = clsDate.Shamsi2Miladi22(SelectedPersianDate);
                _now = clsDate.Shamsi2Miladi22(SelectedPersianDate);
                _currentYear = PersianUtility.GetYear<int>(SelectedDateTime);
                GetMonthDays();
                GetShortSelectedDate();
                StateHasChanged();
            }
            catch
            {
            }

            InvokeAsync(delegate
            {
                StateHasChanged();
            });
        }

        protected override async Task OnParametersSetAsync()
        {
            _now = SelectedDateTime;
            SelectedPersianDate = clsDate.Miladi2ShamsiWithOutTime(_now);
            _currentYear = PersianUtility.GetYear<int>(SelectedDateTime);
            GetMonthDays();
            GetShortSelectedDate();
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            if (MinDateTime == default(DateTime) || MaxDateTime == default(DateTime))
            {
                Console.WriteLine("You Must Input Valid Min & Max DateTime");
                return;
            }

            await Task.Run(delegate
            {
                _now = DateTime.Today;
                SelectedDateTime = DateTime.Today;
                SelectedPersianDate = clsDate.Miladi2ShamsiWithOutTime(SelectedDateTime);
                _currentYear = PersianUtility.GetYear<int>(SelectedDateTime);
                GetMonthDays();
                GetShortSelectedDate();
            });
        }

        private async Task UpdateSelectedDateTimeValue()
        {
            SelectedPersianDate = clsDate.Miladi2ShamsiWithOutTime(SelectedDateTime);
            await SelectedDateTimeValueChanged.InvokeAsync(SelectedDateTime);
            StateHasChanged();
        }

        private void GetShortSelectedDate()
        {
            _currentShortYear = PersianUtility.GetYear<string>(_now);
            _currentShortMonth = PersianUtility.GetMonth<string>(_now);
        }

        private void GoToNextMonth()
        {
            _now = _now.AddMonths(1);
            SetClickInsideElement();
            GetMonthDays();
            GetShortSelectedDate();
            StateHasChanged();
        }

        private void GoToPreviousMonth()
        {
            _now = _now.AddMonths(-1);
            SetClickInsideElement();
            GetMonthDays();
            GetShortSelectedDate();
            StateHasChanged();
        }

        private async Task SetNewDate(int day, bool isActive)
        {
            if (isActive)
            {
                int year = PersianUtility.GetYear<int>(_now);
                int month = PersianUtility.GetMonth<int>(_now);
                SelectedDateTime = new DateTime(year, month, day, new PersianCalendar());
                SelectedPersianDate = clsDate.Miladi2ShamsiWithOutTime(SelectedDateTime);
                _now = new DateTime(year, month, day, new PersianCalendar());
                _currentYear = year;
                await UpdateSelectedDateTimeValue();
                GetShortSelectedDate();
                if (CloseAfterSelectDay)
                {
                    DatebeSelect();
                }

                StateHasChanged();
            }
        }

        private void SetNowDate(int year, int month)
        {
            try
            {
                int num = ((year == 0) ? PersianUtility.GetYear<int>(_now) : year);
                int num2 = ((month == 0) ? PersianUtility.GetMonth<int>(_now) : month);
                int num3 = PersianUtility.GetDaysOfMonth(_now);
                if (num2 == 12 && num3 > 29)
                {
                    num3 = 29;
                }
                else if (num2 > 6 && num3 == 31)
                {
                    num3 = 29;
                }

                _now = new DateTime(num, num2, num3, new PersianCalendar());
                _currentYear = num;
                GetShortSelectedDate();
                GetMonthDays();
                GetShortSelectedDate();
                if (year == 0)
                {
                    ToggleMonthSelect();
                }
                else
                {
                    ToggleYearSelect();
                }

                StateHasChanged();
            }
            catch
            {
            }
        }

        private void GetMonthDays()
        {
            _monthDays = PersianUtility.GetDaysInMonth(_now);
            InvokeAsync(delegate
            {
                StateHasChanged();
            });
        }

        private async Task SetToday()
        {
            _now = DateTime.Today;
            SelectedDateTime = DateTime.Today;
            SelectedPersianDate = clsDate.Miladi2ShamsiWithOutTime(SelectedDateTime);
            await UpdateSelectedDateTimeValue();
            GetMonthDays();
            GetShortSelectedDate();
            SetClickInsideElement();
        }

        private void ToggleYearSelect()
        {
            _showYearSelect = !_showYearSelect;
            SetClickInsideElement();
        }

        private void ToggleMonthSelect()
        {
            _showMonthSelect = !_showMonthSelect;
            SetClickInsideElement();
        }

        private void ChangeYearRange(int parameter)
        {
            _currentYear += parameter;
            SetClickInsideElement();
        }

        public void ToggleActiveDatePicker()
        {
            if (!_isClickInside)
            {
                _datePickerClass = (string.IsNullOrEmpty(_datePickerClass) ? "active" : "");
            }

            _isClickInside = false;
            StateHasChanged();
        }

        public void CloseDatePicker()
        {
            _isClickInside = false;
            ToggleActiveDatePicker();
            StateHasChanged();
        }

        public async void DatebeSelect()
        {
            SelectedPersianDate = clsDate.Miladi2ShamsiWithOutTime(SelectedDateTime);
            _isClickInside = false;
            ToggleActiveDatePicker();
            await InvokeAsync(delegate
            {
                StateHasChanged();
            });
        }

        private void SetClickInsideElement()
        {
            _isClickInside = true;
            StateHasChanged();
        }

        private bool IsToday(int item, string mode)
        {
            return PersianUtility.IsToday(SelectedDateTime, _now, item, mode);
        }

        private bool IsDayActive(int day)
        {
            return PersianUtility.IsDateTimeInRange(_now, MinDateTime, MaxDateTime, day);
        }
    }

}
