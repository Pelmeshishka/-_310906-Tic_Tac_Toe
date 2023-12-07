using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace РИЗ_310906у_Перевощикова_Ксения_Tic_Tac_Toe
{
    
    internal class Players: INotifyPropertyChanged
    {
        static string[] players = new string[] { "X", "O" };
        int _currentPlayer = 0;
        public int CurrentPlayer { 
            get { return _currentPlayer;  }
            set
            {
                _currentPlayer = value >= players.Length ? 0 : value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentPlayer"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public static string GetPlayerValue(int playerId)
        {
            return players[playerId];
        }

        public static int GetPlayersCount()
        {
            return players.Length;
        }
    }
}
