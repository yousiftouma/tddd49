using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UltimateTicTacToe.Model;
using UltimateTicTacToe.ViewModel;
using Unity;

namespace UltimateTicTacToe.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private IGame _game;
        private bool semaphore;

        public MainWindow(IGame game)
        {
            InitializeComponent();
            _game = game;
            semaphore = true;

            //
            //Registers all buttons from all subboards to the click listener btn_click, also adds the parent subboard name.
            //
            
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var subboard = (SubBoardView) FindName("Board" + i + j);
;                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            Button button = (Button)subboard.FindName("button" + k + l);
                            button.Name += ("Board" + i + j);
                            button.Click += btn_Click;
                        }
                    }
                }
            }

        }


        private Move ParseInputToMove(string input)
        {
            var move = new Move
            {
                MarkerPos = new Position((int)char.GetNumericValue(input[6]), (int)char.GetNumericValue(input[7])),
                SubboardPos = new Position((int)char.GetNumericValue(input[13]), (int)char.GetNumericValue(input[14]))
            };
            return move;
        }

        /// <summary>
        /// Displays a buttons position parent subboard position when clicked. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"> Contains the source of the click.</param>
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            if (semaphore)
            {
                semaphore = false;

                Button clickedButton = (Button)e.Source;
                Move move = ParseInputToMove(clickedButton.Name);
                if (_game.PlayOneTurn(move))
                {
                    MarkerType newMarker = _game.GetMarkerInPosition(move);
                    clickedButton.Content = newMarker.MarkerTypeToString();
                    if (_game.IsGameOver)
                    {
                        // TODO Display some text to show that the game is over
                        return;
                    }
                }
                else
                {
                    // TODO Display invalid choice of placement!
                }


                //PARSE INPUT TO MATCH GAME INPUT
                //SEND INPUT TO GAME
                //HANDLE RESULT GOOD/BAD, DISPLAY HELP TEXT
                //UPDATE CONTENT IN VIEW TO MATCH NEW DATA
                semaphore = true;
            }
        }
    }
}
