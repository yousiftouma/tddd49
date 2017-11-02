using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using UltimateTicTacToe.Model;
using UltimateTicTacToe.View;
using Unity;

namespace UltimateTicTacToe
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            IBoard board = new Board(rules);
            IPlayer playerOne = new Player(MarkerType.Cross);
            IPlayer playerTwo = new Player(MarkerType.Circle);
            IGame game = new Game(board, playerOne, playerTwo);
            var mainWindow = new MainWindow(game);
            mainWindow.Show();
        }
    }
}
