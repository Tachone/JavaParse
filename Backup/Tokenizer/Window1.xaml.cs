using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Tokenizer
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void openClick(object sender, RoutedEventArgs e)
        {
            SourceFile source = new SourceFile();
            ColorSyntaxVisitor visitor = new ColorSyntaxVisitor(codeText);
            source.Accept(visitor);
        }
    }
}
