using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace IssueWithMockVerification
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void SendTwoSoldierAntsOutForFood_ExpectTwoAntsWithDifferentIdsToLeaveTheAntHill()
        {
            //Arrange
            var outsideWorldMock = new Mock<IOutsideWorld>();
            var antHill = new AntHill(outsideWorldMock.Object);

            outsideWorldMock.Setup(m => m.SendOut(It.IsAny<Ant>())).Verifiable();

            //Act
            antHill.SendOutTwoSoldierAnts();

            //Assert
            outsideWorldMock.Verify(m => m.SendOut(It.Is<Ant>(a => a.Id.Equals(1))), Times.Once);
            outsideWorldMock.Verify(m => m.SendOut(It.Is<Ant>(a => a.Id.Equals(2))), Times.Once);
        }

        [TestMethod]
        public void SendTwoSoldierAntsOutForFood_ExpectTwoSoldierAntsToLeaveTheAntHill()
        {
            //Arrange
            var outsideWorldMock = new Mock<IOutsideWorld>();
            var antHill = new AntHill(outsideWorldMock.Object);

            //Act
            antHill.SendOutTwoSoldierAnts();

            //Assert
            outsideWorldMock.Verify(m => m.SendOut(It.Is<Ant>(a => a.Type.Equals("Soldier Ant"))), Times.Exactly(2));
        }

        [TestMethod]
        public void JustRunTheCode_ExpectTwoAntsWithDifferentIdsInTheTestOutput()
        {
            //Arrange
            var outsideWorldMock = new Mock<IOutsideWorld>();
            var antHill = new AntHill(outsideWorldMock.Object);

            //Act
            antHill.SendOutTwoSoldierAnts();

            //Assert = see test output
        }
    }
}