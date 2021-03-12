using System.Windows;

namespace WPFClient
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void BtnEntra_Click(object sender, RoutedEventArgs e)
    {
      this.Hide();
      CarNew carNew = new CarNew();
      carNew.Show();
    }
  }
}
