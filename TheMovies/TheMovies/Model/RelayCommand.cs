using System;
using System.Windows.Input;

namespace TheMovies
{
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            // Her sættes _execute til Action execute, der tages som input
            // Hvis den er null, kaldes ArgumentNullException
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            
            // Her sættes _canExecute (som beslutter, om _execute kan ske) til input canExecute
            // canExecute sættes automatisk til null, hvis den ikke gives en værdi, når RelayCommand kaldes
            _canExecute = canExecute;
        }

        // CanExecute kaldes internt af WPF-framework'et, når vi bruger AddMovieCommand-bindingen
        // til en knap (altså AddMovieCommand.CanExecute(null)).
        // Hvis vi har noget, der tjekker, om filmen kan tilføjes eller ej,
        // ville vi bruge den i kaldet af constructoren af RelayCommand.
        // For eksempel: RelayCommand AddMovieCommand = new RelayCommand(AddMovie, CanAddMovie).
        // Hvis _canExecute er null, returneres true fra CanExecute, hvilket betyder, at kommandoen kan udføres.
        // Hvis _canExecute ikke er null, kaldes Func<bool> _canExecute delegaten.
        // => syntaksen er en kortere måde at definere metoder, properties og andre medlemmer,
        // som kun består af en enkelt expression (f.eks. her tjekkes om _canExecute er null;
        // hvis ikke, kaldes _canExecute(), som i dette tilfælde ville være CanAddMovie, der returnerer enten true eller false).
        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

        // Når man klikker på knappen, kommandoen er bundet til, kaldes Execute-kommandoen
        // Den executer _execute, som er givet som argument til RelayCommand constructoren
        // i form af Action execute
        public void Execute(object parameter) => _execute();


        // CanExecuteChanged fortæller WPF-framework'et, når resultatet af
        // CanExecute har ændret sig.
        // Når en eventhandler tilføjes til en metode, bruger vi add, og når den fjernes
        // bruger vi remove. Når et event er tilføjet, kaldes metoden, når der sker et event.
        // WPF-framework'et sørger selv for at tilføje et event til en metde, når de er binded.
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}