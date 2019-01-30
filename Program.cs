using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming_Project
{
    class Program
    {
        const int MAX_CARDS = 5;
        static void Main(string[] args)
        {
            Console.WriteLine("---------------Poker Game---------------");
            int HandOptions = 99;
            int Choice = 99;
            bool playAgain = true;
            int betAmount = 0;
            Deck theDeck = new Deck();
            Card[] theCards = new Card[MAX_CARDS];
            int Funds = InitializeGame();
            while (Funds > 0 && playAgain == true)
            {
                betAmount = GetBet(ref Funds);
                theDeck.isEmpty();
                theDeck.shuffle();

                Console.WriteLine("Your cards are: ");
                for (int C = 0; C < MAX_CARDS; C++)
                {
                    theCards[C] = DealCard(theDeck);
                    Console.WriteLine("{0}) {1} of {2}.", C + 1, theCards[C].getFaceValue(), theCards[C].getSuit());
                }
                Discard(theDeck, theCards);
                Console.WriteLine("Your cards are: ");

                for (int C = 0; C < MAX_CARDS; C++)
                {
                    Console.WriteLine("{0}) {1} of {2}.", C + 1, theCards[C].getFaceValue(), theCards[C].getSuit());
                }


                SortCards(theCards);
                HandOptions = EvaluateCards(theCards);
                Console.WriteLine();
                WinOrNot(HandOptions, ref Funds, betAmount);
                if (Funds > 0)
                {
                    Choice = PlayAgain();
                    if (Choice == 0)
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }

            }
            Console.WriteLine("Thank you for playing!");

        }

        public static int InitializeGame()
        {
            int BankFunds = 1000;
            return BankFunds;
        }
        public static int GetBet(ref int Funds)
        {
            int BetAmount = 0;
            Console.WriteLine();
            Console.Write("Please enter the amount you wish to bet (1...{0}): ", Funds);
            while (Int32.TryParse(Console.ReadLine(), out BetAmount) == false || BetAmount > Funds || BetAmount <= 0)
            {
                Console.Write("Error! Number is invalid. Please try again: ");
            }
            Funds -= BetAmount;
            return BetAmount;
        }
        public static Card DealCard(Deck theDeck)
        {
            Card theCard = new Card();
            return theCard = theDeck.dealACard();
        }
        public static void Discard(Deck theDeck, Card[] CardArray)
        {
            bool Repeat = false;
            int CardChoice = 99;
            int Count = 0;
            int[] Choices = new int[MAX_CARDS - 1] { 0, 0, 0, 0 };
            while (Count < 4)
            {
                Repeat = false;
                bool isNumeric = true;
                Console.Write("Please enter the card that you want to discard(0 to stop): ");
                isNumeric = Int32.TryParse(Console.ReadLine(), out CardChoice);
                if (isNumeric == true && CardChoice == 0)
                {
                    break;
                }
                else if (isNumeric == true && CardChoice > 0 && CardChoice < 6)
                {
                    for (int C = 0; C < Choices.Length; C++)
                    {
                        if (Choices[C] == CardChoice)
                        {
                            Console.WriteLine("Error! Card has already been discarded...");
                            Repeat = true;
                            break;
                        }
                    }
                    if (Repeat == false)
                    {
                        Console.WriteLine("Card {0} has been discarded...", CardChoice);
                        Choices[Count] = CardChoice;
                        CardArray[CardChoice - 1] = theDeck.dealACard();
                        Count++;
                    }
                }
                else
                {
                    Console.WriteLine("Error! Number is invalid.");
                }
            }
        }



        public static void SortCards(Card[] TheCards)
        {
            Card temp = new Card();

            for (int write = 0; write < TheCards.Length; write++)
            {
                for (int sort = 0; sort < TheCards.Length - 1; sort++)
                {
                    if (TheCards[sort].getFaceValue() > TheCards[sort + 1].getFaceValue())
                    {
                        temp = TheCards[sort + 1];
                        TheCards[sort + 1] = TheCards[sort];
                        TheCards[sort] = temp;
                    }
                }
            }
        }
        public static int EvaluateCards(Card[] TheCards)
        {
            int HandPlayed = 0;
            int NumPairs = 0;

            bool ThreeFlag = false;
            bool PairFlag = false;
            bool aStraight = false;
            bool aFlush = false;
            bool highStraight = false;

            if ((int)TheCards[4].getFaceValue() == 12 && TheCards[4].getFaceValue() == TheCards[3].getFaceValue() + 1 &&
                TheCards[3].getFaceValue() == TheCards[2].getFaceValue() + 1 &&
                TheCards[2].getFaceValue() == TheCards[1].getFaceValue() + 1 &&
                TheCards[1].getFaceValue() == TheCards[0].getFaceValue() + 1)
            {
                highStraight = true;

            }
            if (TheCards[0].getSuit() == TheCards[1].getSuit() &&
TheCards[1].getSuit() == TheCards[2].getSuit() &&
TheCards[2].getSuit() == TheCards[3].getSuit() &&
TheCards[3].getSuit() == TheCards[4].getSuit())
            {
                aFlush = true;
            }
            if (highStraight == true && aFlush == true)
            {
                return HandPlayed = 9;
            }
            if (TheCards[4].getFaceValue() == TheCards[3].getFaceValue() + 1 &&
    TheCards[3].getFaceValue() == TheCards[2].getFaceValue() + 1 &&
    TheCards[2].getFaceValue() == TheCards[1].getFaceValue() + 1 &&
    TheCards[1].getFaceValue() == TheCards[0].getFaceValue() + 1)
            {
                aStraight = true;

            }
            if (aStraight == true && aFlush == true)
            {
                return HandPlayed = 8;
            }
            if (aFlush == true)
            {
                return HandPlayed = 5;
            }
            if (aStraight == true)
            {
                return HandPlayed = 4;
            }
            for (int C = 0; C < TheCards.Length - 3; C++)
            {
                if (TheCards[C].getFaceValue() == TheCards[C + 1].getFaceValue() && TheCards[C + 1].getFaceValue() == TheCards[C + 2].getFaceValue() && TheCards[C + 2].getFaceValue() == TheCards[C + 3].getFaceValue())
                {
                    return HandPlayed = 7;
                }
            }


            if ((TheCards[0].getFaceValue() == TheCards[1].getFaceValue() && TheCards[1].getFaceValue() == TheCards[2].getFaceValue()) && (TheCards[3].getFaceValue() == TheCards[4].getFaceValue()))
            {
                return HandPlayed = 6;
            }
            else if ((TheCards[0].getFaceValue() == TheCards[1].getFaceValue()) && (TheCards[2].getFaceValue() == TheCards[3].getFaceValue() && TheCards[3].getFaceValue() == TheCards[4].getFaceValue()))
            {
                return HandPlayed = 6;
            }
            for (int C = 0; C < TheCards.Length - 2; C++)
            {
                if (TheCards[C].getFaceValue() == TheCards[C + 1].getFaceValue() && TheCards[C].getFaceValue() == TheCards[C + 2].getFaceValue())
                {
                    ThreeFlag = true;
                    return HandPlayed = 3;
                }
            }
            for (int C = 0; C < TheCards.Length - 1; C++)
            {
                if (TheCards[C].getFaceValue() == TheCards[C + 1].getFaceValue())
                {
                    NumPairs++;
                    PairFlag = true;
                    if (NumPairs == 2)
                    {
                        HandPlayed = 2;
                        return HandPlayed;
                    }
                }
            }       
            for (int C = 0; C < TheCards.Length - 1; C++)
            {
                    if (NumPairs ==1 && TheCards[C].getFaceValue() == TheCards[C + 1].getFaceValue() && (int)TheCards[C].getFaceValue() >= 9)
                    {
                        return HandPlayed = 1;
                    }
                }

                return HandPlayed = 0;

            
        }
        public static void WinOrNot(int HandPlayed, ref int Bank, int Bet)
        {
            if (HandPlayed == 0)
            {
                Console.WriteLine("You lose");
                Console.WriteLine("You won: {0}", Bet * 0);
                Console.WriteLine("Your Bank Roll is now: {0}", Bank);
            }
            if (HandPlayed == 1)
            {
                Console.WriteLine("You have a pair!");
                Bank += Bet * 1;
                Console.WriteLine("You won: {0}", Bet * 1);
                Console.WriteLine("Your Bank Roll is now: {0}", Bank);
            }
            if (HandPlayed == 2)
            {
                Console.WriteLine("You have two pairs!");
                Bank += Bet * 2;
                Console.WriteLine("You won: {0}", Bet * 2);
                Console.WriteLine("Your Bank Roll is now: {0}", Bank);

            }
            if (HandPlayed == 3)
            {
                Console.WriteLine("You have Three-of-a-Kind!");
                Bank += Bet * 3;
                Console.WriteLine("You won: {0}", Bet * 3);
                Console.WriteLine("Your Bank Roll is now: {0}", Bank);
            }
            if (HandPlayed == 4)
            {
                Console.WriteLine("You have a Straight!");
                Bank += Bet * 4;
                Console.WriteLine("You won: {0}", Bet * 4);
                Console.WriteLine("Your Bank Roll is now: {0}", Bank);
            }
            if (HandPlayed == 5)
            {
                Console.WriteLine("You have a Flush!");
                Bank += Bet * 6;
                Console.WriteLine("You won: {0}", Bet * 6);
                Console.WriteLine("Your Bank Roll is now: {0}", Bank);
            }
            if (HandPlayed == 6)
            {
                Console.WriteLine("You have a Full House!");
                Bank += Bet * 9;
                Console.WriteLine("You won: {0}", Bet * 9);
                Console.WriteLine("Your Bank Roll is now: {0}", Bank);
            }
            if (HandPlayed == 7)
            {
                Console.WriteLine("You have a Four-of-a-Kind!");
                Bank += Bet * 25;
                Console.WriteLine("You won: {0}", Bet * 25);
                Console.WriteLine("Your Bank Roll is now: {0}", Bank);
            }
            if (HandPlayed == 8)
            {
                Console.WriteLine("You have a Straight Flush!");
                Bank += Bet * 50;
                Console.WriteLine("You won: {0}", Bet * 50);
                Console.WriteLine("Your Bank Roll is now: {0}", Bank);
            }
            if (HandPlayed == 9)
            {
                Console.WriteLine("You have a Royal Flush!");
                Bank += Bet *250;
                Console.WriteLine();
                Console.WriteLine("You won: {0}", Bet * 250);
                Console.WriteLine("Your Bank Roll is now: {0}", Bank);
            }
        }
        public static int PlayAgain()
        {
            int Choice = 0;
            Console.Write("Would you like to play again?(0 to quit)");
            while (Int32.TryParse(Console.ReadLine(), out Choice)==false || Choice < 0)
            {
                Console.Write("Error! Number is invalid. Please try again: ");
            }
            return Choice;
        }
    }
}
