using System;
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
            int hostPos = GetTeamIndex(host);
            int guestPos = GetTeamIndex(guest);
            if (hostScore > guestScore)
            {
                Allteams[hostPos].UpdatePointsWith(Victory);
                RankNewScores(host);
            }
            else if (hostScore < guestScore)
            {
                Allteams[guestPos].UpdatePointsWith(Victory);
                RankNewScores(guest);
            }
            else
            {
                Allteams[hostPos].UpdatePointsWith(1);
                Allteams[guestPos].UpdatePointsWith(1);
                RankNewScores(host);
                RankNewScores(guest);
            }
        }

        public void RankNewScores(Team toBeRanked)
            {
            int indexOfUpGrade = GetTeamIndex(toBeRanked);
            if (indexOfUpGrade <= 0)
            {
                return;
            }

            int indexOfDownGrade = GetTeamIndex(Allteams[indexOfUpGrade - 1]);
            UpGradeHigherPoints(indexOfUpGrade, indexOfDownGrade);
        }

        private void UpGradeHigherPoints(int winnerIndex, int loserIndex)
            {
            if (!Allteams[winnerIndex].HasMorePointsThan(Allteams[loserIndex]))
            {
                return;
            }

            Team temp = Allteams[winnerIndex];
            Allteams[winnerIndex] = Allteams[loserIndex];
            Allteams[loserIndex] = temp;
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
            while (nextTeamIndex >= 0)
            {
                UpGradeHigherPoints(newTeamIndex, nextTeamIndex);
                nextTeamIndex--;
                newTeamIndex--;
            }
        }

        public int GetTeamIndex(Team winner)
        {
            int i = 0;
            if (winner != null)
            {
                while (!winner.TheSameTeam(Allteams[i]))
                {
                    i++;
                }
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
