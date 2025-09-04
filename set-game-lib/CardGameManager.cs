namespace set_game_lib;

public class CardGameManager
{
    
    
    public static bool IsSet(Card card1, Card card2, Card card3)
    {
        for (int i = 0; i < card1.Attributes.Count; i++)
        {
            if (card1.Attributes[i] == card2.Attributes[i] && card2.Attributes[i] == card3.Attributes[i])
                continue;

            if (card1.Attributes[i] + card2.Attributes[i] + card3.Attributes[i] == 3)
                continue;

            return false;
        }

        return true;
    }
}
