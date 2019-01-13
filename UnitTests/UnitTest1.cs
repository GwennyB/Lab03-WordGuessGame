using System;
using Xunit;
using WordGuessGame;

namespace UnitTests
{
    public class UnitTest1
    {
        // Test that a file can be updated

        [Fact]
        public void OverwriteWordBank_ReturnsCorrectNumberOfLines()
        {
            string testpath = ("../../../wordBank.txt");
            string[] testwords = { "aaa", "bbb", "ccc", "ddd" };
            Assert.Equal(4, Program.OverwriteWordBank(testpath,testwords));
        }


        // Test that a word can be added to a file
        [Fact]
        public void AddWord_ReturnsTrue()
        {
            Assert.True(Program.AddWord("testingtestingtesting"));
        }


        // Test that you can retrieve all words from the file
        [Fact]
        public void ViewWords_ReturnsCorrectNumberOfLines()
        {
            string testpath = ("../../../wordBank.txt");
            string[] testwords = { "aaa", "bbb", "ccc", "ddd" };
            Assert.Equal(testwords, Program.ViewWords(testpath));
        }


        // Test that the word chosen can accurately detect if the letter exists in the word(test that a letter does exist and does not exist)
        [Fact]
        public void CheckGuess_ReturnsTrueWhenFound()
        {
            char[] letters = { 'a','e','i','o','u' };
            char charGuess = 'e';
            Assert.True(Program.CheckGuess(letters, charGuess));
        }
        [Fact]
        public void CheckGuess_ReturnsFalseWhenNotFound()
        {
            char[] letters = { 'a', 'i', 'o', 'u' };
            char charGuess = 'e';
            Assert.False(Program.CheckGuess(letters, charGuess));
        }

    }
}
