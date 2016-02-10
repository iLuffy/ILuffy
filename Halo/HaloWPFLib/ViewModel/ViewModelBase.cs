using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ILuffy.Halo.Windows.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //http://www.codeproject.com/Articles/15822/Bind-Better-with-INotifyPropertyChanged
        //Check Property Changed
        protected bool CheckPropertyChanged<T>(ref T oldValue, T newValue, [CallerMemberName] String propertyName = "")
        {
            if ((oldValue == null && newValue != null) ||
                (oldValue != null && (!oldValue.Equals(newValue))))
            {
                oldValue = newValue;

                OnPropertyChanged(propertyName);

                return true;
            }

            return false;
        }

        protected void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Forces the CommandManager to raise the RequerySuggested event.
        /// </summary>
        protected void InvalidateRequerySuggested()
        {
            System.Windows.Input.CommandManager.InvalidateRequerySuggested();
        }
    }
}