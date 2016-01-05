using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestSandbox.Tests.TestSandbox.App.Services.VideoServiceTests
{
    public class GetVideosTests : VideoServiceTestsBase
    {
        [Fact]
        public void GetVideos_Default_DoesntReturnNull() 
        {
            Assert.NotNull(VideoService.GetVideos(0));
        }

        [Fact]
        public void GetVideos_HasVideos_ReturnsPopulatedCollection()
        {
            PopulateVideoQueue();
            var videos = VideoService.GetVideos(3);
            Assert.NotEmpty(videos);
        }

        [Fact]
        public void GetVideos_HasKnownVideoAmount_ReturnsCorrectCollectionSize()
        {
            const int expected = 5;
            PopulateVideoQueue(expected);
            var videos = VideoService.GetVideos(5).ToArray();
            int size = videos.Count();
            Assert.Equal(expected ,size);
        }

        [Fact]
        public void GetVideos_HasVideos_ReturnsRequestedCollectionSize()
        {
            const int expected = 4;
            PopulateVideoQueue(5);
            var videos = VideoService.GetVideos(4).ToArray();
            int size = videos.Count();
            Assert.Equal(expected ,size);
        }
    }
}
