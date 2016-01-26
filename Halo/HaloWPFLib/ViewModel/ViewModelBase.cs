using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace ILuffy.Halo.Windows.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] String propertyName="")
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
