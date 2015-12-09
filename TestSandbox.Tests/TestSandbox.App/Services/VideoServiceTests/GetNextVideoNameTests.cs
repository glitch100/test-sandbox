using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSandbox.App.Services;
using Xunit;

namespace TestSandbox.Tests.TestSandbox.App.Services.VideoServiceTests
{
    public class GetNextVideoNameTests : IDisposable
    {
        private VideoService _videoService;

        public GetNextVideoNameTests()
        {
            _videoService = new VideoService();
        }

        public void Dispose()
        {
            _videoService.ClearQueue();
        }

        [Fact]
        public void GetNextVideoName_Default_DoesntReturnNull()
        {
            PopulateVideoQueue();
            Assert.NotNull(_videoService.GetNextVideoName());
        }

        [Fact]
        public void GetNextVideoName_Default_DoesntReturnEmptyString()
        {
            PopulateVideoQueue();
            string name = _videoService.GetNextVideoName();
            Assert.False(string.IsNullOrEmpty(name));
        }        
        
        [Fact]
        public void GetNextVideoName_VideosDontExist_ThrowsException()
        {
            Assert.Throws<Exception>(() => _videoService.GetNextVideoName());
        }
        
        [Fact]
        public void GetNextVideoName_VideosExist_ReturnsKnownName()
        {
            const string expected = "Ultimate Video";
            var v = new Video()
            {
                Name = expected
            };
            _videoService.AddToQueue(v);

            Assert.Equal(expected, _videoService.GetNextVideoName());
        }

        #region Test Helper Methods 
        private void PopulateVideoQueue()
        {
            _videoService.AddToQueue(new Video() { Name = "Our First Video" });
            _videoService.AddToQueue(new Video() { Name = "Our Second Video" });
            _videoService.AddToQueue(new Video() { Name = "Super Awesome Third Video" });
        }
        #endregion

    }
}
