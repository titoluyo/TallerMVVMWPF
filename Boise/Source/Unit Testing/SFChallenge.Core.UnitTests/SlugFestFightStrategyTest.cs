using SFChallenge.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using SFChallenge.Model;
using System.Threading;
using BellaCode.UnitTesting;

namespace SFChallenge.Core.UnitTests
{
    // Note: Contains advanced Moq examples

    [TestClass()]
    public class SlugFestFightStrategyTest
    {
        [TestMethod()]
        public void WhenConstructed_ThenInitialized()
        {
            // Arrange
            var mockDice = new Mock<IDice>();
            IDice dice = mockDice.Object;

            // Act
            SlugFestFightStrategy actual = new SlugFestFightStrategy(dice);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsFalse(actual.IsFightInProgress);
            Assert.IsNull(actual.FightLog);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenConstructedWithNull_ThenThrows()
        {
            // Arrange
            IDice dice = null;

            // Act
            SlugFestFightStrategy actual = new SlugFestFightStrategy(dice);

            // Assert         
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenFightCalledWithNullHero_ThenThrows()
        {
            // Arrange
            var mockVillian = new Mock<ISuperPerson>();
            mockVillian.Setup(x => x.IsAlive).Returns(true);

            var mockDice = new Mock<IDice>();

            IDice dice = mockDice.Object;
            SlugFestFightStrategy target = new SlugFestFightStrategy(dice);

            ISuperPerson hero = null;
            ISuperPerson villian = mockVillian.Object;

            // Act
            target.StartFight(hero, villian);

            // Assert
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenFightCalledWithNullVillain_ThenThrows()
        {
            // Arrange
            var mockHero = new Mock<ISuperPerson>();
            mockHero.Setup(x => x.IsAlive).Returns(true);

            var mockDice = new Mock<IDice>();

            IDice dice = mockDice.Object;
            SlugFestFightStrategy target = new SlugFestFightStrategy(dice);

            ISuperPerson hero = mockHero.Object;
            ISuperPerson villian = null;

            // Act
            target.StartFight(hero, villian);

            // Assert
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void WhenFightCalledWithSameHero_ThenThrows()
        {
            // Arrange
            var mockHero = new Mock<ISuperPerson>();
            mockHero.Setup(x => x.IsAlive).Returns(true);

            var mockDice = new Mock<IDice>();

            IDice dice = mockDice.Object;
            SlugFestFightStrategy target = new SlugFestFightStrategy(dice);

            ISuperPerson hero = mockHero.Object;

            // Act
            target.StartFight(hero, hero);

            // Assert
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void WhenFightCalledWithDeadHero_ThenThrows()
        {
            // Arrange
            var mockHero = new Mock<ISuperPerson>();
            mockHero.Setup(x => x.IsAlive).Returns(false);

            var mockVillian = new Mock<ISuperPerson>();
            mockVillian.Setup(x => x.IsAlive).Returns(true);

            var mockDice = new Mock<IDice>();

            IDice dice = mockDice.Object;
            SlugFestFightStrategy target = new SlugFestFightStrategy(dice);

            ISuperPerson hero = mockHero.Object;
            ISuperPerson villian = mockVillian.Object;

            // Act
            target.StartFight(hero, villian);

            // Assert
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void WhenFightCalledWithDeadVillain_ThenThrows()
        {
            // Arrange
            var mockHero = new Mock<ISuperPerson>();
            mockHero.Setup(x => x.IsAlive).Returns(true);

            var mockVillian = new Mock<ISuperPerson>();
            mockVillian.Setup(x => x.IsAlive).Returns(false);

            var mockDice = new Mock<IDice>();

            IDice dice = mockDice.Object;
            SlugFestFightStrategy target = new SlugFestFightStrategy(dice);

            ISuperPerson hero = mockHero.Object;
            ISuperPerson villian = mockVillian.Object;

            // Act
            target.StartFight(hero, villian);

            // Assert
        }

        [TestMethod()]
        public void WhenFightCalled_ThenStartedEventRaised()
        {
            // Arrange
            bool isVillianAlive = true;

            // Immortal, strong hero
            var mockHero = new Mock<ISuperPerson>();
            mockHero.Setup(x => x.Name).Returns("Hero");
            mockHero.Setup(x => x.Health).Returns(1000);
            mockHero.Setup(x => x.Strength).Returns(100);
            mockHero.Setup(x => x.Speed).Returns(100);
            mockHero.Setup(x => x.Resistance).Returns(100);
            mockHero.Setup(x => x.Intellect).Returns(100);
            mockHero.Setup(x => x.IsAlive).Returns(true);

            // Weak villian, dies on any hit
            var mockVillian = new Mock<ISuperPerson>();
            mockVillian.Setup(x => x.Name).Returns("Villian");
            mockVillian.Setup(x => x.Health).Returns(1);
            mockVillian.Setup(x => x.Strength).Returns(0);
            mockVillian.Setup(x => x.Speed).Returns(0);
            mockVillian.Setup(x => x.Resistance).Returns(0);
            mockVillian.Setup(x => x.Intellect).Returns(0);

            // Note: Moq example of using lambda's to implement logic on each request
            // Note: Returns(isVillianAlive) wouldn't work here because it would always be tru
            mockVillian.Setup(x => x.IsAlive).Returns(() => isVillianAlive);

            // Note: Moq example of using a callback to modify test-scoped state
            mockVillian.Setup(x => x.Damage(It.IsAny<int>())).Callback(() => isVillianAlive = false);

            var mockDice = new Mock<IDice>();

            mockDice.Setup(x => x.NumberOfSides).Returns(100);

            // Note: Moq example of sequencing a set of returns (different each call)
            mockDice.SetupSequence(x => x.Roll())
                .Returns(49)  //coin toss (false)
                .Returns(1)  //hero hits
                .Returns(1); //villian fails to block  

            IDice dice = mockDice.Object;
            SlugFestFightStrategy target = new SlugFestFightStrategy(dice);

            ISuperPerson hero = mockHero.Object;
            ISuperPerson villian = mockVillian.Object;

            // Note: Example of verifying events.
            bool wasStartedRaised = false;
            target.Started += (s, e) =>
            {
                wasStartedRaised = true;
            };

            // Act
            target.StartFight(hero, villian);
            WaitForFightCompleted(target);

            // Assert      
            Assert.IsTrue(wasStartedRaised);
        }

        // Note: This test case is covered repeatedly through the WaitForFightCompleted
        [TestMethod]
        public void WhenFightCalled_ThenCompletedEventRaised()
        {
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void WhenFightCalled_ThenFightLogWriteLineCalled()
        {
            // Arrange
            bool isVillianAlive = true;

            // Immortal, strong hero
            var mockHero = new Mock<ISuperPerson>();
            mockHero.Setup(x => x.Name).Returns("Hero");
            mockHero.Setup(x => x.Health).Returns(1000);
            mockHero.Setup(x => x.Strength).Returns(100);
            mockHero.Setup(x => x.Speed).Returns(100);
            mockHero.Setup(x => x.Resistance).Returns(100);
            mockHero.Setup(x => x.Intellect).Returns(100);
            mockHero.Setup(x => x.IsAlive).Returns(true);

            // Weak villian, dies on any hit
            var mockVillian = new Mock<ISuperPerson>();
            mockVillian.Setup(x => x.Name).Returns("Villian");
            mockVillian.Setup(x => x.Health).Returns(1);
            mockVillian.Setup(x => x.Strength).Returns(0);
            mockVillian.Setup(x => x.Speed).Returns(0);
            mockVillian.Setup(x => x.Resistance).Returns(0);
            mockVillian.Setup(x => x.Intellect).Returns(0);
            mockVillian.Setup(x => x.IsAlive).Returns(() => isVillianAlive);
            mockVillian.Setup(x => x.Damage(It.IsAny<int>())).Callback(() => isVillianAlive = false);

            var mockDice = new Mock<IDice>();

            mockDice.Setup(x => x.NumberOfSides).Returns(100);
            mockDice.SetupSequence(x => x.Roll())
                .Returns(49)  //coin toss (false)
                .Returns(1)  //hero hits
                .Returns(1); //villian fails to block  

            var mockFightLog = new Mock<IFightLog>();

            IDice dice = mockDice.Object;
            SlugFestFightStrategy target = new SlugFestFightStrategy(dice);
            target.FightLog = mockFightLog.Object;

            ISuperPerson hero = mockHero.Object;
            ISuperPerson villian = mockVillian.Object;          

            // Act
            target.StartFight(hero, villian);
            WaitForFightCompleted(target);

            // Assert      
            mockFightLog.Verify(x => x.WriteLine(It.IsAny<string>()), Times.AtLeastOnce());
        }

        // Note: Another example of covering an invisible code path.
        
        [TestMethod()]
        public void WhenFightCalledWithCoinTossFalse_ThenPlayersFirstTurnNotSwapped()
        {
            // Arrange
            bool isVillianAlive = true;

            // Immortal, strong hero
            var mockHero = new Mock<ISuperPerson>();
            mockHero.Setup(x => x.Name).Returns("Hero");
            mockHero.Setup(x => x.Health).Returns(1000);
            mockHero.Setup(x => x.Strength).Returns(100);
            mockHero.Setup(x => x.Speed).Returns(100);
            mockHero.Setup(x => x.Resistance).Returns(100);
            mockHero.Setup(x => x.Intellect).Returns(100);
            mockHero.Setup(x => x.IsAlive).Returns(true);

            // Weak villian, dies on any hit
            var mockVillian = new Mock<ISuperPerson>();
            mockVillian.Setup(x => x.Name).Returns("Villian");
            mockVillian.Setup(x => x.Health).Returns(1);
            mockVillian.Setup(x => x.Strength).Returns(0);
            mockVillian.Setup(x => x.Speed).Returns(0);
            mockVillian.Setup(x => x.Resistance).Returns(0);
            mockVillian.Setup(x => x.Intellect).Returns(0);
            mockVillian.Setup(x => x.IsAlive).Returns(() => isVillianAlive);

            // Note: Moq example of using It.IsAny when we don't care about matching the value of the argument
            mockVillian.Setup(x => x.Damage(It.IsAny<int>())).Callback(() => isVillianAlive = false);

            var mockDice = new Mock<IDice>();

            mockDice.Setup(x => x.NumberOfSides).Returns(100);
            mockDice.SetupSequence(x => x.Roll())
                .Returns(49)  //coin toss (false)
                .Returns(1)  //hero hits
                .Returns(1); //villian fails to block  

            IDice dice = mockDice.Object;
            SlugFestFightStrategy target = new SlugFestFightStrategy(dice);

            ISuperPerson hero = mockHero.Object;
            ISuperPerson villian = mockVillian.Object;

            // Act
            target.StartFight(hero, villian);
            WaitForFightCompleted(target);

            // Note: Moq example of using Verify with It.IsAny to count mocked method invocations.

            // Assert      
            mockHero.Verify(x => x.Damage(It.IsAny<int>()), Times.Never());
            mockVillian.Verify(x => x.Damage(It.IsAny<int>()), Times.Once());
        }

        [TestMethod()]
        public void WhenFightCalledWithCoinTossTrue_ThenPlayersFirstTurnSwapped()
        {
            // Arrange
            bool isHeroAlive = true;

            // Weak hero who dies on any damage
            var mockHero = new Mock<ISuperPerson>();
            mockHero.Setup(x => x.Name).Returns("Hero");
            mockHero.Setup(x => x.Health).Returns(1);
            mockHero.Setup(x => x.Strength).Returns(0);
            mockHero.Setup(x => x.Speed).Returns(0);
            mockHero.Setup(x => x.Resistance).Returns(0);
            mockHero.Setup(x => x.Intellect).Returns(0);
            mockHero.Setup(x => x.IsAlive).Returns(() => isHeroAlive);
            mockHero.Setup(x => x.Damage(It.IsAny<int>())).Callback(() => isHeroAlive = false);

            // Strong, immortal villian
            var mockVillian = new Mock<ISuperPerson>();
            mockVillian.Setup(x => x.Name).Returns("Villian");
            mockVillian.Setup(x => x.Health).Returns(1000);
            mockVillian.Setup(x => x.Strength).Returns(100);
            mockVillian.Setup(x => x.Speed).Returns(100);
            mockVillian.Setup(x => x.Resistance).Returns(100);
            mockVillian.Setup(x => x.Intellect).Returns(100);
            mockVillian.Setup(x => x.IsAlive).Returns(true);

            var mockDice = new Mock<IDice>();

            mockDice.Setup(x => x.NumberOfSides).Returns(100);
            mockDice.SetupSequence(x => x.Roll())
                .Returns(51)  //coin toss (true)
                .Returns(1)   //villian hits
                .Returns(1);   //hero fails to block  

            IDice dice = mockDice.Object;
            SlugFestFightStrategy target = new SlugFestFightStrategy(dice);

            ISuperPerson hero = mockHero.Object;
            ISuperPerson villian = mockVillian.Object;

            // Act
            target.StartFight(hero, villian);
            WaitForFightCompleted(target);

            // Assert      
            mockHero.Verify(x => x.Damage(It.IsAny<int>()), Times.Once());
            mockVillian.Verify(x => x.Damage(It.IsAny<int>()), Times.Never());
        }

        [TestMethod()]
        public void WhenFightCalledAndHitIsNotBlocked_ThenDamageEqualToStrength()
        {
            // Arrange
            bool isVillianAlive = true;

            // Immortal, strong hero
            var mockHero = new Mock<ISuperPerson>();
            mockHero.Setup(x => x.Name).Returns("Hero");
            mockHero.Setup(x => x.Health).Returns(1000);
            mockHero.Setup(x => x.Strength).Returns(52);
            mockHero.Setup(x => x.Speed).Returns(100);
            mockHero.Setup(x => x.Resistance).Returns(100);
            mockHero.Setup(x => x.Intellect).Returns(100);
            mockHero.Setup(x => x.IsAlive).Returns(true);

            // Weak villian, dies on any hit
            var mockVillian = new Mock<ISuperPerson>();
            mockVillian.Setup(x => x.Name).Returns("Villian");
            mockVillian.Setup(x => x.Health).Returns(1);
            mockVillian.Setup(x => x.Strength).Returns(0);
            mockVillian.Setup(x => x.Speed).Returns(0);
            mockVillian.Setup(x => x.Resistance).Returns(0);
            mockVillian.Setup(x => x.Intellect).Returns(0);
            mockVillian.Setup(x => x.IsAlive).Returns(() => isVillianAlive);
            mockVillian.Setup(x => x.Damage(It.IsAny<int>())).Callback(() => isVillianAlive = false);

            var mockDice = new Mock<IDice>();

            // Ensure hero gets first hit
            mockDice.Setup(x => x.NumberOfSides).Returns(100);
            mockDice.SetupSequence(x => x.Roll())
            .Returns(1)   //hero wins coin toss
            .Returns(1)   //hero gets a hit
            .Returns(1);   //villian fails to block

            IDice dice = mockDice.Object;
            SlugFestFightStrategy target = new SlugFestFightStrategy(dice);

            ISuperPerson hero = mockHero.Object;
            ISuperPerson villian = mockVillian.Object;

            // Act
            target.StartFight(hero, villian);
            WaitForFightCompleted(target);

            // Note: Moq example of using Verify with when we care that arguments match exactly.

            // Assert      
            mockVillian.Verify(x => x.Damage(52), Times.Once());
        }

        [TestMethod()]
        public void WhenFightCalledAndHitIsBlocked_ThenDamageReduced()
        {
            // Arrange
            bool isVillianAlive = true;

            // Immortal, strong hero
            var mockHero = new Mock<ISuperPerson>();
            mockHero.Setup(x => x.Name).Returns("Hero");
            mockHero.Setup(x => x.Health).Returns(1000);
            mockHero.Setup(x => x.Strength).Returns(52);
            mockHero.Setup(x => x.Speed).Returns(100);
            mockHero.Setup(x => x.Resistance).Returns(100);
            mockHero.Setup(x => x.Intellect).Returns(100);
            mockHero.Setup(x => x.IsAlive).Returns(true);

            // Weak, resistant villian, dies on any hit
            var mockVillian = new Mock<ISuperPerson>();
            mockVillian.Setup(x => x.Name).Returns("Villian");
            mockVillian.Setup(x => x.Health).Returns(1);
            mockVillian.Setup(x => x.Strength).Returns(0);
            mockVillian.Setup(x => x.Speed).Returns(0);
            mockVillian.Setup(x => x.Resistance).Returns(100);
            mockVillian.Setup(x => x.Intellect).Returns(0);
            mockVillian.Setup(x => x.IsAlive).Returns(() => isVillianAlive);
            mockVillian.Setup(x => x.Damage(It.IsAny<int>())).Callback(() => isVillianAlive = false);

            var mockDice = new Mock<IDice>();

            // Ensure hero gets first hit
            mockDice.Setup(x => x.NumberOfSides).Returns(100);
            mockDice.SetupSequence(x => x.Roll())
            .Returns(1)   //hero wins coin toss
            .Returns(1)   //hero gets a hit
            .Returns(50);   //villian blocks

            IDice dice = mockDice.Object;
            SlugFestFightStrategy target = new SlugFestFightStrategy(dice);

            ISuperPerson hero = mockHero.Object;
            ISuperPerson villian = mockVillian.Object;

            // Act
            target.StartFight(hero, villian);
            WaitForFightCompleted(target);

            // Assert      
            mockVillian.Verify(x => x.Damage(26), Times.Once());
        }

        [TestMethod()]
        public void WhenFightCalledAndDefenseFighterDoesNotDoCounterDamage_ThenNoDamageDoneToOffenseFighter()
        {
            // Arrange
            int villianHealth = 2;

            // Immortal, strong hero
            var mockHero = new Mock<ISuperPerson>();
            mockHero.Setup(x => x.Name).Returns("Hero");
            mockHero.Setup(x => x.Health).Returns(1000);
            mockHero.Setup(x => x.Strength).Returns(52);
            mockHero.Setup(x => x.Speed).Returns(100);
            mockHero.Setup(x => x.Resistance).Returns(100);
            mockHero.Setup(x => x.Intellect).Returns(100);
            mockHero.Setup(x => x.IsAlive).Returns(true);

            // Weak villian, dies on 2nd hit
            var mockVillian = new Mock<ISuperPerson>();
            mockVillian.Setup(x => x.Name).Returns("Villian");
            mockVillian.Setup(x => x.Health).Returns(() => villianHealth);
            mockVillian.Setup(x => x.Strength).Returns(0);
            mockVillian.Setup(x => x.Speed).Returns(0);
            mockVillian.Setup(x => x.Resistance).Returns(0);
            mockVillian.Setup(x => x.Intellect).Returns(0);
            mockVillian.Setup(x => x.IsAlive).Returns(() => villianHealth > 0);
            mockVillian.Setup(x => x.Damage(It.IsAny<int>())).Callback(() =>
            {
                villianHealth -= 1;
            });

            var mockDice = new Mock<IDice>();

            // Ensure hero gets first hit
            mockDice.Setup(x => x.NumberOfSides).Returns(100);
            mockDice.SetupSequence(x => x.Roll())
            .Returns(1)   //coin toss (false)
            .Returns(1)   //hero gets a hit
            .Returns(1)   //villian failed to block
            .Returns(1)   //villian fails to counter-damage
            .Returns(1)   //villain misses
            .Returns(1)   //hero gets a hit
            .Returns(1);  //villian failed to block

            IDice dice = mockDice.Object;
            SlugFestFightStrategy target = new SlugFestFightStrategy(dice);

            ISuperPerson hero = mockHero.Object;
            ISuperPerson villian = mockVillian.Object;

            // Act
            target.StartFight(hero, villian);
            WaitForFightCompleted(target);

            // Note: Moq example of using Verify with Times.Exactly to count invocations

            // Assert      
            mockHero.Verify(x => x.Damage(It.IsAny<int>()), Times.Never());
            mockVillian.Verify(x => x.Damage(It.IsAny<int>()), Times.Exactly(2));
        }

        [TestMethod()]
        public void WhenFightCalledAndDefenseFighterDoesCounterDamage_ThenCounterDamageDoneToOffenseFighter()
        {
            // Arrange
            int villianHealth = 2;

            // Immortal, strong hero
            var mockHero = new Mock<ISuperPerson>();
            mockHero.Setup(x => x.Name).Returns("Hero");
            mockHero.Setup(x => x.Health).Returns(1000);
            mockHero.Setup(x => x.Strength).Returns(52);
            mockHero.Setup(x => x.Speed).Returns(100);
            mockHero.Setup(x => x.Resistance).Returns(100);
            mockHero.Setup(x => x.Intellect).Returns(100);
            mockHero.Setup(x => x.IsAlive).Returns(true);

            // Weak, smart villian, dies on 2nd hit
            var mockVillian = new Mock<ISuperPerson>();
            mockVillian.Setup(x => x.Name).Returns("Villian");
            mockVillian.Setup(x => x.Health).Returns(() => villianHealth);
            mockVillian.Setup(x => x.Strength).Returns(0);
            mockVillian.Setup(x => x.Speed).Returns(0);
            mockVillian.Setup(x => x.Resistance).Returns(0);
            mockVillian.Setup(x => x.Intellect).Returns(100);
            mockVillian.Setup(x => x.IsAlive).Returns(() => villianHealth > 0);
            mockVillian.Setup(x => x.Damage(It.IsAny<int>())).Callback(() =>
            {
                villianHealth -= 1;
            });

            var mockDice = new Mock<IDice>();

            // Ensure hero gets first hit
            mockDice.Setup(x => x.NumberOfSides).Returns(100);
            mockDice.SetupSequence(x => x.Roll())
            .Returns(1)   //coin toss (false)
            .Returns(1)   //hero gets a hit
            .Returns(1)   //villian failed to block
            .Returns(51)   //villian does counter-damage
            .Returns(1)   //villain misses
            .Returns(1)   //hero gets a hit
            .Returns(1);  //villian failed to block

            IDice dice = mockDice.Object;
            SlugFestFightStrategy target = new SlugFestFightStrategy(dice);

            ISuperPerson hero = mockHero.Object;
            ISuperPerson villian = mockVillian.Object;

            // Act
            target.StartFight(hero, villian);
            WaitForFightCompleted(target);

            // Assert      
            mockHero.Verify(x => x.Damage(26), Times.Once());
            mockVillian.Verify(x => x.Damage(It.IsAny<int>()), Times.Exactly(2));
        }

        // Note: Example of testing interrelated code paths - another invisible path for code coverage

        [TestMethod()]
        public void WhenFightCalledAndDefenseFighterBlocksAndDoesCounterDamage_ThenReducedCounterDamageDoneToOffenseFighter()
        {
            // Arrange
            int villianHealth = 2;

            // Immortal, strong hero
            var mockHero = new Mock<ISuperPerson>();
            mockHero.Setup(x => x.Name).Returns("Hero");
            mockHero.Setup(x => x.Health).Returns(1000);
            mockHero.Setup(x => x.Strength).Returns(52);
            mockHero.Setup(x => x.Speed).Returns(100);
            mockHero.Setup(x => x.Resistance).Returns(100);
            mockHero.Setup(x => x.Intellect).Returns(100);
            mockHero.Setup(x => x.IsAlive).Returns(true);

            // Weak, smart, resistant villian, dies on 2nd hit
            var mockVillian = new Mock<ISuperPerson>();
            mockVillian.Setup(x => x.Name).Returns("Villian");
            mockVillian.Setup(x => x.Health).Returns(() => villianHealth);
            mockVillian.Setup(x => x.Strength).Returns(0);
            mockVillian.Setup(x => x.Speed).Returns(0);
            mockVillian.Setup(x => x.Resistance).Returns(100);
            mockVillian.Setup(x => x.Intellect).Returns(100);
            mockVillian.Setup(x => x.IsAlive).Returns(() => villianHealth > 0);
            mockVillian.Setup(x => x.Damage(It.IsAny<int>())).Callback(() =>
            {
                villianHealth -= 1;
            });

            var mockDice = new Mock<IDice>();

            // Ensure hero gets first hit
            mockDice.Setup(x => x.NumberOfSides).Returns(100);
            mockDice.SetupSequence(x => x.Roll())
            .Returns(1)   //coin toss (false)
            .Returns(1)   //hero gets a hit
            .Returns(1)   //villian blocks
            .Returns(51)   //villian does counter-damage
            .Returns(1)   //villain misses
            .Returns(1)   //hero gets a hit
            .Returns(1);  //villian failed to block

            IDice dice = mockDice.Object;
            SlugFestFightStrategy target = new SlugFestFightStrategy(dice);

            ISuperPerson hero = mockHero.Object;
            ISuperPerson villian = mockVillian.Object;

            // Act
            target.StartFight(hero, villian);
            WaitForFightCompleted(target);

            // Assert      
            mockHero.Verify(x => x.Damage(13), Times.Once());
            mockVillian.Verify(x => x.Damage(It.IsAny<int>()), Times.Exactly(2));
        }

        // Note: Example of testing iterations on a loop with a terminating condition
        // Note: Debug.Assert in Fight method is better than testing an impossible non-terminating condition

        [TestMethod()]
        public void WhenFightCalled_ThenFightersTakeTurnsUntilOneDies()
        {
            // Arrange
            int heroHealth = 4;
            int villianHealth = 3;

            // Hero
            var mockHero = new Mock<ISuperPerson>();
            mockHero.Setup(x => x.Name).Returns("Hero");
            mockHero.Setup(x => x.Health).Returns(1000);
            mockHero.Setup(x => x.Strength).Returns(100);
            mockHero.Setup(x => x.Speed).Returns(100);
            mockHero.Setup(x => x.Resistance).Returns(100);
            mockHero.Setup(x => x.Intellect).Returns(100);
            mockHero.Setup(x => x.IsAlive).Returns(() => heroHealth > 0);
            mockHero.Setup(x => x.Damage(It.IsAny<int>())).Callback(() =>
            {
                heroHealth -= 1;
            });

            // Weak, smart, resistant villian, dies on 2nd hit
            var mockVillian = new Mock<ISuperPerson>();
            mockVillian.Setup(x => x.Name).Returns("Villian");
            mockVillian.Setup(x => x.Health).Returns(() => villianHealth);
            mockVillian.Setup(x => x.Strength).Returns(100);
            mockVillian.Setup(x => x.Speed).Returns(100);
            mockVillian.Setup(x => x.Resistance).Returns(100);
            mockVillian.Setup(x => x.Intellect).Returns(100);
            mockVillian.Setup(x => x.IsAlive).Returns(() => villianHealth > 0);
            mockVillian.Setup(x => x.Damage(It.IsAny<int>())).Callback(() =>
            {
                villianHealth -= 1;
            });

            var mockDice = new Mock<IDice>();

            // Ensure hero gets first hit
            mockDice.Setup(x => x.NumberOfSides).Returns(100);
            mockDice.SetupSequence(x => x.Roll())
            .Returns(1)    //coin toss (false)
                // Hero = 4, Villian = 3
            .Returns(1)    //hero hits
            .Returns(200)  //villian fails to block
            .Returns(200)  //villian fails to do counter-damage
                // Hero = 4, Villian = 2
            .Returns(1)    //villain hits
            .Returns(200)  //hero fails to block
            .Returns(200)  //hero fails to do counter-damage
                // Hero = 3, Villian = 2
            .Returns(1)    //hero hits
            .Returns(200)  //villian fails to block
            .Returns(200)  //villian fails to do counter-damage
                // Hero = 3, Villian = 1
            .Returns(1)    //villain hits
            .Returns(200)  //hero fails to block
            .Returns(200)  //hero fails to do counter-damage
                // Hero = 2, Villian = 1
            .Returns(1)    //hero hits
            .Returns(200)  //villian fails to block
            .Returns(200)  //villian fails to do counter-damage
                // Hero = 2, Villian = 0 (fight should stop here)
            .Returns(1)    //villain hits
            .Returns(200)  //hero fails to block
            .Returns(200);  //hero fails to do counter-damage

            IDice dice = mockDice.Object;
            SlugFestFightStrategy target = new SlugFestFightStrategy(dice);

            ISuperPerson hero = mockHero.Object;
            ISuperPerson villian = mockVillian.Object;

            // Act
            target.StartFight(hero, villian);
            WaitForFightCompleted(target);

            // Assert      
            mockHero.Verify(x => x.Damage(It.IsAny<int>()), Times.Exactly(2));
            mockVillian.Verify(x => x.Damage(It.IsAny<int>()), Times.Exactly(3));
        }

        [TestMethod()]
        public void WhenFightCalledWhenAFightIsAlreadyInProgress_ThenThrows()
        {
            // Arrange
            bool isVillianAlive = true;

            // Immortal, strong hero
            var mockHero = new Mock<ISuperPerson>();
            mockHero.Setup(x => x.Name).Returns("Hero");
            mockHero.Setup(x => x.Health).Returns(1000);
            mockHero.Setup(x => x.Strength).Returns(100);
            mockHero.Setup(x => x.Speed).Returns(100);
            mockHero.Setup(x => x.Resistance).Returns(100);
            mockHero.Setup(x => x.Intellect).Returns(100);
            mockHero.Setup(x => x.IsAlive).Returns(true);

            // Weak villian, dies on any hit
            var mockVillian = new Mock<ISuperPerson>();
            mockVillian.Setup(x => x.Name).Returns("Villian");
            mockVillian.Setup(x => x.Health).Returns(1);
            mockVillian.Setup(x => x.Strength).Returns(0);
            mockVillian.Setup(x => x.Speed).Returns(0);
            mockVillian.Setup(x => x.Resistance).Returns(0);
            mockVillian.Setup(x => x.Intellect).Returns(0);
            mockVillian.Setup(x => x.IsAlive).Returns(() => isVillianAlive);
            mockVillian.Setup(x => x.Damage(It.IsAny<int>())).Callback(() =>
            {
                // Note: Example of multi-threading awareness when unit testing concurrent operations.
                // Note: Should not confuse unit testing with concurrency race-condition and load testing.

                isVillianAlive = false;
                Thread.Sleep(500);  //sleep here to ensure second call gets a chance before fight completes.
            });

            var mockDice = new Mock<IDice>();

            mockDice.Setup(x => x.NumberOfSides).Returns(100);
            mockDice.SetupSequence(x => x.Roll())
                .Returns(49)  //coin toss (false)
                .Returns(1)  //hero hits
                .Returns(1); //villian fails to block  

            IDice dice = mockDice.Object;
            SlugFestFightStrategy target = new SlugFestFightStrategy(dice);

            ISuperPerson hero = mockHero.Object;
            ISuperPerson villian = mockVillian.Object;

            // Act
            target.StartFight(hero, villian);

            bool secondCallThrows = false;
            try
            {
                target.StartFight(hero, villian);
            }
            catch (InvalidOperationException)
            {
                secondCallThrows = true;

            }

            WaitForFightCompleted(target);

            // Assert    
            Assert.IsTrue(secondCallThrows);
        }

        // Note: Example of handling asynchronous method completion.

        private static void WaitForFightCompleted(IFightStrategy target)
        {
            bool isFightCompleted = false;
            target.Completed += (s, e) =>
            {
                isFightCompleted = true;
            };

            SpinWait spintWait = new SpinWait();
            DateTime start = DateTime.Now;
            while (DateTime.Now - start < new TimeSpan(0, 1, 0))
            {

                // Note: Example of forcing Dispatcher to work in Unit Tests

                // Dispatcher does not automatically process events during tests, so I manually cause processing.
                DispatcherAssist.DoEvents();
                spintWait.SpinOnce();

                if (isFightCompleted)
                {
                    return;
                }
            }

            Assert.Fail("Fight did not complete in expected amount of time.");
        }
    }
}
