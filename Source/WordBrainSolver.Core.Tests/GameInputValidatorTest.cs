using FluentAssertions;
using Xunit;

namespace WordBrainSolver.Core.Tests
{
    public class GameInputValidatorTest
    {
        [Fact]
        public void TestInput()
        {
            int[] array = { 9 };
            GameInputValidator.IsInputValid(array, "asdasdasd").Should().BeTrue();
            GameInputValidator.IsInputValid(array, "").Should().BeFalse();
            GameInputValidator.IsInputValid(array, "asda").Should().BeFalse();
            int[] array2 = { };
            GameInputValidator.IsInputValid(array2, "abcabcabc").Should().BeFalse();
            int[] array3 = { 10 };
            GameInputValidator.IsInputValid(array3, "abcabcabc").Should().BeFalse();
            int[] array4 = { 5, 5 };
            GameInputValidator.IsInputValid(array4, "abcabcabc").Should().BeFalse();
        }
    }
}