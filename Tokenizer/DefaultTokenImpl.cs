namespace Tokenizer
{
	abstract class DefaultTokenImpl
	{
		new public string ToString()
		{
			return this.name;
		}

		protected DefaultTokenImpl(string name)
		{
			this.name = name;
		}

		private readonly string name;
	}
}
