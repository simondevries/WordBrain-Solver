using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace WordBrainSolver.Core.Tests
{
    public class GameInputValidatorTest
    {
        [Theory]
        [InlineData("", "9", false)]
        [InlineData("asda", "9", false)]
        [InlineData("abcabcabc", "", false)]
        [InlineData("abcabcabc", "10", false)]
        [InlineData("abcabcabc", "5,5", false)]
        [InlineData("asdasdasd", "9", true)]
        public void IsInputValidShouldValidateInputCorrectly(string board, string wordLength, bool expectedResult)
        {
            // Arrange
            int[] wordLengthArray = wordLength.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            // Act
            bool isInputValid = GameInputValidator.IsInputValid(wordLengthArray, board);

            // Assert
            isInputValid.Should().Be(expectedResult);
        }
    }
}