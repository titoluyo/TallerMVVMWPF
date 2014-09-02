using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFChallenge.Core
{
    /// <summary>
    /// A game dice that rolls a random number between 1 and the number of sides on the dice.    
    /// </summary>
    public class Dice : SFChallenge.Core.IDice
    {
        private Random random = new Random();
        private int numberOfSides = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="Dice"/> class.
        /// </summary>
        /// <param name="numberOfSides">The number of sides on the dice.</param>
        public Dice(int numberOfSides)
        {
            if (numberOfSides < 1)
            {
                throw new ArgumentException("Dice has fewer than 1 side.", "numberOfSides");
            }

            this.numberOfSides = numberOfSides;
        }

        /// <summary>
        /// Gets the number of sides on this dice.
        /// </summary>
        public int NumberOfSides
        {
            get
            {
                return this.numberOfSides;
            }
        }

        /// <summary>
        /// Rolls the dice and returns a value between 1 and the number of sides on the dice.
        /// </summary>
        /// <returns></returns>
        public int Roll()
        {
            return this.random.Next(1, this.numberOfSides);
        }
    }
}
