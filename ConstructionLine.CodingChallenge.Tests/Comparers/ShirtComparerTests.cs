using System;
using System.Collections.Generic;
using ConstructionLine.CodingChallenge.Comparers;
using NUnit.Framework;


namespace ConstructionLine.CodingChallenge.Tests.Comparers
{
    [TestFixture]
    public class ShirtComparerTests
    {
        [Test]
        public void ThatAShirtIsNotTheSameAsNull()
        {
            Assert.That(
                ShirtComparer.Instance.Equals(
                new Shirt(Guid.NewGuid(), "Test", Size.Large, Color.Black),
                null), Is.False);
        }

        [Test]
        public void ThatTwoShirtsWithDifferentGuidAreDifferent()
        {
            Assert.That(
                ShirtComparer.Instance.Equals(
                new Shirt(Guid.NewGuid(), "Test", Size.Large, Color.Black),
                new Shirt(Guid.NewGuid(), "Test", Size.Large, Color.Black)), Is.False);
        }

        [Test]
        public void ThatSameShirtsAreEqual()
        {
            var shirt = new Shirt(Guid.NewGuid(), "Test", Size.Large, Color.Black);
            Assert.That(ShirtComparer.Instance.Equals(shirt, shirt));
        }

        [Test]
        public void ThatTwoShirtsWithSameIdAreEqual()
        {
            var sameGuid = Guid.NewGuid();
            Assert.That(
                    ShirtComparer.Instance.Equals(
                    new Shirt(sameGuid, "Test", Size.Large, Color.Black),
                    new Shirt(sameGuid, "Test", Size.Large, Color.Black)));
        }
    }
}
