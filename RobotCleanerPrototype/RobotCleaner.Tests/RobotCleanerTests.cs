using NUnit.Framework;


namespace RobotCleanerPrototype.Tests
{
    public class Tests
    {
        [Test]
        public void TestSimpleMove()
        {
            var robotCleaner = new RobotCleaner((0,0));

            robotCleaner.ExecuteCommand('N', 1);
            robotCleaner.ExecuteCommand('S', 1);

            Assert.AreEqual(2, robotCleaner.CleanedPlacesCount);
        }

        [Test]
        public void TestOriginalExample()
        {
            var robotCleaner = new RobotCleaner((0, 0));

            robotCleaner.ExecuteCommand('E', 2);
            robotCleaner.ExecuteCommand('N', 1);

            Assert.AreEqual(4, robotCleaner.CleanedPlacesCount);
        }

        [Test]
        public void TestEastAndWestReturn()
        {
            var robotCleaner = new RobotCleaner((0, 0));

            robotCleaner.ExecuteCommand('E', 10);
            robotCleaner.ExecuteCommand('W', 10);

            Assert.AreEqual(11, robotCleaner.CleanedPlacesCount);
        }

        [Test]
        public void TestSoundAndNorthReturn()
        {
            var robotCleaner = new RobotCleaner((0, 0));

            robotCleaner.ExecuteCommand('S', 10);
            robotCleaner.ExecuteCommand('N', 10);

            Assert.AreEqual(11, robotCleaner.CleanedPlacesCount);
        }

        [Test]
        public void TestClosedCircle()
        {
            var robotCleaner = new RobotCleaner((0, 0));

            robotCleaner.ExecuteCommand('N', 2);
            robotCleaner.ExecuteCommand('E', 3);
            robotCleaner.ExecuteCommand('N', 2);
            robotCleaner.ExecuteCommand('W', 1);
            robotCleaner.ExecuteCommand('S', 2);
            robotCleaner.ExecuteCommand('W', 2);
           

            Assert.AreEqual(10, robotCleaner.CleanedPlacesCount);
        }

        [Test]
        public void TestComplexMove()
        {
            var robotCleaner = new RobotCleaner((0, 0));

            robotCleaner.ExecuteCommand('S', 3);
            robotCleaner.ExecuteCommand('E', 4);
            robotCleaner.ExecuteCommand('N', 3);
            robotCleaner.ExecuteCommand('W', 1);

            Assert.AreEqual(12, robotCleaner.CleanedPlacesCount);
        }

        [Test]
        public void TestNegativeStartPoint()
        {
            var robotCleaner = new RobotCleaner((-100, -100));

            robotCleaner.ExecuteCommand('N', 1);
            robotCleaner.ExecuteCommand('E', 1);

            Assert.AreEqual(3, robotCleaner.CleanedPlacesCount);
        }       


        [Test]
        public void TestWrongCommand()
        {
            var robotCleaner = new RobotCleaner((0, 0));

            robotCleaner.ExecuteCommand('H', 3);

            Assert.AreEqual(1, robotCleaner.CleanedPlacesCount);
        }
    }
}