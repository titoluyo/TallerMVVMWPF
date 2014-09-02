using SFChallenge.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using SFChallenge.Storage;
using Moq;
using SFChallenge.Model;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SFChallenge.Data.UnitTests
{
    // Note: Contains examples of EF abstraction (explain ISuperDatabaseContext, IEntitySet)
    // Note: Contains basic Moq examples
    [TestClass()]
    public class SuperRepositoryTest
    {
        [TestMethod()]
        public void WhenConstructed_ThenInstantiated()
        {            
            // Note: Moq example of an empty mocked instance.            
            // Note: Default behavior is non-strict (no-ops and default values)

            // Note: Naming of mocked variables is important            

            // Arrange
            var mockContext = new Mock<ISuperDatabaseContext>();

            ISuperDatabaseContext context = mockContext.Object;

            // Act
            SuperRepository actual = new SuperRepository(context);

            // Assert
            Assert.IsNotNull(actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenConstructedWithNull_ThenThrows()
        {
            // Arrange            
            ISuperDatabaseContext context = null;

            // Act
            SuperRepository actual = new SuperRepository(context);

            // Assert            
        }

        [TestMethod]
        public void WhenGetCalled_ThenReturnsSuperPerson()
        {
            // Arrange
            var superPerson = new SuperPerson()
            {
                Id = 1,
                Name = "Name1"
            };

            // Note: Moq example of an mocking a method that takes an argument and returns a value
            // Note: Castle-proxy is under each mock => requires interface or virtual

            var mockContext = new Mock<ISuperDatabaseContext>();

            mockContext.Setup(x => x.SuperPeople.Find(1)).Returns(superPerson);

            ISuperDatabaseContext context = mockContext.Object;
            SuperRepository target = new SuperRepository(context);

            // Act
            var actual = target.Get(1);

            // Assert            
            Assert.AreSame(superPerson, actual);

            mockContext.Verify(x => x.SuperPeople.Find(1), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WhenGetCalledWithIdLessThanOne_ThenThrows()
        {
            // Arrange                        
            var mockContext = new Mock<ISuperDatabaseContext>();

            ISuperDatabaseContext context = mockContext.Object;
            SuperRepository target = new SuperRepository(context);

            // Act
            target.Get(0);

            // Assert                        
        }

        [TestMethod]
        public void WhenGetAllCalled_ThenReturnsCollection()
        {
            // Arrange
            var superPeople = new List<SuperPerson>()
            {
                new SuperPerson() { Id = 1 },
                new SuperPerson() { Id = 2 },
                new SuperPerson() { Id = 3 },                
            };

            var mockEntitySet = new Mock<IEntitySet<SuperPerson>>();

            // Note: Moq example of an mocking a method specific to an interface            

            var mockEnumerable = mockEntitySet.As<IEnumerable<SuperPerson>>();
            mockEnumerable.Setup(x => x.GetEnumerator()).Returns(superPeople.GetEnumerator());

            // Note: Moq example of an mocking the return of a get accessor
            var mockContext = new Mock<ISuperDatabaseContext>();
            mockContext.Setup(x => x.SuperPeople).Returns(mockEntitySet.Object);

            ISuperDatabaseContext context = mockContext.Object;
            SuperRepository target = new SuperRepository(context);

            // Act
            var actual = target.GetAll();

            // Assert            
            var actualList = actual.ToList();
            Assert.AreEqual(superPeople.Count, actualList.Count);

            for (int i = 0; i < superPeople.Count; i++)
            {
                Assert.AreSame(superPeople[i], actualList[i]);
            }
        }

        [TestMethod]
        public void WhenGetTeamCalled_ThenReturnsFilteredCollection()
        {
            // Arrange
            var superPeople = new List<SuperPerson>()
            {
                new SuperPerson() { Id = 1, Allegiance = "A" },
                new SuperPerson() { Id = 2, Allegiance = "B" },
                new SuperPerson() { Id = 3, Allegiance = "A" },                
            };

            var filteredSuperPeople = new List<SuperPerson>()
            {
                superPeople[0],
                superPeople[2]
            };

            var mockEntitySet = new Mock<IEntitySet<SuperPerson>>();

            var mockEnumerable = mockEntitySet.As<IEnumerable<SuperPerson>>();
            mockEnumerable.Setup(x => x.GetEnumerator()).Returns(superPeople.GetEnumerator());

            // Note: Moq example of an mocking a method specific to an interface (complexity => Reflector)           

            var mockQueryable = mockEntitySet.As<IQueryable<SuperPerson>>();
            mockQueryable.Setup(x => x.Provider).Returns(superPeople.AsQueryable().Provider);
            mockQueryable.Setup(x => x.Expression).Returns(superPeople.AsQueryable().Expression);

            var mockContext = new Mock<ISuperDatabaseContext>();
            mockContext.Setup(x => x.SuperPeople).Returns(mockEntitySet.Object);

            ISuperDatabaseContext context = mockContext.Object;
            SuperRepository target = new SuperRepository(context);

            // Act
            var actual = target.GetTeam("A");

            // Assert            
            var actualList = actual.ToList();
            Assert.AreEqual(2, actualList.Count);

            Assert.AreSame(superPeople[0], actualList[0]);
            Assert.AreSame(superPeople[2], actualList[1]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenGetTeamCalledWithNull_ThenThrows()
        {
            // Arrange                        
            var mockContext = new Mock<ISuperDatabaseContext>();

            ISuperDatabaseContext context = mockContext.Object;
            SuperRepository target = new SuperRepository(context);

            // Act
            target.GetTeam(null);

            // Assert                        
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WhenGetTeamCalledWithEmptyString_ThenThrows()
        {
            // Arrange                        
            var mockContext = new Mock<ISuperDatabaseContext>();

            ISuperDatabaseContext context = mockContext.Object;
            SuperRepository target = new SuperRepository(context);

            // Act
            target.GetTeam(string.Empty);

            // Assert                        
        }

        [TestMethod]
        public void WhenInsertCalled_ThenAddsSuperPerson()
        {
            // Arrange
            var superPerson = new SuperPerson() { Id = 1, Allegiance = "A" };

            // Note: Moq example marking a call as verifiable, then verifying all at the end

            var mockEntitySet = new Mock<IEntitySet<SuperPerson>>();
            mockEntitySet.Setup(x => x.Add(superPerson)).Verifiable();

            var mockContext = new Mock<ISuperDatabaseContext>();
            mockContext.Setup(x => x.SuperPeople).Returns(mockEntitySet.Object);

            ISuperDatabaseContext context = mockContext.Object;
            SuperRepository target = new SuperRepository(context);

            // Act
            target.Insert(superPerson);

            // Assert            
            mockEntitySet.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenInsertCalledWithNull_ThenThrows()
        {
            // Arrange                        
            var mockContext = new Mock<ISuperDatabaseContext>();            

            ISuperDatabaseContext context = mockContext.Object;
            SuperRepository target = new SuperRepository(context);

            // Act
            target.Insert(null);

            // Assert                        
        }

        [TestMethod]
        public void WhenUpdateCalled_ThenUpdatesSuperPerson()
        {
            // Arrange
            var superPerson = new SuperPerson() { Id = 1, Allegiance = "A" };

            var mockContext = new Mock<ISuperDatabaseContext>();
            mockContext.Setup(x => x.SetEntityStateModified(superPerson)).Verifiable();

            ISuperDatabaseContext context = mockContext.Object;
            SuperRepository target = new SuperRepository(context);

            // Act
            target.Update(superPerson);

            // Assert            
            mockContext.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenUpdateCalledWithNull_ThenThrows()
        {
            // Arrange                        
            var mockContext = new Mock<ISuperDatabaseContext>();

            ISuperDatabaseContext context = mockContext.Object;
            SuperRepository target = new SuperRepository(context);

            // Act
            target.Update(null);

            // Assert                        
        }

        [TestMethod]
        public void WhenDeleteCalled_ThenRemovesSuperPerson()
        {
            // Arrange
            var superPerson = new SuperPerson() { Id = 1, Allegiance = "A" };

            var mockEntitySet = new Mock<IEntitySet<SuperPerson>>();
            mockEntitySet.Setup(x => x.Remove(superPerson)).Verifiable();

            var mockContext = new Mock<ISuperDatabaseContext>();
            mockContext.Setup(x => x.SuperPeople).Returns(mockEntitySet.Object);

            ISuperDatabaseContext context = mockContext.Object;
            SuperRepository target = new SuperRepository(context);

            // Act
            target.Delete(superPerson);

            // Assert            
            mockEntitySet.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenDeleteCalledWithNull_ThenThrows()
        {
            // Arrange                        
            var mockContext = new Mock<ISuperDatabaseContext>();

            ISuperDatabaseContext context = mockContext.Object;
            SuperRepository target = new SuperRepository(context);

            // Act
            target.Delete(null);

            // Assert                        
        }

        [TestMethod]
        public void WhenSaveChangesCalled_ThenSavesChanges()
        {
            // Arrange
            var mockContext = new Mock<ISuperDatabaseContext>();
            mockContext.Setup(x => x.SaveChanges()).Verifiable();

            ISuperDatabaseContext context = mockContext.Object;
            SuperRepository target = new SuperRepository(context);

            // Act
            target.SaveChanges();

            // Assert            
            mockContext.VerifyAll();
        }
    }
}
