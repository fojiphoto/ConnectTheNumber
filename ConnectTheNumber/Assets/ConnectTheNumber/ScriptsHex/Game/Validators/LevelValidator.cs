namespace Ilumisoft.Hex
{
    public class LevelValidator : IConnectionValidator
    {
        ISelection selection;

        public LevelValidator(ISelection selection)
        {
            this.selection = selection;
        }

        public bool IsValid(GameTile first, GameTile second)
        {
            if (first is ICanLevelUp firstLevel && second is ICanLevelUp secondLevel)
            {
                return AreEqual(firstLevel, secondLevel) || (selection.Count>=2 && AreSequel(firstLevel, secondLevel));
            }

            return false;
        }

        private bool AreSequel(ICanLevelUp firstLevel, ICanLevelUp secondLevel)
        {
            return (firstLevel.CurrentLevel + 1 == secondLevel.CurrentLevel);
        }

        private bool AreEqual(ICanLevelUp firstLevel, ICanLevelUp secondLevel)
        {
            return (firstLevel.CurrentLevel == secondLevel.CurrentLevel);
        }
    }
}