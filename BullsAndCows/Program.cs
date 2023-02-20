using System;
using System.Threading;
/*
Info: This is a console game "Bulls & Cows" which can be executed and played with the use of C# .NET 5 compiler. 
The game has simple rules enough to quickly understand the main thing of it, take part in several rounds and win.
Discipline: "Programming".
Group: Anonymous.
Student: Anonymous Anonym.
Deadline date: Tuesday 21st September, 23:59.
*/
namespace BullsAndCows
{
    /*
        The main Program class.
        Contains all methods for performing basic game "Bulls & Cows".
    */
    class Program
    {
        /// <summary>
        /// Checks all digits of user's number on their uniqueness.
        /// </summary>
        /// <param name="localUserNumber">User's number.</param>
        /// <returns>
        /// Bool value of check (true if all digits in number are unique, false otherwise).
        /// </returns>
        public static bool CheckUniqueNumbers(string localUserNumber)
        {
            // Declaration of variables.
            var check = true;
            var checkString = "";
            
            // "For" loop which checks whether all digits in number are unique.
            for (int i = 0; i < localUserNumber.Length; i++)
            {
                if (checkString.Contains(localUserNumber[i]))
                {
                    check = false;
                    break;
                }

                checkString += localUserNumber[i];
            }

            return check;
        }

        /// <summary>
        /// Gets user name by asking them to enter it from the keyboard until it does not have correct format.
        /// </summary>
        /// <returns>
        /// User's name in the following format: uppercase first letter and lowercase another part of the name.
        /// </returns>
        public static string GetUserName()
        {
            // Declaration of variables.
            var check = false;
            string userNameLocal = Console.ReadLine();
            
            // Left-right borders of Ascii lower and upper case letters.
            const uint leftLowerBorder = 65; const uint rightLowerBorder = 90;
            const uint leftUpperBorder = 97; const uint rightUpperBorder = 122;
            
            // Declaration of while loop which works until user's name is not correct.
            while (check != true | string.IsNullOrEmpty(userNameLocal))
            {
                check = true;
                
                for (int i = 0; i < userNameLocal.Length; i++)
                {
                    if (((char)userNameLocal[i] >= leftLowerBorder & (char)userNameLocal[i] <= rightLowerBorder) |
                          ((char)userNameLocal[i] >= leftUpperBorder & (char)userNameLocal[i] <= rightUpperBorder))
                    {
                        continue;
                    }
                    else
                    {
                        check = false;
                        break;
                    }
                }

                // Message about incorrectness of user's input and request about repetition of action. 
                if (check == false | string.IsNullOrEmpty(userNameLocal))
                {
                    Console.Write("Hm... Looks like incorrect input. Try again!\nEnter your name: ");
                    userNameLocal = Console.ReadLine();
                }
            }

            return (userNameLocal[0].ToString().ToUpper() + userNameLocal[1..].ToLower());
        }
        
        /// <summary>
        /// Greets user and tells to enter their name.
        /// </summary>
        public static void UserGreeting1()
        {
            Console.Write("Hello! I'm really happy to see you in game BULLS & COWS!\n" +
                          "Before I tell you the rules of the game let you introduce yourself.\nSo, " +
                          "enter your name (it should only have lower or uppercase letters from English alphabet): ");
        }
        
        /// <summary>
        /// Greets user and tells the rules of the game "Bulls & Cows".
        /// </summary>
        /// <param name="userName">User's name.</param>
        public static void UserGreeting2(string userName)
        {
            Console.WriteLine($"\nNice to acquaint with you, {userName}! It's time to know the rules " +
                              "of the game. They are the following:\n" +
                              "1) you enter an integer number - the length of number you're going to guess;\n" +
                              "2) computer creates that number and asks you to enter it from the keyboard;\n" +
                              "3) you enter the number and see the result:\n" +
                              "         \"B (bull)\" - you guessed the digit and its' position;\n" +
                              "         \"C (cow)\" - you guessed the digit but didn't guess its' position;\n" +
                              "         \"0 (miss)\" - you didn't guess digit at all, it is not in number;\n" +
                              "4) also you should remember that every digit can't be used in number more than " +
                              "once;\n" +
                              "5) number can't begin with \"0\";\n" +
                              "6) you guess number until all digits wouldn't be the same as in secret number.\n" +
                              "I think that now rules are clear and suggest you to start the game! Good luck!\n");
        }
        
