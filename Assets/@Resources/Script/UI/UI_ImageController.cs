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
        Texture
    }
    void Start()
    {
        image = GetComponent<Image>();
    }
    public void ChangeColor(Color color)
    {
        image.color = color;
    }
}
