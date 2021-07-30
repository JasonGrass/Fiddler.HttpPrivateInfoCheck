using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Fiddler.HttpPrivateInfoCheck.View.CheckInformation
{
    /// <summary>
    /// 检测到敏感信息的接口数据展示
    /// </summary>
    public partial class CheckInfoCardView : UserControl
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof(string), typeof(CheckInfoCardView), new PropertyMetadata(default(string)));

        public string Title
        {
            get => (string) GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            "Message", typeof(string), typeof(CheckInfoCardView), new PropertyMetadata(default(string)));

        public string Message
        {
            get => (string) GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        public static readonly DependencyProperty DetailTitleProperty = DependencyProperty.Register(
            "DetailTitle", typeof(string), typeof(CheckInfoCardView), new PropertyMetadata(default(string)));

        public string DetailTitle
        {
            get => (string) GetValue(DetailTitleProperty);
            set => SetValue(DetailTitleProperty, value);
        }

        public static readonly DependencyProperty DetailContentProperty = DependencyProperty.Register(
            "DetailContent", typeof(string), typeof(CheckInfoCardView), new PropertyMetadata(default(string)));

        public string DetailContent
        {
            get => (string) GetValue(DetailContentProperty);
            set => SetValue(DetailContentProperty, value);
        }

        public CheckInfoCardView()
        {
            InitializeComponent();
            DetailRichTextBox.Visibility = Visibility.Collapsed;
            this.DataContext = this;
        }

        private void DetailContentShow_OnClick(object sender, RoutedEventArgs e)
        {
            if (DetailRichTextBox.Visibility == Visibility.Visible)
            {
                DetailRichTextBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                DetailRichTextBox.Visibility = Visibility.Visible;
            }

        }

        /// <summary>
        /// 删除当前这个 view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clear_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.Parent is Panel panel)
            {
                panel.Children.Remove(this);
            }

        }

        private void MessageTextBox_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (DetailRichTextBox.Visibility == Visibility.Visible)
            {
                DetailRichTextBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                DetailRichTextBox.Visibility = Visibility.Visible;
            }
        }
    }
}
