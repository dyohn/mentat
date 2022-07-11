using System;
using Xunit;
using Autofac.Extras.Moq;
using Mentat.Domain.Interfaces;
using Mentat.Domain.Bash;
using FluentAssertions;
using Moq;
using System.Linq;

namespace Mentat.Domain.Tests
{
    public class BashTestDriverTests
    {
        [Fact]
        public void ThrowsExceptionWhenBuildingFromNullConfig()
        {
            using var mock = AutoMock.GetLoose();

            var systemUnderTest = mock.Create<BashTestDriver>();

            systemUnderTest.Configure(null);

            var buildAction = () => systemUnderTest.Build();

            buildAction.Should().Throw<NullReferenceException>();
        }

        [Fact]
        public void EmptyScriptsAreNotWritten()
        {
            using var mock = AutoMock.GetLoose();

            mock.Mock<IBashTestConfig>()
                .Setup(config => config.TestFileNames)
                .Returns(Enumerable.Empty<string>());

            var mockConfig = mock.Create<IBashTestConfig>();

            var systemUnderTest = mock.Create<BashTestDriver>();

            systemUnderTest.Configure(mockConfig);

            systemUnderTest.Build();

            mock.Mock<IFileManagerService>()
                .Verify(service => service.SaveScript(It.IsAny<byte[]>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void ScriptsAreWritten()
        {
            using var mock = AutoMock.GetLoose();

            mock.Mock<IBashTestConfig>()
                .Setup(config => config.TestFileNames)
                .Returns(new string[] { "a" });

            mock.Mock<IBashTestConfig>()
                .Setup(config => config.SampleExecutableName)
                .Returns("exec");

            var mockConfig = mock.Create<IBashTestConfig>();

            var systemUnderTest = mock.Create<BashTestDriver>();

            systemUnderTest.Configure(mockConfig);

            systemUnderTest.Build();

            mock.Mock<IFileManagerService>()
                .Verify(service => service.SaveScript(It.IsAny<byte[]>(), It.IsAny<string>()), Times.Once);
        }
    }
}
