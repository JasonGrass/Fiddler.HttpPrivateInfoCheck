using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Fiddler.HttpPrivateInfoCheck.Configurations;
using Fiddler.HttpPrivateInfoCheck.View.CheckInformation;
using Fiddler.HttpPrivateInfoCheck.ViewModel;
using HandyControl.Controls;

namespace Fiddler.HttpPrivateInfoCheck.View
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            ConfigurationsManager.Instance.LoadConfiguration();
            InitializeComponent();

            // 本来就是没有 Application 的，但 HandyControl 这个库依赖了 Application.Current
            var app = Application.Current;
            if (app == null)
            {
                var application = new Application();
                application.Resources = this.Resources;
            }
            
            Growl.Register(GrowlToken, GrowlTipPanel);
        }

        public const string GrowlToken = "fiddler.private.info.check";
    }
}
