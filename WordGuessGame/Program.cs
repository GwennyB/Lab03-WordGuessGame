using System;
using System.IO;
using System.Linq;

namespace WordGuessGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            // set path for external word bank file
            string path = ("../../../wordBank.txt");
            // initial set up of external word bank
            string[] words = { "baby", "door", "banana", "finger", "fence", "big", "swimming", "pool", "sun", "church", "boy", "bag" }; // seed file contents
            OverwriteWordBank(path, words);

            bool runMainMenu = true;
            MainMenu(runMainMenu,path);
            while (runMainMenu)
            {
                runMainMenu = MainMenu(runMainMenu,path);
            }
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
            string selected = PickWord(path); // select word from word bank
            char[] letters = selected.ToCharArray(); // put lettes of word into an array
            char[] spaces = new Char[letters.Length]; // build array to hold game set


            Console.Clear();

            bool wonGame = PlayGame(letters, spaces);
            Console.WriteLine("(Press ENTER to return to Main Menu.)");
            Console.ReadLine();
        }

        static string PickWord(string path)
        {
            Random random = new Random();
            string[] words = ReadWordBank(path);
            int selectedIndex = random.Next(words.Length);
            return words[selectedIndex];
        }

        static void PrintGameBoard(char[] spaces)
        {
            Console.WriteLine("\n\n");
            Console.Write("     ");
            for (int i = 0; i < spaces.Length; i++)
            {
                if(spaces[i] == '\0')
                {
                    Console.Write(" ___ ");
                }
                else
                {
                    Console.Write($" {spaces[i]} ");
                }
            }
            Console.WriteLine("\n\n");
        }

        static bool PlayGame(char[] letters, char[] spaces)
        {
            Console.WriteLine("Let's play!\n\n\n\n");
            bool wonGame = false;
            string guess = "";
            string allGuesses = "";
            PrintGameBoard(spaces);

            while (spaces.Contains('\0'))
            {
                Console.WriteLine("Choose a letter and press ENTER (or press ENTER to exit game):");
                guess = Console.ReadLine();
                if(guess == "")
                {
                    return wonGame;
                }
                if(ValidateNewWord(guess))
                {
                    char charGuess = guess.ToLower().ToCharArray()[0];
                    if (letters.Contains(charGuess))
                    {
                        for (int i = 0; i < letters.Length; i++)
                        {
                            if (letters[i] == charGuess)
                            {
                                spaces[i] = letters[i];
                                letters[i] = '_';
                            }
                        }
                        if(spaces.Contains('\0'))
                        {
                            Console.Clear();
                            Console.WriteLine("Great guess! Choose another?");
                            PrintGameBoard(spaces);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Great guess! You won!");
                            wonGame = true;
                            PrintGameBoard(spaces);
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Aww...too bad. Try again.");
                        PrintGameBoard(spaces);
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid guess.  Try again.");
                    PrintGameBoard(spaces);
                }
                allGuesses += guess + ",";
            }
            return wonGame;
        }


        static string[] ReadWordBank(string path)
        {
            try
            {
                string[] lines = File.ReadAllLines(path);
                return lines;
            }
            catch (Exception)
            {
                Console.WriteLine($"ERROR: Could not read file at {path}");
                Console.ReadLine();
                throw;
            }
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
                    Console.Clear();
                    ViewWords(path);
                    Console.WriteLine("\nPress ENTER to return to Administrative Menu.");
                    Console.ReadLine();
                    Admin(path);
                    break;
                case "2": // add word
                    Console.Clear();
                    AddWord(path);
                    Admin(path);
                    break;
                case "3": // delete word
                    Console.Clear();
                    DeleteWord(path);
                    Admin(path);
                    break;
                case "4": // trash and rebuild word bank file
                    Console.Clear();
                    string[] words = { "baby", "door", "banana", "finger", "fence", "big", "swimming", "pool", "sun", "church", "boy", "bag" }; // seed file contents
                    OverwriteWordBank(path, words);
                    Console.WriteLine("\nWord bank rebuilt.  Press ENTER to return to the Administrative Menu.");
                    Console.ReadLine();
                    Admin(path);
                    break;
                case "5":
                    MainMenu(true, path); // return home
                    break;
                default: // none identified - all other cases excluded by 'while' conditions
                    break;
            }
        }

        static void AddWord(string path)
        {
            Console.WriteLine("Please enter the word to add: ");
            string addWord = Console.ReadLine();
            if (ValidateNewWord(addWord))
            {
                string[] newWord = { addWord.ToLower() };
                File.AppendAllLines(path, newWord);
                Console.WriteLine($"\nAdded {addWord} to the word bank.  Press ENTER to return to the Administrative Menu.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Sorry, no special characters or numerals allowed.");
                Console.ReadLine();
            }
        }

        static bool ValidateNewWord(string addWord)
        {
            char[] notAllowed = { ' ', ',', '.', ':', '\t',';','!','@','#','$','%','^','&'
            ,'*','(',')','-','_','/','|','[',']','{','}','<','>','?','1','2','3','4','5','6','7','8','9','0'};

            foreach (char badChar in notAllowed)
            {
                if(addWord.Contains(badChar))
                {
                    return false;
                }
            }
            return true;
        }

        static void DeleteWord(string path)
        {
            Console.WriteLine("The word bank contains these words:");
            string[] words = ViewWords(path);
            Console.WriteLine("\nWhich word would you like to delete?");
            string selected = Console.ReadLine();
            int indexToDelete = Array.IndexOf(words, selected.ToLower());
            if (indexToDelete == -1)
            {
                Console.Write($"{selected} isn't in the list.");
            }
            else
            {
                words[indexToDelete] = words[words.Length - 1];
                words[words.Length - 1] = null;
                OverwriteWordBank(path, words);
                Console.WriteLine($"\nDeleted {selected} from the word bank.  Press ENTER to return to the Administrative Menu.");
                Console.ReadLine();

            }
        }

        static string[] ViewWords(string path)
        {
            string[] words = ReadWordBank(path);

            Console.WriteLine("\nThese words are available in the word bank:");
            foreach (string word in words)
            {
                Console.WriteLine($"{word}");
            }
            return words;
        }

        static void OverwriteWordBank(string path, string[] words)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(path))
                {
                    foreach (string word in words)
                    {
                        if(word != null)
                        {
                            streamWriter.WriteLine(word);
                        }
                    }
                }
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
