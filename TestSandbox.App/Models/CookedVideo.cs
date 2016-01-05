namespace TestSandbox.App.Models
{
    public class CookedVideo
    {
        public string Name;
        public object[] Marks { get; set; }
        public CookedVideo(string name)
        {
            Name = name;
        }
    }
}