        /// <summary>
        /// A button user has to press and activate the game.
        /// </summary>
        public static void GameActivation()
        {
            // Declaration of variable "keyToStart" which activates the game.
            ConsoleKeyInfo keyToStart;
            
            // Post-condition loop which asks user to press "ENTER" button on keyboard to start the game 
            // until they do not press it.
            do
            {
                Console.WriteLine("Press ENTER to start game");
                keyToStart = Console.ReadKey();
                Console.WriteLine();
                
                // Message about incorrectness of user's input and request about repetition of action.
                if (keyToStart.Key != ConsoleKey.Enter)
                    Console.WriteLine("Hm... Incorrect button pressed. Try again!");
            } while (keyToStart.Key != ConsoleKey.Enter); 
        }
        
        /// <summary>
        /// Asks to enter length of number user is going to guess and returns it.
        /// </summary>
        /// <returns>
        /// Length of number user will guess.
        /// </returns>
        public static int LengthOfNumber()
        {
            // Declaration of variables.
            int numberLength;
            string numberLengthString;
            
            // Post-condition loop which asks user to enter length of number until it does not have correct format.
            do
            {
                Console.Write("Enter the length of number (1 - 10) you're going to guess: ");
                numberLengthString = Console.ReadLine();
                
                // Message about incorrectness of user's input and request about repetition of action.
                if (!int.TryParse(numberLengthString, out numberLength) | numberLength < 1 | numberLength > 10 |
                    numberLengthString.StartsWith("0"))
                {
                    Console.WriteLine("Hm... Looks like incorrect input. Try again!");
                }
            } while (!int.TryParse(numberLengthString, out numberLength) | numberLength < 1 | numberLength > 10 |
                     numberLengthString.StartsWith("0"));

            return numberLength;
        }
        
        /// <summary>
        /// Creates secret number consisting of unique digits using methods of Random class and returns its'
        /// string representation.
        /// </summary>
        /// <param name="length">Length of secret number entered by user.</param>
        /// <returns>
        /// String representation of secret number.
        /// </returns>
        public static string CreateSecretNumber(int length)
        {
            // Instantiate random number generator using system-supplied value as seed.
            var rand = new Random();
            
            // Creates and initializes a new integer array consisting of all decimal digits.
            var digitsArray = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            
            // Makes slice of integer array consisting of digits from 1 to 9.
            var subArray = digitsArray[1..];
            
            // Pre-condition loop which creates secret number with assigned length by user and unique digits.
            var number = "";
            while (number.Length != length)
            {
                var randomNumber = rand.Next(digitsArray.Length);
                var subRandomNumber = rand.Next(subArray.Length);
                
                if (number.Length == 0)
                    number += subArray[subRandomNumber].ToString();
                else
                {
                    var secretDigit = digitsArray[randomNumber].ToString();
                    if (!number.Contains(secretDigit))
                        number += secretDigit;
                }
            }

            return number;
        }
        
        /// <summary>
        /// Displays creation of secret number.
        /// </summary>
        public static void CreationProcess()
        {
            Console.WriteLine("The computer is creating a number:");
            
            // "For" loop which displays left seconds till the end of secret number creation.
            for (int i = 1000; i < 3001; i += 1000)
            {
                Thread.Sleep(1000);
                Console.WriteLine($"{4 - (i / 1000)} seconds left...");
            }
            Thread.Sleep(1000);
            
            Console.WriteLine($"Secret number is created!");
        }
        
        /// <summary>
        /// Compares a number from user and secret number composed by computer and returns answer according to the
        /// rules of the "Bulls & Cows" game.
        /// </summary>
        /// <param name="userNum">User's number.</param>
        /// <param name="secretNum">Secret number composed by computer.</param>
        /// <returns>
        /// Amount of bulls and cows and result string according to the rules of the game.
        /// </returns>
        public static string Comparison(long userNum, long secretNum)
        {
            // Declaration of variables.
            var userNumberString = userNum.ToString();
            var secretNumberString = secretNum.ToString();
            var resultString = "";
            var countBulls = 0; var countCows = 0;
            
            // "For" loop which compares two string representation of numbers and forms result string.
            for (int i = 0; i < userNumberString.Length; i++)
            {
                if (userNumberString[i] == secretNumberString[i])
                {
                    resultString += "B";
                    countBulls++;
                }
                else if (secretNumberString.Contains(userNumberString[i]))
                {
                    resultString += "C";
                    countCows++;
                }
                else
                    resultString += "0";
            }
            
            return $"{countBulls} Bull, {countCows} Cow = {resultString}";
        }
        
