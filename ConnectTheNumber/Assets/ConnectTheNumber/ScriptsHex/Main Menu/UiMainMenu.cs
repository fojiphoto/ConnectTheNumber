using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiMainMenu : MonoBehaviour
{

    private void Awake()
    {
        AdManager.instance.ShowBanner();
    }

    [SerializeField] private GameObject helpPopup;
    public void PlayGame()
    {
        AudioController.Instance.PlaySound(AudioController.Instance.click);
        SceneManager.LoadScene("GamePlay2");
    }

    public void Help()
    {
        AudioController.Instance.PlaySound(AudioController.Instance.click);
        helpPopup.SetActive(true);
    }

    public void BackMenu()
    {
        AudioController.Instance.PlaySound(AudioController.Instance.click);

        helpPopup.SetActive(false);
    }

    
}
