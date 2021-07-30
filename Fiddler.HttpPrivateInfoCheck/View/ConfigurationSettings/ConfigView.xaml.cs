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
using HandyControl.Controls;
using HandyControl.Data;

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
            ExcludeHostsTextBlock.TextChanged += ExcludeHostsTextBlockOnTextChanged;
            FeatureEnableCheckBox.Checked += FeatureEnableCheckBoxOnChecked;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // 如果当前配置为空，则加载配置
            var configuration = ConfigurationsManager.Instance.Configurations;
            if (configuration.MatchHosts.Count < 1 && configuration.CheckRules.Count < 1)
            {
                ConfigurationsManager.Instance.LoadConfiguration();
                configuration = ConfigurationsManager.Instance.Configurations;
            }

            MatchHostsTextBlock.Text = string.Join(";", configuration.MatchHosts);
            ExcludeHostsTextBlock.Text = string.Join(";", configuration.ExcludeHosts);
            foreach (var rule in configuration.CheckRules)
            {
                AddRuleView(rule);
            }
            FeatureEnableCheckBox.IsChecked = configuration.IsEnable;
        }

        private void MatchHostsTextBlockOnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MatchHostsTextBlock.Text))
            {
                ConfigurationsManager.Instance.Configurations.MatchHosts = new List<string>();
                return;
            }

            var hosts = MatchHostsTextBlock.Text.Split(new string[] {";"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim()).Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            ConfigurationsManager.Instance.Configurations.MatchHosts = hosts;
        }

        private void ExcludeHostsTextBlockOnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ExcludeHostsTextBlock.Text))
            {
                ConfigurationsManager.Instance.Configurations.ExcludeHosts = new List<string>();
                return;
            }

            var hosts = ExcludeHostsTextBlock.Text.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim()).Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            ConfigurationsManager.Instance.Configurations.ExcludeHosts = hosts;
        }

        private void FeatureEnableCheckBoxOnChecked(object sender, RoutedEventArgs e)
        {
            ConfigurationsManager.Instance.Configurations.IsEnable = FeatureEnableCheckBox.IsChecked is true;
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ConfigurationsManager.Instance.Save();
                Growl.Success($"保存成功", MainView.GrowlToken);
                SetChanged(false);
            }
            catch (Exception ex)
            {
                Growl.Error($"保存失败({ex.Message})", MainView.GrowlToken);
            }
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
            SetChanged();
        }

        private void AddRuleView(CheckRule rule)
        {
            var ruleView = new ConfigRuleView(rule);
            if (CheckRulesWaterfallPanel.Children.OfType<ConfigRuleView>().
                Any(v=> string.Equals(v.Rule.Id, rule.Id, StringComparison.Ordinal)))
            {
                return;
            }
            CheckRulesWaterfallPanel.Children.Add(ruleView);
            ruleView.Delete += RuleViewOnDelete;
            ruleView.ContentChanged += RuleViewOnContentChanged;
        }

        private void RuleViewOnContentChanged(object sender, EventArgs e)
        {
            SetChanged();
        }

        private void RuleViewOnDelete(object sender, CheckRule e)
        {
            if (sender is ConfigRuleView ruleView)
            {
                ruleView.Delete -= RuleViewOnDelete;
                CheckRulesWaterfallPanel.Children.Remove(ruleView);
            }
        }

        /// <summary>
        /// 设置是否有内部变更
        /// </summary>
        /// <param name="changed"></param>
        private void SetChanged(bool changed = true)
        {
            // 有变化
            if (changed)
            {
                SaveButton.Foreground = this.TryFindResource(ResourceToken.DangerBrush) as Brush;
            }
            else
            {
                SaveButton.Foreground = this.TryFindResource(ResourceToken.InfoBrush) as Brush;
            }

        }
    }
}
