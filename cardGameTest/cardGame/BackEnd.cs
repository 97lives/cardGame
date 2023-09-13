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
        /// Calculates the total score based on a list of playing cards.
        /// </summary>
        /// <param name="input">A string containing a list of playing cards separated by commas (e.g., "2C,5D,KH").</param>
        /// <returns>The total score calculated from the input cards.</returns>
        /// <exception cref="InvalidTriplicateJokerException">
        /// Thrown if there are three or more Jokers ("JR") in the input.
        /// </exception>
        /// <exception cref="InvalidDuplicateException">
        /// Thrown if there are duplicate cards in the input (excluding Jokers).
        /// </exception>
        /// <exception cref="InvalidCardRankException">
        /// Thrown if a card in the input has an invalid rank (not 2-9, T, J, Q, K, A).
        /// </exception>
        /// <exception cref="InvalidSuitException">
        /// Thrown if a card in the input has an invalid suit (not C, D, H, S).
        /// </exception>
        /// <exception cref="InvalidInputException">
        /// Thrown if the input format is invalid or contains unrecognized cards.
        /// </exception>
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
