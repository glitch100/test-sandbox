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
        IEnumerable<CookedVideo> GrabRecentFromCloud();
    }

    public class VideoService : IVideoService
    {
        private Queue<Video> Videos { get; set; }
        private readonly ICloud _cloudProvider;
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
            var rawVideos = _cloudProvider.GetRawVideos(5, DateTime.UtcNow.AddDays(-1).Date, DateTime.UtcNow.Date);
            return rawVideos.Select(r => new CookedVideo(r.Name)
            {
                Id = r.Id,
                Marks = PrepareMarks(r.Alpha, r.Beta, r.Charlie)
            });
        }

        public string GetNextVideoName()
        {
            if (!Videos.Any())
            {
                throw new Exception("No Videos in the Queue");
            }
            return Videos.Peek().Name;
        }

        private float[] PrepareMarks(IEnumerable<float> alpha, IEnumerable<float> beta, IEnumerable<float> charlie)
        {
            var tempCollection = new List<float>();
            if (alpha != null)
            {
                tempCollection.AddRange(alpha);
            }            
            if (beta != null)
            {
                tempCollection.AddRange(beta);
            }
            if (charlie != null)
            {
                tempCollection.AddRange(charlie);
            }
            return tempCollection.ToArray();
        }
    }
}
