namespace ILuffy.IOP.UI.View
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using Input;

    /// <summary>
    /// Interaction logic for IOPMessageBar.xaml
    /// </summary>
    public partial class IOPMessageBar : UserControl
    {
        public IOPMessageBar()
        {
            InitializeComponent();
        }

        public MessageItem Message
        {
            get { return (MessageItem)GetValue(MessageProperty); }
            set
            {
                SetValue(MessageProperty, value);
            }
        }

        public void ShowMessage(MessageItem message)
        {
            var messageBox = new IOPMessageBox(Application.Current.MainWindow);
            messageBox.DataContext = message;
            var result = messageBox.ShowDialog();

            if (message.Callback != null)
            {
                message.Callback(new MessageResult(result.Value));
            }
        }

        public static readonly DependencyProperty MessageProperty =
          DependencyProperty.Register(
              "Message", 
              typeof(MessageItem), 
              typeof(IOPMessageBar), 
              new UIPropertyMetadata(PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var message = e.NewValue as MessageItem;

            var bar = (d as IOPMessageBar);

            if (bar != null)
            {
                bar.ShowMessage(message);
            }
        }
    }
}
