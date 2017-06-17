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
            mock.Setup(foo => foo.Validate(1,1,"asdbc")).Returns(true);

//            var mock = new Mock<IFoo>();
//            mock.Setup(foo => foo.DoSomething("ping")).Returns(true);
        }

    }
}