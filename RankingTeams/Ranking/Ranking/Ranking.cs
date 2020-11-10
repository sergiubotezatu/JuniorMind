using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking
{
    public class Ranking
    {
        private Team[] Allteams;

        public Ranking(Team[] teams)
        {
            Allteams = teams;
        }

        const int Victory = 3;

        public void AddNewTeam(Team another)
        {
            Array.Resize(ref Allteams, Allteams.Length + 1);
            Allteams[^1] = another;
        }

        public void Rank()
        {
            for (int i = 0; i < Allteams.Length - 1; i++)
            {
                for (int j = 0; j < Allteams.Length - 1; j++)
                {
                    if (!this.Allteams[j].HasMorePointsThan(this.Allteams[j + 1]))
                    {
                        Team winner = Allteams[j + 1];
                        Allteams[j + 1] = Allteams[j];
                        Allteams[j] = winner;
                    }
                }
            }
        }

        public void UpdateWinner(Team winner)
        {
            Allteams[GetTeamIndex(winner)] = Allteams[GetTeamIndex(winner)].UpdatePointsWith(Victory);
        }

        public void UpdateTie(Team host, Team guest)
        {
            Allteams[GetTeamIndex(host)] = Allteams[GetTeamIndex(host)].UpdatePointsWith(1);
            Allteams[GetTeamIndex(guest)] = Allteams[GetTeamIndex(guest)].UpdatePointsWith(1);
        }

        public int GetTeamIndex(Team winner)
        {
            int i = 0;
            while (!winner.TheSameTeam(Allteams[i]))
            {
                i++;
            }

            return i;
        }

        public void PrintTeamPosition(Team toBeFound)
        {
            Console.WriteLine(GetTeamIndex(toBeFound) + 1);
        }

        public void FindTeamSpecifiedPos(int index)
        {
            Allteams[index - 1].PrintName();
        }

        public void PrintTeams()
        {
            foreach (Team element in Allteams)
            {
                element.PrintTeam();
            }
        }
    }
}
