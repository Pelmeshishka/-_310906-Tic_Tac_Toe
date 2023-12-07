using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace РИЗ_310906у_Перевощикова_Ксения_Tic_Tac_Toe
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {
        public delegate void ReturnToMainPageEventHandler();
        public event ReturnToMainPageEventHandler returnToMainPage;

        public delegate void SaveDataEventHandler(GameHistoryElement savedElement);
        public event SaveDataEventHandler saveData;

        CellTTT[] cellsTTT = {};
        Players players = new Players();

        int progress = 0;

        int winner = -1;

        public GamePage()
        {
            InitializeComponent();
            gridTTT.BackgroundColor = Color.Black;
            players.PropertyChanged += OnPlayerChanged;
        }

        public void StartNewGame()
        {
            ClearData();
            this.IsEnabled = true;

            GridLength size = new GridLength(100, GridUnitType.Auto);
            double cellSize = size.Value / (App.RowCount / 3f);

            for (int i = 0; i < App.RowCount; i++)
            {
                gridTTT.ColumnDefinitions.Add(new ColumnDefinition { Width = size });
                gridTTT.RowDefinitions.Add(new RowDefinition { Height = size });
            }

            cellsTTT = new CellTTT[App.RowCount * App.RowCount];

            for (int row = 0; row < App.RowCount; row++)
            {
                for (int column = 0; column < App.RowCount; column++)
                {
                    int cellId = row + column * App.RowCount;

                    cellsTTT[cellId] = new CellTTT();

                    Button button = new Button
                    {
                        Text = "",
                        TextColor = Color.WhiteSmoke,
                        BackgroundColor = Color.FromHex("262626"),
                        FontSize = cellSize / 2f,
                        HeightRequest = cellSize,
                        WidthRequest = cellSize,
                    };
                    button.BindingContext = cellsTTT[cellId];
                    button.SetBinding(Button.TextProperty, "Value");
                    button.Clicked += (s, a) => OnCellClicked(s, a, cellId);

                    gridTTT.Children.Add(button, row, column);
                }
            }
        }

        private async void OnCellClicked(object sender, EventArgs args, int cellId)
        {
            if (!IsCellFree(cellId))
            {
                this.IsEnabled = false;
                await App.Current.MainPage.DisplayAlert("Клетка занята", "Выберите другую клетку", "Ок");
                this.IsEnabled = true;
                return;
            }

            cellsTTT[cellId].Player = players.CurrentPlayer;
            progress += 1;

            winner = FindWinner(cellId);
            if (winner >= 0)
            {
                this.IsEnabled = false;
                OnGameOverYesNoClicked(await App.Current.MainPage.DisplayAlert("Победа!", $"Игрок {winner + 1} победил!", "Новый раунд", "Выйти"));
                return;
            }

            if (progress >= App.RowCount * App.RowCount)
            {
                this.IsEnabled = false;
                OnGameOverYesNoClicked(await App.Current.MainPage.DisplayAlert("Ничья!", "В игре нет победителя", "Новый раунд", "Выйти"));
                return;
            }

            players.CurrentPlayer += 1;
            
            return;
        }

        private void OnGameOverYesNoClicked(bool newGame)
        {
            if (newGame)
            {
                SaveData();
                StartNewGame();
            }
            else
            {
                SaveData();
                ClearData();
                returnToMainPage?.Invoke();
            }
            
        }

        private void OnPlayerChanged(object sender, EventArgs ea)
        {
            currPlayerLabel.Text = $"Игрок {players.CurrentPlayer + 1}: - '{Players.GetPlayerValue(players.CurrentPlayer)}'";
        }

        private bool IsCellFree(int cellId)
        {
            return cellsTTT[cellId].Player < 0;
        }

        private int FindWinner(int cellId)
        {
            int curRow = cellId / App.RowCount;
            int curColumn = cellId % App.RowCount;

            List<int> markCells = new List<int>();

            bool fullRow = true;
            List<int> markCellsTemp = new List<int>();
            for (int column = 0; column < App.RowCount; column++)
            {
                markCellsTemp.Add(curRow + column * App.RowCount);
                if (cellsTTT[column + curRow * App.RowCount].Player != cellsTTT[cellId].Player)
                {
                    fullRow = false;
                    break;
                }
            }
            if (fullRow)
            {
                markCells.AddRange(markCellsTemp);
            }

            bool fullColumn = true;
            markCellsTemp.Clear();
            for (int row = 0; row < App.RowCount; row++)
            {
                markCellsTemp.Add(row + curColumn * App.RowCount);
                if (cellsTTT[curColumn + row * App.RowCount].Player != cellsTTT[cellId].Player)
                {
                    fullColumn = false;
                    break;
                }
            }
            if (fullColumn)
            {
                markCells.AddRange(markCellsTemp);
            }

            bool fullDiagonal = false;
            markCellsTemp.Clear();
            if (curRow == curColumn)
            {
                fullDiagonal = true;
                for (int rc = 0; rc < App.RowCount; rc++)
                {
                    markCellsTemp.Add(rc + rc * App.RowCount);
                    if (cellsTTT[rc + rc * App.RowCount].Player != cellsTTT[cellId].Player)
                    {
                        fullDiagonal = false;
                        break;
                    }
                }
                if (fullDiagonal)
                {
                    markCells.AddRange(markCellsTemp);
                }
            }

            bool fullDiagonal2 = false;
            markCellsTemp.Clear();
            if (curRow + curColumn == App.RowCount-1)
            {
                fullDiagonal2 = true;
                for (int rc = 0; rc < App.RowCount; rc++)
                {
                    markCellsTemp.Add((App.RowCount-1 - rc) + rc * App.RowCount);
                    if (cellsTTT[rc + (App.RowCount-1 - rc) * App.RowCount].Player != cellsTTT[cellId].Player)
                    {
                        fullDiagonal2 = false;
                        break;
                    }
                }
                if (fullDiagonal2)
                {
                    markCells.AddRange(markCellsTemp);
                }
            }

            if (fullRow || fullColumn || fullDiagonal || fullDiagonal2)
            {
                foreach (int id in markCells)
                {
                    gridTTT.Children[id].BackgroundColor = Color.Red;
                }
                return cellsTTT[cellId].Player;
            }
            

            return -1;
        }

        private void ClearData()
        {
            gridTTT.Children.Clear();
            gridTTT.ColumnDefinitions.Clear();
            gridTTT.RowDefinitions.Clear();
            players.CurrentPlayer = 0;
            progress = 0;

        }
    
        private void OnReturnToMainMenuClicked(object sender, EventArgs args)
        {
            ClearData();
            returnToMainPage?.Invoke();
        }

        private void SaveData()
        {
            if (winner < 0 && progress < App.RowCount * App.RowCount)
            {
                return;
            }

            saveData?.Invoke(new GameHistoryElement()
            {
                GameDate = DateTime.Now,
                GameWinner = winner
            });
        }
    }
}