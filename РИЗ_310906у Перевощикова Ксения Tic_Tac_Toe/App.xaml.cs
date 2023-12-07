using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace РИЗ_310906у_Перевощикова_Ксения_Tic_Tac_Toe
{
    public partial class App : Application
    {
        MainPage mainPage = new MainPage();
        GamePage gamePage = new GamePage();
        GamesHistoryPage gamesHistoryPage = new GamesHistoryPage();

        public static int RowCount = 3;

        public App()
        {
            InitializeComponent();

            mainPage.startNewGame += StartNewGame;
            mainPage.gamesHistoryShow += GamesHistoryShowPage;
            gamePage.returnToMainPage += ReturnToMainPage;
            gamePage.saveData += gamesHistoryPage.AddGameHistoryElement;
            gamesHistoryPage.returnToMainPage += ReturnToMainPage;

            MainPage = mainPage;
        }

        protected override void OnStart()
        {
           

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void StartNewGame()
        {
            gamePage.StartNewGame();
            MainPage = gamePage;
        }

        private void ReturnToMainPage()
        {
            MainPage = mainPage;
        }

        private void GamesHistoryShowPage()
        {
            MainPage = gamesHistoryPage;
        }
    }
}
