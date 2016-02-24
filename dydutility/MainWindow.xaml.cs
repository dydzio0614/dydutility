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

        private DispatcherTimer consoleTimer;
        private DispatcherTimer userdataTimer;

        public MainWindow()
        {
            InitializeComponent();
            dydUtility = (Utility)FindResource("dydutility");
            consoleTimer = new DispatcherTimer();
            consoleTimer.Interval = TimeSpan.FromMilliseconds(200);
            consoleTimer.Tick += consoleTimer_Tick;
            consoleTimer.Start();
            userdataTimer = new DispatcherTimer();
            userdataTimer.Interval = TimeSpan.FromSeconds(2);
            userdataTimer.Tick += userdataTimer_Tick;
            userdataTimer.Start();
        }

        private void userdataTimer_Tick(object sender, EventArgs e)
        {
            if(dydUtility.GameHooked)
            {
                dydUtility.GetGameData();
            }
        }

        private void consoleTimer_Tick(object sender, EventArgs e)
        {
            if (dydUtility.GameHooked)
            {
                string consoleLog = dydUtility.ReadConsole();
                //chatViewer.Text = consoleLog;
                chatViewer.Text = dydUtility.StateMsg;
                dydUtility.ProcessLastChatLine(dydUtility.GetLastChatLine(consoleLog));
            }
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
            dydUtility.SendChatMessage("^0(^1DU^0) " + textBox.Text);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            //chatViewer.Content = dydUtility.ReadConsole();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            dydUtility.ExecuteConsoleCommand(@"announce 10 ^5If you want to ^4donate money\n^5to keep server alive\nvisit ^3pay.lugormod.tk");
        }
    }
}
