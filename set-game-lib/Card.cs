namespace set_game_lib;

public class Card
{
    public List<int> Attributes { get; private set; }

    public Card()
    {
        // generate a random card
        Attributes = new List<int>();
        Random random = new Random();
        
        for (var i = 0; i < 4; i++)
        {
            Attributes.Add(random.Next(0, 2)); 
        }
    }

    public Card(IList<int> inputAttributes)
    {
        if (inputAttributes.Count != 4)
            throw new Exception("Card attribute count doesn't match number of allowed attributes");

        foreach (var attribute in inputAttributes)
        {
            if (attribute is < 0 or > 2)
                throw new Exception($"Card contains value that is out of bounds: {attribute}");
        }
        
        Attributes = inputAttributes.ToList();
    }
    
    public override int GetHashCode()
    {
        // there are only 3^4 (81) possible cards
        var hashedValue = 0;
        
        for (int i = 0; i < 4; i++)
        {
            hashedValue += Attributes[i] * (int)Math.Pow(3, 4 - 1 - i);
        }
        
        return hashedValue;
    }

    protected bool Equals(Card other)
    {
        return Attributes.SequenceEqual(other.Attributes);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Card)obj);
    }

    public static bool operator ==(Card? left, Card? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Card? left, Card? right)
    {
        return !Equals(left, right);
    }
    
    // Returns the unique third card that creates a Set out of all three cards.
    public static Card operator *(Card left, Card right)
    {
        var resultingAttributes = new List<int>();
        
        for (int i = 0; i < left.Attributes.Count; i++)
        {
            if (left.Attributes[i] == right.Attributes[i])
            {
                resultingAttributes.Add(left.Attributes[i]);
            }
            else
            {
                resultingAttributes.Add(3 - (left.Attributes[i] + right.Attributes[i]));
            }
        }
        
        return new Card(resultingAttributes);
    }
}