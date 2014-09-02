using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFChallenge.Model;

namespace SFChallenge.Core
{
    /// <summary>
    /// An algorithm strategy for a fight between two super persons: a hero and a villian.
    /// </summary>
    public interface IFightStrategy
    {
        /// <summary>
        /// Gets a value indicating whether a fight in progress.
        /// </summary>
        /// <value>
        /// <c>true</c> if a fight in progress; otherwise, <c>false</c>.
        /// </value>
        bool IsFightInProgress { get; }

        /// <summary>
        /// Gets or sets a log for outputing information as the fight progresses.
        /// </summary>        
        /// <value>
        /// An IFightLog instance.  Can be null.
        /// </value>
        IFightLog FightLog { get; set; }

        /// <summary>
        /// Starts a fight between the specified hero and villian.
        /// </summary>
        /// <param name="hero">The hero.</param>
        /// <param name="villian">The villian.</param>
        /// <remarks>
        /// This method is often asynchronous and follows the Async event pattern.
        /// </remarks>
        void StartFight(ISuperPerson hero, ISuperPerson villian);

        /// <summary>
        /// Raised when the fight is started.
        /// </summary>
        event EventHandler Started;

        /// <summary>
        /// Raised when the fight is completed.
        /// </summary>
        event EventHandler Completed;
    }
}
