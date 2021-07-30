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
using Fiddler.HttpPrivateInfoCheck.FakeIoc;
using Fiddler.HttpPrivateInfoCheck.ViewModel;

namespace Fiddler.HttpPrivateInfoCheck.View.CheckInformation
{
    /// <summary>
    /// 展示检测到的接口信息的页面
    /// </summary>
    public partial class CheckInformationView : UserControl, ICheckInformationView
    {
        private IList<HttpCheckInfo> CheckInfos { get; }

        public CheckInformationView()
        {
            CheckInfos = new List<HttpCheckInfo>();
            InitializeComponent();
            Container.View = this;
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
            view.Tag = info;

            CheckInfos.Add(info);
            HttpCheckInfosWaterfallPanel.Children.Add(view);
        }

        //清空全部
        private void ClearButton_OnClick(object sender, RoutedEventArgs e)
        {
            HttpCheckInfosWaterfallPanel.Children.Clear();
            CheckInfos.Clear();
        }

        /// <summary>
        /// 去除重复
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DistinctButton_OnClick(object sender, RoutedEventArgs e)
        {
            IList<HttpCheckInfo> remainInfos = new List<HttpCheckInfo>();

            IList<UIElement> removeViews = new List<UIElement>();

            foreach (UIElement child in HttpCheckInfosWaterfallPanel.Children)
            {
                if (child is CheckInfoCardView { Tag: HttpCheckInfo info })
                {
                    if (remainInfos.Any(r => IsSame(r, info)))
                    {
                        removeViews.Add(child);
                        CheckInfos.Remove(info);
                        continue;
                    }
                    remainInfos.Add(info);
                }
            }

            foreach (var view in removeViews)
            {
                HttpCheckInfosWaterfallPanel.Children.Remove(view);
            }

        }

        private bool IsSame(HttpCheckInfo info1, HttpCheckInfo info2)
        {
            return string.Equals(info1.RequestUrl, info2.RequestUrl, StringComparison.Ordinal) &&
                   string.Equals(info1.Message, info2.Message, StringComparison.Ordinal);
        }

    }
}
