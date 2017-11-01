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
        public MainWindow(IGame game)
        {
            InitializeComponent();
            _game = game;


            //
            //Registers all buttons from all subboards to the click listener btn_click, also adds the parent subboard name.
            //
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var subboard = (SubBoardView) FindName("Board" + i.ToString() + j.ToString());
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            Button button = (Button)subboard.FindName("button" + k.ToString() + l.ToString());
                            button.Name += ("Board" + i + j);
                            button.Click += btn_Click;
                        }
                    }
                    
                }
            }

        }
        /// <summary>
        /// Displays a buttons position parent subboard position when clicked. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"> Contains the source of the click.</param>
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button test = (Button)e.Source;
            Console.WriteLine(test.Name);
            Console.WriteLine(test.Parent);
        }
    }
}
