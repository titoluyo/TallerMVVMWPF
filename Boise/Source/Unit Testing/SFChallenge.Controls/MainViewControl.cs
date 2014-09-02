using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SFChallenge.Core;
using SFChallenge.Data;
using SFChallenge.Model;
using SFChallenge.Storage;

namespace SFChallenge.Controls
{   
    [TemplatePart(Name="PART_FightLogTextBox", Type=typeof(TextBox))]    
    public class MainViewControl : Control, IFightLog
    {
        static MainViewControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MainViewControl), new FrameworkPropertyMetadata(typeof(MainViewControl)));

            Fight = new RoutedCommand("Fight", typeof(MainViewControl));

            CommandManager.RegisterClassCommandBinding(typeof(MainViewControl),
                new CommandBinding(MainViewControl.Fight,
                    (s, e) => { ((MainViewControl)s).Fight_Executed(s, e); },
                    (s, ce) => { ((MainViewControl)s).Fight_CanExecute(s, ce); }));

            Reset = new RoutedCommand("Reset", typeof(MainViewControl));

            CommandManager.RegisterClassCommandBinding(typeof(MainViewControl),
                new CommandBinding(MainViewControl.Reset,
                    (s, e) => { ((MainViewControl)s).Reset_Executed(s, e); },
                    (s, ce) => { ((MainViewControl)s).Reset_CanExecute(s, ce); }));

        }

        private TextBox fightLogTextBox;        

        public MainViewControl()
        {            
            this.Loaded += new RoutedEventHandler(MainViewControl_Loaded);            
        }        

        #region Dependency Properties

        public ChallengeArena Arena
        {
            get { return (ChallengeArena)GetValue(ArenaProperty); }
            set { SetValue(ArenaProperty, value); }
        }

        public static readonly DependencyProperty ArenaProperty = DependencyProperty.Register("Arena", typeof(ChallengeArena), typeof(MainViewControl), new FrameworkPropertyMetadata(null));       

        #endregion

        #region Commands

        public static RoutedCommand Fight { get; private set; }

        public static RoutedCommand Reset { get; private set; }        

        #endregion        

        #region Command Callbacks

        private void Fight_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (this.Arena != null) && this.Arena.CanFight();
        }

        private void Fight_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.Arena.CanFight())
            {
                if (this.fightLogTextBox != null)
                {
                    this.fightLogTextBox.Clear();
                }

                this.Arena.Fight();                
            }
        }

        private void Reset_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (this.Arena != null) && !this.Arena.IsFightInProgress;
        }

        private void Reset_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Arena.Reset();
        }

        #endregion

        #region Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.fightLogTextBox = this.GetTemplateChild("PART_FightLogTextBox") as TextBox;            
        }

        private void InitializeArena()
        {
            var repository = new SuperRepository(new SuperDatabaseContext());
            var arena = new ChallengeArena(repository, new SlugFestFightStrategy(new Dice(100)));
            arena.FightLog = this;
            arena.Load(TeamNames.SuperFriends, TeamNames.LegionOfDoom);

            this.Arena = arena;

            this.Arena.PropertyChanged += this.Arena_PropertyChanged;
        }

        void IFightLog.WriteLine(string message)
        {
            if (this.fightLogTextBox != null)
            {
                this.fightLogTextBox.AppendText(message + "\r\n");
                this.fightLogTextBox.ScrollToEnd();
            }
        }
        
        #endregion

        #region Model Event Handlers

        private void Arena_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ChallengeArena.PropertyNames.IsFightInProgress:
                    CommandManager.InvalidateRequerySuggested();
                    break;
            }
        }

        #endregion

        #region UI Event Handlers

        void MainViewControl_Loaded(object sender, RoutedEventArgs e)
        {            
            SFChallenge.Storage.App_Start.EntityFramework_SqlServerCompact.Start();
            InitializeArena();
        }        

        #endregion
    }
}
