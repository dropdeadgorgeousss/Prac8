using GeoProgLib;
using System.Printing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace Prac8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GraphWindow? _graphWindow;
        public MainWindow()
        {
            InitializeComponent();
        }
      

        private void OnGraphClick(object sender, RoutedEventArgs e)
        {
            if (_graphWindow is null)
            {
                _graphWindow = new GraphWindow(ParseNumbers(InputBox.Text)) { Owner = this };
                _graphWindow.Closed += (_, _) => _graphWindow = null;
            }

            _graphWindow.Update(ParseNumbers(InputBox.Text));
            _graphWindow.Show();
            _graphWindow.Activate();
        }
        private void OnInputTextChanged(object sender, TextChangedEventArgs e)
        {
            _graphWindow?.Update(ParseNumbers(InputBox.Text));
        }
        // "2 4 8 16 0"  →  [2, 4, 8, 16]
        private static List<int> ParseNumbers(string text)
        {
            var numbers = new List<int>();

            foreach (var token in text.Split(
                         new[] { ' ', '\r', '\n', '\t', ',' },
                         StringSplitOptions.RemoveEmptyEntries))
            {
                if (!int.TryParse(token, out int n)) continue;  // мусор — пропустить
                if (n == 0) break;                              // 0 — конец ввода
                numbers.Add(n);
            }

            return numbers;
        }
    }
}