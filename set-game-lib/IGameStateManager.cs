namespace set_game_lib;

public interface IGameStateManager
{
    List<Card> GenerateInitialBoard();
    
    List<Card> ExpandBoard();
    
    List<Card> SubmitCards(List<Card> selectedCards);
}
