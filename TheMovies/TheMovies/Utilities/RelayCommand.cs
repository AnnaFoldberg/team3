namespace TheMovies
{
    public class RelayCommand : RelayCommandT<object>
    {
        public RelayCommand(Action execute, Func<bool> canExecute = null)
            : base(param => execute(), canExecute == null ? (Func<object, bool>)null : new Func<object, bool>(param => canExecute()))
        {
        }
    }
}