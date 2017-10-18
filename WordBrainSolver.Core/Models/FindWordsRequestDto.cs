namespace WordBrainSolver.Core.Models
{
    /// <summary>
    /// Represent a request to generate a game solutions
    /// </summary>
    public class FindWordsRequestDto
    {
        /// <summary>
        /// The length of the words that we are looking for
        /// </summary>
        public int[] WordLength { get; set; }
        
        /// <summary>
        /// The board layout
        /// </summary>
        public string Board { get; set; }
    }
}