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
using System.IO;

namespace Tokenizer
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : Window
    {
        bool flage = false;
        public Window1()
        {
            InitializeComponent();
             StreamReader m_streamReader = new StreamReader("javacode.txt",System.Text.Encoding.Default);
              // codeText
             m_streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
             // 从数据流中读取每一行，直到文件的最后一行，并在richTextBox1中显示出内容
            // codeText.AppendText = "";
             string strLine = m_streamReader.ReadLine();
             while (strLine != null)
             {
                 codeText.AppendText(strLine + "\n");
                 strLine = m_streamReader.ReadLine();
             }
             //关闭此StreamReader对象
             m_streamReader.Close();  
        
        }

        private void openClick(object sender, RoutedEventArgs e)
        {
            SourceFile source = new SourceFile(codeText);
            ColorSyntaxVisitor visitor = new ColorSyntaxVisitor(codeText, flage);
            source.Accept(visitor);
            flage = !flage;

            if (flage == true)
                open.Content = "已经转换为C#程序";
            
        }
    }
}
