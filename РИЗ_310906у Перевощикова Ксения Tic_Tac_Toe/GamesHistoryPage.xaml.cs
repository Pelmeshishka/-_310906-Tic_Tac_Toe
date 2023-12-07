using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace РИЗ_310906у_Перевощикова_Ксения_Tic_Tac_Toe
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamesHistoryPage : ContentPage, INotifyPropertyChanged
    {
        public delegate void ReturnToMainPageEventHandler();
        public event ReturnToMainPageEventHandler returnToMainPage;

        public List<GameHistoryElement> GamesHistoryElements { get; set; }
        public GamesHistoryPage()
        {
            InitializeComponent();
            GamesHistoryElements = new List<GameHistoryElement>();
            BindingContext = this;
        }


        private void OnReturnToMainMenuClicked(object sender, EventArgs args)
        {
            returnToMainPage?.Invoke();
        }

        public void AddGameHistoryElement(GameHistoryElement element)
        {
            GamesHistoryElements.Add(element);
        }
    }
}