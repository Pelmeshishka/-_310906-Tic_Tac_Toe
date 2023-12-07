using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace РИЗ_310906у_Перевощикова_Ксения_Tic_Tac_Toe
{
    internal class CellTTT : INotifyPropertyChanged
    {
        private string _value = "";
        public string Value => _value;

        private int _player = -1;
        public int Player
        {
            get { return _player; }
            set
            {
                _player = value;
                _value = Players.GetPlayerValue(_player);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
    }
}