        /// <summary>
        /// Asks user to guess secret number until they will not give the correct answer.
        /// </summary>
        /// <param name="secretNumber">Secret number composed by computer.</param>
        /// <param name="numberLength">Length of secret number.</param>
        public static void GuessNumber(long secretNumber, int numberLength)
        {
            // Declaration of number which user enters every time until they don't guess it.
            long userNumber;
            
            // Post-condition loop which continues until secret number and user's number are not the same.
            do
            {
                string userNumberString;
                
                // Post-condition loop which works until user's number does not have correct format of input.
                do
                {
                    Console.Write("Enter secret number by your assumption: ");
                    userNumberString = Console.ReadLine();
                    
                    // Message about incorrectness of user's input and request about repetition of action.
                    if (!long.TryParse(userNumberString, out userNumber) | userNumber <= 0 |
                        userNumber.ToString().Length != numberLength | userNumberString.StartsWith("0") |
                        !CheckUniqueNumbers(userNumberString))
                    {
                        Console.WriteLine("Hm... Looks like incorrect input. Try again!");
                    }
                } while (!long.TryParse(userNumberString, out userNumber) | userNumber < 0 |
                         userNumber.ToString().Length != numberLength | userNumberString.StartsWith("0") |
                         !CheckUniqueNumbers(userNumberString));

                // Output with result of comparison of user's number and secret number.
                Console.WriteLine($"{userNumber} - the result you entered.");
                Console.WriteLine($"{Comparison(userNumber, secretNumber)} - the result you got.");
            } while (userNumber != secretNumber);
        }
        
        /// <summary>
        /// Executes the game after "ENTER" button was pressed and until it is not stopped by user. 
        /// </summary>
        public static void GameProcess()
        {
            // Executes LengthOfNumber method.
            var numberLength = LengthOfNumber();
            
            // Creates secret number which user has to guess and executes CreationProcess method.
            long secretNumber;
            long.TryParse(CreateSecretNumber(numberLength), out secretNumber);
            CreationProcess();
                
            // Processes the guessing of number by executing GuessNumber method.
            GuessNumber(secretNumber, numberLength);
        }
        
        /// <summary>
        /// Ends game if user does not press "ENTER" - the button activating new game round.
        /// </summary>
        /// <param name="userName">User's name.</param>
        public static void GameEnd(string userName)
        {
            // Declaration of variable "keyToExit" which exits the game if "ENTER" is not pressed and "flag" variable.
            ConsoleKeyInfo keyToExit;
            var flag = false;
            
            // Post-condition loop which executes game and repeats it until user presses "ENTER" button.
            do
            {
                // Executes game round if it is first round or if user pressed "ENTER" button.
                if (flag == false)
                {
                    GameProcess();
                    Console.WriteLine($"{userName}, you've done a great job and told the correct number!");
                }

                // Repetition of game or quitting from it or message about incorrectness of pressed button.
                Console.WriteLine("To play again press \"ENTER\".\nTo quit press \"BACKSPACE\".");
                keyToExit = Console.ReadKey();
                Console.WriteLine();
                
                if (keyToExit.Key == ConsoleKey.Enter)
                {
                    flag = false;
                    continue;
                }
                else if (keyToExit.Key == ConsoleKey.Backspace)
                    break;
                else
                {
                    flag = true;
                    Console.WriteLine("Hm... Incorrect button pressed. Try again!");
                    continue;
                }
            } while (keyToExit.Key != ConsoleKey.Backspace);
        }
        
        /// <summary>
        /// Farewells with user.
        /// </summary>
        /// <param name="userName">User's name.</param>
        public static void UserFarewell(string userName)
        {
            Console.WriteLine($"{userName}, I'm sure you enjoyed the game!\nThank you for playing with me! " +
                              $"I'm looking forward meeting you here again.\nSee you soon, bye!");
        }
        
        /// <summary>
        /// Main method entry point (executes whole game process).
        /// </summary>
        public static void Main()
        {
            // Getting user's name and greeting them.
            UserGreeting1();
            var userName = GetUserName();
            UserGreeting2(userName);
            
            // Activating game.
            GameActivation();
            
            // Processing game and quitting from it.
            GameEnd(userName);
            
            // Farewell with user.
            UserFarewell(userName);
        }
    }
}