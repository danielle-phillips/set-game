namespace set_game_lib;

public class Card
{
    public List<sbyte> Attributes { get; private set; }

    // generate a random card
    public Card()
    {
        Attributes = new List<sbyte>();
        Random random = new Random();
        
        for (var i = 0; i < 4; i++)
        {
            Attributes.Add((sbyte)random.Next(0, 2)); 
        }
    }

    public Card(int hashValue)
    {
        // Reverse-hash of a card: array form of its value in base-3
        if (hashValue is < 0 or > 80)
        {
            throw new ArgumentOutOfRangeException(nameof(hashValue));
        }
        
        Attributes = new List<sbyte>( new sbyte[4] );

        var runningCount = hashValue;
        
        for (int i = Attributes.Count - 1; i >= 0; i--)
        {
            // grab both the quotient and remainder of the current number and 3.
            // The current least significant digit (i) is equal to the remainder.
            int quotient = Math.DivRem(runningCount, 3, out int remainder);
            Attributes[i] = (sbyte)remainder;
            runningCount = quotient;
        }
    }

    public Card(IList<sbyte> inputAttributes)
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
        
        for (int i = 0; i < Attributes.Count; i++)
        {
            hashedValue += Attributes[i] * (int)Math.Pow(3, Attributes.Count - 1 - i);
        }
        
        return hashedValue;
    }

    private bool Equals(Card other)
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
        var resultingAttributes = new List<sbyte>();
        
        for (int i = 0; i < left.Attributes.Count; i++)
        {
            if (left.Attributes[i] == right.Attributes[i])
            {
                resultingAttributes.Add(left.Attributes[i]);
            }
            else
            {
                resultingAttributes.Add((sbyte)(3 - (left.Attributes[i] + right.Attributes[i])));
            }
        }
        
        return new Card(resultingAttributes);
    }
}