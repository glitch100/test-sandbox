using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSandbox.App.Models
{
    public class Video
    {
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        public string Name { get; set; }
        public VideoData[] Chunks { get; set; }

        public Video(string name)
        {
            Name = name;
        }
    }
}
