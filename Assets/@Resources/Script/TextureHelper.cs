using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

public class TextureHelper
{
    public byte[] ChangeTextureColor(Material texture, Color color)
    {
        Texture2D t = texture.mainTexture as Texture2D;
        Texture2D tex = new Texture2D(t.width, t.height);
        //Texture2D는 참조 타입이기 때문에 복사 해준다.
        for (int y = 0; y < tex.height; y++)
        {
            for (int x = 0; x < tex.width; x++)
            {
                Color pixelColor = t.GetPixel(x, y);
                tex.SetPixel(x, y, pixelColor);
            }
        }
        tex.Apply();
        for (int y = 0; y < tex.height; y++)
        {
            for (int x = 0; x < tex.width; x++)
            {
                Color pixelColor = tex.GetPixel(x, y);
                tex.SetPixel(x, y, pixelColor * color);
            }
        }
        tex.Apply();
        byte[] binaryImage = tex.EncodeToPNG();
        return binaryImage;
    }
    public byte[] RenderTextureToByteArray(RenderTexture renderTexture)
    {
        Texture2D texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);



        texture2D.ReadPixels(new Rect(0,0, renderTexture.width, renderTexture.height), 0,0);
        return null;
    }
}
