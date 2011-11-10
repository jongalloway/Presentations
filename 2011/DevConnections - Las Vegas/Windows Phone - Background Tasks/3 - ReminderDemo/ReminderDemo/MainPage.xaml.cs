using System;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Scheduler;

namespace ReminderDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void Remind_Click( object sender, RoutedEventArgs e )
        {
            Reminder r = new Reminder( "Run demo!" );
            r.Title = "Demo Reminder";
            r.Content = "Time to run the demo!";
            r.BeginTime = DateTime.Now.AddSeconds( 10 );
            r.NavigationUri = NavigationService.CurrentSource;
            ScheduledActionService.Add( r );

            #region "Alarm sample"
            Alarm alarm = new Alarm("Sample alarm");
            alarm.Content = "How alarming!!!";
            //alarm.Sound = new Uri("/Ringtones/Ring01.wma", UriKind.Relative);
            alarm.BeginTime = DateTime.Now.AddDays(2);
            alarm.ExpirationTime = DateTime.Now.AddYears(2);
            alarm.RecurrenceType = RecurrenceInterval.Weekly;

            ScheduledActionService.Add(alarm);
            #endregion
        }
    }
}