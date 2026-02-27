using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InvestmentPredictor.wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IInvestmentCalculator _calculator;

        public MainWindow()
        {
            InitializeComponent();
            _calculator = new InvestmentCalculator();
            IndexSelector.ItemsSource = Enum.GetValues(typeof(MarketIndex));
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            int period = int.Parse(Period.Text);
            decimal monthlySubsidy = decimal.Parse(MonthlySubsidy.Text);
            decimal inital = decimal.Parse(InitialAmountInput.Text);
            MarketIndex selectedIndex = (MarketIndex)IndexSelector.SelectedIndex;
            decimal annualReturn = _calculator.GetIndexAnnualReturn(selectedIndex);
            decimal result = _calculator.CalculatedValue(monthlySubsidy, inital, annualReturn, period);
            
            ResultDisplay.Text = $"Predicted Value: {result:C}";


        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}