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

namespace ILuffy.IOP.Windows.View
{
    /// <summary>
    /// Interaction logic for ImageButton.xaml
    /// </summary>
    public partial class ImageButton : Button
    {
        public ImageButton()
        {
            InitializeComponent();
            
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
          DependencyProperty.Register("Text", typeof(string), typeof(ImageButton), new UIPropertyMetadata(""));

        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty ImageProperty =
           DependencyProperty.Register("Image", typeof(ImageSource), typeof(ImageButton), new UIPropertyMetadata(null));

        public bool IsExportMode
        {
            get { return (bool)GetValue(IsExportModeProperty); }
            set { SetValue(IsExportModeProperty, value); }
        }

        public static readonly DependencyProperty IsExportModeProperty =
          DependencyProperty.Register("IsExportMode", typeof(bool), typeof(ImageButton), new UIPropertyMetadata(false));

        public string ExportModeDefaultExtension
        {
            get { return (string)GetValue(ExportModeDefaultExtensionProperty); }
            set { SetValue(ExportModeDefaultExtensionProperty, value); }
        }

        public static readonly DependencyProperty ExportModeDefaultExtensionProperty =
          DependencyProperty.Register("ExportModeDefaultExtension", typeof(string), typeof(ImageButton), new UIPropertyMetadata(""));

        public string ExportModeExtensionFilter
        {
            get { return (string)GetValue(ExportModeExtensionFilterProperty); }
            set { SetValue(ExportModeExtensionFilterProperty, value); }
        }

        public static readonly DependencyProperty ExportModeExtensionFilterProperty =
          DependencyProperty.Register("ExportModeExtensionFilter", typeof(string), typeof(ImageButton), new UIPropertyMetadata(""));

        protected override void OnClick()
        {
            if (IsExportMode)
            {
                var saveFile = new System.Windows.Forms.SaveFileDialog();
                saveFile.AddExtension = true;
                saveFile.AutoUpgradeEnabled = true;
                saveFile.CheckPathExists = true;
                saveFile.DefaultExt = ExportModeDefaultExtension;
                saveFile.Filter = ExportModeExtensionFilter;
                saveFile.FilterIndex = 0;
                var result = saveFile.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    var fileName = saveFile.FileName;
                    this.CommandParameter = fileName;
                    base.OnClick();
                }
            }
            else
            {
                base.OnClick();
            }
        }

        //public double ImageHeight
        //{
        //    get { return (double)GetValue(ImageHeightProperty); }
        //    set { SetValue(ImageHeightProperty, value); }
        //}

        //public static readonly DependencyProperty ImageHeightProperty =
        //   DependencyProperty.Register("ImageHeight", typeof(double), typeof(ImageButton), new UIPropertyMetadata(0.0));

        //public double ImageWidth
        //{
        //    get { return (double)GetValue(ImageWidthProperty); }
        //    set { SetValue(ImageWidthProperty, value); }
        //}

        //public static readonly DependencyProperty ImageWidthProperty =
        //   DependencyProperty.Register("ImageWidth", typeof(double), typeof(ImageButton), new UIPropertyMetadata(0.0));
    }
}
