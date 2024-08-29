using System.Windows.Controls;
using TheMovies.ViewModel;

namespace TheMovies
{
    public partial class ProgramView : Page
    {
        public ProgramView()
        {
            InitializeComponent();
            DataContext = new ProgramViewModel();
        }
    }
}