using System;
using Xunit;
using Ranking;

namespace ClassTeamFacts
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
            Team methodCheck = Barcelona.UpdatePointsWith(3);
            Assert.True(methodCheck.HasMorePointsThan(Barcelona));
        }

        [Fact]
        public void ProgramIdentifiesTeamsWithSameName()
        {
            Team Barcelona = new Team("Barcelona", 28);
            Team methodCheck = new Team("Barcelona", 28);
            Assert.True(methodCheck.TheSameTeam(methodCheck));
        }
    }
}
