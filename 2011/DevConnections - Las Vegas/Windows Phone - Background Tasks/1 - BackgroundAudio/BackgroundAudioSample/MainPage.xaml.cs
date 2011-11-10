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
using System.Windows.Navigation;
using Microsoft.Phone.BackgroundAudio;
namespace BackgroundAudioSample
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            BackgroundAudioPlayer.Instance.PlayStateChanged +=
                 new EventHandler( Instance_PlayStateChanged );
        }
        void Instance_PlayStateChanged( object sender, EventArgs e )
        {
            switch (BackgroundAudioPlayer.Instance.PlayerState)
            {
                case PlayState.Playing:
                    playButton.Content = "pause";
                    break;

                case PlayState.Paused:
                case PlayState.Stopped:
                    playButton.Content = "play";
                    break;
            }

            if ( BackgroundAudioPlayer.Instance.Track != null)
            {
                txtCurrentTrack.Text = BackgroundAudioPlayer.Instance.Track.Title +
                                       " by " +
                                       BackgroundAudioPlayer.Instance.Track.Artist;
            }
        }

        protected override void OnNavigatedTo( NavigationEventArgs e )
        {
            if (PlayState.Playing == BackgroundAudioPlayer.Instance.PlayerState)
            {
                playButton.Content = "pause";
                txtCurrentTrack.Text = BackgroundAudioPlayer.Instance.Track.Title +
                                 " by " +
                                 BackgroundAudioPlayer.Instance.Track.Artist;

            }
            else
            {
                playButton.Content = "play";
                txtCurrentTrack.Text = "";
            }
        }

        private void prevButton_Click( object sender, RoutedEventArgs e )
        {
            BackgroundAudioPlayer.Instance.SkipPrevious();
        }

        private void playButton_Click( object sender, RoutedEventArgs e )
        {
            if (PlayState.Playing == BackgroundAudioPlayer.Instance.PlayerState)
            {
                BackgroundAudioPlayer.Instance.Pause();
            }
            else
            {
                BackgroundAudioPlayer.Instance.Play();
            }
        }

        private void nextButton_Click( object sender, RoutedEventArgs e )
        {
            BackgroundAudioPlayer.Instance.SkipNext();
        }

    }
}