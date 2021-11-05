using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Diverse
{
    class SquareTriples
    {
        public IEnumerable<(int, int, int)> GetAllPythTriples(int[] input)
        {
            int[] squared = input.Select(x => x * x ).ToArray();
            return GetAllTriples(squared).Where(x => IsPythagorean(x));
        }

        private IEnumerable<(int, int, int)> GetAllTriples(int[] sequence)
        {
            return Enumerable.Range(0, sequence.Length - 2)
                .SelectMany(i => Enumerable.Range(i + 1, sequence.Length - 1)
                .SelectMany(j => sequence.Skip(j + 1)
                .Select(x => (sequence[i], sequence[j], x))));
        }

        private bool IsPythagorean ((int, int, int) triple)
        {
            return triple.Item1 + triple.Item2 == triple.Item3;
        }
    }
}
