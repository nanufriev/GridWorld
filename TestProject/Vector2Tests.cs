using GridWorld;

namespace Vector2Tests
{
    public class Vector2Tests
    {
        [Test]
        public void TestDistanceToWithPositiveCoordinates()
        {
            var point1 = new Vector2(1, 2);
            var point2 = new Vector2(4, 6);
            
            var expectedDistance = 7.0;
            var actualDistance = point1.DistanceTo(point2);

            Assert.That(actualDistance, Is.EqualTo(expectedDistance));
        }

        [Test]
        public void TestDistanceToWithNegativeCoordinates()
        {
            var point1 = new Vector2(-1, -2);
            var point2 = new Vector2(-4, -6);

            var expectedDistance = 7.0;
            var actualDistance = point1.DistanceTo(point2);

            Assert.That(actualDistance, Is.EqualTo(expectedDistance));
        }

        [Test]
        public void TestEquality()
        {
            var point1 = new Vector2(1, 2);
            var point2 = new Vector2(1, 2);
            var point3 = new Vector2(2, 1);

            Assert.IsTrue(point1.Equals(point2));
            Assert.IsFalse(point1.Equals(point3));
        }

        [Test]
        public void TestNonEqualityWithOtherObject()
        {
            var point1 = new Vector2(1, 2);
            var otherObject = new object();

            Assert.IsFalse(point1.Equals(otherObject));
        }
    }
}