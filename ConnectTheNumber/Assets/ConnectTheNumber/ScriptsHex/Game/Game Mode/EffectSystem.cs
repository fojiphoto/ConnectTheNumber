using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class EffectSystem : MonoBehaviour
{
    public static EffectSystem Instance;

    [SerializeField] private Text mergeText;

    [SerializeField] private GameObject effect;

    private void Awake()
    {
        Instance = this;

        // Ensure the text starts as inactive
        mergeText.gameObject.SetActive(false);
        mergeText.transform.localScale = Vector3.zero; // Start with scale 0
    }

    public void SpawnEffect(Transform Pos)
    {
        if (effect == null)
        {
            return;
        }

        Instantiate(effect, Pos);
    }

    public void UpdateMergeText(int mergeCount, Color objectColor)
    {
        if (mergeCount == 2)
        {
            mergeText.text = "GOOD!";
        }
        else if (mergeCount > 3 && mergeCount <= 6)
        {
            mergeText.text = "AWESOME!";
        }
        else if (mergeCount > 6)
        {
            mergeText.text = "EXCELLENT!";
        }

        // Set text color to match the object's color
        mergeText.color = objectColor;

        // Start the DOTween animation coroutine
        StartCoroutine(ShowTextTemporarily());
    }

    private IEnumerator ShowTextTemporarily()
    {
        mergeText.gameObject.SetActive(true); 
        mergeText.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBack);
        
        yield return new WaitForSeconds(0.5f); 

        mergeText.transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InBack).OnComplete(() =>
        {
            mergeText.gameObject.SetActive(false);
        });
    }

    private Color GetRandomColor()
    {
        return new Color(
            Random.Range(0f, 1f),  // Random red component
            Random.Range(0f, 1f),  // Random green component
            Random.Range(0f, 1f)   // Random blue component
        );
    }



}
