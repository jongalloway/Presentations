#define DEBUG_AGENT

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Scheduler;

namespace BackgroundAgents
{
    public partial class MainPage : PhoneApplicationPage
    {
        PeriodicTask periodicTask;
        string periodicTaskName = "PeriodicAgent";
        public bool AgentIsEnabled = true;

        public MainPage()
        {
            InitializeComponent();
        }

        private void RemoveAgent( string name )
        {
            try
            {
                ScheduledActionService.Remove( name );
            }
            catch (Exception)
            {
            }
        }

        private void PeriodicCheckBox_Checked( object sender, RoutedEventArgs e )
        {
            StartPeriodicAgent();
        }
        private void PeriodicCheckBox_Unchecked( object sender, RoutedEventArgs e )
        {
            RemoveAgent( periodicTaskName );
        }

        private void StartPeriodicAgent()
        {
            AgentIsEnabled = true;

            // If this task already exists, remove it
            periodicTask = ScheduledActionService.Find( periodicTaskName ) as PeriodicTask;
            if (periodicTask != null)
            {
                RemoveAgent( periodicTaskName );
            }

            periodicTask = new PeriodicTask( periodicTaskName );
            periodicTask.Description = "This demonstrates a periodic task.";

            // Place the call to Add in a try block in case the user has disabled agents.
            try
            {
                ScheduledActionService.Add( periodicTask );
                PeriodicStackPanel.DataContext = periodicTask;

                // If debugging is enabled, use LaunchForTest to launch the agent in one minute.
#if(DEBUG_AGENT)
                ScheduledActionService.LaunchForTest( periodicTaskName, TimeSpan.FromSeconds( 20 ) );
#endif
            }
            catch (InvalidOperationException exception)
            {
                if (exception.Message.Contains( "BNS Error: The action is disabled" ))
                {
                    MessageBox.Show( "Background agents for this application have been disabled by the user." );
                    AgentIsEnabled = false;
                    PeriodicCheckBox.IsChecked = false;
                }
            }
        }


        protected override void OnNavigatedTo( System.Windows.Navigation.NavigationEventArgs e )
        {

            periodicTask = ScheduledActionService.Find( periodicTaskName ) as PeriodicTask;

            if (periodicTask != null)
            {
                PeriodicStackPanel.DataContext = periodicTask;
            }



        }

    }
}