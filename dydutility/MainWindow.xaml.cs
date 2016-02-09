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
using System.Windows.Threading;

namespace dydutility
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Utility dydUtility;

        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            dydUtility = (Utility)FindResource("dydutility");
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            bool found = dydUtility.FindJKAConsole();

            if (found)
                textBlock.Text = "Game found";
            else
                textBlock.Text = "Game not found";
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            dydUtility.SendChatMessage("TEST");
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            dydUtility.SendChatMessage(textBox.Text);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            chatViewer.Content = dydUtility.ReadConsole();
        }
    }
}
