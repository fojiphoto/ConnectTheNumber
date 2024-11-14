using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiMainMenu : MonoBehaviour
{

    private void Awake()
    {
       // AdManager.instance.ShowBanner();
    }

    [SerializeField] private GameObject helpPopup;
    public void PlayGame()
    {
        ColorConnectAudioController.Instance.PlaySound(ColorConnectAudioController.Instance.click);
        SceneManager.LoadScene("GamePlay2");
    }

    public void Help()
    {
        ColorConnectAudioController.Instance.PlaySound(ColorConnectAudioController.Instance.click);
        helpPopup.SetActive(true);
    }

    public void BackMenu()
    {
        ColorConnectAudioController.Instance.PlaySound(ColorConnectAudioController.Instance.click);

        helpPopup.SetActive(false);
    }

    
}
