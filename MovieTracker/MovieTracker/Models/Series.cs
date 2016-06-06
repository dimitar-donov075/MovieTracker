using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieTracker.Models
{
    public class Series
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime AirDate { get; set; }
        public Int16 Season { get; set; }
        public int Episode { get; set; }

        public virtual ApplicationUser ApplicationUserId { get; set; }
    }
}