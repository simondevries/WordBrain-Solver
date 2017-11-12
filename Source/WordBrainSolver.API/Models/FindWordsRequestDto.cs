using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WordBrainSolver.Core;

namespace WordBrainSolver.Api.Models
{
    /// <summary>
    /// Represent a request to generate a game solutions
    /// </summary>
    public class FindWordsRequestDto : IValidatableObject
    {
        /// <summary>
        /// The length of the words that we are looking for
        /// </summary>
        public int[] WordLength { get; set; }

        /// <summary>
        /// The board layout
        /// </summary>
        public string Board { get; set; }

        /// <summary>
        /// Determines whether the specified object is valid.
        /// </summary>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            foreach (string error in GameInputValidator.GetInputValidationErrors(WordLength, Board))
            {
                yield return new ValidationResult(error);
            }
        }
    }
}