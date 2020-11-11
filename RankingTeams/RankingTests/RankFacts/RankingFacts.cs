using System;
using Xunit;
using RankingTeams;
using System.Linq;

namespace RankFacts
{
    public class RankingFacts
    {
        [Fact]
        public void RanksCorrectlyAlreadySortedList()
        {
            Team Barcelona = new Team("Barcelona", 23);
            Team Rapid = new Team("Rapid", 22);
            Team Manchester = new Team("Manchester UTD", 18);
            Team[] teams = { Barcelona, Rapid, Manchester };
            Ranking ranking = new Ranking(teams);
            Assert.True(ranking.TeamAtPosition(1) == Barcelona);
            Assert.True(ranking.TeamAtPosition(2) == Rapid);
            Assert.True(ranking.TeamAtPosition(3) == Manchester);
        }

        [Fact]
        public void RanksCorrectlyOnlyOneTeamMisplaced()
        {
            Team Barcelona = new Team("Barcelona", 23);
            Team Rapid = new Team("Rapid", 18);
            Team Manchester = new Team("Manchester UTD", 22);
            Team Chelsea = new Team("Chelsea", 20);
            Team[] teams = { Barcelona, Rapid, Manchester, Chelsea };
            Ranking ranking = new Ranking(teams);
            Assert.True(ranking.TeamAtPosition(1) == Barcelona);
            Assert.True(ranking.TeamAtPosition(2) == Manchester);
            Assert.True(ranking.TeamAtPosition(3) == Chelsea);
            Assert.True(ranking.TeamAtPosition(4) == Rapid);
        }

        [Fact]
        public void RanksCorrectlyUnSortedList()
        {
            Team Barcelona = new Team("Barcelona", 22);
            Team Rapid = new Team("Rapid", 21);
            Team Manchester = new Team("Manchester UTD", 23);
            Team Chelsea = new Team("Chelsea", 20);
            Team Juventus = new Team("Juventus", 25);
            Team[] teams = { Barcelona, Rapid, Manchester, Chelsea, Juventus };
            Ranking ranking = new Ranking(teams);
            Assert.True(ranking.TeamAtPosition(1) == Juventus);
            Assert.True(ranking.TeamAtPosition(2) == Manchester);
            Assert.True(ranking.TeamAtPosition(3) == Barcelona);
            Assert.True(ranking.TeamAtPosition(4) == Rapid);
            Assert.True(ranking.TeamAtPosition(5) == Chelsea);
        }

        [Fact]
        public void AddsTeamInCorrectPosition()
        {
            Team Barcelona = new Team("Barcelona", 23);
            Team Rapid = new Team("Rapid", 18);
            Team Manchester = new Team("Manchester UTD", 22);
            Team Chelsea = new Team("Chelsea", 20);
            Team[] teams = { Barcelona, Rapid, Manchester };
            Ranking ranking = new Ranking(teams);
            ranking.AddNewTeam(Chelsea);
            Assert.True(ranking.TeamAtPosition(3) == Chelsea);
        }


        [Fact]
        public void AddsTeamWhileKeepingRankingOrder()
        {
            Team Barcelona = new Team("Barcelona", 23);
            Team Rapid = new Team("Rapid", 18);
            Team Manchester = new Team("Manchester UTD", 22);
            Team Chelsea = new Team("Chelsea", 20);
            Team[] teams = { Barcelona, Rapid, Manchester };
            Ranking ranking = new Ranking(teams);
            ranking.AddNewTeam(Chelsea);
            Assert.True(ranking.TeamAtPosition(1) == Barcelona);
            Assert.True(ranking.TeamAtPosition(2) == Manchester);
            Assert.True(ranking.TeamAtPosition(3) == Chelsea);
            Assert.True(ranking.TeamAtPosition(4) == Rapid);
        }

        [Fact]
        public void WinnerPointsAreIncrementedByThree()
        {
            Team Barcelona = new Team("Barcelona", 23);
            Team Rapid = new Team("Rapid", 18);
            Team Manchester = new Team("Manchester UTD", 22);
            Team Chelsea = new Team("Chelsea", 20);
            Team[] teams = { Barcelona, Rapid, Manchester, Chelsea };
            Ranking ranking = new Ranking(teams);
            ranking.UpdateRanking(Barcelona, Chelsea, 1, 2);
            Assert.True(Chelsea.TeamPointsAre(23));
        }

        [Fact]
        public void WinnerIsUpgradedCorrectly()
        {
            Team Barcelona = new Team("Barcelona", 23);
            Team Rapid = new Team("Rapid", 18);
            Team Manchester = new Team("Manchester UTD", 22);
            Team Chelsea = new Team("Chelsea", 20);
            Team[] teams = { Barcelona, Rapid, Manchester, Chelsea };
            Ranking ranking = new Ranking(teams);
            ranking.UpdateRanking(Barcelona, Chelsea, 1, 2);
            Assert.True(ranking.TeamAtPosition(2) == Chelsea);
        }

        [Fact]
        public void TiesResultInIncrementingBothTeamsByOne()
        {
            Team Barcelona = new Team("Barcelona", 23);
            Team Rapid = new Team("Rapid", 18);
            Team Manchester = new Team("Manchester UTD", 22);
            Team Chelsea = new Team("Chelsea", 20);
            Team[] teams = { Barcelona, Rapid, Manchester, Chelsea };
            Ranking ranking = new Ranking(teams);
            ranking.UpdateRanking(Barcelona, Chelsea, 2, 2);
            Assert.True(Barcelona.TeamPointsAre(24));
            Assert.True(Chelsea.TeamPointsAre(21));            
        }

