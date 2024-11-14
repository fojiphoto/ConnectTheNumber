using Ilumisoft.Hex.Events;
using System.Collections;
using UnityEngine;
using TMPro;

namespace Ilumisoft.Hex.Operations
{
    public class MergeSelection : IOperation
    {
        IGameBoard gameBoard;
        ISelection selection;
        IValidator selectionValidator;
        
        public MergeSelection(IGameBoard gameBoard, ISelection selection)
        {
            this.gameBoard = gameBoard;
            this.selection = selection;
            this.selectionValidator = new SelectionValidator(selection);
            

        }

        public IEnumerator Execute()
        {
            // Cancel if the selection is not valid
            if (selectionValidator.IsValid == false)
            {
                selection.Clear();
                yield break;
            }
            int mergeCount = selection.Count; // Get the number of merged objects
            int sum = GetSelectionSum();


            int newLevel = (int)Mathf.Log(sum, 2);

            var last = selection.GetLast();



            ClearSelectionLine();

            ColorConnectAudioController.Instance.PlaySound(ColorConnectAudioController.Instance.merge);


            /*if (AdsController.Instance.InternAdsTime <= 0)
            {
                AdManager.instance.ShowInter(() =>
                {
                    AdsController.Instance.ResetTime();
                    MoveSelected(last.transform.position);

                },
                () =>
                {
                    AdsController.Instance.ResetTime();
                    MoveSelected(last.transform.position);


                }, "Null");
            }
            else
            {
                MoveSelected(last.transform.position);

            }*/

            MoveSelected(last.transform.position);

            Score.Add(sum);
            TileLevelBehaviour tileLevelBehaviour = last.GetComponent<TileLevelBehaviour>();
            if (tileLevelBehaviour != null)
            {
                Color objectColor = tileLevelBehaviour.Color;
                EffectSystem.Instance.UpdateMergeText(mergeCount, objectColor); // Pass the color to update the UI
            }
            yield return new WaitForTileMovement(gameBoard);

            LevelUp(last, newLevel);

            ColorConnectAudioController.Instance.PlaySound(ColorConnectAudioController.Instance.pop);


            // Spawn effect after merging is complete
            if (EffectSystem.Instance != null)
            {
                EffectSystem.Instance.SpawnEffect(last.transform);  // Spawn the effect at the last tile's position
            }





            PopSelected();

            selection.Clear();

            

            yield return new WaitForSeconds(0.2f);

        }




        private int GetSelectionSum()
        {
            int result = 0;

            for (int i = 0; i < selection.Count; i++)
            {
                var tileLevel = selection.Get(i).GetComponent<ICanLevelUp>().CurrentLevel;

                result += (int)Mathf.Pow(2, tileLevel);
            }

            return result;
            
        }

        private void LevelUp(GameTile gameTile, int newLevel)
        {
            if (gameTile is ICanLevelUp canLevelUp)
            {
                canLevelUp.CurrentLevel = newLevel;

                selection.Remove(gameTile);

                if (newLevel > GameBoard.MaxReachedLevel)
                {
                    GameBoard.MaxReachedLevel = newLevel;
                }
            }
        }

        private void ClearSelectionLine()
        {
            if (selection is LineSelection lineSelection)
            {
                lineSelection.ClearLine();
            }
        }

        private void MoveSelected(Vector3 position)
        {

            for (int i = 0; i < selection.Count - 1; i++)
            {
                var gameTile = selection.Get(i);

                if (gameTile is ICanMoveTo canMoveTo)
                {
                    canMoveTo.MoveTo(position, 0.2f);
                }
            }
        }

        private void PopSelected()
        {
            for (int i = 0; i < selection.Count; i++)
            {
                var gameTile = selection.Get(i);

                gameTile.Pop();
            }
        }


    }
}