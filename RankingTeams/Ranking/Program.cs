using System;
using System.Globalization;

namespace Ranking
{
    public class Program
    {   
        public static void Main()
        {
            Team Barcelona = new Team("Barcelona", 23);
            Team Rapid = new Team("Rapid", 18);
            Team[] values = { Barcelona, Rapid };
            Ranking teams = new Ranking(values);
            teams.AddNewTeam(new Team("Chelsea", 20));
            teams.AddNewTeam(new Team("Manchester UTD", 22));
            teams.Rank();
            Match first = new Match(Barcelona, Rapid, '1');
            first.GetWinner(teams);
            teams.PrintTeams();
        }
    }    
}
