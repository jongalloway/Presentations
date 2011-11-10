#define DEBUG_AGENT

using System.Windows;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using System;

namespace ScheduledTaskAgent1
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        private static volatile bool _classInitialized;

        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        public ScheduledAgent()
        {
            if (!_classInitialized)
            {
                _classInitialized = true;
                // Subscribe to the managed exception handler
                Deployment.Current.Dispatcher.BeginInvoke( delegate
                {
                    Application.Current.UnhandledException += ScheduledAgent_UnhandledException;
                } );
            }
        }

        /// Code to execute on Unhandled Exceptions
        private void ScheduledAgent_UnhandledException( object sender, ApplicationUnhandledExceptionEventArgs e )
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        /// <summary>
        /// Agent that runs a scheduled task
        /// </summary>
        /// <param name="task">
        /// The invoked task
        /// </param>
        /// <remarks>
        /// This method is called when a periodic or resource intensive task is invoked
        /// </remarks>
        protected override void OnInvoke( ScheduledTask task )
        {
            string toastMessage = "Periodic task running.";

            ShellToast toast = new ShellToast();
            toast.Title = "Background Agent Sample";
            toast.Content = toastMessage;
            toast.Show();

#if DEBUG_AGENT
            ScheduledActionService.LaunchForTest(
                task.Name, TimeSpan.FromSeconds(30));
#endif

            NotifyComplete();
        }
    }
}