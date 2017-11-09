using System;
using System.Windows;
using System.Windows.Controls;
using UltimateTicTacToe.Model;
using UltimateTicTacToe.Model.CustomExceptions;

namespace UltimateTicTacToe.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly IGame _game;
        private bool _isBoardActive;

        private readonly TextBlock _activePlayerTextBlock;
        private readonly TextBlock _gameInfoTextBlock;

        public MainWindow(IGame game)
        {
            InitializeComponent();
            _game = game;
            _isBoardActive = !_game.IsGameOver;

            _gameInfoTextBlock = (TextBlock)FindName("GameInfoTextBlock");
            if (_game.IsGameOver) DisplayWinner();
            _activePlayerTextBlock = (TextBlock) FindName("ActivePlayerTextBlock");
            _activePlayerTextBlock.Text = "Active Player: " + _game.ActivePlayer.Marker.MarkerTypeToString();

            //
            //Registers all buttons from all subboards to the click listener btn_click, also adds the parent subboard name.
            //

            var activeSubboards = _game.GetActiveSubboards();
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    var subboard = (SubBoardView) FindName("Board" + i + j);
                    subboard.SetActive(activeSubboards[i,j]);
                    for (var k = 0; k < 3; k++)
                    {
                        for (var l = 0; l < 3; l++)
                        {
                            var button = (Button)subboard.FindName("Button" + k + l);
                            button.Name += ("Board" + i + j);
                            button.Click += btn_Click;
                            var move = new Move
                            {
                                SubboardPos = new Position(i, j),
                                MarkerPos = new Position(k, l)
                            };
                            try
                            {
                                button.Content = _game.GetMarkerInPosition(move).MarkerTypeToString();
                            }
                            catch (Exception e)
                            {
                                _gameInfoTextBlock.Text =
                                    "Failed to display the game properly, try deleting GameState file and restart";
                                Console.WriteLine(
                                    $@"Got exception when initializing view, exception was {e}");
                            }
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
        /// Handle button clicks by parsing where it was clicked. 
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

            var gameIsUpdated = false;
            try
            {
                gameIsUpdated = _game.PlayOneTurn(move);
            }
            catch (MoveFailedException)
            {
                _gameInfoTextBlock.Text +=
                    $"Failed to perform move on position " +
                    $"( ({move.SubboardPos.X}, {move.SubboardPos.Y}), ({move.MarkerPos.X}, {move.MarkerPos.Y}) ).\n";
            }
            catch (SaveStateFailException)
            {
                _gameInfoTextBlock.Text += "Failed to save the state to file.\n";
            }
            if (gameIsUpdated)
            {
                // Move was valid, update view using model
                _activePlayerTextBlock.Text = "Active Player: " + _game.ActivePlayer.Marker.MarkerTypeToString();
                try
                {
                    var newMarker = _game.GetMarkerInPosition(move);
                    clickedButton.Content = newMarker.MarkerTypeToString();
                }
                catch (MoveFailedException)
                {
                    _gameInfoTextBlock.Text += "Failed to get and display marker from position " +
                            $"( ({move.SubboardPos.X}, {move.SubboardPos.Y}), ({move.MarkerPos.X}, {move.MarkerPos.Y}) ).\n";
                }

                SetActiveSubboards(_game.IsGameOver);
            }
            else
            {
                // Move was invalid, inform user in view
                _gameInfoTextBlock.Text += "Invalid move, try again!";

            }

            if (_game.IsGameOver)
            {
                DisplayWinner();
            }
            // Unlock the board again
            _isBoardActive = true;
        }

        private void DisplayWinner()
        {
            _gameInfoTextBlock.Text = _game.Winner == MarkerType.None ?
                                $"Game over! It's a draw!" :
                                $"Game over! Winner is {_game.Winner.MarkerTypeToString()}!";
        }

        private void SetActiveSubboards(bool gameover)
        {
            // Show which boards are active in view
            var activeSubboards = _game.GetActiveSubboards();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var subboard = (SubBoardView) FindName("Board" + i + j);
                    subboard?.SetActive(!gameover && activeSubboards[i, j]);
                }
            }
        }
    }
}
