using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming_Project
{
    public class Card
    {
        public enum Suit { Hearts, Diamonds, Clubs, Spades };
        public enum FaceValue { Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace };

        private String[] faceValues = new String[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "j", "q", "k", "a" };
        private String[] suits = new String[] { "hearts", "diamonds", "clubs", "spades" };

        private FaceValue faceValue;
        private Suit suit;

        public Card()
        {
            this.suit = Suit.Spades;
            this.faceValue = FaceValue.Ace;
        }

        public Card(Suit theSuit, FaceValue theFaceValue)
        {
            this.suit = theSuit;
            this.faceValue = theFaceValue;
        }

        public String buildCardName()
        {
            return faceValues[(int)this.faceValue] + " of " + suits[(int)this.suit];
        }

        public String buildCardFileName()
        {
            return "150\\" + suits[(int)this.suit] + "-" + faceValues[(int)this.faceValue] + "-150.png";
        }

        public Suit getSuit()
        {
            return this.suit;
        }

        public FaceValue getFaceValue()
        {
            return this.faceValue;
        }

    }
}
