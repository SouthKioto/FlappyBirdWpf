using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace FlappyBirdWpf
{
    public partial class GameWindow : Window
    {
        private int gravity = 2;
        private int pipeSpeed = 2;
        private int pipeGap = 100;

        private double birdPositionY = 0;

        private bool isSpacePressed = false;

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
            if (isSpacePressed)
            {
                elapsedTime = elapsedTime.Add(TimeSpan.FromMilliseconds(60));

                birdPositionY += gravity;
                Canvas.SetTop(birdImage, birdPositionY);

                Rect birdRect = new Rect(Canvas.GetLeft(birdImage), birdPositionY, birdImage.ActualWidth, birdImage.ActualHeight);
                Rect groundRect = new Rect(Canvas.GetLeft(ground), Canvas.GetTop(ground), ground.ActualWidth, ground.ActualHeight);
                Rect pipeTopRect = new Rect(Canvas.GetLeft(pipeTop), Canvas.GetTop(pipeTop), pipeTop.ActualWidth, pipeTop.ActualHeight);
                Rect pipeDownRect = new Rect(Canvas.GetLeft(pipeDown), Canvas.GetTop(pipeDown), pipeDown.ActualWidth, pipeDown.ActualHeight);

                if (birdRect.IntersectsWith(groundRect) || birdRect.IntersectsWith(pipeTopRect) || birdRect.IntersectsWith(pipeDownRect))
                {
                    GameOver();
                }


                if (Canvas.GetLeft(pipeTop) + pipeTop.ActualWidth < 0)
                {
                    Canvas.SetLeft(pipeTop, this.Width);
                    Canvas.SetLeft(pipeDown, this.Width);

                    Random random = new Random();
                    double randomHeight = random.Next(-50, (int)this.Height - pipeGap - 50);
                    Canvas.SetTop(pipeTop, randomHeight - pipeTop.ActualHeight);
                    Canvas.SetTop(pipeDown, randomHeight + pipeGap);
                }

                Canvas.SetLeft(pipeTop, Canvas.GetLeft(pipeTop) - pipeSpeed);
                Canvas.SetLeft(pipeDown, Canvas.GetLeft(pipeDown) - pipeSpeed);


                timerLabel.Content = elapsedTime.ToString("mm':'ss");
            }
        }

        private void BirdUp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                isSpacePressed = true;
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

            if (answer == MessageBoxResult.Yes)
            {
                Canvas.SetLeft(pipeTop, this.Width);
                Canvas.SetLeft(pipeDown, this.Width);


                birdPositionY = 0;
                gravity = 5;
                elapsedTime = TimeSpan.Zero;
                timerLabel.Content = "00:00";
                isSpacePressed = false;
                timer.Start();
            }
            if (answer == MessageBoxResult.No)
            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
        }

    }
}
