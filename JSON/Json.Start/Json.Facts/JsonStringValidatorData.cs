using System.Collections;
using System.Collections.Generic;

namespace Json.Facts
{
    internal class JsonStringValidatorData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { JsonStringFacts.Quoted(@"⛅⚾abc\""a\"" bdea \\ bfg\u1234a \fijk\b\\""d156\u45Aa \/ bB \nlmnopqrs-\\⚾tuvxyz"), true };
            yield return new object[] { JsonStringFacts.Quoted("⛅⚾abca\\ bdea \\ bfg\\u1234a \fijk\\b\\d156\\u45Aa / bB \\nlmnopqrs-\\⚾tuvxyz"), false };
            yield return new object[] { JsonStringFacts.Quoted(@"⛅⚾abc\""a\"" bdea \\+[]''&89#####2g!\u1234a \fijk\b\\""d156\u45Aa \/ bB \nlmnopqrs-\\⚾tuvxyz"), true };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}