using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Tokenizer
{
    sealed class ColorSyntaxVisitor : ITokenVisitor
    {

        bool isChangeWord = false;

        public ColorSyntaxVisitor(RichTextBox rtb, bool state)
        {
            this.isChangeWord = state;
            this.target = rtb;
            if (isChangeWord == false)
            {
                this.target.Document.Blocks.Clear();
                //this.target.Document.Blocks.Clear();
               // this.target.AppendText("\n\n");
            }
            else
            {
                
                this.target.Document.Blocks.Clear();
               // this.target.AppendText("\n\n---------------C#Code------------\n\n");
            }
               
        }

        private void Write(string token, SolidColorBrush color)
        {
            target.AppendText(token);
            int offsetToStartOfToken = -1 * token.Length - 2;
            int offsetToEndOfToken = -2;
            TextPointer start =
                target.Document.ContentEnd.GetPositionAtOffset(offsetToStartOfToken);
            TextPointer end =
                target.Document.ContentEnd.GetPositionAtOffset(offsetToEndOfToken);
            TextRange text = new TextRange(start, end);
            text.ApplyPropertyValue(TextElement.ForegroundProperty, color);
        }

        private readonly RichTextBox target;

        #region ITokenVisitor Members

        void ITokenVisitor.VisitComment(string token)
        {
            Write(token, Brushes.Green);
        }

        void ITokenVisitor.VisitIdentifier(string token)
        {
            Write(token, Brushes.Black);
        }

        void ITokenVisitor.VisitKeyword(string token)
        {

            Write(token, Brushes.Blue);
        }

        void ITokenVisitor.VisitOperator(string token)
        {
            Write(token, Brushes.Black);
        }

        void ITokenVisitor.VisitPunctuator(string token)
        {
            Write(token, Brushes.Black);
        }

        void ITokenVisitor.VisitStringLiteral(string token)
        {
            Write(token, Brushes.Red);
        }

        void ITokenVisitor.VisitWhitespace(string token)
        {
            Write(token, Brushes.Black);
        }



        string ITokenVisitor.ChangeKeyword(string token)
        {
            if (isChangeWord == false)
                return token;

            if (String.Equals(token, "package") == true)
                token = "namespace";
            else if (String.Equals(token, "import") == true)
                token = "using";

            return token;
        }

        string ITokenVisitor.ChangeIdentifier(string token)
        {

            if (isChangeWord == false)
                return token;

            if (String.Equals(token, "java") == true)
                token = "System";
            else if (String.Equals(token, "String") == true)
                token = "string";
            else if (String.Equals(token, "String") == true)
                token = "string";
            else if (String.Equals(token, "out") == true)
                token = "Console";
            else if (String.Equals(token, "println") == true)
                token = "WriteLine";
            else if (String.Equals(token, "main") == true)
                token = "Main";

            return token;
        }

        #endregion
    }
}
