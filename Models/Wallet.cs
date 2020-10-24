using System;
using System.ComponentModel.DataAnnotations;

namespace BettingApp.Models
{
    public class Wallet
    {
        public int Id { get; set; }

        [Range(0, int.MaxValue)]
        public int Balance { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
