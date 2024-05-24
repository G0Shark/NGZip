using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NGZip
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            string pather = "";
            string auth = "";
            for (int i = 0; i != e.Args.Length; ++i)
            {
                if (e.Args[i] == "/Path")
                {
                    pather = e.Args[i+=1];
                }
                if (e.Args[i] == "/Auth")
                {
                    auth = e.Args[i += 1];
                }
            }

            MainWindow wind = new MainWindow(pather, auth);
            wind.Show();
        }
    }
    
}
