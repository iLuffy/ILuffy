namespace ILuffy.IOP.UI.View
{
    using System;
    using System.Security;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// 
    /// http://stackoverflow.com/questions/15390727/passwordbox-and-mvvm/15391318#15391318
    /// 
    /// http://blog.functionalfun.net/2008/06/wpf-passwordbox-and-data-binding.html
    /// 
    /// http://gigi.nullneuron.net/gigilabs/security-risk-in-binding-wpf-passwordbox-password/
    /// 
    /// http://stackoverflow.com/questions/23778232/how-risky-is-it-to-store-passwords-in-memory/23778635#23778635
    /// 
    /// http://stackoverflow.com/questions/23031549/is-it-a-bad-idea-to-bind-passwordbox-password/23057571#23057571
    /// </summary>
    public static class PasswordBoxAssistant
    {
        public static readonly DependencyProperty AttachProperty = 
            DependencyProperty.RegisterAttached("Attach", typeof(bool), 
                typeof(PasswordBoxAssistant), new PropertyMetadata(false, AttachEvent));

        public static void SetAttach(DependencyObject dp, bool value)
        {
            dp.SetValue(AttachProperty, value);
        }

        public static bool GetAttach(DependencyObject dp)
        {
            return (bool)dp.GetValue(AttachProperty);
        }

        private static void AttachEvent(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            // when the BindPassword attached property is set on a PasswordBox,
            // start listening to its PasswordChanged event

            var box = dp as PasswordBox;

            if (box == null)
            {
                return;
            }

            bool wasBound = (bool)(e.OldValue);
            bool needToBind = (bool)(e.NewValue);

            if (wasBound)
            {
                box.PasswordChanged -= HandlePasswordChanged;
            }

            if (needToBind)
            {
                box.PasswordChanged += HandlePasswordChanged;
            }
        }

        private static void HandlePasswordChanged(object sender, RoutedEventArgs e)
        {
            var box = sender as PasswordBox;

            if (box != null)
            {
                var havePassword = box.DataContext as Input.IHavePassword;

                if (havePassword != null)
                {
                    havePassword.SecurePassword = box.SecurePassword;
                }
            }
        }
    }
}