        [Fact]
        public void TiesResultInCorrectUpgradingOfBothTeams()
        {
            Team Barcelona = new Team("Barcelona", 23);
            Team Rapid = new Team("Rapid", 18);
            Team Manchester = new Team("Manchester UTD", 22);
            Team Chelsea = new Team("Chelsea", 20);
            Team[] teams = { Barcelona, Rapid, Manchester, Chelsea };
            Ranking ranking = new Ranking(teams);
            ranking.UpdateRanking(Barcelona, Chelsea, 2, 2);
            Assert.True(ranking.TeamAtPosition(1) == Barcelona);
            Assert.True(ranking.TeamAtPosition(3) == Chelsea);
        }      

        [Fact]
        public void WinnerGetsPointsAndPositionUpgrade()
        {
            Team Barcelona = new Team("Barcelona", 23);
            Team Rapid = new Team("Rapid", 18);
            Team Manchester = new Team("Manchester UTD", 22);
            Team Chelsea = new Team("Chelsea", 20);
            Team[] teams = { Barcelona, Rapid, Manchester, Chelsea };
            Ranking ranking = new Ranking(teams);
            ranking.UpdateRanking(Chelsea, Manchester, 1, 0);
            Assert.True(ranking.TeamAtPosition(2) == Chelsea);
            Assert.True(Chelsea.TeamPointsAre(23));
        }

        [Fact]
        public void ReturnCorrectlyPositionOfSearchedTeam()
        {
            Team Barcelona = new Team("Barcelona", 23);
            Team Rapid = new Team("Rapid", 18);
            Team Manchester = new Team("Manchester UTD", 22);
            Team Chelsea = new Team("Chelsea", 20);
            Team Juventus = new Team("Juventus", 24);
            Team[] teams = { Barcelona, Rapid, Manchester, Chelsea, Juventus };
            Ranking ranking = new Ranking(teams);
            Assert.True(ranking.TeamPositionOf(Juventus) == 1);
        }

        [Fact]
        public void WorksWithBiggerRankingAndMultipleMatches()
        {
            Team Barcelona = new Team("Barcelona", 35);
            Team Rapid = new Team("Rapid", 29);
            Team Manchester = new Team("Manchester UTD", 22);
            Team Chelsea = new Team("Chelsea", 20);
            Team Juventus = new Team("Juventus", 24);
            Team Arsenal = new Team("Arsenal", 32);
            Team Liverpool = new Team("Liverpool", 48);
            Team ManchesterCity = new Team("Manchester City", 29);
            Team PSG = new Team("PSG", 46);
            Team CFRCluj = new Team("CFRCluj", 45);
            Team Jiul = new Team("Jiul Petrosani", 11);
            Team Valencia = new Team("Valencia", 19);
            Team CSKSofia = new Team("CSKSofia", 9);
            Team BayernMunchen = new Team("Bayern Munchen", 40);
            Team RealMadrid = new Team("Real Madrid", 40);
            Team[] teams = { Barcelona , Rapid, Manchester, Chelsea, Juventus, Liverpool, Arsenal,
            ManchesterCity, PSG, CFRCluj, Jiul, Valencia, CSKSofia, BayernMunchen, RealMadrid };
            Ranking ranking = new Ranking(teams);
            ranking.UpdateRanking(Rapid, Jiul, 0, 3);
            ranking.UpdateRanking(Manchester, RealMadrid, 1, 1);
            ranking.UpdateRanking(Liverpool, PSG, 1, 3);
            ranking.UpdateRanking(BayernMunchen, CSKSofia, 3, 0);
            ranking.UpdateRanking(Arsenal, ManchesterCity, 6, 5);
            ranking.UpdateRanking(CSKSofia, Chelsea, 0, 4);
            Assert.True(ranking.TeamAtPosition(1) == PSG && PSG.TeamPointsAre(49));
            Assert.True(ranking.TeamAtPosition(2) == Liverpool && Liverpool.TeamPointsAre(48));
            Assert.True(ranking.TeamAtPosition(3) == CFRCluj && CFRCluj.TeamPointsAre(45));
            Assert.True(ranking.TeamAtPosition(4) == BayernMunchen && BayernMunchen.TeamPointsAre(43));
            Assert.True(ranking.TeamAtPosition(5) == RealMadrid && RealMadrid.TeamPointsAre(41));
            Assert.True(ranking.TeamAtPosition(6) == Barcelona && Barcelona.TeamPointsAre(35));
            Assert.True(ranking.TeamAtPosition(7) == Arsenal && Arsenal.TeamPointsAre(35));
            Assert.True(ranking.TeamAtPosition(8) == Rapid && Rapid.TeamPointsAre(29));
            Assert.True(ranking.TeamAtPosition(9) == ManchesterCity && ManchesterCity.TeamPointsAre(29));
            Assert.True(ranking.TeamAtPosition(10) == Juventus && Juventus.TeamPointsAre(24));
            Assert.True(ranking.TeamAtPosition(11) == Manchester && Manchester.TeamPointsAre(23));
            Assert.True(ranking.TeamAtPosition(12) == Chelsea && Chelsea.TeamPointsAre(23));
            Assert.True(ranking.TeamAtPosition(13) == Valencia && Valencia.TeamPointsAre(19));
            Assert.True(ranking.TeamAtPosition(14) == Jiul && Jiul.TeamPointsAre(14));
            Assert.True(ranking.TeamAtPosition(15) == CSKSofia && CSKSofia.TeamPointsAre(9));
        }
    }
}
