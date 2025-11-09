namespace set_game_lib;

public class Deck
{
    private readonly Random _random = new();

    // All 81 cards in the deck: true if already pulled from deck, false otherwise
    private readonly List<bool> _deck = [..new bool[81]];

    public void ReturnCardsToDeck(List<Card> cards)
    {
        foreach (var card in cards)
        {
            _deck[card.GetHashCode()] = false;
        }
    }
    
    // Draws n unique random cards not currently on the board
    public List<Card> DrawCards(int quantity)
    {
        List<Card> cards = [];
        
        for (int i = 0; i < quantity; ++i)
        {
            var card = DrawCard();
            if (card is null)
                break;
            
            cards.Add(card);
        }

        return cards;
    }
    
    // draw a random card from the deck that has not been used yet and mark that card as used
    private Card? DrawCard()
    {
        var availableCardHashValues = _deck
            .Select((used, index) => new { used, index })
            .Where(card => !card.used)
            .Select(card => card.index)
            .ToList();

        if (availableCardHashValues.Count == 0)
            return null;

        int cardHash = availableCardHashValues[_random.Next(0, availableCardHashValues.Count)];

        _deck[cardHash] = true;
        return new Card(cardHash);
    }
}
