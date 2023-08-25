using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController : Singleton<BGController>
{
    public Sprite[] sprites;

    public SpriteRenderer bgImage;

    public override void Awake()
    {
        MakeSingleton(false);
    }

    public void ChangeSprite()
    {
        if(bgImage != null && sprites != null && sprites.Length > 0)
        {
            int randomIdx = Random.Range(0, sprites.Length);

            if(sprites[randomIdx] != null)
            {
                bgImage.sprite = sprites[randomIdx];
            }
        } 
    }

    // Start is called before the first frame update
    public override void Start()
    {
        ChangeSprite();
    }

}
