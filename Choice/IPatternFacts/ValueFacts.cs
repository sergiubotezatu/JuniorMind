using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using InterFace;
using ChoiceLibrary;

namespace ChoiceFacts
{
    public class ValueFacts
    {
        [Fact]
        public void ObjectsCanBeEmptyBetweenBraces()
        {
            var value = new Value();
            string test = "{ }";
            var result = value.Match(test);
            Assert.True(result.Success());
            Assert.True(result.RemainingText() == string.Empty);
        }

        [Fact]
        public void ObjectStartsWithLeftBraces()
        {
            var value = new Value();
            string test = " \"name\" :\"Andrew\" }";
            var result = value.Match(test);
            Assert.True(result.Success());
            Assert.True(result.RemainingText() == ":\"Andrew\" }");
        }

        [Fact]
        public void ObjectEndsWithRightBraces()
        {
            var value = new Value();
            string test = "{ \"name\" ";
            var result = value.Match(test);
            Assert.False(result.Success());
            Assert.True(result.RemainingText() == "{ \"name\" ");
        }

        [Fact]
        public void HasSpaceAfterTheFirstBraces()
        {
            var value = new Value();
            string test = "{\"name\" :\"Andrew\" }";
            var result = value.Match(test);
            Assert.False(result.Success());
            Assert.True(result.RemainingText() == "{\"name\" }");
        }

        [Fact]
        public void HasSpaceAfterTheFirstString()
        {
            var value = new Value();
            string test = "{ \"age\":30 }";
            var result = value.Match(test);
            Assert.False(result.Success());
            Assert.True(result.RemainingText() == "{ \"age\":30 }");
        }

        [Fact]
        public void HasColonBetweenStringAndValues()
        {
            var value = new Value();
            string test = "{ \"Name\" \"Andrei\" \"age\" :30 }";
            var result = value.Match(test);
            Assert.False(result.Success());
            Assert.True(result.RemainingText() == "{ \"Name\" \"Andrei\" \"age\" :30 }");
        }

        [Fact]
        public void HasCommaBetweenValues()
        {
            var value = new Value();
            string test = "{ \"Name\" :\"Andrei\" \"age\" :30 }";
            var result = value.Match(test);
            Assert.False(result.Success());
            Assert.True(result.RemainingText() == "{ \"Name\" :\"Andrei\" \"age\" :30 }");
        }

        [Fact]
        public void ObjectAcceptsAllBaseValues()
        {
            var value = new Value();
            string test = "{ \"Name\" :\"Andrei\", \"age\" :30, \"brother\" :true, \"sister\" :false, \"car\" :null }";
            var result = value.Match(test);
            Assert.True(result.Success());
            Assert.True(result.RemainingText() == string.Empty);
        }

        [Fact]
        public void ArraysCanContainWhiteSpace()
        {
            var value = new Value();
            string test = "[ ]";
            var result = value.Match(test);
            Assert.True(result.Success());
            Assert.True(result.RemainingText() == string.Empty);
        }

        [Fact]
        public void ArraysStartWithLeftBracket()
        {
            var value = new Value();
            string test = " \"name\" :\"Andrew\"]";
            var result = value.Match(test);
            Assert.True(result.Success());
            Assert.True(result.RemainingText() == ":\"Andrew\"]");
        }

        [Fact]
        public void ArraysEndWithRightBracket()
        {
            var value = new Value();
            string test = "[\"name\"";
            var result = value.Match(test);
            Assert.False(result.Success());
            Assert.True(result.RemainingText() == "[\"name\"");
        }

        [Fact]
        public void ArrayHasCommaAfterValue()
        {
            var value = new Value();
            string test = "[\"name\" \"address\"]";
            var result = value.Match(test);
            Assert.False(result.Success());
            Assert.True(result.RemainingText() == "[\"name\" \"address\"]");
        }

        [Fact]
        public void ArrayHasSpaceAfterComma()
        {
            var value = new Value();
            string test = "[\"name\",\"address\"]";
            var result = value.Match(test);
            Assert.False(result.Success());
            Assert.True(result.RemainingText() == "[\"name\",\"address\"]");
        }

        [Fact]
        public void ValidatesArrayOfStrings()
        {
            var value = new Value();
            string test = "[\"Ford\", \"BMW\", \"Fiat\"]";
            var result = value.Match(test);
            Assert.True(result.Success());
            Assert.True(result.RemainingText() == string.Empty);
        }

        [Fact]
        public void ValidatesArrayOfNumbers()
        {
            var value = new Value();
            string test = "[12, -10, 5.2e3]";
            var result = value.Match(test);
            Assert.True(result.Success());
            Assert.True(result.RemainingText() == string.Empty);
        }

        [Fact]
        public void ValidatesArrayOfBooleans()
        {
            var value = new Value();
            string test = "[true, false]";
            var result = value.Match(test);
            Assert.True(result.Success());
            Assert.True(result.RemainingText() == string.Empty);
        }

        [Fact]
        public void ValidatesArrayOfObjects()
        {
            var value = new Value();
            string test = "[{ \"name\" :\"John\", \"age\" :30, \"car\" :null }," +
                " { \"Name\" :\"Andrei\", \"age\" :30, \"brother\" :true, \"sister\" :false }]";
            var result = value.Match(test);
            Assert.True(result.Success());
            Assert.True(result.RemainingText() == string.Empty);
        }

        [Fact]
        public void ValidatesArrayOfArays()
        {
            var value = new Value();
            string test = "[[true, false], [12, -10, 5.2e3], [\"Ford\", \"BMW\", \"Fiat\"]]";
            var result = value.Match(test);
            Assert.True(result.Success());
            Assert.True(result.RemainingText() == string.Empty);
        }

    }
}
