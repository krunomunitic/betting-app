using System;
using System.Collections.Generic;

namespace BettingApp.Dtos
{
    public class FixturesDto
    {
        public string Name { get; set; }

        public IList<FixtureDto> Fixtures { get; set; }
    }
}
