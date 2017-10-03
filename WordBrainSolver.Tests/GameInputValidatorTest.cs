using NUnit.Framework;
using WordBrainSolver.Core;

namespace WordBrainSolver.Tests
{
    public class GameInputValidatorTest
    {
        [Test]
        public void TestInput()
        {
            int[] array = { 9 };
            Assert.AreEqual(GameInputValidator.IsInputValid(array, "asdasdasd"), true);
            Assert.AreEqual(GameInputValidator.IsInputValid(array, ""), false);
            Assert.AreEqual(GameInputValidator.IsInputValid(array, "asda"), false);
            int[] array2 = { };
            Assert.AreEqual(GameInputValidator.IsInputValid(array2, "abcabcabc"), false);
            int[] array3 = { 10 };
            Assert.AreEqual(GameInputValidator.IsInputValid(array3, "abcabcabc"), false);
            int[] array4 = { 5, 5 };
            Assert.AreEqual(GameInputValidator.IsInputValid(array4, "abcabcabc"), false);
        }
    }
}