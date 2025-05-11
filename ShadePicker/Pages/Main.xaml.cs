using ShadePicker.Elements;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShadePicker.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
        }
        public static int NumberOfColors;
        private void PickColor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var colorDialog = new ColorDialog();
            List<SolidColorBrush> Colors = new List<SolidColorBrush>();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Windows.Media.Color selectedColor = System.Windows.Media.Color.FromRgb(colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B);
                ColorBackground.Background = new SolidColorBrush(selectedColor);
                ColorName.Content = selectedColor;
                double oppositeColor_temp = (Common.ColorConverter.RgbToHue(colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B) + 180) % 360;
                var oppositeColorRGB = Common.ColorConverter.HueToRgb((float)oppositeColor_temp, 0.75f, 0.5f);
                System.Windows.Media.Color oppositeColor = System.Windows.Media.Color.FromRgb(oppositeColorRGB.R, oppositeColorRGB.G, oppositeColorRGB.B);
                Colors.Clear();
                int TickAngle = 47 - ((NumberOfColors-1)/2)*5;
                for (int i=1;i<=(NumberOfColors-1)/2;i++)
                {
                    double Color_temp = (Common.ColorConverter.RgbToHue(colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B) + TickAngle * i) % 360;
                    var ColorRGB = Common.ColorConverter.HueToRgb((float)Color_temp, 0.75f, 0.5f);
                    System.Windows.Media.Color Color = System.Windows.Media.Color.FromRgb(ColorRGB.R, ColorRGB.G, ColorRGB.B);
                    Colors.Add(new SolidColorBrush(Color));
                }
                for (int i = 1; i <= (NumberOfColors - 1) / 2; i++)
                {
                    double Color_temp = (Common.ColorConverter.RgbToHue(colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B) - TickAngle * i) % 360;
                    var ColorRGB = Common.ColorConverter.HueToRgb((float)Color_temp, 0.75f, 0.5f);
                    System.Windows.Media.Color Color = System.Windows.Media.Color.FromRgb(ColorRGB.R, ColorRGB.G, ColorRGB.B);
                    Colors.Add(new SolidColorBrush(Color));
                }
                FillPalette(Colors);
                parent.Children.Add(new ColorElement(new SolidColorBrush(oppositeColor),"Противоположный"));
                //parent.Children.Add(new ColorElement(new SolidColorBrush(selectedColor), "Основной"));
            }
        }
        public void FillPalette(List<SolidColorBrush> ColorPalette)
        {
            parent.Children.Clear();
            foreach (SolidColorBrush colorBrush in ColorPalette)
            {
                parent.Children.Add(new ColorElement(colorBrush,"Дополнительный"));
            }
            
        }

        private void AmountOfColors_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((Slider)sender).SelectionEnd = e.NewValue;
            NumberOfColors = Convert.ToInt32(e.NewValue);
            if (Convert.ToInt32(NumberOfColors) % 2 == 0) NumberOfColors+=1;
            lbAmountOfColors.Content = $"Количество цветов: {NumberOfColors}";
        }
    }
}
