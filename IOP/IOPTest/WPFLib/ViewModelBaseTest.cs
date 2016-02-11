using ILuffy.IOP.Windows.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ILuffy.IOP.Test.WPFLib
{
    [TestClass]
    public class ViewModelBaseTest : ViewModelBase
    {
        private bool isFake;

        public bool IsFake
        {
            get
            {
                return isFake;
            }
            set
            {
                isFake = value;
                OnPropertyChanged();
            }
        }

        private bool isReal;

        public bool IsReal
        {
            get
            {
                return isReal;
            }
            set
            {
                CheckPropertyChanged(ref isReal, value);
            }
        }

        [TestMethod]
        public void OnPropertyChanged_PropertyName_Correct()
        {
            string propertyName = string.Empty;

            System.ComponentModel.PropertyChangedEventHandler handler = (sender, e) => { propertyName = e.PropertyName; };

            PropertyChanged += handler;

            IsFake = true;

            PropertyChanged -= handler;

            Assert.AreEqual(propertyName, nameof(IsFake));
        }

        [TestMethod]
        public void CheckPropertyChanged_ChangeValue_IsChanged()
        {
            bool oldValue = false;

            var changed = CheckPropertyChanged(ref oldValue, true);

            Assert.IsTrue(changed);
        }

        [TestMethod]
        public void CheckPropertyChanged_ChangeValue_ValueIsRight()
        {
            string propertyName = string.Empty;

            bool oldValue = false;

            var changed = CheckPropertyChanged(ref oldValue, true);

            Assert.IsTrue(oldValue);
        }

        [TestMethod]
        public void CheckPropertyChanged_ChangeValue_TriggerEvent()
        {
            string propertyName = string.Empty;

            PropertyChanged += (sender, e) => { propertyName = e.PropertyName; };

            bool oldValue = false;

            var changed = CheckPropertyChanged(ref oldValue, true);

            Assert.AreEqual(propertyName, nameof(CheckPropertyChanged_ChangeValue_TriggerEvent));
        }

        [TestMethod]
        public void CheckPropertyChanged_ChangePropertyValue_TriggerEvent()
        {
            string propertyName = string.Empty;

            isReal = false;

            PropertyChanged += (sender, e) => { propertyName = e.PropertyName; };

            IsReal = true;

            Assert.AreEqual(propertyName, nameof(IsReal));
        }
    }
}