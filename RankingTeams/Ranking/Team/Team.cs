using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking
{
    public class Team
    {
        readonly private string Name;
        private int Points;
        public Team(string name, int points)
        {
            this.Name = name;
            this.Points = points;
        }

        public bool HasMorePointsThan(Team toCompareTo)
        {
            return this.Points > toCompareTo.Points;
        }

        public Team UpdatePointsWith(int pointsToAdd)
        {
            return new Team(this.Name, this.Points + pointsToAdd);
        }

        public bool TheSameTeam(Team toCompareTo)
        {
            return this.Name == toCompareTo.Name;
        }

        public void PrintName()
        {
            Console.WriteLine(this.Name);
        }

        public void PrintTeam()
        {
            Console.WriteLine($"{this.Name} - { this.Points}");
        }
    }
}
