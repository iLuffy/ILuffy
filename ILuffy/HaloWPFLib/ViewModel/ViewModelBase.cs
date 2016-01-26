using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ILuffy.Halo.Windows.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException(nameof(propertyExpression));
            }

            var memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("PropertySupport_NotMemberAccessExpression_Exception", nameof(propertyExpression));
            }

            var property = memberExpression.Member as PropertyInfo;
            if (property == null)
            {
                throw new ArgumentException("PropertySupport_ExpressionNotProperty_Exception", nameof(propertyExpression));
            }

            var getMethond = property.GetGetMethod(true);
            if (getMethond.IsStatic)
            {
                throw new ArgumentException("PropertySupport_StaticExpression_Exception", nameof(propertyExpression));
            }

            return memberExpression.Member.Name;
        }

        protected void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var handler = PropertyChanged;

            if (handler != null)
            {
                var propertyName = ExtractPropertyName(propertyExpression);

                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void OnPropertyChanged(string propertyName)
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
