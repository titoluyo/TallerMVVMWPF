using SFChallenge.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SFChallenge.Model.UnitTests
{    
    // Note: Example of testing a concrete class
    [TestClass()]
    public class SuperPersonTest
    {
        // Note: Test naming convension
        [TestMethod()]
        public void WhenConstructed_ThenInitialized()
        {
            // Note: Triple-A Pattern

            // Arrange
            
            // Act
            SuperPerson actual = new SuperPerson();

            // Assert
            Assert.IsNotNull(actual);

            // Note: Example of expected vs. actual

            Assert.AreEqual(1000, actual.Health);
            Assert.AreEqual(0, actual.Id);
            Assert.IsNull(actual.Name);
            Assert.IsTrue(actual.IsAlive);
        }

        [TestMethod()]
        public void WhenIdSet_ThenIdUpdated()
        {            
            // Arrange            
            SuperPerson target = new SuperPerson();

            // Note: Auto-backed properties will not show code coverage,
            // Note:but they need tests because that is an implementation detail.

            // Act
            target.Id = 4;

            var actual = target.Id;            

            // Assert
            Assert.AreEqual(4, actual);
        }

        [TestMethod()]
        public void WhenNameSet_ThenNameUpdated()
        {
            // Arrange            
            SuperPerson target = new SuperPerson();

            // Act
            target.Name = "Name";

            var actual = target.Name;

            // Assert
            Assert.AreEqual("Name", actual);
        }

        [TestMethod()]
        public void WhenAllegianceSet_ThenAllegianceUpdated()
        {
            // Arrange            
            SuperPerson target = new SuperPerson();

            // Act
            target.Allegiance = "Allegiance";

            var actual = target.Allegiance;

            // Assert
            Assert.AreEqual("Allegiance", actual);
        }

        [TestMethod()]
        public void WhenRankSet_ThenRankUpdated()
        {
            // Arrange            
            SuperPerson target = new SuperPerson();

            // Act
            target.Rank = 3;

            var actual = target.Rank;

            // Assert
            Assert.AreEqual(3, actual);
        }        

        [TestMethod()]
        public void WhenHeathSet_ThenHealthUpdated()
        {
            // Note: Example of target vs. actual

            // Arrange
            SuperPerson target = new SuperPerson();

            // Act
            target.Health = 50;

            // Note: var is useful to keep tests clean, 
            // Note: but var can make type validation easy to miss
            // Note: show "Error" in AreEqual as example.

            var actual = target.Health;

            // Note: Don't want too many asserts per test.

            // Assert
            Assert.AreEqual(50, actual);
        }

        [TestMethod()]
        public void WhenHeathSetToDifferentValue_ThenHealthPropertyChangedRaised()
        {
            // Arrange
            SuperPerson target = new SuperPerson();

            bool wasPropertyChangedRaised = false;
            target.PropertyChanged += (s, e) => 
            {
                wasPropertyChangedRaised = (e.PropertyName == SuperPerson.PropertyNames.Health);                    
            };
            
            // Act
            target.Health = 50;            

            // Assert
            Assert.IsTrue(wasPropertyChangedRaised);            
        }

        [TestMethod()]
        public void WhenHeathSetToSameValue_ThenHealthPropertyChangedIsNotRaised()
        {
            // Note: Example of testing a non-visible code-path

            // Arrange
            SuperPerson target = new SuperPerson();
            target.Health = 50;

            bool wasPropertyChangedRaised = false;
            target.PropertyChanged += (s, e) =>
            {
                wasPropertyChangedRaised = (e.PropertyName == SuperPerson.PropertyNames.Health);
            };

            // Act
            target.Health = 50;

            // Assert
            Assert.IsFalse(wasPropertyChangedRaised);
        }

        // Note: VS Shortcuts
        // Note: Run test in context: Ctrl+R, T
        // Note: Debug test in context: Ctrl+R, Ctrl+T
        // Note: Run all tests in context: Ctrl+R, A (great for code coverage runs)
        // Note: Debug failed tests: Ctrl+R, Ctrl+F 

        [TestMethod()]
        public void WhenHeathSetToAnotherPositiveValue_ThenIsAlivePropertyChangedIsNotRaised()
        {            
            // Arrange
            SuperPerson target = new SuperPerson();
            target.Health = 50;

            bool wasPropertyChangedRaised = false;
            target.PropertyChanged += (s, e) =>
            {
                wasPropertyChangedRaised = (e.PropertyName == SuperPerson.PropertyNames.IsAlive);
            };

            // Act
            target.Health = 70;

            // Assert
            Assert.IsFalse(wasPropertyChangedRaised);
        }

        [TestMethod()]
        public void WhenHeathSetToZero_ThenIsAliveSetToFalse()
        {
            // Arrange
            SuperPerson target = new SuperPerson();        

            // Act
            target.Health = 0;

            bool actual = target.IsAlive;

            // Assert
            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public void WhenHeathSetToPositive_ThenIsAliveSetToTrue()
        {
            // Arrange
            SuperPerson target = new SuperPerson();
            target.Health = 0;

            // Act
            target.Health = 1;

            bool actual = target.IsAlive;

            // Assert
            Assert.IsTrue(actual);
        }

        [TestMethod()]
        public void WhenHeathSetToZero_ThenIsAlivePropertyChangedRaised()
        {
            // Arrange
            SuperPerson target = new SuperPerson();

            bool wasPropertyChangedRaised = false;
            target.PropertyChanged += (s, e) =>
            {
                wasPropertyChangedRaised = (e.PropertyName == SuperPerson.PropertyNames.IsAlive);
            };

            // Act
            target.Health = 0;

            // Assert
            Assert.IsTrue(wasPropertyChangedRaised);
        }

        [TestMethod()]
        public void WhenStrengthSet_ThenStrengthUpdated()
        {
            // Arrange            
            SuperPerson target = new SuperPerson();

            // Act
            target.Strength = 123;

            var actual = target.Strength;

            // Assert
            Assert.AreEqual(123, actual);
        }

        [TestMethod()]
        public void WhenSpeedSet_ThenSpeedUpdated()
        {
            // Arrange            
            SuperPerson target = new SuperPerson();

            // Act
            target.Speed = 456;

            var actual = target.Speed;

            // Assert
            Assert.AreEqual(456, actual);
        }

        [TestMethod()]
        public void WhenResistanceSet_ThenResistanceUpdated()
        {
            // Arrange            
            SuperPerson target = new SuperPerson();

            // Act
            target.Resistance = 789;

            var actual = target.Resistance;

            // Assert
            Assert.AreEqual(789, actual);
        }

        [TestMethod()]
        public void WhenIntellectSet_ThenIntellectUpdated()
        {
            // Arrange            
            SuperPerson target = new SuperPerson();

            // Act
            target.Intellect = 321;

            var actual = target.Intellect;

            // Assert
            Assert.AreEqual(321, actual);
        }

        [TestMethod()]
        public void WhenDamaged_ThenHealthReducedByDamageAmount()
        {
            // Arrange
            SuperPerson target = new SuperPerson();            

            // Act
            target.Damage(80);

            var actual = target.Health;

            // Assert
            Assert.AreEqual(920, actual);
        }

        [TestMethod()]
        public void WhenDamagedGreaterThanHealth_ThenHealthSetToZero()
        {
            // Arrange
            SuperPerson target = new SuperPerson();

            // Act
            target.Damage(80000);

            var actual = target.Health;

            // Assert
            Assert.AreEqual(0, actual);
        }

        [TestMethod()]
        public void WhenRevived_ThenHealthSetToDefault()
        {
            // Arrange
            SuperPerson target = new SuperPerson();
            target.Health = 100;            

            // Act
            target.Revive();

            var actual = target.Health;

            // Assert
            Assert.AreEqual(1000, actual);
        }

        //TODO: Test GET/SET of all other properties
    }
}
