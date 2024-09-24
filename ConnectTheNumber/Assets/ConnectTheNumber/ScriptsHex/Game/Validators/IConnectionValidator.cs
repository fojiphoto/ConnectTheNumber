namespace Ilumisoft.Hex
{
    public interface IConnectionValidator
    {
        bool IsValid(GameTile first, GameTile second);
    }
}