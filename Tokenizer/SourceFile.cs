using System.Windows.Controls;
using System.Collections.Generic;
using System.Collections;
using System;
namespace Tokenizer
{
    class SourceFile : IVisitable
    {

        public SourceFile(RichTextBox codeText)
        {
            JavaRead cur = new JavaRead();
            cur.richTextBox = codeText;
      
            List<JavaRead.JavaWord> javaWords = cur.BeginParse("javacode.txt");
            // public IVisitableToken[] tokens;
            tokens = new IVisitableToken[javaWords.Count];

            Hashtable hashtable = new Hashtable();
            hashtable.Add("import", "using");
            hashtable.Add("java", "System");
            hashtable.Add("System", "Console");
            hashtable.Add("out", "Out");
            hashtable.Add("println", "WriteLine");
           // string name = (string)hashtable["Ãû×Ö"];
           
            for (int i = 0; i < javaWords.Count; i++)
            {
                JavaRead.JavaWord javaWord = javaWords[i];
          
                if (hashtable.Contains(javaWord.Word))
                    javaWord.Word =(string) hashtable[javaWord.Word];
               
                switch (javaWord.Type)
                {
                    case 0: tokens[i] = new CommentToken(javaWord.Word); break;
                    case 1: tokens[i] = new IdentifierToken(javaWord.Word); break;
                    case 2: tokens[i] = new KeywordToken(javaWord.Word); break;
                    case 3: tokens[i] = new OperatorToken(javaWord.Word); break;
                    case 4: tokens[i] = new PunctuatorToken(javaWord.Word); break;
                    case 5: tokens[i] = new StringLiteralToken(javaWord.Word); break;
                    case 6: tokens[i] = new WhitespaceToken(javaWord.Word); break;
                }
            }
        }

        public void Accept(ITokenVisitor visitor)
        {
            foreach (IVisitableToken token in tokens)
            {
                token.Accept(visitor);
            }
        }

        private IVisitableToken[] tokens =
		{
            new KeywordToken("using"),
            new WhitespaceToken(" "),
            new IdentifierToken("System"),
            new PunctuatorToken(";"),
            new WhitespaceToken("\n"),
            new WhitespaceToken("\n"),
			new KeywordToken("class"),
			new WhitespaceToken(" "),
			new IdentifierToken("Greeting"),
			new WhitespaceToken("\n"),
			new PunctuatorToken("{"),
			new WhitespaceToken("\n"),
			new WhitespaceToken("    "),
			new KeywordToken("static"),
			new WhitespaceToken(" "),
			new KeywordToken("void"),
			new WhitespaceToken(" "),
			new IdentifierToken("Main"),
			new PunctuatorToken("("),
			new PunctuatorToken(")"),
			new WhitespaceToken("\n"),
			new WhitespaceToken("    "),
			new PunctuatorToken("{"),
			new WhitespaceToken("\n"),
			new WhitespaceToken("        "),
			new IdentifierToken("Console"),
			new OperatorToken("."),
			new IdentifierToken("WriteLine"),
			new PunctuatorToken("("),
			new StringLiteralToken("\"Hello, world\""),
			new PunctuatorToken(")"),
			new PunctuatorToken(";"),
			new WhitespaceToken("\n"),
			new WhitespaceToken("    "),
			new PunctuatorToken("}"),
			new WhitespaceToken("\n"),
			new PunctuatorToken("}"),

		};
    }

    class CommentToken : DefaultTokenImpl, IVisitableToken
    {
        public CommentToken(string name)
            : base(name)
        {
        }

        void IVisitable.Accept(ITokenVisitor visitor)
        {
            visitor.VisitComment(this.ToString());
        }
    }

    class IdentifierToken : DefaultTokenImpl, IVisitableToken
    {
        public IdentifierToken(string name)
            : base(name)
        {
        }

        void IVisitable.Accept(ITokenVisitor visitor)
        {
            visitor.VisitIdentifier(visitor.ChangeIdentifier(this.ToString()));
        }
    }

    class KeywordToken : DefaultTokenImpl, IVisitableToken
    {
        public KeywordToken(string name)
            : base(name)
        {
        }

        void IVisitable.Accept(ITokenVisitor visitor)
        {
           
            visitor.VisitKeyword( visitor.ChangeKeyword(this.ToString()));
        }
    }

    class WhitespaceToken : DefaultTokenImpl, IVisitableToken
    {
        public WhitespaceToken(string name)
            : base(name)
        {
        }

        void IVisitable.Accept(ITokenVisitor visitor)
        {
            visitor.VisitWhitespace(this.ToString());
        }
    }

    class PunctuatorToken : DefaultTokenImpl, IVisitableToken
    {
        public PunctuatorToken(string name)
            : base(name)
        {
        }

        void IVisitable.Accept(ITokenVisitor visitor)
        {
            visitor.VisitPunctuator(this.ToString());
        }
    }

    class OperatorToken : DefaultTokenImpl, IVisitableToken
    {
        public OperatorToken(string name)
            : base(name)
        {
        }

        void IVisitable.Accept(ITokenVisitor visitor)
        {
            visitor.VisitOperator(this.ToString());
        }
    }

    class StringLiteralToken : DefaultTokenImpl, IVisitableToken
    {
        public StringLiteralToken(string name)
            : base(name)
        {
        }

        void IVisitable.Accept(ITokenVisitor visitor)
        {
            visitor.VisitStringLiteral(this.ToString());
        }
    }
}
