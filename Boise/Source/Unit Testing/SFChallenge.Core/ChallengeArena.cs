using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFChallenge.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using SFChallenge.Model;

namespace SFChallenge.Core
{
    public class ChallengeArena : INotifyPropertyChanged
    {
        private ISuperRepository repository;
        private IFightStrategy fightStrategy;

        private IEnumerable<ISuperPerson> heroes;
        private IEnumerable<ISuperPerson> villians;

        private ISuperPerson currentHero;
        private ISuperPerson currentVillian;

        public ChallengeArena(ISuperRepository repository, IFightStrategy fightStrategy)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            if (fightStrategy == null)
            {
                throw new ArgumentNullException("fightStrategy");
            }

            this.repository = repository;
            this.fightStrategy = fightStrategy;

            this.Heroes = new ObservableCollection<SuperPerson>();
            this.Villians = new ObservableCollection<SuperPerson>();

            this.fightStrategy.Started += this.FightStrategy_Started;
            this.fightStrategy.Completed += this.FightStrategy_Completed;
        }        

        /// <summary>
        /// The property names used with INotifyPropertyChanged.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = "A container for constants used with INotifyPropertyChanged.")]
        public static class PropertyNames
        {
            public const string Heroes = "Heroes";
            public const string Villians = "Villians";
            public const string CurrentHero = "CurrentHero";
            public const string CurrentVillian = "CurrentVillian";
            public const string IsFightInProgress = "IsFightInProgress";
        }

        public IEnumerable<ISuperPerson> Heroes
        {
            get
            {
                return this.heroes;
            }

            set
            {
                this.heroes = value;
                this.RaisePropertyChanged(PropertyNames.Heroes);
            }
        }

        public IEnumerable<ISuperPerson> Villians
        {
            get
            {
                return this.villians;
            }

            set
            {
                this.villians = value;
                this.RaisePropertyChanged(PropertyNames.Villians);
            }
        }        

        public ISuperPerson CurrentHero
        {
            get
            {
                return this.currentHero;
            }

            set
            {
                this.currentHero = value;
                this.RaisePropertyChanged(PropertyNames.CurrentHero);
            }
        }

        public ISuperPerson CurrentVillian
        {
            get
            {
                return this.currentVillian;
            }

            set
            {
                this.currentVillian = value;
                this.RaisePropertyChanged(PropertyNames.CurrentVillian);
            }
        }

        public bool IsFightInProgress
        {
            get
            {
                return this.fightStrategy.IsFightInProgress;
            }
        }

        public IFightLog FightLog
        {
            get
            {
                return this.fightStrategy.FightLog;
            }
            set
            {
                this.fightStrategy.FightLog = value;
            }
        }

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

        public void Load(string heroesTeamName, string villiansTeamName)
        {
            this.heroes = this.repository.GetTeam(heroesTeamName);
            this.villians = this.repository.GetTeam(villiansTeamName);

            this.CurrentHero = this.heroes.FirstOrDefault();
            this.CurrentVillian = this.villians.FirstOrDefault();
        }

        public bool CanFight()
        {
            return (this.currentHero != null && this.currentHero.IsAlive && 
                    this.currentVillian != null && this.currentVillian.IsAlive &&
                    !this.fightStrategy.IsFightInProgress);
        }

        public void Fight()
        {
            this.fightStrategy.StartFight(this.currentHero, this.currentVillian);
        }

        public void Reset()
        {
            foreach (ISuperPerson hero in this.heroes)
            {
                hero.Revive();
            }

            foreach (ISuperPerson villian in this.villians)
            {
                villian.Revive();
            }
        }
        
        private void FightStrategy_Started(object sender, EventArgs e)
        {
            this.RaisePropertyChanged(PropertyNames.IsFightInProgress);
        }

        private void FightStrategy_Completed(object sender, EventArgs e)
        {
            this.RaisePropertyChanged(PropertyNames.IsFightInProgress);
        }

    }
}
