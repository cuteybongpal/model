using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ImageController : MonoBehaviour
{
    Image image;

    public ImageType type;

    public enum ImageType
    {
        Color,
        Texture,
        CurrentColor,
        Undo,
        Redo,
        None
    }
    public int ColorNum;

    void Start()
    {
        image = GetComponent<Image>();
    }
    public void ChangeColor(Color color)
    {
        //Debug.Log($"»ö±ò {color}");
        image.color = color;
    }

    public void ChangeImage(Material material)
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }
        Texture2D texture = material.mainTexture as Texture2D;
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        image.sprite = sprite;
    }
}
