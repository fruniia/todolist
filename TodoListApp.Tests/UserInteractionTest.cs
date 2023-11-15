using Moq;
using TodoApp.Cli;
using TodoApp.Core;

namespace TodoListApp.Tests
{
    public class UserInteractionTests
    {

        [Fact]
        public void GetInputWorksWhenUserInputsString()
        {
            // Arrange
            var mock = new Mock<IConsoleWrapper>();
            var expected = "Anna";
            mock.Setup(x => x.ReadLine()).Returns(expected);
            var sut = new UserInteraction(mock.Object);

            // Act
            var actual = sut.GetInput("Vad heter du?");

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetInputAndParseToIntShouldReturnTrue()
        {
            // Arrange
            var mock = new Mock<IConsoleWrapper>();
            var expected = 1;

            mock.SetupSequence(x => x.ReadLine())
                .Returns("s")
                .Returns("f")
                .Returns("")
                .Returns(expected.ToString());
            
            var sut = new UserInteraction(mock.Object);

            // Act
            var actual = sut.GetInputAndParseToInt();

            // Assert
            Assert.Equal(expected, actual);
            mock.Verify(x => x.ReadLine(), Times.Exactly(4));
        }

    }
}

