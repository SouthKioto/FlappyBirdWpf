using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;

namespace FlappyBirdWpf
{
    public partial class YourScore : Window
    {
        private class ScoreData
        {
            public string LastBestScore { get; set; }
            public string BestScore { get; set; }
        }

        public YourScore()
        {
            InitializeComponent();
            LoadAndDisplayBestScore();
        }

        private void LoadAndDisplayBestScore()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "scoring_data.json");

            if (File.Exists(filePath))
            {
                try
                {
                    string jsonData = File.ReadAllText(filePath);
                    var scoreData = JsonConvert.DeserializeObject<ScoreData>(jsonData);

                    lastbestScore.Content = scoreData.LastBestScore;
                    bestScore.Content = scoreData.BestScore;

                    if (string.IsNullOrEmpty(scoreData.BestScore))
                    {
                        bestScore.Content = scoreData.LastBestScore;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd podczas odczytu danych z pliku JSON: " + ex.Message);
                }
            }
            else
            {
                bestScore.Content = "Brak danych";
                lastbestScore.Content = "Brak danych";
            }
        }


        private void BackToMainPage(object sender, RoutedEventArgs e)
        {
            MainWindow main = new();
            main.Show();
            this.Close();
        }
    }
}
