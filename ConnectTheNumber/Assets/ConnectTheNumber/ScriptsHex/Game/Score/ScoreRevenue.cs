namespace Ilumisoft.Hex
{
    public struct ScoreRevenue
    {
        ISelection selection;

        public ScoreRevenue(ISelection selection)
        {
            this.selection = selection;
        }

        public int GetValue()
        {
            if (selection.Count > 0)
            {
                int multiplier = selection.Count;
                int value = GetValue(selection.GetLast());

                return value * multiplier;
            }

            return 0;
        }

        private int GetValue(GameTile gameTile)
        {
            int result = 0;

            if (gameTile is ICanLevelUp canLevelUp)
            {
                result = canLevelUp.CurrentLevel + 1;
            }

            return result;
        }
    }
}