using SFChallenge.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SFChallenge.Data;
using Moq;

namespace SFChallenge.Core.UnitTests
{
    [TestClass()]
    public class ChallengeArenaTest
    {
        [TestMethod()]
        public void WhenConstructed_ThenInitialized()
        {
            // Arrange
            Mock<ISuperRepository> mockRepository = new Mock<ISuperRepository>();
            Mock<IFightStrategy> mockFightStrategy = new Mock<IFightStrategy>();

            ISuperRepository repository = mockRepository.Object;
            IFightStrategy fightStrategy = mockFightStrategy.Object;

            // Act
            ChallengeArena actual = new ChallengeArena(repository, fightStrategy);

            // Assert
            Assert.IsNotNull(actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenConstructedWithNullRepository_ThenThrows()
        {
            // Arrange            
            Mock<IFightStrategy> mockFightStrategy = new Mock<IFightStrategy>();

            ISuperRepository repository = null;
            IFightStrategy fightStrategy = mockFightStrategy.Object;

            // Act
            ChallengeArena actual = new ChallengeArena(repository, fightStrategy);

            // Assert            
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenConstructedWithNullFightStrategy_ThenThrows()
        {
            // Arrange
            Mock<ISuperRepository> mockRepository = new Mock<ISuperRepository>();            

            ISuperRepository repository = mockRepository.Object;
            IFightStrategy fightStrategy = null;

            // Act
            ChallengeArena actual = new ChallengeArena(repository, fightStrategy);

            // Assert
            Assert.IsNotNull(actual);
        }


    }
}
