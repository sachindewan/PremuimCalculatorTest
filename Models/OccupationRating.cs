using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PremuimCalculator.Models
{
    public class OccupationRating
    {
        public int Id { get; set; }
        public string Rating { get; set; }
        public double? Factor { get; set; }
    }
}
