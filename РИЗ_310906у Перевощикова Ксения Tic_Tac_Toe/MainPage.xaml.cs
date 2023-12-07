using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace РИЗ_310906у_Перевощикова_Ксения_Tic_Tac_Toe
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public delegate void StartNewGameEventHandler();
        public event StartNewGameEventHandler startNewGame;

        public delegate void GamesHistoryShowEventHandler();
        public event GamesHistoryShowEventHandler gamesHistoryShow;

        

        private void OnStartNewGameClicked(object sender, EventArgs args)
        {
            startNewGame?.Invoke();
        }

        private void OnGamesHistoryShowClicked(object sender, EventArgs args)
        {
            gamesHistoryShow?.Invoke();
        }

        private void OnSliderRowCountChanged(object sender, ValueChangedEventArgs args)
        {
            App.RowCount = (int)Math.Ceiling(args.NewValue);
            RowCountLabel.Text = App.RowCount.ToString();
        }
    }
}
