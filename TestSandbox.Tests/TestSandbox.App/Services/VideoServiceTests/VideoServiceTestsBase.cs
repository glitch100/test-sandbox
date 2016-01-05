using System;
using TestSandbox.App.Models;
using TestSandbox.App.Services;

namespace TestSandbox.Tests.TestSandbox.App.Services.VideoServiceTests
{
    public abstract class VideoServiceTestsBase :IDisposable
    {
        protected IVideoService VideoService;

        protected VideoServiceTestsBase()
        {
            VideoService = new VideoService();
        }

        public void Dispose()
        {
            VideoService.ClearQueue();
        }

        #region Test Helper Methods
        protected void PopulateVideoQueue(int amount = 3 )
        {
            for (int i = 0; i < amount; i++)
            {
                VideoService.AddToQueue(new Video(string.Format("Test Video {0}", i))
                {
                    Id = i, 
                });
            }
        }
        #endregion
    }
}