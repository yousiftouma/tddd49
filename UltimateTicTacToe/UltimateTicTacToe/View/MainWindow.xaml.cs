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
        private bool _isBoardActive;

        private TextBlock _activePlayerTextBlock;
        private TextBlock _gameInfoTextBlock;

        public MainWindow(IGame game)
        {
            InitializeComponent();
            _game = game;
            _isBoardActive = true;

            _gameInfoTextBlock = (TextBlock)FindName("GameInfoTextBlock");
            _activePlayerTextBlock = (TextBlock) FindName("ActivePlayerTextBlock");
            _activePlayerTextBlock.Text = "Active Player: " + _game.ActivePlayer.Marker.MarkerTypeToString();

            //
            //Registers all buttons from all subboards to the click listener btn_click, also adds the parent subboard name.
            //

            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    var subboard = (SubBoardView) FindName("Board" + i + j);
                    subboard.SetActive(true);
                    for (var k = 0; k < 3; k++)
                    {
                        for (var l = 0; l < 3; l++)
                        {
                            var button = (Button)subboard.FindName("Button" + k + l);
                            button.Name += ("Board" + i + j);
                            button.Click += btn_Click;
                        }
                    }
                }
            }
        }


        private static Move ParseInputToMove(string input)
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
            if (!_isBoardActive)
            {
                // The board is locked, ignore click
                return;
            }
            // Lock the board and process click
            _isBoardActive = false;

            _gameInfoTextBlock.Text = "";

            var clickedButton = (Button)e.Source;
            var move = ParseInputToMove(clickedButton.Name);

            var gameIsUpdated = _game.PlayOneTurn(move);
            if (gameIsUpdated)
            {
                // Move was valid, update view using model
                _activePlayerTextBlock.Text = "Active Player: " + _game.ActivePlayer.Marker.MarkerTypeToString();
                var newMarker = _game.GetMarkerInPosition(move);
                clickedButton.Content = newMarker.MarkerTypeToString();

                SetActiveSubboards();
            }
            else
            {

                // Move was invalid, inform user in view

                _gameInfoTextBlock.Text = "Invalid move, try again!";

            }


            if (_game.IsGameOver)
            {
                _gameInfoTextBlock.Text = _game.Winner == MarkerType.None ?
                    $"Game over! It's a draw!" :
                    $"Game over! Winner is {_game.Winner.MarkerTypeToString()}!";
            }
            // Unlock the board again
            _isBoardActive = true;
        }

        private void SetActiveSubboards()
        {
            // Show which boards are active in view
            var activeSubboards = _game.GetActiveSubboards();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var subboard = (SubBoardView) FindName("Board" + i + j);
                    subboard.SetActive(activeSubboards[i, j]);
                }
            }
        }
    }
}
