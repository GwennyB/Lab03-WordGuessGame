using System;
using Xunit;
using WordGuessGame;

namespace UnitTests
{
    public class UnitTest1
    {
        /// <summary>
        /// Test that a file can be updated
        /// Looks for correct number of lines following overwrite with words list of known length
        /// </summary>
        [Fact]
        public void OverwriteWordBank_ReturnsCorrectNumberOfLines()
        {
            string testpath = ("../../../wordBank.txt");
            string[] testwords = { "aaa", "bbb", "ccc", "ddd" };
            Assert.Equal(4, Program.OverwriteWordBank(testpath,testwords));
        }


        /// <summary>
        /// Test that a word can be added to a file
        /// </summary>
        [Fact]
        public void AddWord_ReturnsTrue()
        {
            string testpath = ("../../../wordBank.txt");
            Assert.True(Program.AddWord(testpath,"testingtestingtesting"));
        }


        /// <summary>
        /// Test that you can retrieve all words from the file
        /// builds a new word bank using known words and reads it back to confirm words are same
        /// </summary>
        [Fact]
        public void ViewWords_ReturnsCorrectNumberOfLines()
        {
            string testpath = ("../../../wordBank.txt");
            string[] testwords = { "aaa", "bbb", "ccc", "ddd" };
            Program.OverwriteWordBank(testpath, testwords);
            Assert.Equal(testwords, Program.ViewWords(testpath));
        }


        /// <summary>
        /// Test that the word chosen can accurately detect if the letter exists in the word(test that a letter does exist and does not exist)
        /// Checks for presence of correct guess in a known letters list
        /// </summary>
        [Fact]
        public void CheckGuess_ReturnsTrueWhenFound()
        {
            char[] letters = { 'a','e','i','o','u' };
            char charGuess = 'e';
            Assert.True(Program.CheckGuess(letters, charGuess));
        }
        /// <summary>
        /// Test that the word chosen can accurately detect if the letter exists in the word(test that a letter does exist and does not exist)
        /// Checks for presence of incorrect guess in a known letters list
        /// </summary>        
        [Fact]
        public void CheckGuess_ReturnsFalseWhenNotFound()
        {
            char[] letters = { 'a', 'i', 'o', 'u' };
            char charGuess = 'e';
            Assert.False(Program.CheckGuess(letters, charGuess));
        }

    }
}
