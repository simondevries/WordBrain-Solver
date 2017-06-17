using Moq;
using NUnit.Framework;
using WordBrainSolver.Core.Interfaces;

namespace WordBrainSolver.Tests
{
    public class GameInputValidatorTest
    {
        [Test]
        public void CanVerifyValidWordLength()
        {
            var mock = new Mock<IGameInputValidator>();
            mock.Setup(foo => foo.Validate(It.IsInRange<int>(0, 10, Range.Inclusive), 1,"asdbc")).Returns(false);
            mock.Setup(foo => foo.Validate(It.IsInRange<int>(-10, -1, Range.Inclusive), 1,"asdbc")).Returns(false);
            mock.Setup(foo => foo.Validate(5, 1,"abc")).Returns(false);
        }

        [Test]
        public void CanValidateBoardSize()
        {
            var mock = new Mock<IGameInputValidator>();
            mock.Setup(foo => foo.Validate(1, 3, "as")).Returns(false);
            mock.Setup(foo => foo.Validate(1, 3, "asd")).Returns(true);
            mock.Setup(foo => foo.Validate(1, 9, "abcabcabcabc")).Returns(true);
        }
    }
}