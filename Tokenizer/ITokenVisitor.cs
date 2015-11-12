namespace Tokenizer
{
	interface ITokenVisitor
	{
		void VisitComment      (string token);
		void VisitIdentifier   (string token);
		void VisitKeyword      (string token);
		void VisitOperator     (string token);
		void VisitPunctuator   (string token);
		void VisitStringLiteral(string token);
		void VisitWhitespace   (string token);


        string ChangeKeyword     (string token);
        string ChangeIdentifier(string token);
	}
}
