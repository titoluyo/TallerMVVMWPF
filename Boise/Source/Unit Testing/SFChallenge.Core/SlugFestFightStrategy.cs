using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFChallenge.Model;
using System.Diagnostics;
using System.Windows.Threading;

namespace SFChallenge.Core
{
    /// <summary>
    /// A turn-based fight strategy where each fighter takes turns hitting the other.
    /// </summary>
    public class SlugFestFightStrategy : IFightStrategy
    {
        private IDice dice;

        private object syncLock = new object();
        private Dispatcher dispatcher;
        private volatile bool isFightInProgress;

        /// <summary>
        /// Initializes a new instance of the <see cref="SlugFestFightStrategy"/> class.
        /// </summary>
        /// <param name="dice">The dice to use for rolling during turns.</param>
        public SlugFestFightStrategy(IDice dice)
        {
            if (dice == null)
            {
                throw new ArgumentNullException("dice");
            }

            this.dice = dice;
            this.dispatcher = Dispatcher.CurrentDispatcher;
        }

        /// <summary>
        /// Gets a value indicating whether a fight in progress.
        /// </summary>
        /// <value>
        /// <c>true</c> if a fight in progress; otherwise, <c>false</c>.
        /// </value>
        public bool IsFightInProgress
        {
            get
            {
                return this.isFightInProgress;
            }
        }

        /// <summary>
        /// Gets or sets a log for outputing information as the fight progresses.
        /// </summary>
        /// <value>
        /// An IFightLog instance.  Can be null.
        /// </value>
        public IFightLog FightLog { get; set; }

        /// <summary>
        /// Raised when the fight is started.
        /// </summary>
        public event EventHandler Started;

        private void RaiseStarted()
        {
            if (this.Started != null)
            {
                this.Started(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raised when the fight is completed.
        /// </summary>
        public event EventHandler Completed;

        private void RaiseCompleted()
        {
            if (this.Completed != null)
            {
                this.Completed(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Starts a fight between the specified hero and villian.
        /// </summary>
        /// <param name="hero">The hero.</param>
        /// <param name="villian">The villian.</param>
        /// <remarks>
        /// This method is often asynchronous and follows the Async event pattern.
        /// </remarks>
        public void StartFight(ISuperPerson hero, ISuperPerson villian)
        {
            if (hero == null)
            {
                throw new ArgumentNullException("hero");
            }

            if (villian == null)
            {
                throw new ArgumentNullException("villian");
            }

            if (object.ReferenceEquals(hero, villian))
            {
                throw new ArgumentException("A hero can not fight themself.", "hero");
            }

            if (!hero.IsAlive)
            {
                throw new ArgumentException("A dead hero can not fight.", "hero");
            }

            if (!villian.IsAlive)
            {
                throw new ArgumentException("A dead villian can not fight.", "villian");
            }

            lock (this.syncLock)
            {
                if (this.isFightInProgress)
                {
                    throw new InvalidOperationException("There is already a fight in progress.");
                }

                this.isFightInProgress = true;
            }

            var action = new Action<ISuperPerson, ISuperPerson>(this.RunFight);
            action.BeginInvoke(hero, villian, null, null);
        }


        private void RunFight(ISuperPerson hero, ISuperPerson villian)
        {
            this.DispatchRaiseStarted();

            ISuperPerson offenseFighter = hero;
            ISuperPerson defenseFighter = villian;

            // Who gets the first turn is random
            this.CoinTossSwapFighters(ref offenseFighter, ref defenseFighter);

            // The fight is on
            while (true)
            {
                this.DispatchLog("----------");

                TakeTurn(offenseFighter, defenseFighter);

                this.DispatchLog(string.Format("{0} = {1} health | {2} = {3} health.", hero.Name, hero.Health, villian.Name, villian.Health));

                if ((!hero.IsAlive) || (!villian.IsAlive))
                {
                    Debug.Assert(hero.IsAlive || villian.IsAlive);

                    ISuperPerson winner = hero;
                    ISuperPerson loser = villian;

                    if (villian.IsAlive)
                    {
                        SwapFighters(ref winner, ref loser);
                    }

                    this.DispatchLog(string.Format("{0} defeated {1}.", winner.Name, loser.Name));
                    winner.Revive();
                    break;
                }

                SwapFighters(ref offenseFighter, ref defenseFighter);
            }

            lock (this.syncLock)
            {
                this.isFightInProgress = false;
            }

            this.DispatchRaiseCompleted();
        }

        private void TakeTurn(ISuperPerson offenseFighter, ISuperPerson defenseFighter)
        {
            int damage = 0;

            // Determine if fighter1 gets a hit
            int speed = dice.Roll();
            if (speed <= offenseFighter.Speed)
            {
                damage = offenseFighter.Strength;

                //Determine if fighter2 blocks and reduces damage by 50%
                bool wasBlocked = false;
                int resistance = dice.Roll();
                if (resistance <= defenseFighter.Resistance)
                {
                    damage = damage / 2;
                    wasBlocked = true;
                }

                this.DispatchDamage(defenseFighter, damage);

                if (!wasBlocked)
                {
                    this.DispatchLog(string.Format("{0} hits {1} with {2} damage.", offenseFighter.Name, defenseFighter.Name, damage));
                }
                else
                {
                    this.DispatchLog(string.Format("{0} hits {1} with {2} partially blocked damage.", offenseFighter.Name, defenseFighter.Name, damage));
                }

                // Verify fighter2 is alive
                if (defenseFighter.IsAlive)
                {
                    // Determine if fighter2 does counterdamage of 50%
                    int intellect = dice.Roll();
                    if (intellect <= defenseFighter.Intellect)
                    {
                        int counterDamage = damage / 2;
                        this.DispatchDamage(offenseFighter, counterDamage);

                        this.DispatchLog(string.Format("{0} counters {1} with {2} damage.", defenseFighter.Name, offenseFighter.Name, counterDamage));
                    }
                }
            }
            else
            {
                this.DispatchLog(string.Format("{0} misses {1}.", offenseFighter.Name, defenseFighter.Name));
            }
        }

        private void CoinTossSwapFighters(ref ISuperPerson superPerson1, ref ISuperPerson superPerson2)
        {
            if (dice.Roll() > (dice.NumberOfSides / 2))
            {
                SwapFighters(ref superPerson1, ref superPerson2);
            }
        }

        private static void SwapFighters(ref ISuperPerson superPerson1, ref ISuperPerson superPerson2)
        {
            ISuperPerson temp = superPerson2;
            superPerson2 = superPerson1;
            superPerson1 = temp;
        }

        private void DispatchRaiseStarted()
        {
            this.dispatcher.Invoke(new Action(() => { this.RaiseStarted(); }), DispatcherPriority.Normal);
        }

        private void DispatchRaiseCompleted()
        {
            this.dispatcher.Invoke(new Action(() => { this.RaiseCompleted(); }), DispatcherPriority.Normal);
        }

        private void DispatchDamage(ISuperPerson superPerson, int damage)
        {
            this.dispatcher.Invoke(new Action<ISuperPerson, int>((s, d) => { s.Damage(d); }), DispatcherPriority.Render, superPerson, damage);
        }

        private void DispatchLog(string message)
        {
            this.dispatcher.Invoke(new Action<string>((m) =>
            {
                if (this.FightLog != null)
                {
                    this.FightLog.WriteLine(m);
                }
            }), DispatcherPriority.Render, message);
        }
    }
}
