using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.IO.Compression;
using System.Threading;

namespace NGZip
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string auth = "";
        public string path = "";
        public MainWindow(string path, string auth)
        {
            InitializeComponent();
            if (path != "")
            {
                Pathih.IsEnabled = false;
            }
            Pathih.Text = path;

            this.auth = auth;
            this.path = path;

            Exit.Click += Exit_Click;
            Download.Click += Download_Click;
        }

        private void Download_Click(object sender, RoutedEventArgs e)
        {
            path = Pathih.Text;

            if (!Directory.Exists(path))
            {
                MessageBox.Show("Папка не существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                return;
            }

            if ((bool)onefile.IsChecked)
            {
                string path_to_zip = "";
                if ((bool)zip.IsChecked)
                {
                    Directory.CreateDirectory(path+"\\tozip");
                    path_to_zip = path + "\\tozip";
                }
                else
                {
                    path_to_zip = Directory.GetParent(path).FullName;
                }

                Process arhive = new Process();
                arhive.StartInfo.UseShellExecute = false;
                arhive.StartInfo.FileName = "oleg.exe";
                arhive.StartInfo.Arguments = $" save \"{path_to_zip+"\\"+path.Split('\\').Last().Split('.').First() + ".ng"}\" \"{path}\"";
                arhive.StartInfo.CreateNoWindow = true;
                
                try
                {
                    arhive.Start();

                    arhive.WaitForExit();
                }catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка #1", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                }

                if ((bool)zip.IsChecked)
                {

                    Process ziper = new Process();
                    ziper.StartInfo.UseShellExecute = false;
                    ziper.StartInfo.FileName = "zipper\\oleginidea.exe";
                    ziper.StartInfo.Arguments = $" -zip {path_to_zip} {Directory.GetParent(path).FullName + "\\"+path.Split('\\', '/').Last().Split('.').First()+".ngzip"} {ziptype.SelectedIndex+=1}";
                    ziper.StartInfo.CreateNoWindow = true;
                    try
                    {
                        ziper.Start();

                        ziper.WaitForExit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка #2", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                    }
                    Directory.Delete(path_to_zip, true);
                }



                return;
            }


            MessageBox.Show(
                $"один файл {path + "\\tozip"} сжать {path + "\\" + path.Split('\\', '/').Last().Split('.').First() + ".ngzip"} а как {ziptype.SelectedIndex += 1}",
                "Успешно",
                MessageBoxButton.OK,
                MessageBoxImage.Information,
                MessageBoxResult.OK
                );
            Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ngonly_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)ngonly.IsChecked)
            {
                enctype.Opacity = 1;
                enctypetext.Opacity = 1;
            }
            else
            {
                enctypetext.Opacity = 0;
                enctype.Opacity = 0;
            }
        }

        private void zip_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)zip.IsChecked)
            {
                ziptype.Opacity = 1;
                ziptypetext.Opacity = 1;
            }
            else
            {
                ziptype.Opacity = 0;
                ziptypetext.Opacity = 0;
            }
        }

        private void enctype_Selected(object sender, RoutedEventArgs e)
        {
            if (enctype.SelectedIndex == 2)
            {
                keytext.Opacity = 1;
                key.Opacity = 1;
            }
            else
            {
                keytext.Opacity = 0;
                key.Opacity = 0;
            }
        }
    }
}
