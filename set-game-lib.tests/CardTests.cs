namespace set_game_lib.tests;

[TestFixture]
public class CardTests
{
    [TestFixture]
    public class EqualityComparisonTests : CardTests
    {
        [Test]
        public void CardsAreMarkedUnequal()
        {
            Card card1 = new(new List<int> { 0, 1, 2, 0 });
            Card card2 = new(new List<int> { 2, 2, 2, 2 });
        
            Assert.That(card1.Equals(card2), Is.False);
            Assert.That(card1 != card2, Is.True);
        }

        [Test]
        public void CardsAreMarkedEqual()
        {
            Card card1 = new(new List<int> { 1, 1, 1, 1 });
            Card card2 = new(new List<int> { 1, 1, 1, 1 });
        
            Assert.That(card1.Equals(card2), Is.True);
            Assert.That(card1 == card2, Is.True);
        }
    }

    [TestFixture]
    public class SetOperationTests : CardTests
    {
        [Test]
        public void SetOperationFindsThirdCard()
        {
            Card card1 = new(new List<int> { 0, 1, 2, 0 });
            Card card2 = new(new List<int> { 2, 2, 2, 2 });
            Card card3 = new(new List<int> { 1, 0, 2, 1 });
        
            Assert.That(card1 * card2, Is.EqualTo(card3));
        }
        
        [Test]
        public void SetOperationFindsOnlyThirdCard()
        {
            Card card1 = new(new List<int> { 0, 1, 2, 0 });
            Card card2 = new(new List<int> { 2, 2, 2, 2 });
            Card card3 = new(new List<int> { 1, 1, 1, 1 });
        
            Assert.That(card1 * card2, !Is.EqualTo(card3));
        }
    }
    
    [TestFixture]
    public class CardHashingTests : CardTests
    {
        [Test]
        public void HashingTest1()
        {
            Card card = new(new List<int> { 0, 1, 2, 0 });
        
            Assert.That(card.GetHashCode(), Is.EqualTo(15));
        }
        
        [Test]
        public void HashingTest2()
        {
            Card card = new(new List<int> { 0, 0, 0, 0 });
        
            Assert.That(card.GetHashCode(), Is.EqualTo(0));
        }

        [Test]
        public void HashingTest3()
        {
            Card card = new(new List<int> { 2, 2, 2, 2 });
        
            Assert.That(card.GetHashCode(), Is.EqualTo(80));
        }
        
        [Test]
        public void ReverseHashingTest1()
        {
            var card = new Card(80);
        
            Assert.That(card, Is.EqualTo(new Card([2, 2, 2, 2])));
        }

        [Test]
        public void ReverseHashingTest2()
        {
            var card = new Card(0);
        
            Assert.That(card, Is.EqualTo(new Card([0, 0, 0, 0])));
        }

        [Test]
        public void ReverseHashingTest3()
        {
            var card = new Card(15);
        
            Assert.That(card, Is.EqualTo(new Card([0, 1, 2, 0])));
        }
    }
}
