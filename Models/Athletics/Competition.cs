using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api4u.Models.Athletics
{
    public class Competition
    {
        public int CompetitionId { get; set; }

        [Required]
        public string EventName { get; set; }
        public List<Athlete> Athletes { get; set; }
    }
}