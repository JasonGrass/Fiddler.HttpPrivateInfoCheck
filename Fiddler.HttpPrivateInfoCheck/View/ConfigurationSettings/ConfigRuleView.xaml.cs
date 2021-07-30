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
    /// 单条规则的展示 view
    /// </summary>
    public partial class ConfigRuleView : UserControl
    {
        private static readonly Brush EnableBrush = new SolidColorBrush(Color.FromRgb(183, 235, 143));
        private static readonly Brush OffEnableBrush = new SolidColorBrush(Color.FromRgb(217, 217, 217));

        /// <summary>
        /// 规则描述的数据模型
        /// </summary>
        public CheckRule Rule { get; set; }

        /// <summary>
        /// 规则被删除之后触发
        /// </summary>
        public event EventHandler<CheckRule> Delete;

        /// <summary>
        /// 使用规则配置数据，创建一个规则展示 view
        /// </summary>
        /// <param name="rule"></param>
        public ConfigRuleView(CheckRule rule)
        {
            Rule = rule;
            InitializeComponent();
            
            // 匹配模式
            MatchTypeComboBox.Items.Add(new MatchTypeInfoHelper(RuleMatchType.String, "字符串"));
            MatchTypeComboBox.Items.Add(new MatchTypeInfoHelper(RuleMatchType.Regex, "正则表达式"));
            MatchTypeComboBox.SelectedIndex = rule.MatchType == RuleMatchType.Regex ? 1 : 0;

            // pattern 字符串的值
            PatternValueTextBox.Text = rule.Value;

            // 提示信息的配置
            HintMessageTextBox.Text = rule.Message;

            // 是否启用
            RefreshEnable(rule.IsEnable);

            MatchTypeComboBox.SelectionChanged += MatchTypeComboBoxOnSelectionChanged;
            PatternValueTextBox.TextChanged += PatternValueTextBoxOnTextChanged;
            HintMessageTextBox.TextChanged += HintMessageTextBoxOnTextChanged;
            EnableCheckBox.Checked += EnableCheckBoxOnChecked;
            EnableCheckBox.Unchecked += EnableCheckBoxOnChecked;
        }

        private void HintMessageTextBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {
            Rule.Message = HintMessageTextBox.Text.Trim();
        }

        private void PatternValueTextBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {
            Rule.Value = PatternValueTextBox.Text.Trim();
        }

        private void MatchTypeComboBoxOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MatchTypeComboBox.SelectedItem is MatchTypeInfoHelper helpler)
            {
                Rule.MatchType = helpler.MatchType;
            }
        }

        private void EnableCheckBoxOnChecked(object sender, RoutedEventArgs e)
        {
            RefreshEnable(EnableCheckBox.IsChecked is true);
        }

        private void RefreshEnable(bool enable)
        {
            EnableCheckBox.IsChecked = enable;
            Rule.IsEnable = enable;
            EnableCheckBox.Content = enable ? "启用" : "禁用";
            MainBoard.Background = enable ? EnableBrush : OffEnableBrush;
        }

        private void RemoveButton_OnClick(object sender, RoutedEventArgs e)
        {
            ConfigurationsManager.Instance.Configurations.CheckRules.Remove(Rule);
            Delete?.Invoke(this, Rule);
        }
    }
}
