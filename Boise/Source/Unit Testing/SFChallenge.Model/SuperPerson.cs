using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SFChallenge.Model
{
    /// <summary>
    /// Represents a super hero or super villian.
    /// </summary>
    /// <remarks>
    /// This class is a POCO model for Entity Framework.
    /// </remarks>
    public class SuperPerson : ISuperPerson, INotifyPropertyChanged
    {
        private const int StartingHealth = 1000;

        private int health = SuperPerson.StartingHealth;

        /// <summary>
        /// The property names used with INotifyPropertyChanged.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = "A container for constants used with INotifyPropertyChanged.")]
        public static class PropertyNames
        {
            public const string IsAlive = "IsAlive";            
            public const string Health = "Health";            
        }

        /// <summary>
        /// Gets or sets the unique id of this super person.
        /// </summary>
        /// <value>A unique id.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>A single line of text.</value>
        public string Name { get; set; }       

        /// <summary>
        /// Gets or sets the allegiance to a team.
        /// </summary>
        /// <value>The name of a team.</value>
        public string Allegiance { get; set; }       

        /// <summary>
        /// Gets or sets the rank relative to others on the same team.
        /// </summary>
        /// <value>A number from 1 to N.</value>
        public int Rank { get; set; }

        /// <summary>
        /// Gets a value indicating whether this person is alive.
        /// </summary>
        /// <value><c>true</c> if health is greater than zero; otherwise, <c>false</c>.</value>
        public bool IsAlive
        {
            get
            {
                return (this.Health > 0);
            }
        }          

        /// <summary>
        /// Gets or sets the current amount of health.  A health of zero indicates death.
        /// </summary>
        /// <value>A number typically starting at 1000.</value>
        public int Health
        {
            get
            {
                return this.health;
            }
            set
            {
                if (this.health != value)
                {
                    bool oldIsAlive = this.IsAlive;
                    this.health = Math.Max(0, value);
                    this.RaisePropertyChanged(PropertyNames.Health);

                    if (oldIsAlive != this.IsAlive)
                    {
                        this.RaisePropertyChanged(PropertyNames.IsAlive);
                    }
                }
            }

        }

        /// <summary>
        /// Gets or sets the how much damage is done when hitting an opponent.
        /// </summary>
        /// <value>A number typically from 0 to 10.</value>
        public int Strength { get; set; }

        /// <summary>
        /// Gets or sets the likelyhood of hitting an opponent.
        /// </summary>
        /// <value>A number from 0 to 100.</value>
        public int Speed { get; set; }

        /// <summary>
        /// Gets or sets the likelyhood of blocking a hit from an opponent.
        /// </summary>
        /// <value>A number from 0 to 100.</value>
        public int Resistance { get; set; }

        /// <summary>
        /// Gets or sets the likelyhood of doing equal counterdamage to an opponent.
        /// </summary>
        /// <value>A number from 0 to 100.</value>
        public int Intellect { get; set; }

        /// <summary>
        /// Raised when a property value changes.
        /// </summary>
        /// <remarks>
        /// This class supports PropertyChanged&lt;T&gt;, see each property for details.
        /// </remarks>
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Reduces the health of this super person by the the specified amount.
        /// </summary>
        /// <param name="damage">A number.</param>
        /// <remarks>
        /// A damage greather than the current super person's health with result in a health of zero.
        /// </remarks>
        public void Damage(int damage)
        {
            this.Health -= damage;
        }

        /// <summary>
        /// Revives this super person to full health (even if dead).
        /// </summary>
        public void Revive()
        {
            this.Health = SuperPerson.StartingHealth;
        }
    }
}
