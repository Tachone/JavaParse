using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Tokenizer
{
    sealed class ColorSyntaxVisitor : ITokenVisitor
    {
        public ColorSyntaxVisitor(RichTextBox rtb)
        {
            this.target = rtb;
            this.target.Document.Blocks.Clear();
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
            Write(token, Brushes.Black);
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
            Write(token, Brushes.Green);
        }

        void ITokenVisitor.VisitWhitespace(string token)
        {
            Write(token, Brushes.Black);
        }

        #endregion
    }
}
