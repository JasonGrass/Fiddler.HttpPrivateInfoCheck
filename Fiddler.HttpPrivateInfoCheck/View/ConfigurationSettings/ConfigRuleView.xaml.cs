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
    /// ConfigRuleView.xaml 的交互逻辑
    /// </summary>
    public partial class ConfigRuleView : UserControl
    {
        public CheckRule Rule { get; set; }

        public event EventHandler<CheckRule> Delete;

        public ConfigRuleView(CheckRule rule)
        {
            Rule = rule;
            InitializeComponent();
            MatchTypeComboBox.Items.Add(new MatchTypeInfoHelper(RuleMatchType.String, "字符串"));
            MatchTypeComboBox.Items.Add(new MatchTypeInfoHelper(RuleMatchType.Regex, "正则表达式"));
            MatchTypeComboBox.SelectedIndex = rule.MatchType == RuleMatchType.Regex ? 1 : 0;

            PatternValueTextBox.Text = rule.Value;
            HintMessageTextBox.Text = rule.Message;


            MatchTypeComboBox.SelectionChanged += MatchTypeComboBoxOnSelectionChanged;
            PatternValueTextBox.TextChanged += PatternValueTextBoxOnTextChanged;
            HintMessageTextBox.TextChanged += HintMessageTextBoxOnTextChanged;
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

        private void RemoveButton_OnClick(object sender, RoutedEventArgs e)
        {
            ConfigurationsManager.Instance.Configurations.CheckRules.Remove(Rule);
            Delete?.Invoke(this, Rule);
        }
    }
}
