using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Fiddler.HttpPrivateInfoCheck.FakeIoc;
using Fiddler.HttpPrivateInfoCheck.ViewModel;
using HandyControl.Controls;
using HandyControl.Data;
using Newtonsoft.Json;
using UserControl = System.Windows.Controls.UserControl;

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

        /// <summary>
        /// 添加一个新的检测信息展示
        /// </summary>
        /// <param name="info"></param>
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
            view.Deleted += ViewItemOnDeleted;

            CheckInfos.Add(info);
            HttpCheckInfosWaterfallPanel.Children.Add(view);
            UpdateCount();
        }

        /// <summary>
        /// 清空全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_OnClick(object sender, RoutedEventArgs e)
        {
            HttpCheckInfosWaterfallPanel.Children.Clear();
            CheckInfos.Clear();
            UpdateCount();
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
                UpdateCount();
            }

            Growl.Info($"去除 {removeViews.Count} 个重复项", MainView.GrowlToken);
        }

        private void ViewItemOnDeleted(object sender, EventArgs e)
        {
            if (sender is CheckInfoCardView { Tag: HttpCheckInfo info })
            {
                CheckInfos.Remove(info);
                UpdateCount();
            }
        }

        /// <summary>
        /// 更新界面上的计数显示
        /// </summary>
        private void UpdateCount()
        {
            CountTextBlock.Text = HttpCheckInfosWaterfallPanel.Children.Count.ToString();
        }

        private bool IsSame(HttpCheckInfo info1, HttpCheckInfo info2)
        {
            return string.Equals(info1.RequestUrl, info2.RequestUrl, StringComparison.Ordinal) &&
                   string.Equals(info1.Message, info2.Message, StringComparison.Ordinal);
        }

        /// <summary>
        /// 导出数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (CheckInfos.Count < 1)
            {
                Growl.Warning($"没有内容可以导出", MainView.GrowlToken);
                return;
            }

            var dialog = new SaveFileDialog()
            {
                Title = "选择导出的文件(json)，如果存在则会覆盖",
                Filter = "JSON文件(*.json)|*.json",
                RestoreDirectory = true,
            };
            var result = dialog.ShowDialog();

            if (result != DialogResult.OK)
            {
                return;
            }

            try
            {
                var str = JsonConvert.SerializeObject(CheckInfos);
                File.WriteAllText(dialog.FileName, str);
                Growl.Success("导出成功", MainView.GrowlToken);
            }
            catch (Exception ex)
            {
                Growl.Error($"导出失败({ex.Message})", MainView.GrowlToken);
            }


        }
    }
}
