using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Ilumisoft.Hex
{
    public class TileRenderer : MonoBehaviour
    {
        [SerializeField]
        SpriteRenderer spriteRenderer = null;

        [SerializeField]
        Text textMesh = null;


        HexGameTile gameTile;

        TileLevelBehaviour levelBehaviour;

        private void Awake()
        {
            gameTile = GetComponent<HexGameTile>();
            gameTile.OnTileDestroyed += OnTileDestroyed;
            gameTile.OnLevelChanged += OnLevelChanged;

            levelBehaviour = gameTile.GetComponent<TileLevelBehaviour>();
        }

        private void OnLevelChanged()
        {
            spriteRenderer.color = levelBehaviour.Color;
            float a = Mathf.Pow(2, levelBehaviour.CurrentLevel + 1);
            textMesh.text = a.ToString();
            
            
            
        }

        

        private void OnTileDestroyed(GameTile tile)
        {
            spriteRenderer.gameObject.SetActive(false);
        }
    }
}