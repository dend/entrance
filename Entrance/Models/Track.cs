using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entrance.Models
{
    [DataTable("Tracks")]
    public class Track
    {
        public int? Id { get; set; }
        public string Artist { get; set; }
        public string Name { get; set; }
        public string YouTubeLink { get; set; }
        public string Association { get; set; }
    }
}
