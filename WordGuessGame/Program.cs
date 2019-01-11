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
            MainMenu(runMainMenu,path);
            /*while (runMainMenu)
            {
                runMainMenu = MainMenu(runMainMenu);
            }*/
        }

        static bool MainMenu(bool runMainMenu, string path)
        {
            // receive and validate direction from user
            string selected = "";
            try
            {
                while (selected != "1" && selected != "2" && selected != "3")
                {
                    // display menu
                    Console.Clear();
                    Console.WriteLine("Please select an option: ");
                    Console.WriteLine("(1) Play New Game");
                    Console.WriteLine("(2) Administration Options");
                    Console.WriteLine("(3) Exit Program");
                    selected = Console.ReadLine();
                }
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
                    NewGame(path);
                    break;
                case "2":
                    Admin(path);
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

        static void NewGame(string path)
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

        static void PlayGame(string path)
        {

        }

        static void Admin(string path)
        {
            // receive and validate direction from user
            string selected = "";
            try
            {
                while (selected != "1" && selected != "2" && selected != "3" && selected != "4" && selected != "5")
                {
                    // display menu
                    Console.Clear();
                    Console.WriteLine("Please select an option: ");
                    Console.WriteLine("(1) View all available words in the word bank");
                    Console.WriteLine("(2) Add a new word to the word bank");
                    Console.WriteLine("(3) Delete a word from the word bank");
                    Console.WriteLine("(4) Rebuild the word bank");
                    Console.WriteLine("(5) Return to Main Menu");
                    selected = Console.ReadLine();
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"\nInvalid entry. Press ENTER to try again.");
                Console.ReadLine();
                throw;
            }

            // route to menu selection
            switch (selected)
            {
                case "1": // view all words
                    string[] words = ReadWordBank(path);
                    //TODO: Print words to console
                    break;
                case "2": // add word
                    Console.WriteLine("Please enter the word to add: ");
                    string addWord = Console.ReadLine();
                    // TODO: Add input validation and exception handling
                    AddWord(path,addWord);
                    break;
                case "3": // delete word
                    // TODO: Delete word logic
                    break;
                case "4": // trash and rebuild word bank file
                    MakeWordBank(path);
                    break;
                case "5":
                    MainMenu(true, path); // return home
                    break;
                default: // none identified - all other cases excluded by 'while' conditions
                    break;
            }





        }

        static void AddWord(string path, string addWord)
        {
            string[] newWord = { addWord };
            File.AppendAllLines(path, newWord);
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
