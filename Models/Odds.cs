using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BettingApp.Models
{
    public class Odds
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "decimal(7,2)")]
        public decimal? Value { get; set; }

        public List<FixtureOdds> FixtureOdds { get; set; }
        public List<FixtureOddsSpecial> FixtureOddsSpecial { get; set; }
    }
}
