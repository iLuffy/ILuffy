using ILuffy.Halo.Windows.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILuffy.Halo.Test.WPFLib
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
    }
}
