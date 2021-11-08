using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Diverse
{
    public class TestResults
    {
        public string Id { get; set; }
        public string FamilyId { get; set; }
        public int Score { get; set; }
    }

    public class FamHighScore
    {
        public IEnumerable<TestResults> GetHighScores(IEnumerable<TestResults> results)
        {
            return results.GroupBy(family => family.FamilyId)
                .Select(x => x
                .Aggregate(x.First(),(highest, current) => current.Score > highest.Score ? highest = current : highest));
        }
    }
    
}
