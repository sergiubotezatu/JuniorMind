using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Diverse
{
    class SudokuValidator
    {
        public bool ValidateSudoku(int[][] sudoku)
        {
            if(sudoku.Any(line => line.Length < 9) || sudoku.Any(line => line.Any(digit => !Enumerable.Range(1, 9).Contains(digit))))
            {
                return false;
            }

            return Validate(sudoku);
        }


        private bool Validate(int[][] board)
        {
            return GetLines(board)
                .Concat(GetRows(board)
                .Concat(GetSquares(board)))
                .All(sequence => sequence.Count() == 9);
        }

        private IEnumerable<IEnumerable<int>> CreateGroups(IEnumerable<IEnumerable<int>> board, int skipped)
        {
            return Enumerable.Range(0, 2).SelectMany(i => board.Select(x => x.Skip(i * skipped).Take(skipped)));
        }

        private IEnumerable<IEnumerable<int>> GetSquares(int[][] board)
        {
            IEnumerable<IEnumerable<int>> sudoku = board;
            return CreateGroups(CreateGroups(sudoku, 3), 9);
        }

        private IEnumerable<IEnumerable<int>> GetRows(int[][] board)
        {
            return Enumerable.Range(0, 8)
                .Select(j => Enumerable.Range(0, 8)
                .Select(i => board[i][j])
                .Distinct());
        }

        private IEnumerable<IEnumerable<int>> GetLines(int[][] board)
        {
            return board.Select(lines => lines.Distinct());
        }
    }
}
