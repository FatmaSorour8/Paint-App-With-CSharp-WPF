using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Paint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void small_size(object sender, RoutedEventArgs e)
        {
            double newSize = 0.5;
            inkCanvas.DefaultDrawingAttributes.Width = newSize;
            inkCanvas.DefaultDrawingAttributes.Height = newSize;

        }
        private void normal_size(object sender, RoutedEventArgs e)
        {
            int newSize = 2;
            inkCanvas.DefaultDrawingAttributes.Width = newSize;
            inkCanvas.DefaultDrawingAttributes.Height = newSize;

        }
        private void large_size(object sender, RoutedEventArgs e)
        {
            int newSize = 10;
            inkCanvas.DefaultDrawingAttributes.Width = newSize;
            inkCanvas.DefaultDrawingAttributes.Height = newSize;

        }

        private void new_canvas(object sender, RoutedEventArgs e)
        {
            inkCanvas.Strokes.Clear();
        }

        private void color_clicked(object sender, RoutedEventArgs e)
        {
            string content = (sender as RadioButton).Content.ToString();
            switch (content)
            {
                case "Red":
                    inkCanvas.DefaultDrawingAttributes.Color = Colors.Red;
                    break;
                case "Green":
                    inkCanvas.DefaultDrawingAttributes.Color = Colors.Green;
                    break;
                case "Blue":
                    inkCanvas.DefaultDrawingAttributes.Color = Colors.Blue;
                    break;
                case "Magenta":
                    inkCanvas.DefaultDrawingAttributes.Color = Colors.Magenta;
                    break;
            }
        }

        private void rec(object sender, RoutedEventArgs e)
        {
            string content = (sender as RadioButton).Content.ToString();
            switch (content)
            {
                case "ellipse":
                    inkCanvas.DefaultDrawingAttributes.StylusTip = StylusTip.Ellipse;
                    break;
                case "rectangle":
                    inkCanvas.DefaultDrawingAttributes.StylusTip = StylusTip.Rectangle;
                    break;
            }
        }
        private void changeMode(object sender, RoutedEventArgs e)
        {
            string content = (sender as RadioButton).Content.ToString();
            switch (content)
            {
                case "ink":
                    inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                    break;
                case "Select":
                    inkCanvas.EditingMode = InkCanvasEditingMode.Select;
                    break;
                case "erase":
                    inkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
                    break;
                case "erase by stroke":
                    inkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
                    break;

            }
        }

        private void copy_click(object sender, RoutedEventArgs e)
        {
            inkCanvas.CopySelection();
        }
        private void paste_click(object sender, RoutedEventArgs e)
        {
            inkCanvas.Paste();
        }

        private void save_click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog
            {
                FileName = "inkcanvas",
                DefaultExt = ".strokes",
                Filter = "Stroke Collection (.strokes)|*.strokes"
            };

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                string filePath = dlg.FileName;

                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    inkCanvas.Strokes.Save(fs);
                }
                MessageBox.Show("inkCanvas Saved Successfully!");
            }
        }

        private void load_click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".strokes",
                Filter = "Stroke Collection (.strokes)|*.strokes"
            };

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                string filePath = dlg.FileName;

                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    StrokeCollection strokes = new StrokeCollection(fs);
                    inkCanvas.Strokes = strokes;
                }
            }
        }

        private void cut_click(object sender, RoutedEventArgs e)
        {
            inkCanvas.CutSelection();
        }
    }
}