using UnityEngine;

namespace Ilumisoft.Hex
{
    public class WaitForTileMovement : CustomYieldInstruction
    {
        GameBoardMovement boardMovement;

        public WaitForTileMovement(IGameBoard gameBoard)
        {
            this.boardMovement = new GameBoardMovement(gameBoard);
        }

        public override bool keepWaiting
        {
            get
            {
                return boardMovement.IsAnyGameTileMoving();
            }
        }
    }
}