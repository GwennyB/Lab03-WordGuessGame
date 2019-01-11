using System;
using System.IO;

namespace WordGuessGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            bool runMainMenu = true;
            string path = ("../../../wordBank.txt");
            // MakeWordBank(path);
            // ReadWordBank(path);
            /*while (runMainMenu)
            {
                runMainMenu = MainMenu(runMainMenu);
            }*/
        }

        static bool MainMenu(bool runMainMenu)
        {
            // display menu
            Console.Clear();
            Console.WriteLine("Please select an option: ");
            Console.WriteLine("(1) Play New Game");
            Console.WriteLine("(2) Administration Options");
            Console.WriteLine("(3) Exit Program");

            // receive and validate direction from user
            string selected = "";
            try
            {
                selected = Console.ReadLine();
            }
            catch(Exception)
            {
                Console.WriteLine($"\nInvalid entry. Press ENTER to try again.");
                Console.ReadLine();
                throw;
            }

            // route to menu selection
            switch (selected)
            {
                case "1":
                    NewGame();
                    break;
                case "2":
                    Admin();
                    break;
                case "3":
                    runMainMenu = false;
                    break;
                default:
                    Console.WriteLine($"{selected} isn't a valid selection. Starting over.");
                    break;
            }


            return runMainMenu;
        }

        static void NewGame()
        {

        }

        static string[] ReadWordBank(string path)
        {
            try
            {
                string[] lines = File.ReadAllLines(path);
                Console.WriteLine($"Line 1: {lines[0]}");
                Console.ReadLine();
                return lines;
            }
            catch (Exception)
            {
                Console.WriteLine($"ERROR: Could not read file at {path}");
                Console.ReadLine();
                throw;
            }
        }

        static void SetUpGame()
        {

        }

        static void PlayGame()
        {

        }

        static void Admin()
        {

        }

        static void AddWord()
        {

        }

        static void DeleteWord()
        {

        }

        static void ViewWords()
        {
            //string words = ReadWordBank();
        }

        static void MakeWordBank(string path)
        {
            try
            {
                File.Delete(path);

                using (StreamWriter streamWriter = new StreamWriter(path))
                {
                    streamWriter.WriteLine("baby");
                    streamWriter.WriteLine("door");
                    streamWriter.WriteLine("banana");
                    streamWriter.WriteLine("finger");
                    streamWriter.WriteLine("fence");
                    streamWriter.WriteLine("big");
                    streamWriter.WriteLine("swimming");
                    streamWriter.WriteLine("pool");
                    streamWriter.WriteLine("sun");
                    streamWriter.WriteLine("church");
                    streamWriter.WriteLine("boy");
                    streamWriter.WriteLine("bag");
                }
                Console.WriteLine("Successfully created word bank file.");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    /*
     * FUNCTIONAL FLOW
     * Menu
         * New Game
            * access word bank
            * read word bank
            * Choose word
            * Build array of chars from word
            * Display blanks
            * loop:
                * read guess
                * log guess
                * check for guess in word
                * (Y) display letter in blanks
                    * check for word finished
                    * (Y) congratulate
                    * return to Menu
                * feedback
            * edge cases:
            * enter num, special char, blank, enter, esc
            * duplicate entries
     *  Admin
        *  view words
            *  access word bank
            *  read word bank lines
            *  print words (lines) to console
        *  add word
            *  read admin input
                *  validity check
            *  access word bank
            *  append to word bank
        *  delete word
            *  access word bank
            *  read word bank
            *  replace word to delete with end word
            *  replace end word to blank
            *  delete file
            *  create new file
            *  write modified array to new file
     *  Exit
     * 
     * ARCHITECTURE
     * 
    */

    /*
    Josie Cat has requested that a “Word Guess Game” be built.The main idea of the game is she must guess what a mystery word is by inputting (1) letter at a time.The game should save all of her guesses (both correct and incorrect) throughout each session of the game, along with the ability to show her how many letters out of the word she has guessed correctly.

    Each time a new game session starts, the mystery word chosen should come from an external text file that randomly selects one of the words listed.This bank of words should be editable by Josie so that she may view, add, and delete words as she wishes.She expects the game to have a simple user interface that is easy to navigate.

    Using everything you’ve learned up to this point, create a word guess game that will meet all of the requirements described in the user story above.


    Program Components
    The program (should) contain the following:
    Methods for each action (suggestions: Home navigation, View words in the external file, add a word to the external file, Remove words from a text file, exit the game, start a new game)
    When playing a game, randomly select one of the words to output to the console for the user to guess(Use the Random class)
    You should have a record of the letters they have attempted so far
    If they guess a correct letter, display that letter in the console for them to refer back to when making guesses(i.e.C _ T S)
    Your program does not need to be case sensitive.
    Errors should be handled through Exception handling
    Do not create external classes to accomplish this task.All code should live in the Program.cs file
    Stay within scope, you may use the methods/classes listed below if desired.
    Once the game is completed, the user should be presented with the option to “Play again” (a new random word is generated), or “Exit” (the program terminates)
    the user should only be allowed to guess only 1 letter at a time.Do not make it so that they can input the whole alphabet and get the answer.
    */

}
