using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ILuffy.IOP.UI.View
{
    /// <summary>
    /// Interaction logic for IOPMessageBox.xaml
    /// </summary>
    public partial class IOPMessageBox : Window
    {
        public IOPMessageBox(Window parentWindows)
        {
            InitializeComponent();
            Owner = parentWindows;

            var originalOpacity = 0.0;
            var originalMask = (Brush)null;

            Closed += (sender, e) =>
            {
                Owner.Opacity = originalOpacity;
                Owner.OpacityMask = originalMask;
            };

            Loaded += (sender, e) =>
            {
                originalOpacity = Owner.Opacity;
                originalMask = Owner.OpacityMask;

                Owner.Opacity = 0.4;
                Owner.OpacityMask = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            };
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            var pp = e.GetPosition(header);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (pp.X <= header.ActualWidth && pp.Y <= header.ActualHeight)
                {
                    this.DragMove();
                }
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
