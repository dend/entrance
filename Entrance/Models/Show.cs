using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entrance.Models
{
    public class Show
    {
        public int? Id { get; set; }
        public int Episode { get; set; }
        public List<Track> Tracks { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
