using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming_Project
{
    class Deck
    {
        private Card[] deck = new Card[52];
        private int currentCard = 0;

        public Deck()
        {
            int current = 0;

            foreach (Card.Suit s in Enum.GetValues(typeof(Card.Suit)))
            {
                foreach (Card.FaceValue f in Enum.GetValues(typeof(Card.FaceValue)))
                {
                    deck[current++] = new Card(s, f);
                }
            }
        }

        public void shuffle()
        {
            Random random = new Random();
            currentCard = 0;
            for (int loop = 0; loop < 500; loop++)
            {
                int num1 = random.Next(52);
                int num2 = random.Next(52);

                Card temp = deck[num1];
                deck[num1] = deck[num2];
                deck[num2] = temp;
            }
        }


        // Pre-Condtion - the deck can not be empty.
        public Card dealACard()
        {
            return this.deck[currentCard++];
        }

        public int isEmpty()
        {
            if (currentCard > 51)
            {
                return currentCard = 0;
            }
            return -1; // program can't reach there
        }

    }
}