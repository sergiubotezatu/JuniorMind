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

            return ValidateLines(sudoku) && ValidateRows(sudoku) && ValidateSquares(sudoku);
        }


        private bool ValidateLines(int[][] board)
        {
            return board.All(line => line.Length == line.Distinct().Count());
        }

        private bool ValidateRows(int[][] board)
        {
            return Enumerable.Range(0, 8)
                .Select(j => Enumerable.Range(0, 8)
                .Select(i => board[i][j])
                .Distinct())
                .All(rows => rows.Count() == 9);                
        }

        private bool ValidateSquares(int[][] board)
        {
            return GetSquares(board).All(square => square.Distinct().Count() == 9);
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
    }
}
