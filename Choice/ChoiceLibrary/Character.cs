namespace InterFace
{
    public class Character : IPattern
    {
        private readonly char pattern;

        public Character(char pattern)
        {
            this.pattern = pattern;
        }

        public IMatch Match(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new Match(false, text);
            }

            string remainder = text[0] == this.pattern ? text.Substring(1) : text;
            return new Match(text[0] == this.pattern, remainder);
        }
    }
}
