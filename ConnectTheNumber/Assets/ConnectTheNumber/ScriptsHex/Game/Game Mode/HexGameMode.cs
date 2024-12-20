using Ilumisoft.Hex.Events;
using Ilumisoft.Hex.Operations;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Ilumisoft.Hex
{
    public class HexGameMode : GameMode
    {
        [SerializeField]
        GameBoard gameBoard = null;

        [SerializeField]
        SelectionLineRenderer lineRenderer = null;

        [SerializeField]
        LoadSavegameUI loadSavegameUIPrefab = null;

        ISelection selection;

        IGameOverCheck gameOverCheck;

        

        

        OperationQueue operations = new OperationQueue();

        HexSavesystem savesystem;


        private void Awake()
        {
            savesystem = FindObjectOfType<HexSavesystem>();

            selection = new LineSelection(lineRenderer);
            gameOverCheck = new HexGameOverCheck(gameBoard);

            operations.Clear();
            operations.Add(new ProcessInput(gameBoard, selection));

            operations.Add(new MergeSelection(gameBoard, selection));
            operations.Add(new ProcessVerticalMovement(gameBoard));
            operations.Add(new FillEmptyCells(gameBoard));

           
            
        }


        public void Revive()
        {
            ColorConnectAudioController.Instance.PlaySound(ColorConnectAudioController.Instance.click);
            AdsManager.instance.ShowRewardedAd(() =>
            {
                PlayerPrefs.SetInt("isReviving", 1); // Đặt isReviving thành true
                PlayerPrefs.Save();
                SceneManager.LoadScene("GamePlay2");
            });
            
            //NAdeem ads Colorconnect
            //if (Application.internetReachability == NetworkReachability.NotReachable)
            //{

            //    LoadScene.Instance.ShowNoInternetMessage();
            //    return;
            //}
            //AdManager.instance.ShowReward(() =>
            //{
            //    PlayerPrefs.SetInt("isReviving", 1); // Đặt isReviving thành true
            //    PlayerPrefs.Save();
            //    SceneManager.LoadScene("GamePlay2");
            //}, () =>
            //{

            //}, "YourPlacementID");
            
        }

        public void Reload()
        {
            ColorConnectAudioController.Instance.PlaySound(ColorConnectAudioController.Instance.click);
            PlayerPrefs.SetInt("isReloading", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene("GamePlay2");
        }

        public void ReloadAdsInter()
        {
            AdsManager.instance.ShowInterstitialWithoutConditions("");
            Reload();
           // Nadeem Ads ColorConnect
            //if (AdsController.Instance.InternAdsTime <= 0)
            //{
            //    AdManager.instance.ShowInter(() =>
            //    {
            //        AdsController.Instance.ResetTime();
            //        Reload();
            //    },
            //    () =>
            //    {
            //        AdsController.Instance.ResetTime();

            //        Reload();
            //    }, "Null");
            //}
            //else
            //{
            //    Reload();
            //}
        }

        

        


        public void ResetBoard()
        {
            Score.Reset();

            GameBoard.MaxReachedLevel = 3;
            

            gameBoard.Spawn();
        }


        

        public void ResetBoardRV()
        {
            
            var json = PlayerPrefs.GetString("Savestate");
            var savestate = JsonUtility.FromJson<Savestate>(json);

            GameBoard.MaxReachedLevel = savestate.MaxReachedLevel;
            Score.Value = savestate.Score;
            Score.OnScoreChanged?.Invoke(Score.Value);

            GameBoard.MaxReachedLevel = 3;
            

            gameBoard.Spawn();
        }




        public override IEnumerator StartGame()
        {
            bool isReviving = PlayerPrefs.GetInt("isReviving", 0) == 1;

            bool isReloading = PlayerPrefs.GetInt("isReloading", 0) == 1;

            if (!isReviving && !isReloading && savesystem.HasSavestate())
            {
                var savegameUI = Instantiate(loadSavegameUIPrefab);

                yield return savegameUI.Execute(savesystem.Load, ResetBoard);

                Destroy(savegameUI.gameObject);
            }
            else
            {
                if (isReviving)
                {
                    // Logic nếu người chơi revive
                    ResetBoardRV();
                    
                    PlayerPrefs.SetInt("isReviving", 0); // Reset lại giá trị để không revive lần sau
                }
                else if (isReloading)
                {
                    // Logic for when the player reloads
                    ResetBoard();
                    PlayerPrefs.SetInt("isReloading", 0); // Reset the flag so it doesn't reload next time
                }
                else
                {
                    ResetBoard();
                }
            }

            yield return null;
        }




        public override IEnumerator RunGame()
        {
           
            while (IsGameOver() == false)
            {
                
                savesystem.Save();

                yield return new WaitForInput();

                yield return operations.Execute();

            }

            yield return new WaitForSeconds(1);
        }

        public override IEnumerator EndGame()
        {
            GameEvents<UIEventType>.Trigger(UIEventType.GameOver);

            savesystem.ClearSavestate();

            yield return null;
        }

        bool IsGameOver()
        {
            return gameOverCheck.IsGameOver();
        }
    }
}