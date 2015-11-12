namespace Tokenizer
{
	interface IVisitable
	{
		void Accept(ITokenVisitor visitor);
	}
}
