using System;
using System.Collections.Generic;

namespace BlackJackCS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Blackjack!");

            bool continuePlaying = true;

            // Main game loop
            while (continuePlaying)
            {
                // Create starting deck
                List<Card> deck = generateListDeck();

                // Shuffle Deck and create a stack for playing
                Stack<Card> playingDeck = shuffleDeck(deck);

                // Deal hands for player and dealer 
                List<Card> playerHand = new List<Card>() { };
                List<Card> dealerHand = new List<Card>() { };
                int playerHandValue = 0;
                int dealerHandValue = 0;

                playerHand.Insert(0, playingDeck.Pop());
                dealerHand.Insert(0, playingDeck.Pop());
                playerHand.Insert(0, playingDeck.Pop());
                //dealerHand.Insert(0, playingDeck.Pop());

                // Show player their cards
                Console.WriteLine("Your hand: ");
                foreach (Card card in playerHand)
                {
                    Console.WriteLine($"{card.Name} : {card.Value}");
                    playerHandValue += card.Value;
                }
                Console.WriteLine($"Total hand value: {playerHandValue}");

                // Show dealer's first card
                Console.WriteLine($"The dealer's card is : {dealerHand[0].Name} with value {dealerHand[0].Value}");
                dealerHandValue += dealerHand[0].Value;

                // BEGIN LOOP
                // Ask player for "Hit" or "Stand" until "Stand" or bust (total hand value is > 21)
                string playerResponse = "";
                while (!(playerHandValue > 21 || playerResponse == "stand"))
                {
                    Console.WriteLine("Would you like to hit or stand?");
                    playerResponse = Console.ReadLine();

                    if (playerResponse == "hit")
                    {
                        Card dealtCard = playingDeck.Pop();
                        playerHand.Insert(playerHand.Count, playingDeck.Pop());
                        playerHandValue += playerHand[playerHand.Count - 1].Value;
                        Console.WriteLine($"You were dealt a {dealtCard.Name} and your new total hand value is : {playerHandValue}");
                    }
                }

                // Calculate if the player busted
                if (playerHandValue > 21)
                {
                    Console.WriteLine("Uh-oh! Your hand value is over 21 and you've lost. Sucks for you!");
                    Console.WriteLine("Would you like to play again? y/n");
                    continuePlaying = (Console.ReadLine() == "y") ? true : false;
                    continue;
                }

                // Dealer reveals hand and keeps hitting until handValue >= 17
                while (!(dealerHandValue >= 17))
                {
                    Card dealtCard = playingDeck.Pop();
                    dealerHand.Insert(dealerHand.Count, dealtCard);
                    dealerHandValue += dealtCard.Value;
                    Console.WriteLine($"The dealer was dealt a {dealtCard.Name} and their new total hand value is : {dealerHandValue}");
                }

                // Calculate if dealer busts. If not, calculate winner
                if (dealerHandValue > 21)
                {
                    Console.WriteLine("Success! The dealer hand value is over 21 and you've won. Hurray!");
                    Console.WriteLine("Would you like to play again? y/n");
                    continuePlaying = (Console.ReadLine() == "y") ? true : false;
                    continue;
                }

                // Calculate winner if dealer hasn't busted
                // in the event of a tie, dealer wins
                if (playerHandValue > dealerHandValue)
                {
                    Console.WriteLine("Success! You've won. Hurray!");
                }
                else
                {
                    Console.WriteLine("Uh-oh! You've lost. Sucks for you!");
                }

                // Ask user if they want to play again, and set the continuePlaying variable to the response
                Console.WriteLine("Would you like to play again? y/n");
                continuePlaying = (Console.ReadLine() == "y") ? true : false;
            }
        }
        // method called shuffleDeck which takes a list of cards deck and outputs
        // a stack of cards deck
        public static Stack<Card> shuffleDeck(List<Card> deck)
        {
            var randomNumberGenerator = new Random();
            int leftIndex;
            Card leftCard;
            Card rightCard;

            for (int rightIndex = deck.Count - 1; rightIndex > 0; rightIndex--)
            {
                leftIndex = randomNumberGenerator.Next(rightIndex + 1);

                // save cards in variables so we don't lose them!
                leftCard = deck[leftIndex];
                rightCard = deck[rightIndex];

                // swap
                deck[leftIndex] = rightCard;
                deck[rightIndex] = leftCard;
            }

            return new Stack<Card>(deck);
        }

        public static List<Card> generateListDeck()
        {
            List<Card> deck = new List<Card>() { };
            var suits = new List<string>() { "Clubs", "Diamonds", "Hearts", "Spades" };
            var ranks = new List<string>() { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };
            var values = new List<int>() { 11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10 };

            for (int i = 0; i < ranks.Count; i++)
            {
                foreach (string suit in suits)
                {
                    deck.Insert(0, new Card(ranks[i] + " of " + suit, values[i]));
                }
            }

            return deck;
        }

    }

    class Card
    {
        public string Name { get; set; }
        public int Value { get; set; }

        // constructor
        public Card(string newName, int newValue)
        {
            Name = newName;
            Value = newValue;
        }
    }
}
