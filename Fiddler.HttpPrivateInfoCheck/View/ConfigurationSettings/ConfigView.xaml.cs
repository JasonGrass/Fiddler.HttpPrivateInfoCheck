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
            MatchHostsTextBlock.TextChanged += MatchHostsTextBlockOnTextChanged;
            FeatureEnableCheckBox.Checked += FeatureEnableCheckBoxOnChecked;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var configuration = ConfigurationsManager.Instance.Configurations;
            if (configuration.MatchHosts.Count < 1 && configuration.CheckRules.Count < 1)
            {
                ConfigurationsManager.Instance.LoadConfiguration();
                configuration = ConfigurationsManager.Instance.Configurations;
                MatchHostsTextBlock.Text = string.Join(";", configuration.MatchHosts);
                foreach (var rule in configuration.CheckRules)
                {
                    AddRuleView(rule);
                }
            }
        }

        private void MatchHostsTextBlockOnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MatchHostsTextBlock.Text))
            {
                return;
            }

            var hosts = MatchHostsTextBlock.Text.Split(new string[] {";"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim()).Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            ConfigurationsManager.Instance.Configurations.MatchHosts = hosts;
        }

        private void FeatureEnableCheckBoxOnChecked(object sender, RoutedEventArgs e)
        {
            ConfigurationsManager.Instance.Configurations.IsEnable = FeatureEnableCheckBox.IsChecked is true;
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            ConfigurationsManager.Instance.Save();
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            var rule = new CheckRule()
            {
                Id = Guid.NewGuid().ToString(),
                MatchType = RuleMatchType.String,
                Value = "",
                Message = "当出现匹配规则对应的问题时，给出的提示"
            };

            AddRuleView(rule);
            ConfigurationsManager.Instance.Configurations.CheckRules.Add(rule);
        }

        private void AddRuleView(CheckRule rule)
        {
            var ruleView = new ConfigRuleView(rule);
            CheckRulesWaterfallPanel.Children.Add(ruleView);
            ruleView.Delete += RuleViewOnDelete;
        }

        private void RuleViewOnDelete(object sender, CheckRule e)
        {
            if (sender is ConfigRuleView ruleView)
            {
                ruleView.Delete -= RuleViewOnDelete;
                CheckRulesWaterfallPanel.Children.Remove(ruleView);
            }
        }
    }
}
