using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestSandbox.App.Models;
using TestSandbox.App.Services;
using Xunit;

namespace TestSandbox.Tests.TestSandbox.App.Services.VideoServiceTests
{
    public class GetNextVideoNameTests : VideoServiceTestsBase
    {

        [Fact]
        public void GetNextVideoName_Default_DoesntReturnNull()
        {
            PopulateVideoQueue();
            Assert.NotNull(VideoService.GetNextVideoName());
        }

        [Fact]
        public void GetNextVideoName_Default_DoesntReturnEmptyString()
        {
            PopulateVideoQueue();
            string name = VideoService.GetNextVideoName();
            Assert.False(string.IsNullOrEmpty(name));
        }        
        
        [Fact]
        public void GetNextVideoName_VideosDontExist_ThrowsException()
        {
            Assert.Throws<Exception>(() => VideoService.GetNextVideoName());
        }
        
        [Fact]
        public void GetNextVideoName_VideosExist_ReturnsKnownName()
        {
            const string expected = "Ultimate Video";
            var v = new Video(expected)
            {
                Name = expected
            };
            VideoService.AddToQueue(v);

            Assert.Equal(expected, VideoService.GetNextVideoName());
        }
    }
}
