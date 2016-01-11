using System;

namespace TestSandbox.App.Models
{
    public class RawVideo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float[] Alpha { get; set; }
        public float[] Beta { get; set; }
        public float[] Charlie { get; set; }
        public DateTime Created { get; set; }
    }
}