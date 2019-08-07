using System;

namespace ConsoleMastermindGame.Game
{
    public static class Mastermind
    {
        const int MAX_NUMBER = 6666;
        const int MIN_NUMBER = 1111;
        const int INT_SIX = 6;
        const int INT_ONE = 1;
        const int INT_TEN = 10;

        public static void InitGame()
        {
            while (true)
            {
                DisplayGreetingMessage();

                bool[] guessArr = { false, false, false, false };
                bool[] ansArr = { false, false, false, false };
                bool correctGuess = false;
                Console.Clear();

                int secretNumber = GetSecretNumber();

                for (int i = 0; i < 10; i++)
                {
                    string userInput = GetPlayerEntry(i);
                    if (ValidateInput(ref userInput))
                    {
                        int inputNumber = Int16.Parse(userInput);
                        
                        if (inputNumber == secretNumber)
                        {
                            correctGuess = true;
                            DisplayWinningMessage();
                            break;
                        }
                        int correctPlaceCnt = GetCorrectPlacedDigitCount(inputNumber, guessArr, ansArr, secretNumber);
                        int inCorrectPlaceCnt = GetInCorrectPlacedDigitCount(inputNumber, guessArr, ansArr, secretNumber);
                        PrintTurnFeedback(correctPlaceCnt, inCorrectPlaceCnt);
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input. Make sure the number is between 1111 and 6666");
                    }
                }
                if(!correctGuess)
                {
                    Console.WriteLine("\nBad luck.Better luck next time :(\n");
                    Console.WriteLine("The code was " + secretNumber);
                }
                if (PlayAgain())
                {
                    Console.Clear();
                    continue;
                }
                break;
            }


        }

        public static void DisplayWinningMessage()
        {
            Console.WriteLine("--------------------\n");
            Console.WriteLine("\nYou rock.You solved it !");
        }
        public static void DisplayGreetingMessage()
        {
            Console.WriteLine("Lets play Mastermind.");
            System.Threading.Thread.Sleep(2000);
        }
        public static int GetSecretNumber()
        {
            string strNum = "";
            Random srand = new Random();
            for (int i = 0; i < 4; i++)
            {
                strNum = strNum.Insert(strNum.Length, srand.Next(INT_ONE, INT_SIX).ToString());
            }
            return Int32.Parse(strNum);
        }
        public static string GetPlayerEntry(int turn)
        {
            Console.WriteLine("Guesses Remaining: " + (INT_TEN - turn));
            Console.WriteLine("Make your guess at " + (turn + INT_ONE) + " turn");
            return (Console.ReadLine());
        }
        private static bool ValidateInput(ref string userInput)
        {
            int intUserGuess = 0;
            try
            {
                intUserGuess = Int32.Parse(userInput);
                int guessDigit = 0;
                int tempGuess = intUserGuess;
                for (int i = 0; i < 4; i++)
                {
                    guessDigit = tempGuess % 10;
                    if (guessDigit > INT_SIX || guessDigit < INT_ONE || intUserGuess < MIN_NUMBER || intUserGuess > MAX_NUMBER)
                    {
                        Console.WriteLine("\nMake sure your input is between 1111 and 6666, with each digit being no larger that 6.");
                        System.Threading.Thread.Sleep(2000);
                        Console.WriteLine("\nMake your guess:\n");
                        userInput = Console.ReadLine();
                        if (ValidateInput(ref userInput))
                            return true;
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private static int GetCorrectPlacedDigitCount(int intUserGuess, bool[] guessArr, bool[] answerArr, int intSecretCode)
        {
            for (int i = 0; i < 4; i++)
            {
                guessArr[i] = false;
                answerArr[i] = false;
            }

            int inPlaceCount = 0;
            int guessDigit = 0;
            int randDigit = 0;
            int tempGuess = intUserGuess;
            int tempRand = intSecretCode;

            for (int i = 0; i < 4; i++)
            {
                guessDigit = tempGuess % 10;
                tempGuess = tempGuess / 10;
                randDigit = tempRand % 10;
                tempRand = tempRand / 10;

                if (guessDigit == randDigit)
                {
                    guessArr[i] = true;
                    answerArr[i] = true;
                    inPlaceCount++;
                }
            }
            return inPlaceCount;
        }

        public static int GetInCorrectPlacedDigitCount(int userGuess, bool[] guessArr, bool[] answerArr, int intSecretCode)
        {
            int outOfPlaceCount = 0;
            int guessDigit;
            int randDigit;
            int tempRand = intSecretCode;

            for (int i = 0; i < 4; i++)
            {
                guessDigit = userGuess % 10;
                userGuess = userGuess / 10;
                randDigit = tempRand % 10;
                tempRand = intSecretCode;
                if (guessArr[i] == false)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        randDigit = tempRand % 10;
                        tempRand = tempRand / 10;
                        if (answerArr[j] == false)
                        {
                            if (guessDigit == randDigit)
                            {
                                outOfPlaceCount++;
                                guessArr[i] = true;
                                answerArr[j] = true;
                                break;
                            }
                        }
                    }
                }
            }
            return outOfPlaceCount;
        }

        public static void PrintTurnFeedback(int corectPlaceCount, int incorrectPlaceCount)
        {
            string feedback = "\n Score is: ";
            //Switch statement builds feedback string.
            #region Switch statement w/cases
            switch (corectPlaceCount)
            {
                case 0:
                    break;
                case 1:
                    feedback += "+";
                    break;
                case 2:
                    feedback += "++";
                    break;
                case 3:
                    feedback += "+++";
                    break;
            }
            switch (incorrectPlaceCount)
            {
                case 0:
                    break;
                case 1:
                    feedback += "-";
                    break;
                case 2:
                    feedback += "--";
                    break;
                case 3:
                    feedback += "---";
                    break;
                case 4:
                    feedback += "----";
                    break;
            }
            #endregion

            Console.WriteLine(feedback + "\n");
            Console.WriteLine("--------------------\n");

        }

        private static bool PlayAgain()
        {
            Console.WriteLine("\nWould you like to play again? (Y/N)\n");
            while (true)
            {
                string strPlayAgain = Console.ReadLine();
                if (strPlayAgain == "N" || strPlayAgain == "n" || strPlayAgain == "No" || strPlayAgain == "no")
                {
                    return false;
                }
                else if (strPlayAgain == "Y" || strPlayAgain == "y" || strPlayAgain == "Yes" || strPlayAgain == "yes")
                {
                    return true;
                }
                Console.WriteLine("\nPlease enter either a Y or a N.\n");
            }
        }
    }
}
