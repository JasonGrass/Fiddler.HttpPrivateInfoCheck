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
            Growl.Register(GrowlToken, GrowlTipPanel);

            // 本来就是没有 Application 的，但 HandyControl 这个库依赖了 Application.Current
            var application = Application.Current;
            if (application == null)
            {
                var application1 = new Application();
                application1.Resources = this.Resources;
            }

        }

        public const string GrowlToken = "fiddler.private.info.check";
    }
}
