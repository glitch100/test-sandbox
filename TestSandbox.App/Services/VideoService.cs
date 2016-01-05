using System;
using System.Collections.Generic;
using System.Linq;
using TestSandbox.App.Models;

namespace TestSandbox.App.Services
{
    public interface IVideoService
    {
        void AddToQueue(Video v);
        void ClearQueue();
        string GetNextVideoName();
        IEnumerable<Video> GetVideos(int amount = 3);
    }

    public class VideoService : IVideoService
    {
        private Queue<Video> Videos { get; set; }
        private ICloud _cloudProvider;
        public VideoService(ICloud cloud)
        {
            Videos = new Queue<Video>();
            _cloudProvider = cloud;
        }

        public void AddToQueue(Video v)
        {
            Videos.Enqueue(v);
        }

        public void ClearQueue()
        {
            Videos.Clear();
        }

        public IEnumerable<Video> GetVideos(int amount = 3)
        {
            return Videos.Take(amount).ToArray();
        }

        public IEnumerable<CookedVideo> GrabRecentFromCloud()
        {
            throw new NotImplementedException();
        }

        public string GetNextVideoName()
        {
            if (!Videos.Any())
            {
                throw new Exception("No Videos in the Queue");
            }
            return Videos.Peek().Name;
        }
    }
}
