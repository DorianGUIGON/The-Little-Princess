using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : MonoBehaviour
{

    [System.Serializable]
    public class Character
    {
        public GameObject obj;
        public Sprite coloredSprite;
        public Sprite greySprite;
    }

    public List<Character> characters = new List<Character>();

    void Start()
    {
        Debug.Log("CharactersManager est chargé dans la scène : " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        UpdateAllCharacters();
    }

    public void UpdateAllCharacters()
    {
        foreach (var character in characters)
        {
            bool isColored = GameState.isColored();
            SpriteRenderer spriteRenderer = character.obj.GetComponent<SpriteRenderer>();
            if (isColored || character.greySprite == null)
            {
                spriteRenderer.sprite = character.coloredSprite;
                Debug.Log("Sprite color applied: " + character.coloredSprite.name);
            }
            else
            {
                spriteRenderer.sprite = character.greySprite;
                Debug.Log("Sprite grey applied: " + character.greySprite.name);
            }
        }
    }
}
