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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UltimateTicTacToe.View
{
    /// <summary>
    /// Interaction logic for SubBoardView.xaml
    /// </summary>
    public partial class SubBoardView : UserControl
    {
        public SubBoardView()
        {
            InitializeComponent();
        }

        public void SetActive(bool active)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var button = (Button)FindName("Button" + i + j);
                    if (active)
                    {
                        button.Background = Brushes.GreenYellow;
                    }
                    else
                    {
                        button.ClearValue(Button.BackgroundProperty);
                    }
                }
            }
        }
    }
}
