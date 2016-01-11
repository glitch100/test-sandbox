namespace TestSandbox.App.Models
{
    public class CookedVideo
    {
        public int Id { get; set; }
        public string Name;
        public float[] Marks { get; set; }
        public CookedVideo(string name)
        {
            Name = name;
        }
    }
}
