using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BettingApp.Models
{
    public class Wallet
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        [Range(typeof(decimal), "0", "999999999")]
        public decimal Balance { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
