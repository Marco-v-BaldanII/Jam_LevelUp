using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverBackground : MonoBehaviour
{
    Texture2D background;
    SpriteRenderer renderer;
    void Start()
    {
        background = GameManager.Instance.screenshot;
        renderer = GetComponent<SpriteRenderer>();

        Rect rect = new Rect(0, 0, background.width, background.height);
        Sprite screenshotSprite = Sprite.Create(background, rect, new Vector2(0.5f, 0.5f), 100.0f);
        renderer.sprite = screenshotSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
