using System;
using System.Windows;
using System.Windows.Media;
using Microsoft.Phone.Controls;
using System.Windows.Threading;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.BackgroundAudio;

namespace PomodoroWP
{
    public partial class MainPage : PhoneApplicationPage
    {
        private bool _timerIsRunning = false;
        private DispatcherTimer _timer = new DispatcherTimer();
        private Pomodoro _currentPomodoro;
        private List<String> _taskList = new List<string>();
        private IsolatedStorageSettings _isoSettings;

        List<String> _interruptionMessages = new List<String>();

        public MainPage()
        {
            InitializeComponent();
            _isoSettings = IsolatedStorageSettings.ApplicationSettings;
            _timer.Interval = TimeSpan.FromSeconds( 1 );
            ToggleButton.Click += Pomodoro_Click;
            NewTaskButton.Click += new RoutedEventHandler( NewTaskButton_Click );
            TaskListBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler( TaskListBox_SelectionChanged );
            Interrupt.Click += new RoutedEventHandler( Interrupt_Click );
            _timer.Tick += timer_Tick;

        }


        void TaskListBox_SelectionChanged( object sender, System.Windows.Controls.SelectionChangedEventArgs e )
        {
            CurrentTask.Text = TaskListBox.SelectedItem.ToString();
        }

        void Interrupt_Click( object sender, RoutedEventArgs e )
        {
            NavigationService.Navigate( new Uri( "/Interruption.xaml", UriKind.Relative ) );
        }
       void NewTaskButton_Click( object sender, RoutedEventArgs e )
        {
            NavigationService.Navigate( new Uri( "/GetNewTask.xaml", UriKind.Relative ) );
        }

        private void SetTaskList()
        {
            TaskListBox.ItemsSource = _taskList;
        }

        private void RestoreState()
        {
            object outVal;
            if (_isoSettings.TryGetValue( "CurrentPomodoro", out outVal ))
            {
                _currentPomodoro = outVal as Pomodoro;
                if (_currentPomodoro != null)
                {
                    _timerIsRunning = true;
                    StartStopPomodoro();
                }
            }

            if (_isoSettings.TryGetValue( "TaskList", out outVal ))
            {
                _taskList = outVal as List<string>;
                SetTaskList();
            }

        }

        


        private void SaveState()
        {
            if (_isoSettings.Contains( "CurrentPomodoro" ))
                _isoSettings["CurrentPomodoro"] = _currentPomodoro;
            else
                _isoSettings.Add( "CurrentPomodoro", _currentPomodoro );

            if (_isoSettings.Contains( "TaskList" ))
                _isoSettings["TaskList"] = _taskList;
            else
                _isoSettings.Add( "TaskList", _taskList );


        }




        void timer_Tick( object sender, EventArgs e )
        {
                SetTimeDisplay();
            
        }

        void Pomodoro_Click( object sender, RoutedEventArgs e )
        {
            _timerIsRunning = !_timerIsRunning;
            StartStopPomodoro();
        }

        private void StartStopPomodoro()
        {
            ToggleButton.Content = _timerIsRunning ? "Stop" : "Start";
            Interrupt.IsEnabled = _timerIsRunning;
            NewTaskButton.IsEnabled = !_timerIsRunning;
            Configure.IsEnabled = !_timerIsRunning;
            TaskListBox.IsEnabled = !_timerIsRunning;


            if (_timerIsRunning)
            {

                StartPomodoro();
                SetTimeDisplay();
            }
            else
            {
                StopPomodoro();
            }

        }

        private void StartPomodoro()
        {
            if (_currentPomodoro == null)
            {
                _currentPomodoro = new Pomodoro();
            }
            _timer.Start();
            BackgroundAudioPlayer.Instance.Play();
        }

        private void SetTimeDisplay()
        {
            bool isCompleted;
            Color newColor; 
            TimerDisplay.Text = _currentPomodoro.GetDisplayString(out newColor, out isCompleted);
            if (isCompleted)
            {
                AudioPlayer.Play();
                StopPomodoro();
            }
            TimerDisplay.Foreground = new System.Windows.Media.SolidColorBrush( newColor );
        }

        private void StopPomodoro()
        {
            _timer.Stop();
            _timerIsRunning = false;

            BackgroundAudioPlayer.Instance.Pause();

            // Todo - dialog box to test user wants to stop 
            _currentPomodoro = null;
            // todo - switch between timer and break timer?
            ToggleButton.Content = "Start";



            if (ScheduledActionService.Find( "Break_Over" ) != null)
            {
                ScheduledActionService.Remove("Break_Over");
            }

            Reminder r = new Reminder("Break_Over");
            r.Title = "Back To Work";
            r.Content = "Break's Over!";
            r.BeginTime = DateTime.Now.AddSeconds(10);
            r.NavigationUri = NavigationService.CurrentSource;
            ScheduledActionService.Add(r);
        }

        protected override void OnNavigatedFrom( System.Windows.Navigation.NavigationEventArgs e )
        {
            SaveState();
            base.OnNavigatedFrom( e );
        }

        protected override void OnNavigatedTo( System.Windows.Navigation.NavigationEventArgs e )
        {
            RestoreState();


            string newMessage = string.Empty;
            if (NavigationContext.QueryString.TryGetValue( "NewTask", out newMessage ))
            {
                _taskList.Add( newMessage );
            }


            // TODO store reminders in a list to be displayed on stop 
            if (NavigationContext.QueryString.TryGetValue( "NewReminder", out newMessage ))
            {
                _interruptionMessages.Add( newMessage );
            }



            base.OnNavigatedTo( e );
        }

    }
}