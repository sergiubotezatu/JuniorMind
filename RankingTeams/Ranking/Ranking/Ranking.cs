using System;
using System.Collections.Generic;
using System.Text;

namespace RankingTeams
{
    public class Ranking
    {
        private Team[] Allteams;
        
        public Ranking(Team[] teams)
        {
            Allteams = teams;
            for (int i = 0; i < Allteams.Length - 1; i++)
            {
                for (int j = 0; j < Allteams.Length - (i+ 1); j++)
                {
                    UpGradeHigherPoints(j + 1, j);
                }
            }
        }

        const int Victory = 3;

        public void UpdateRanking(Team host, Team guest, int hostScore, int guestScore)
        {
            if (hostScore > guestScore)
            {
                Allteams[GetTeamIndex(host)].UpdatePointsWith(Victory);
                RankNewScores(host);
            }
            else if (hostScore < guestScore)
            {
                Allteams[GetTeamIndex(guest)].UpdatePointsWith(Victory);
                RankNewScores(guest);
            }
            else
            {
                Allteams[GetTeamIndex(host)].UpdatePointsWith(1);
                Allteams[GetTeamIndex(guest)].UpdatePointsWith(1);
                RankNewScores(host);
                RankNewScores(guest);
            }
        }

        public void RankNewScores(Team toBeRanked)
        {
            int indexOfUpGrade = GetTeamIndex(toBeRanked);
            if (indexOfUpGrade > 0)
            {
                int indexOfDownGrade = GetTeamIndex(Allteams[indexOfUpGrade - 1]);
                UpGradeHigherPoints(indexOfUpGrade, indexOfDownGrade);
            }
        }

        public void UpGradeHigherPoints(int winnerIndex, int loserIndex)
        {
            if (Allteams[winnerIndex].HasMorePointsThan(Allteams[loserIndex]))
            {
                Team temp = Allteams[winnerIndex];
                Allteams[winnerIndex] = Allteams[loserIndex];
                Allteams[loserIndex] = temp;
            }
        }

        public void AddNewTeam(Team another)
        {
            Array.Resize(ref Allteams, Allteams.Length + 1);
            Allteams[^1] = another;
            PlaceNewTeam(another);
        }

        private void PlaceNewTeam(Team newTeam)
        {
            int newTeamIndex = GetTeamIndex(newTeam);
            int nextTeamIndex = GetTeamIndex(newTeam) - 1;
            while (nextTeamIndex >= 0 && newTeam.HasMorePointsThan(Allteams[nextTeamIndex]))
            {
                Team temp = Allteams[newTeamIndex];
                Allteams[newTeamIndex] = Allteams[nextTeamIndex];
                Allteams[nextTeamIndex] = temp;
                nextTeamIndex--;
                newTeamIndex--;
            }
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

        public int TeamPositionOf(Team toBeFound)
        {
            return GetTeamIndex(toBeFound) + 1;
        }

        public Team TeamAtPosition(int index)
        {
            return Allteams[index - 1];
        }              
    }
}
