using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Model
{
    public class Board : IBoard
    {

        private SubBoard[,] subboards;

        public Board() {

            subboards = new SubBoard[3, 3];
            InitializeBoards();
        }

        private void InitializeBoards() {
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    subboards[i, j] = new SubBoard();
                }
            }
        }



    }
}
