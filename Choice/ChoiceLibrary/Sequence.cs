using InterFace;

namespace ChoiceLibrary
{
    public class Sequence : IPattern
    {
        private IPattern[] patterns;

        public Sequence(params IPattern[] patterns)
        {
            this.patterns = patterns;
        }

        public IMatch Match(string text)
        {
            Match result = new Match(true, text);
            foreach (IPattern pattern in this.patterns)
            {
                result = (Match)pattern.Match(text);
                if (!result.Success())
                {
                    return result;
                }

                text = result.RemainingText();
            }

            return new Match(true, text);
        }
    }
}
