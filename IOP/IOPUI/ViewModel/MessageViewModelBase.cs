namespace ILuffy.IOP.UI.ViewModel
{
    using System;
    using Input;
    public abstract class MessageViewModelBase : ViewModelBase
    {
        private MessageItem message;

        public MessageItem Message
        {
            get { return message; }
            set
            {
                CheckPropertyChanged(ref message, value);
            }
        }
    }
}
