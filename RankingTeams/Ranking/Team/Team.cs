﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RankingTeams
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

        public void UpdatePointsWith(int pointsToAdd)
        {
            this.Points += pointsToAdd;
        }

        public bool TheSameTeam(Team toCompareTo)
        {
            return this.Name == toCompareTo.Name;
        }
        
        public bool TeamPointsAre(int points)
        {
            return this.Points == points;
        }
    }
}
