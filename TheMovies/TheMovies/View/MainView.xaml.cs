using System.Windows;
using TheMovies.View;
using TheMovies.ViewModel;

namespace TheMovies
{
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            MainViewModel vm = new MainViewModel();

            DataContext = vm;
            Main.Content = new Greeting();
        }

        private void BtnClick_OpretFilm(object sender, RoutedEventArgs e)
        {
            Main.Content = new MovieView();
        }

        private void BtnClick_Program(object sender, RoutedEventArgs e)
        {
            Main.Content = new ProgramView();
        }
    }
}