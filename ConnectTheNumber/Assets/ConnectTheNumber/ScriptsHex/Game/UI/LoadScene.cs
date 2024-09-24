using Ilumisoft.Hex;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadScene : MonoBehaviour
{
    public static LoadScene Instance;

    HexSavesystem savesystem;
    [SerializeField] private Text noInternetText; // Assign this in the inspector
    [SerializeField] private GameObject noInternetPanel; // Assign this in the inspector
    [SerializeField]
    LoadSavegameUI loadSavegameUIPrefab = null;
    [SerializeField]
    GameBoard gameBoard = null;

    
    private Vector3 initialPosition;

    private bool isShowingNoInternetMessage = false;




    private void Awake()
    {
        Instance = this;
        noInternetPanel.SetActive(false);
        initialPosition = noInternetPanel.transform.localPosition;
        // Find the HexSavesystem in the scene
        savesystem = FindObjectOfType<HexSavesystem>();
    }

  

   
    public void Restart()
    {
        AudioController.Instance.PlaySound(AudioController.Instance.click);

        SceneManager.LoadScene("GamePlay2");
    }
    public void BackMenu()
    {

        AudioController.Instance.PlaySound(AudioController.Instance.click);
        SceneManager.LoadScene("Menu");
    }

    public void ShowNoInternetMessage()
    {
        if (!isShowingNoInternetMessage)
        {
            noInternetPanel.SetActive(true);
            noInternetPanel.transform.localPosition = initialPosition; // Reset position

            StartCoroutine(HideNoInternetMessage());
        }
    }

    private IEnumerator HideNoInternetMessage()
    {
        isShowingNoInternetMessage = true;
        yield return new WaitForSeconds(1f); // Wait for 2 seconds

        // Start the animation
        StartCoroutine(AnimateNoInternetMessage());
    }

    private IEnumerator AnimateNoInternetMessage()
    {
        float duration = 0.5f;
        Vector3 initialPosition = noInternetPanel.transform.localPosition;
        Vector3 targetPosition = initialPosition + new Vector3(0, 70, 0); // Move up by 50 units
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            noInternetPanel.transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, t);
            yield return null;
        }

        noInternetPanel.SetActive(false);
        isShowingNoInternetMessage = false;
    }

    

    public void RestartAdsInter()
    {
        if (AdsController.Instance.InternAdsTime <= 0)
        {
            AdManager.instance.ShowInter(() =>
            {
                AdsController.Instance.ResetTime();
                Restart();
            },
            () =>
            {
                AdsController.Instance.ResetTime();

                Restart();
            }, "Null");
        }
        else
        {
            Restart();
        }
    }

    public void BackMenuAdsInter()
    {
        if (AdsController.Instance.InternAdsTime <= 0)
        {
            AdManager.instance.ShowInter(() =>
            {
                AdsController.Instance.ResetTime();
                BackMenu();
            },
            () =>
            {
                AdsController.Instance.ResetTime();

                BackMenu();
            }, "Null");
        }
        else
        {
            BackMenu();
        }
    }





}
