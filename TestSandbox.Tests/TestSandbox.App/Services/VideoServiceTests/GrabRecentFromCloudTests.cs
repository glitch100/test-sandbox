using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using TestSandbox.App.Models;
using TestSandbox.App.Services;
using Xunit;

namespace TestSandbox.Tests.TestSandbox.App.Services.VideoServiceTests
{
    public class GrabRecentFromCloudTests : VideoServiceTestsBase
    {
        private const string FirstVidName = "FIRSTMOCK";
        public GrabRecentFromCloudTests()
        {
            MockCloudService.Setup(c => c.GetRawVideos(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                            .Returns(() => new[] { new RawVideo() { Name = FirstVidName } });
        }

        [Fact]
        public void GrabRecentFromCloud_Default_DoesntReturnNull()
        {
            Assert.NotNull(VideoService.GrabRecentFromCloud());
        }

        [Fact]
        public void GrabRecentFromCloud_Default_DoesntReturnEmptyCollection()
        {
            var videos = VideoService.GrabRecentFromCloud();
            Assert.True(videos.Any());
        }

        [Fact]
        public void GrabRecentFromCloud_Default_VerifyCallToCloudService()
        {
            VideoService.GrabRecentFromCloud();
            MockCloudService.Verify(cloud => cloud.GetRawVideos(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()));
        }

        [Fact]
        public void GrabRecentFromCloud_CalledWithAny_ReturnsValidCollection()
        {
            var result = VideoService.GrabRecentFromCloud();
            Assert.Contains(result, video => video.Name == FirstVidName);
        }

        [Fact]
        public void GrabRecentFromCloud_CalledWithDifferentDays_ReturnsDifferentVideos()
        {
            int index = 1;
            MockCloudService.Setup(t => t.GetRawVideos(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                            .Returns(() => new[] { new RawVideo() { Name = FirstVidName, Id = index }})
                            .Callback(() => index ++);
            var result1 = VideoService.GrabRecentFromCloud();
            var result2 = VideoService.GrabRecentFromCloud();
            Assert.NotStrictEqual(result1.First().Id,result2.First().Id);
        }

        [Fact]
        public void GrabRecentFromCloud_CalledWithLastDay5_ReturnsValidCollectionAndCount()
        {
            var createdDate = DateTime.UtcNow.AddHours(-5);
            MockCloudService.Setup(c => c.GetRawVideos(5, DateTime.UtcNow.AddDays(-1).Date, DateTime.UtcNow.Date))
                            .Returns(() => new[]
                            {
                                new RawVideo() { Name = FirstVidName, Created = createdDate},
                                new RawVideo() { Name = "Second", Created = createdDate },
                                new RawVideo() { Name = "Third", Created = createdDate },
                                new RawVideo() { Name = "Fourth", Created = createdDate},
                                new RawVideo() { Name = "Fifth", Created = createdDate}
                            });

            var result = VideoService.GrabRecentFromCloud().ToList();
            Assert.Contains(result, video => video.Name == FirstVidName);
            Assert.Contains(result, video => video.Name == "Second");
            Assert.Contains(result, video => video.Name == "Third");
            Assert.Contains(result, video => video.Name == "Fourth");
            Assert.Contains(result, video => video.Name == "Fifth");
            Assert.Equal(5, result.Count);
        }

        [Fact]
        public void GrabRecentFromCloud_CalledWithLastDay5_ElementContainsConcatMarks()
        {
            var createdDate = DateTime.UtcNow.AddHours(-5);
            var rawVid = new RawVideo()
            {
                Name = FirstVidName,
                Created = createdDate,
                Alpha = new float[] {12, 1515, 15123, 651},
                Beta = new float[] {15, 1655, 1253, 6531},
                Charlie = new float[] {99, 1666}
            };

            MockCloudService.Setup(c => c.GetRawVideos(5, DateTime.UtcNow.AddDays(-1).Date, DateTime.UtcNow.Date))
                            .Returns(() => new[]
                            {
                                rawVid
                            });

            var result = VideoService.GrabRecentFromCloud().ToList();
            Assert.True(MarksMatch(rawVid,result.First().Marks));
        }

        private bool MarksMatch(RawVideo raw, IEnumerable<float> cookedMarks)
        {
            var marks = raw.Alpha.Concat(raw.Beta).Concat(raw.Charlie).ToArray();
            return cookedMarks.SequenceEqual(marks);
        }
    }
}