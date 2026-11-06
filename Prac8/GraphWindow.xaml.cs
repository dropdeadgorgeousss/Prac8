using GeoProgLib;
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

namespace Prac8
{
    /// <summary>
    /// Логика взаимодействия для GraphWindow.xaml
    /// </summary>
    public partial class GraphWindow : Window
    {
        private IReadOnlyList<int> _numbers = Array.Empty<int>();

        public GraphWindow(IReadOnlyList<int> numbers)
        {
            InitializeComponent();
            _numbers = numbers;
            Loaded += (_, _) => Draw();
            SizeChanged += (_, _) => Draw();
        }

        public void Update(IReadOnlyList<int> numbers)
        {
            _numbers = numbers;
            if (IsLoaded)
                Draw();
        }

        private void Draw()
        {
            // Статус прямо в окне графика
            bool isGeometric = ProgressionChecker.IsGeometric(_numbers);
            StatusText.Text = isGeometric
                ? "Геометрическая прогрессия"
                : "Не геометрическая прогрессия";
            StatusText.Foreground = isGeometric ? Brushes.SeaGreen : Brushes.IndianRed;

            Plot.Children.Clear();

            if (_numbers.Count == 0)
            {
                EmptyMessage.Visibility = Visibility.Visible;
                return;
            }
            EmptyMessage.Visibility = Visibility.Collapsed;

            double w = Plot.ActualWidth;
            double h = Plot.ActualHeight;
            if (w < 1 || h < 1) return;

            const double pad = 24;

            int minY = _numbers.Min();
            int maxY = _numbers.Max();
            double rangeY = Math.Max(1, maxY - minY);

            double X(int i) => _numbers.Count == 1
                ? w / 2
                : pad + i * (w - 2 * pad) / (_numbers.Count - 1);

            double Y(int v) => h - pad - (v - minY) * (h - 2 * pad) / rangeY;

            var polyline = new Polyline
            {
                Stroke = Brushes.SteelBlue,
                StrokeThickness = 2
            };
            for (int i = 0; i < _numbers.Count; i++)
                polyline.Points.Add(new Point(X(i), Y(_numbers[i])));
            Plot.Children.Add(polyline);

            for (int i = 0; i < _numbers.Count; i++)
            {
                var dot = new Ellipse { Width = 8, Height = 8, Fill = Brushes.OrangeRed };
                Canvas.SetLeft(dot, X(i) - 4);
                Canvas.SetTop(dot, Y(_numbers[i]) - 4);
                Plot.Children.Add(dot);

                var label = new TextBlock
                {
                    Text = _numbers[i].ToString(),
                    FontSize = 11,
                    Foreground = Brushes.DimGray
                };
                Canvas.SetLeft(label, X(i) + 6);
                Canvas.SetTop(label, Y(_numbers[i]) - 8);
                Plot.Children.Add(label);
            }
        }
    }
}
