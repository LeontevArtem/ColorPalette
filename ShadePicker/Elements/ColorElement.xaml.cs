using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShadePicker.Elements
{
    /// <summary>
    /// Логика взаимодействия для ColorElement.xaml
    /// </summary>
    public partial class ColorElement : UserControl
    {
        public ColorElement(SolidColorBrush ColorBrush, string ColorStatus)
        {
            InitializeComponent();
            ColorBackground.Background = ColorBrush;
            ColorName.Content = ColorBrush;
            this.ColorStatus.Content = ColorStatus;
        }
    }
}
