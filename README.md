# Lab03-WordGuessGame
CF 401 Lab 3 - Word Guessing Game (System.IO)

## Introduction to Word Guessing Game
This application is a game with both player and administrative interfaces. A player can choose to play a round, wherein (s)he guesses letters in a word of specified length (source: chosen at random from an external file of administrator chosen words), and (s)he is given immediate feedback on whether the letter is or isn't in the word. Play continues until the player guesses the word or exits. The administrator can view and modify the word list from the external file.

## Visuals
This sample session shows:
 - <enter stuff here>
 ![sample_session](assets/sample_session.PNG)

## How to use (*****UPDATE WITH MENU DETAILS*****)
The application automatically launches a console window upon compile/run, and a menu of options is presented. Interaction requires only input of selections (1, 2, 3, or 4). Selecting 4 (exit) or making an invalid selection will end the session and display a session receipt. Each transaction type contains transaction-specific prompts for user inputs (ex: deposit asks for deposit amount), and each transaction ends with transaction details (transaction type, amount, and ending balance). Attempting to make a negative deposit or a withdrawal in excess of account balance will result in an appropriate error message and no change to the account balance (failed transactions also included in session receipt).

## Other details (*****UPDATE WITH ANY 'OTHER' DETAILS*****)
Unit tests confirm that each transaction method produces correct transaction accounting and returns a transaction string to include in the final session receipt. Unit tests do not confirm input validation due to limitations on console input testing.
