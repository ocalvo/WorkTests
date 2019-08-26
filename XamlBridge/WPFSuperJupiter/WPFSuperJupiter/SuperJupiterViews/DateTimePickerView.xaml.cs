using System;
using Windows.Globalization;
using Windows.Globalization.DateTimeFormatting;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SuperJupiter.Views
{
    public sealed partial class DateTimePickerView : Page
    {
        public DateTimePickerView()
        {
            this.InitializeComponent();
            this.toggleYear.IsChecked = true;
        }

        private void showDayOfWeek_Click(object sender, RoutedEventArgs e)
        {
            // Explicitly set day format
            this.datePicker.DayFormat = "{day.integer} ({dayofweek.full})";

            StatusBlock.Text = "DatePicker with format changed to include day of week";
        }

        private void showMonthAsNumber_Click(object sender, RoutedEventArgs e)
        {
            // Explicitly set month format
            this.datePicker.MonthFormat = "{month.integer}";

            StatusBlock.Text = "DatePicker with format changed to display month as a number";
        }

        private void toggleYear_Update(object sender, RoutedEventArgs e)
        {
            // Explicitly set visibility of year
            if ((bool)this.toggleYear.IsChecked)
            {
                this.datePicker.YearVisible = true;
                StatusBlock.Text = "DatePicker with visible year component";
            }
            else
            {
                this.datePicker.YearVisible = false;
                StatusBlock.Text = "DatePicker without visible year component";
            }
        }

        private void combine_Click(object sender, RoutedEventArgs e)
        {
            DateTimeFormatter dateFormatter = new DateTimeFormatter("shortdate");
            DateTimeFormatter timeFormatter = new DateTimeFormatter("shorttime");

            // We use a calendar to determine daylight savings time transition days
            Calendar calendar = new Calendar();
            calendar.ChangeClock("24HourClock");

            // The value of the selected time in a TimePicker is stored as a TimeSpan, so it is possible to add it directly to the value of the selected date
            DateTimeOffset selectedDate = this.datePicker.Date;
            DateTimeOffset combinedValue = new DateTimeOffset(new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day) + this.timePicker.Time);

            calendar.SetDateTime(combinedValue);

            // If the day does not have 24 hours, then the user has selected a day in which a Daylight Savings Time transition occurs.
            //    It is the app developer's responsibility for validating the combination of the date and time values.
            if (calendar.NumberOfHoursInThisPeriod != 24)
            {
                StatusBlock.Text = "You selected a DST transition day";
            }
            else
            {
                StatusBlock.Text = "Combined value: " + dateFormatter.Format(combinedValue) + " " + timeFormatter.Format(combinedValue);
            }
        }

        private void changeDate_Click(object sender, RoutedEventArgs e)
        {
            // The DateTimeFormatter class formats dates and times with the user's default settings
            DateTimeFormatter dateFormatter = new DateTimeFormatter("shortdate");

            // A DateTimeOffset instantiated with a DateTime will have its Offset set to the user default
            //    (i.e. the same Offset used to display the DatePicker value)
            this.datePicker.Date = new DateTimeOffset(new DateTime(2013, 1, 31));
            StatusBlock.Text = "DatePicker date set to " + dateFormatter.Format(this.datePicker.Date);
        }

        private void changeYearRange_Click(object sender, RoutedEventArgs e)
        {
            // MinYear and MaxYear are type DateTimeOffset. We set the month to 2 to avoid time zone issues with January 1. 
            this.datePicker.MinYear = new DateTimeOffset(new DateTime(2000, 2, 1));
            this.datePicker.MaxYear = new DateTimeOffset(new DateTime(2020, 2, 1));
            StatusBlock.Text = "DatePicker year range set from 2000 to 2020";
        }
    }
}
