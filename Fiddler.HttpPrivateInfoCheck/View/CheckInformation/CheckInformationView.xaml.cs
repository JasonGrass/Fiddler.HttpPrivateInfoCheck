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
using Fiddler.HttpPrivateInfoCheck.ViewModel;

namespace Fiddler.HttpPrivateInfoCheck.View.CheckInformation
{
    /// <summary>
    /// CheckInformationView.xaml 的交互逻辑
    /// </summary>
    public partial class CheckInformationView : UserControl
    {
        public CheckInformationView()
        {
            InitializeComponent();
        }

        public void AddInfo(HttpCheckInfo info)
        {
            var view = new CheckInfoCardView()
            {
                Title = info.RequestUrl,
                Message = info.Message,
                DetailTitle = info.RequestUrl,
                DetailContent = info.Detail
            };
            HttpCheckInfosWaterfallPanel.Children.Add(view);
        }

        private void ClearButton_OnClick(object sender, RoutedEventArgs e)
        {
            HttpCheckInfosWaterfallPanel.Children.Clear();
        }

    }
}
