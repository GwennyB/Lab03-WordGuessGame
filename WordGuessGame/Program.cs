using System;
using System.IO;
using System.Linq;

namespace WordGuessGame
{
    public class Program
    {
        /// <summary>
        /// Initializes the word bank with standard set of words.
        /// Launches Main Menu and keeps it running until user quits.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // set path for external word bank file
            string path = ("../../../wordBank.txt");
            // initial set up of external word bank
            string[] words = { "baby", "door", "banana", "finger", "fence", "big", "swimming", "pool", "sun", "church", "boy", "bag" }; // seed file contents
            OverwriteWordBank(path, words);

            bool runMainMenu = true;
            MainMenu(runMainMenu, path);
            while (runMainMenu)
            {
                runMainMenu = MainMenu(runMainMenu, path);
            }
        }

        /// <summary>
        /// Displays Main Menu.
        /// Accepts, validates, and routes user selections.
        /// </summary>
        /// <param name="runMainMenu"> switches to 'false' when user wants to end program </param>
        /// <param name="path"> path to external word bank file </param>
        /// <returns> win or lose </returns>
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
            catch (Exception)
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

        /// <summary>
        /// Launches new game.
        /// Chooses word from word bank and calls game to start.
        /// </summary>
        /// <param name="path"> path to external word bank file </param>
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

        /// <summary>
        /// Picks word from word bank at random.
        /// </summary>
        /// <param name="path"> path to external word bank file </param>
        /// <returns></returns>
        static string PickWord(string path)
        {
            Random random = new Random();
            string[] words = ReadWordBank(path);
            int selectedIndex = random.Next(words.Length);
            return words[selectedIndex];
        }

        /// <summary>
        /// Sets up and displays game board at game start and after each guess
        /// </summary>
        /// <param name="spaces"> state array - holds blanks and all correct guesses - used here to print board each round </param>
        static void PrintGameBoard(char[] spaces)
        {
            Console.WriteLine("\n\n");
            Console.Write("     ");
            for (int i = 0; i < spaces.Length; i++)
            {
                if (spaces[i] == '\0')
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

        /// <summary>
        /// Manages game state and controls flow of game play.
        /// Interacts with user during game play.
        /// Determines end of game, keeps track of guesses, oversees input validation and guess checking, deals with input acceptions.
        /// </summary>
        /// <param name="letters"> array holding letters of word in play </param>
        /// <param name="spaces"> state array - holds blanks and all correct guesses - used here to capture state changes </param>
        /// <returns> game won or lost </returns>
        static bool PlayGame(char[] letters, char[] spaces)
        {
            Console.WriteLine("Let's play!\n\n\n\n");
            bool wonGame = false;

            PrintGameBoard(spaces);

            string allGuesses = "";
            string guess = "";

            while (spaces.Contains('\0'))
            {
                Console.WriteLine("Choose a letter and press ENTER (or press ENTER to exit game):");
                try
                {
                    guess = Console.ReadLine();
                }
                catch (Exception)
                {
                    Console.WriteLine("\n\n  NOTE: An exception occurred, but it's been caught. Please continue.\n\n");
                }
                if (guess == "")
                {
                    return wonGame;
                }
                if (ValidateNewWord(guess))
                {
                    allGuesses += guess + ", ";
                    char charGuess = guess.ToLower().ToCharArray()[0];
                    if (CheckGuess(letters, charGuess))
                    {
                        for (int i = 0; i < letters.Length; i++)
                        {
                            if (letters[i] == charGuess)
                            {
                                spaces[i] = letters[i];
                                letters[i] = '_';
                            }
                        }
                        if (spaces.Contains('\0'))
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
            }
            Console.WriteLine($"Your guesses: {allGuesses}");
            return wonGame;
        }

        /// <summary>
        /// Checks user's current guess against the letters that haven't been guessed yet
        /// </summary>
        /// <param name="letters"> array holding letters of word in play </param>
        /// <param name="charGuess"> char representation of user's current guess </param>
        /// <returns> guess is correct (true) or incorrect (false) </returns>
        public static bool CheckGuess(char[] letters, char charGuess)
        {
            return letters.Contains(charGuess);
        }

        /// <summary>
        /// Reads the current contents of the external word bank
        /// </summary>
        /// <param name="path"> path to external word bank file </param>
        /// <returns> list of words in the word bank </returns>
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

        /// <summary>
        /// Displays admin menu
        /// Accepts, validates, and routes user selections.
        /// </summary>
        /// <param name="path"> path to external word bank file </param>
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
                    Console.WriteLine("Please enter the word to add: ");
                    string addWord = Console.ReadLine();
                    AddWord(path, addWord);
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

        /// <summary>
        /// Adds a word to the external word bank at user's direction (if word satisfies validation)
        /// </summary>
        /// <param name="path"> path to external word bank file </param>
        /// <param name="addWord"> the word that the user wants to add </param>
        /// <returns> word added (true) or not added (false) </returns>
        public static bool AddWord(string path, string addWord)
        {
            if (ValidateNewWord(addWord))
            {
                string[] newWord = { addWord.ToLower() };
                File.AppendAllLines(path, newWord);
                Console.WriteLine($"\nAdded {addWord} to the word bank.");
                return true;
            }
            else
            {
                Console.WriteLine("Sorry, no special characters or numerals allowed.");
                return false;
            }
        }

        /// <summary>
        /// Validates a word that the user wants to add to the word bank
        /// </summary>
        /// <param name="addWord"> the word that the user wants to add </param>
        /// <returns> valid (true) or invalid (false) </returns>
        static bool ValidateNewWord(string addWord)
        {
            char[] notAllowed = { ' ', ',', '.', ':', '\t',';','!','@','#','$','%','^','&'
            ,'*','(',')','-','_','/','|','[',']','{','}','<','>','?','1','2','3','4','5','6','7','8','9','0'};

            foreach (char badChar in notAllowed)
            {
                if (addWord.Contains(badChar))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Deletes a word from the external word bank (if contained)
        /// Method: Reads file lines into array; overwrites element containing target word with last element; replaces last element with empty string; rebuilds the file with modified array
        /// </summary>
        /// <param name="path"> path to external word bank file </param>
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

        /// <summary>
        /// Displays all the words in the word bank
        /// </summary>
        /// <param name="path"> path to external word bank file </param>
        /// <returns> array of words from the word bank (for test only) </returns>
        public static string[] ViewWords(string path)
        {
            string[] words = ReadWordBank(path);

            Console.WriteLine("\nThese words are available in the word bank:");
            foreach (string word in words)
            {
                Console.WriteLine($"{word}");
            }
            return words;
        }

        /// <summary>
        /// Overwrites current word bank with new list of words
        /// </summary>
        /// <param name="path"> path to external word bank file </param>
        /// <param name="words"> words to write to the word bank </param>
        /// <returns> number of lines in the word bank after overwrite </returns>
        public static int OverwriteWordBank(string path, string[] words)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(path))
                {
                    foreach (string word in words)
                    {
                        if (word != null)
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
            string[] linesInNewFile = ReadWordBank(path);
            return linesInNewFile.Length;

        }
    }
}