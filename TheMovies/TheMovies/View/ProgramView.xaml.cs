using System.Windows;
using System.Windows.Controls;
using TheMovies.ViewModel;

namespace TheMovies
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ProgramView : Page
    {
        public Month SelectedEnumValue { get; set; }
        public Movie SelectedMovie { get; set; }
        public DateOnly SelectedDate { get; set; }

        public ProgramView()
        {
            InitializeComponent();

            DataContext = new ProgramViewModel();            

        }

        private void Dropdown_Maanede_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnClick_Hjerm_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnClick_Raehr_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnClick_Videbaek_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnClick_Thorsminde_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Dropdown_Film_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListBox_OprettetForestilling_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}