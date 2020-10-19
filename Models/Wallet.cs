using System;
using System.ComponentModel.DataAnnotations;

namespace BettingApp.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public int Balance { get; set; }
    }
}
