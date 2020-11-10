using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking
{
    public class Match
    {
        Team Host;
        Team Guest;
        char Result;
        public Match(Team host, Team guest, char result)
        {
            this.Host = host;
            this.Guest = guest;
            this.Result = result;
        }

        public void GetWinner(Ranking ranking)
        {
            switch (Result)
            {
                case '1':
                    ranking.UpdateWinner(this.Host);
                    break;
                case 'x':
                    ranking.UpdateTie(this.Host, this.Guest);
                    break;
                case '2':
                    ranking.UpdateWinner(this.Guest);
                    break;
                default:
                    break;
            }
        }

        public char GetResult()
        {
            return Result;
        }
    }
}
