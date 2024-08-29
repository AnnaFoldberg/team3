using System.Windows.Controls;
using TheMovies.ViewModel;

namespace TheMovies
{
    public partial class MovieView : Page
    {
        public MovieView()
        {
            InitializeComponent();

            DataContext = new MovieViewModel();
        }
    }
}