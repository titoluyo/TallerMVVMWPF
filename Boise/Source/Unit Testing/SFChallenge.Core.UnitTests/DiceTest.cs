using SFChallenge.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SFChallenge.Core.UnitTests
{
    [TestClass()]
    public class DiceTest
    {
        [TestMethod()]
        public void WhenConstructed_ThenInitialized()
        {
            // Arrange
            int numberOfSides = 100;            

            // Act
            Dice actual = new Dice(numberOfSides);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(numberOfSides, actual.NumberOfSides);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void WhenConstructedWithZero_ThenThrows()
        {
            // Arrange
            int numberOfSides = 0;

            // Act
            Dice actual = new Dice(numberOfSides);

            // Assert
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void WhenConstructedWithNegative_ThenThrows()
        {
            // Arrange
            int numberOfSides = -5;

            // Act
            Dice actual = new Dice(numberOfSides);

            // Assert
        }

        [TestMethod()]
        public void WhenConstructedWithOne_ThenInitialized()
        {
            // Arrange
            int numberOfSides = 1;

            // Act
            Dice actual = new Dice(numberOfSides);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(numberOfSides, actual.NumberOfSides);
        }

        // Note: As close to using random in a unit test as anyone should get.

        [TestMethod()]
        public void WhenRollCalled_ThenReturnsANumber()
        {
            // Arrange            
            Dice dice = new Dice(100);

            // Act
            int actual = dice.Roll();        

            // Assert
            Assert.IsTrue(1 < actual);
            Assert.IsTrue(actual < 100);
        }
    }
}
