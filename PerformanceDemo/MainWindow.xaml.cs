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
using System.Threading;
using System.Diagnostics;

namespace PerformanceDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var getallen = new[] { 5, 8, 3, 2, 7, 9, 4, 3 };

            var sw = Stopwatch.StartNew();

            var query = from g in getallen.AsParallel()
                        where Kwadraat(g) > 10
                        select g;

            var resultaat = query.ToList();

            sw.Stop();

            MessageBox.Show(sw.ElapsedMilliseconds.ToString());
        }

        private int Kwadraat(int g)
        {
            Thread.Sleep(1000 * g);
            return g * g;
        }

        Task<int> KwadraatAsync(int getal)
        {
            var taak = new Task<int>(() => Kwadraat(getal));

            taak.Start();

            return taak;
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int getal = int.Parse(txt.Text);
            var sw = Stopwatch.StartNew();
            var taak = new Task<int>(() => {
                
                return Kwadraat(getal);                
            });

            taak.Start();

            lbx.Items.Add($"Begin met kwadraat {(getal)} berekenen");
            await taak;

            lbx.Items.Add($"Het Kwadraat van {getal} is {taak.Result}");
            sw.Stop();

            MessageBox.Show(sw.ElapsedMilliseconds.ToString());
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int getal = int.Parse(txt.Text);

            int antwoord = await KwadraatAsync(getal);
        }
    }
}
