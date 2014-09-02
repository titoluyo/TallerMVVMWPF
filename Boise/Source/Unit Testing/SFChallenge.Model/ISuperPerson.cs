using System;
namespace SFChallenge.Model
{
    /// <summary>
    /// Represents a super hero or super villian.
    /// </summary>    
    public interface ISuperPerson
    {
        /// <summary>
        /// Gets the unique id of this super person.
        /// </summary>
        /// <value>A unique id.</value>
        int Id { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>A single line of text.</value>
        string Name { get; }

        /// <summary>
        /// Gets the allegiance to a team.
        /// </summary>
        /// <value>The name of a team.</value>
        string Allegiance { get; }

        /// <summary>
        /// Gets the rank relative to others on the same team.
        /// </summary>
        /// <value>A number from 1 to N.</value>
        int Rank { get; }

        /// <summary>
        /// Gets a value indicating whether this person is alive.
        /// </summary>
        /// <value><c>true</c> if health is greater than zero; otherwise, <c>false</c>.</value>
        bool IsAlive { get; }

        /// <summary>
        /// Gets the current amount of health.  A health of zero indicates death.
        /// </summary>
        /// <value>A number typically starting at 1000.</value>
        int Health { get; }

        /// <summary>
        /// Gets the how much damage is done when hitting an opponent.
        /// </summary>
        /// <value>A number typically from 0 to 10.</value>
        int Strength { get; }

        /// <summary>
        /// Gets the likelyhood of hitting an opponent.
        /// </summary>
        /// <value>A number from 0 to 100.</value>
        int Speed { get; }

        /// <summary>
        /// Gets the likelyhood of blocking a hit from an opponent.
        /// </summary>
        /// <value>A number from 0 to 100.</value>
        int Resistance { get; }

        /// <summary>
        /// Gets the likelyhood of doing equal counterdamage to an opponent.
        /// </summary>
        /// <value>A number from 0 to 100.</value>
        int Intellect { get; }

        /// <summary>
        /// Reduces the health of this super person by the the specified amount.
        /// </summary>
        /// <param name="damage">A number.</param>
        /// <remarks>
        /// A damage greather than the current super person's health with result in a health of zero.
        /// </remarks>
        void Damage(int damage);

        /// <summary>
        /// Revives this super person to full health (even if dead).
        /// </summary>
        void Revive();
    }
}
