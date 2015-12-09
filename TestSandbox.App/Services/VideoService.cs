using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TestSandbox.App.Services
{
    public class Video
    {
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        public string Name { get; set; }
        public object[] Chunks { get; set; }
    }

    public class VideoService
    {
        private Queue<Video> Videos { get; set; }

        public VideoService()
        {
            Videos = new Queue<Video>();
        }

        public void AddToQueue(Video v)
        {
            Videos.Enqueue(v);
        }

        public void ClearQueue()
        {
            Videos.Clear();
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
