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
using Fiddler.HttpPrivateInfoCheck.Configurations;

namespace Fiddler.HttpPrivateInfoCheck.View.ConfigurationSettings
{
    /// <summary>
    /// ConfigView.xaml 的交互逻辑
    /// </summary>
    public partial class ConfigView : UserControl
    {
        public ConfigView()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            AddRuleView(new CheckRule()
            {
                MatchType = RuleMatchType.String,
                Value = "15612341234",
                Message = "test123"
            });
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void AddRuleView(CheckRule rule)
        {
            CheckRulesWaterfallPanel.Children.Add(new ConfigRuleView(rule));
        }
    }
}
