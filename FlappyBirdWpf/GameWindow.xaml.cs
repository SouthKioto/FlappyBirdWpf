using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace FlappyBirdWpf
{
    public partial class GameWindow : Window
    {
        private int gravity = 2;
        private int pipeSpeed = 8;
        private double birdPositionY = 0;

        private DispatcherTimer timer;
        private TimeSpan elapsedTime;

        public GameWindow()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(60);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            elapsedTime = elapsedTime.Add(TimeSpan.FromMilliseconds(30));

            birdPositionY += gravity;
            Canvas.SetTop(birdImage, birdPositionY);

            Rect birdRect = new Rect(Canvas.GetLeft(birdImage), birdPositionY, birdImage.ActualWidth, birdImage.ActualHeight);
            Rect groundRect = new Rect(Canvas.GetLeft(ground), Canvas.GetTop(ground), ground.ActualWidth, ground.ActualHeight);


            if (birdRect.IntersectsWith(groundRect))
            {
                GameOver();
            }

            timerLabel.Content = elapsedTime.ToString("mm':'ss");
        }

        private void BirdUp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                gravity = -15;
            }
        }

        private void BirdDown_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                gravity = 15;
            }
        }

        private void GameOver()
        {
            timer.Stop();

            var answer = MessageBox.Show("Game Over!\nYour time: " + elapsedTime.ToString("mm':'ss") + "\nCzy chcesz rozpocząć ponownie?", "Game Over", MessageBoxButton.YesNo);

            if(answer == MessageBoxResult.Yes)
            {
                birdPositionY = 0;
                gravity = 5;
                elapsedTime = TimeSpan.Zero;
                timerLabel.Content = "00:00";
                timer.Start();
            }
            if(answer == MessageBoxResult.No)
            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }

        }
    }
}
