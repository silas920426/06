using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace _1114
{
    /// <summary>
    /// _04.xaml 的互動邏輯
    /// </summary>
    public partial class _04 : Window
    {
        Color fontcolor = Colors.Black;
        public _04()
        {
            InitializeComponent();
            fontColorPicker.SelectedColor = fontcolor;
            foreach (var fontFamily in Fonts.SystemFontFamilies) 
            {
                fontFamilyComboBox.Items.Add(fontFamily.Source);
            }
            fontFamilyComboBox.SelectedIndex = 0;

            fontSizeComboBox.ItemsSource = new List<Double> { 8, 9, 10, 11, 12, 20, 24, 32, 40, 50, 60, 70 };
            fontSizeComboBox.SelectedIndex = 4;
        }

        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _04 _04 = new _04();
            _04.Show();
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Rich Text Format (*.rtf)|*.rtf|HTML Files(*.html)|*.html|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                TextRange textRange = new TextRange
                    (rtbEditor.Document.ContentStart,
                    rtbEditor.Document.ContentEnd);

                using (FileStream fileStream = new FileStream(openFileDialog.FileName, FileMode.Open))
                {
                    textRange.Load(fileStream, DataFormats.Rtf);
                }
            }
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Rich Text Format (*.rtf)|*.rtf|HTML Files(*.html)|*.html|All files (*.*)|*.*"
            };

            if(saveFileDialog.ShowDialog()==true)
            {
                TextRange textRange = new TextRange
                    (rtbEditor.Document.ContentStart,
                    rtbEditor.Document.ContentEnd);
                
                    using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    textRange.Save(fileStream, DataFormats.Rtf);
                }
            }

        }
    
        private void rtbEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //依據所選取的文字狀態，同步更新Boldbutton的狀態
            //粗體
            object property = rtbEditor.Selection.GetPropertyValue (TextElement.FontWeightProperty);
            boldbutton.IsChecked = (property is FontWeight && (FontWeight) property == FontWeights.Bold);

            //斜體 同步更新italicbutton的狀態
            property = rtbEditor.Selection.GetPropertyValue(TextElement.FontStyleProperty);
            italicbutton.IsChecked = (property is FontStyle && (FontStyle) property == FontStyles.Italic);

            //底線 同步更新underlinebutton的狀態
            property = rtbEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            underlinebutton.IsChecked = (property  != DependencyProperty.UnsetValue && property is TextDecorationCollection decorations &&
            decorations.Contains(TextDecorations.Underline[0]));

            //依據所選取文字的字體大小，同步更新fontsizecombox的狀態
            property = rtbEditor.Selection.GetPropertyValue(TextElement.FontSizeProperty);
            fontSizeComboBox.SelectedItem = property;

            //依據所選取文字的字型，同步更新fontfamilycombox的狀態
            property = rtbEditor.Selection.GetPropertyValue(TextElement.FontFamilyProperty);
            fontFamilyComboBox.SelectedItem = property;

            //依據所選取文字的字體顏色，同步更新fontfamilycombox的狀態
            SolidColorBrush? forgroundProperty = rtbEditor.Selection.GetPropertyValue(TextElement.ForegroundProperty) as SolidColorBrush;
            if(forgroundProperty != null)
            {
                fontColorPicker.SelectedColor = forgroundProperty.Color;
            } 
        }

        private void fontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
               rtbEditor.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, fontFamilyComboBox.SelectedItem);
            
        }

        private void fontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

                rtbEditor.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, fontSizeComboBox.SelectedItem);
            
        }
        private void fontColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            fontcolor = (Color)fontColorPicker.SelectedColor;
            SolidColorBrush fontBrush = new SolidColorBrush(fontcolor);
            rtbEditor.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, fontBrush);

        }

        private void clearbutton_Click(object sender, RoutedEventArgs e)
        {
            rtbEditor.Document.Blocks.Clear();
        }

    }
}
