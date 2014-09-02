using System;

namespace SFChallenge.Core
{    
    /// <summary>
    /// A game dice that rolls a random number between 1 and the number of sides on the dice.    
    /// </summary>
    public interface IDice
    {
        /// <summary>
        /// Gets the number of sides on this dice.
        /// </summary>
        int NumberOfSides {get;}

        /// <summary>
        /// Rolls the dice and returns a value between 1 and the number of sides on the dice.
        /// </summary>
        /// <returns></returns>
        int Roll();
    }
}
