using System;
namespace BettingApp.Models
{
    public class Competition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SportId { get; set; }
        public Sport Sport { get; set; }
    }
}
