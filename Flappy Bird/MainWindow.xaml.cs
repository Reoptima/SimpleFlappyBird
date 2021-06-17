using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using AudioPlayer.Class;
using Microsoft.Win32;

namespace Flappy_Bird_WPF
{
    public partial class MainWindow
    {
        DispatcherTimer gameTimer = new DispatcherTimer();
        double score;
        int gravity = 8;
        bool gameOver;
        Rect flappyBirdHitBox;
        public MainWindow()
        {
            InitializeComponent();
            gameTimer.Tick += MainEventTimer;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            StartGame();
            string directory = Environment.CurrentDirectory + "\\..\\..\\";
            Playlist list = new Playlist(new List<Music>
            {
                new Music {source = new Uri(directory + "AC_DC — Highway to Hell.wav"), name = "Rock"},
            });
            CustomPlayer.SetPlayList(list);
        }
        private void Grid_MouseDown(Object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void exit_Click(object sender, RoutedEventArgs e) // Закрыть программу
        {
            Close();
            Environment.Exit(0);
        }
        private void MainEventTimer(object sender, EventArgs e)
        {
            txtScore.Content = "Очки: " + score;
            flappyBirdHitBox = new Rect(Canvas.GetLeft(flappyBird), Canvas.GetTop(flappyBird), flappyBird.Width - 12,
                flappyBird.Height);
            Canvas.SetTop(flappyBird, Canvas.GetTop(flappyBird) + gravity);
            if (Canvas.GetTop(flappyBird) < -30 || Canvas.GetTop(flappyBird) + flappyBird.Height > 460)
            {
                EndGame();
            }
            foreach (var x in MyCanvas.Children.OfType<Image>())
            {
                if ((string) x.Tag == "obs1" || (string) x.Tag == "obs2" || (string) x.Tag == "obs3")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - 5);
                    if (Canvas.GetLeft(x) < -100)
                    {
                        Canvas.SetLeft(x, 800);
                        score += .5;
                    }
                    Rect PillarHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (flappyBirdHitBox.IntersectsWith(PillarHitBox))
                    {
                        EndGame();
                    }
                }
                if ((string) x.Tag == "clouds")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - 1);
                    if (Canvas.GetLeft(x) < -250)
                    {
                        Canvas.SetLeft(x, 550);
                        score += .5;
                    }
                }
            }
        }
        public String path = "";
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                flappyBird.RenderTransform = new RotateTransform(-20, flappyBird.Width / 2, flappyBird.Height / 2);
                gravity = -8;
            }
            if (e.Key == Key.R && gameOver)
            {
                StartGame();
            }
        }
        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            flappyBird.RenderTransform = new RotateTransform(5, flappyBird.Width / 2, flappyBird.Height / 2);
            gravity = 8;
        }
        private void StartGame()
        {
            MyCanvas.Focus();
            int temp = 300;
            score = 0;
            gameOver = false;
            Canvas.SetTop(flappyBird, 190);
            foreach (var x in MyCanvas.Children.OfType<Image>())
            {
                if ((string) x.Tag == "obs1")Canvas.SetLeft(x, 500);
                if ((string) x.Tag == "obs2")Canvas.SetLeft(x, 800);
                if ((string) x.Tag == "obs3")Canvas.SetLeft(x, 1100);
                if ((string) x.Tag == "clouds")Canvas.SetLeft(x, 300 + temp); temp = 800;
            }
            gameTimer.Start();
        }
        private void EndGame()
        {
            gameTimer.Stop();
            gameOver = true;
            txtScore.Content += " Вы проиграли, нажмите R";
        }
        private void Upload_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "PNG Files (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                flappyBird.Source = new BitmapImage(new Uri(op.FileName));
            }
        }
    }
}