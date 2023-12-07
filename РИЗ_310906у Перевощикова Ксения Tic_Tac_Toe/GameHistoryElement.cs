using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace РИЗ_310906у_Перевощикова_Ксения_Tic_Tac_Toe
{
    public class GameHistoryElement
    {
        public DateTime GameDate { get; set; }
        public int GameWinner { get; set; } = -1;
        public string GameResult
        {
            get
            {
                if (GameWinner >= 0)
                    return $"Игрок {GameWinner+1} выиграл";
                else
                    return $"Ничья";
            }
        }
    }
}
