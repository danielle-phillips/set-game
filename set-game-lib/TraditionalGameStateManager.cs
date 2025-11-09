namespace set_game_lib;

public class TraditionalGameStateManager(bool infiniteMode = false) : IGameStateManager
{
    private Deck _deck = new Deck();
    
    // draw 12 cards at the beginning of the game
    public List<Card> GenerateInitialBoard()
    {
        return _deck.DrawCards(12);
    }
    
    // if cards make a Set, place cards back in deck and pull 3 new cards
    public List<Card> SubmitCards(List<Card> selectedCards)
    {
        if (selectedCards.Count != 3)
            throw new InvalidOperationException("Must have exactly 3 selected cards");
        
        if (selectedCards[0] * selectedCards[1] != selectedCards[2])
            return [];
        
        var newCards = _deck.DrawCards(3);
        
        if (infiniteMode)
        {
            _deck.ReturnCardsToDeck(selectedCards);
        }
        
        return newCards;
    }
    
    // pull 3 additional cards if there are no found Sets on the board
    public List<Card> ExpandBoard()
    {
        return _deck.DrawCards(3);
    }
}
