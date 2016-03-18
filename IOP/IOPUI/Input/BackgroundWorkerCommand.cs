namespace ILuffy.IOP.UI.Input
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows.Input;
    using I18N;
    public class BackgroundWorkerCommand : BackgroundWorker, ICommand
    {
        #region Fields 
        //private readonly DoWorkEventHandler execute;
        private readonly Predicate<object> canExecute;
        #endregion
        // Fields 

        #region Constructors 
        public BackgroundWorkerCommand(DoWorkEventHandler execute) 
            : this(execute, null)
        { }

        public BackgroundWorkerCommand(DoWorkEventHandler execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            DoWork += execute;
            RunWorkerCompleted += RunWorkerCompletedTrack;
            this.canExecute = canExecute;
        }

        #endregion
        // Constructors 

        private void RunWorkerCompletedTrack(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null || e.Cancelled)
            {
                LoggerUtility.WriteMessage(Severity.Debug,
                    CoreRS.TraceBackgroundJobCompletedFormat(e.Cancelled, null, e.Error));
            }
            else
            {
                LoggerUtility.WriteMessage(Severity.Debug,
                    CoreRS.TraceBackgroundJobCompletedFormat(e.Cancelled, e.Result, e.Error));
            }
        }


        #region ICommand Members 

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return (!IsBusy) && (canExecute == null ? true : canExecute(parameter));
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            this.RunWorkerAsync(parameter);
        }

        #endregion
    }
}
