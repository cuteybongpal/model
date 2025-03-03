using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ColorPicker : MonoBehaviour
{
    Image picker;
    Image pallete;
    Vector2 sizeOfPallete = Vector2.zero;
    public Color selectedColor = Color.white;
    public Color SelectedColor
    {
        get { return selectedColor; }
        set
        {
            selectedColor = value;
            UserManager.Instance.CurrentColor = selectedColor;
        }
    }
    void Start()
    {
        pallete = transform.GetChild(0).gameObject.GetComponent<Image>();
        picker = transform.GetChild(1).gameObject.GetComponent<Image>();
        sizeOfPallete = new Vector2(pallete.rectTransform.rect.width, pallete.rectTransform.rect.height);
    }
    

    public void OnDrag(BaseEventData eventData)
    {
        PointerEventData mouseEvent = eventData as PointerEventData;
        

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(),
            mouseEvent.position, null, out localPoint);
        Vector2 pickerPos = new Vector2(
            Mathf.Clamp(localPoint.x + pallete.rectTransform.position.x, pallete.rectTransform.position.x - sizeOfPallete.x / 2, pallete.rectTransform.position.x + sizeOfPallete.x / 2),
            Mathf.Clamp(localPoint.y + pallete.rectTransform.position.y, pallete.rectTransform.position.y - sizeOfPallete.y / 2, pallete.rectTransform.position.y + sizeOfPallete.y / 2)
            );
        picker.rectTransform.position = pickerPos;
        GetColor();
    }
    public void OnPointerDown(BaseEventData eventData)
    {
        PointerEventData mouseEvent = eventData as PointerEventData;

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(),
            mouseEvent.position, null, out localPoint);
        Vector2 pickerPos = new Vector2(
            Mathf.Clamp(localPoint.x + pallete.rectTransform.position.x, pallete.rectTransform.position.x - sizeOfPallete.x / 2, pallete.rectTransform.position.x + sizeOfPallete.x / 2),
            Mathf.Clamp(localPoint.y + pallete.rectTransform.position.y, pallete.rectTransform.position.y - sizeOfPallete.y / 2, pallete.rectTransform.position.y + sizeOfPallete.y / 2)
            );
        picker.rectTransform.position = pickerPos;
        GetColor();
    }

    Color GetColor()
    {
        Vector2 position = (Vector2)picker.rectTransform.position - (Vector2)pallete.rectTransform.position + sizeOfPallete / 2;

        // 좌표를 0~1 범위로 정규화
        float uvX = position.x / sizeOfPallete.x;
        float uvY = position.y / sizeOfPallete.y;

        // 텍스처에서 색상 샘플링 (GetPixelBilinear 사용)
        Texture2D texture = pallete.mainTexture as Texture2D;
        if (texture == null)
        {
            Debug.LogError("Palette texture is missing!");
            return Color.white;
        }

        Color circularSelectedColor = texture.GetPixelBilinear(uvX, uvY);
        SelectedColor = circularSelectedColor;
        
        return circularSelectedColor;
    }
}
