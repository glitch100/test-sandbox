using System;

namespace TestSandbox.App.Models
{
    public class RawVideo
    {
        public string Name { get; set; }
        public VideoData Alpha { get; set; }
        public VideoData Beta { get; set; }
        public VideoData Charlie { get; set; }
        public DateTime Created { get; set; }
    }
}