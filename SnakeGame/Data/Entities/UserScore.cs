using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Data.Entities
{
    public class UserScore
    {
        [Key]
        [Required]
        public int UserScoreID { get; set; }

        [Required]
        public long Score { get; set; }
    }
}
