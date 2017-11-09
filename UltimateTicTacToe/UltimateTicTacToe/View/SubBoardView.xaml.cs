using System.Windows.Controls;
using System.Windows.Media;

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
