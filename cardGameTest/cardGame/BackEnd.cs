using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cardGame
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException(string message) : base(message)
        {
        }
    }

    public class InvalidCardRankException : Exception
    {
        public InvalidCardRankException(char rank) : base($"Invalid card rank: {rank}")
        {
        }
    }

    public class InvalidSuitException : Exception
    {
        public InvalidSuitException(char suit) : base($"Invalid suit: {suit}")
        {
        }
    }

    public class InvalidDuplicateException : Exception
    {
        public InvalidDuplicateException() : base($"Invalid input: Duplicate cards detected.")
        {
        }
    }

    public class InvalidTriplicateJokerException : Exception
    {
        public InvalidTriplicateJokerException() : base($"Invalid input: Too many Jokers (JR).")
        {
        }
    }

    public class BackEnd
    {
        /// <summary>
        /// hello
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="InvalidTriplicateJokerException"></exception>
        /// <exception cref="InvalidDuplicateException"></exception>
        /// <exception cref="InvalidCardRankException"></exception>
        /// <exception cref="InvalidSuitException"></exception>
        /// <exception cref="InvalidInputException"></exception>
        public int CalculateTotalScore(string input)
        {
            Dictionary<char, int> cardValues = new Dictionary<char, int>
        {
            { '2', 2 },
            { '3', 3 },
            { '4', 4 },
            { '5', 5 },
            { '6', 6 },
            { '7', 7 },
            { '8', 8 },
            { '9', 9 },
            { 'T', 10 },
            { 'J', 11 },
            { 'Q', 12 },
            { 'K', 13 },
            { 'A', 14 }
        };

            int totalScore = 0;

            string[] cards = input.Split(',');

            int jokerCount = cards.Count(card => card == "JR");

            if (jokerCount >= 3)
            {
                throw new InvalidTriplicateJokerException();
            }

            List<String> cardsWithNoJR = Array.FindAll(cards, card => card != "JR").ToList();

            if (cardsWithNoJR.Count != cardsWithNoJR.Distinct().Count())
            {
                throw new InvalidDuplicateException();
            }

            foreach (string card in cards)
            {
                if (card == "JR")
                {
                    totalScore += 0;
                }
                else if (card.Length == 2)
                {
                    char rank = card[0];
                    char suit = card[1];

                    if (!cardValues.ContainsKey(rank))
                    {
                        throw new InvalidCardRankException(rank);
                    }

                    int cardValue = cardValues[rank];
                    int multiplier = 0;

                    switch (suit)
                    {
                        case 'C':
                            multiplier = 1;
                            break;
                        case 'D':
                            multiplier = 2;
                            break;
                        case 'H':
                            multiplier = 3;
                            break;
                        case 'S':
                            multiplier = 4;
                            break;
                        default:
                            throw new InvalidSuitException(suit);
                    }

                    totalScore += cardValue * multiplier;
                }
                else
                {
                    throw new InvalidInputException($"Invalid card format: {card}");
                }
            }

            return totalScore;
        }
    }
}
