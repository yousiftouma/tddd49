using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Windows;
using UltimateTicTacToe.Model;
using UltimateTicTacToe.Model.CustomExceptions;
using UltimateTicTacToe.Storage;
using UltimateTicTacToe.View;

namespace UltimateTicTacToe
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // TODO handle possible exceptions

            try
            {
                IPlayer playerOne;
                IPlayer playerTwo;
                IBoard board;
                IRules rules = new Rules();
                var playerOnesTurn = true;
                var filePath = GetFilePath("GameState.txt");
                IFileHandler fileHandler = new FileHandler(filePath);
                IDataStorageHandler storageHandler = new DataStorageHandler(fileHandler);

                // Load board if there is a saved one
                BoardDto boardDto = new BoardDto();
                var couldLoadBoard = false;
                try
                {
                    couldLoadBoard = storageHandler.LoadBoard(rules, out boardDto);
                }
                catch (Exception)
                {
                    Console.WriteLine(@"Failed to load board from file, will init a new game.");
                }
                if (couldLoadBoard)
                {
                    board = new Board(rules, boardDto.Winner, boardDto.Subboards);
                    playerOne = new Player(boardDto.PlayerOne);
                    playerTwo = new Player(boardDto.PlayerTwo);
                    playerOnesTurn = boardDto.IsPlayerOneTurn;
                }
                else
                {
                    board = new Board(rules);
                    playerOne = new Player(MarkerType.Cross);
                    playerTwo = new Player(MarkerType.Circle);
                }
                IGame game = new Game(board, playerOne, playerTwo, playerOnesTurn, storageHandler);
                var mainWindow = new MainWindow(game);
                mainWindow.Show();
            }
            catch (Exception exception)
            {
                Console.WriteLine($@"Failed starting the game, got exception {exception}");
            }
        }

        private static string GetFilePath(string filename)
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            var folder = Path.GetDirectoryName(path);
            return folder + "\\" + filename;
        }
    }
}
