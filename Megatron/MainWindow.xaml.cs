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
using QuantTechnologies.Shell.Modules.SettingsManager.Models;

namespace Megatron
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Setting settings;
        public MainWindow()
        {
            InitializeComponent();

            settings = new Setting();

            settings.WriteSetting<string>("TesterPlugin", "Name0", "Hello World", "foo", eScope.Application);
            settings.WriteSetting<string>("TesterPlugin", "Name1", "Hello World", "fook", eScope.Application);
            settings.WriteSetting<string>("TesterPlugin", "Name2", "Hello World", "fooka", eScope.Application);
            settings.WriteSetting<string>("TesterPlugin", "Name3", "Hello World", "Bar", eScope.Application);
            settings.WriteSetting<string>("TesterPlugin", "Name4", "Hello World", "Barf", eScope.Application);

            string s = settings.ReadSetting<string>("TesterPlugin", "Name0");

            x = 42;
            y = 68;
        }

        [SettingAttribute(0, eScope.User)]
        int x { get; set; }
        
        [SettingAttribute(0, eScope.User)]
        int y { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var z = this;

            var foo = typeof(MainWindow).GetCustomAttributes(false);

            settings.Save();
        }

    }
}
