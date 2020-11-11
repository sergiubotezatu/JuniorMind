using System;
using Xunit;
using RankingTeams;

namespace TeamFacts
{
    public class TeamFacts
    {
        [Fact]
        public void ProgramChecksWhichTeamHasMorePoints()
        {
            Team Barcelona = new Team("Barcelona", 32);
            Team Chelsea = new Team("Chelsea", 28);
            Assert.True(Barcelona.HasMorePointsThan(Chelsea));
        }

        [Fact]
        public void ProgramUpdatesWinnersPoints()
        {
            Team Barcelona = new Team("Barcelona", 28);
            Team methodCheck = new Team("methodCheck", 28);
            Barcelona.UpdatePointsWith(3);
            Assert.True(Barcelona.HasMorePointsThan(methodCheck));
        }

        [Fact]
        public void ProgramIdentifiesTeamsWithSameName()
        {
            Team Barcelona = new Team("Barcelona", 28);
            Team methodCheck = new Team("Barcelona", 24);
            Assert.True(methodCheck.TheSameTeam(Barcelona));
        }
    }
}
