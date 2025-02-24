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
    Color selectedColor = Color.white;
    public Color SelectedColor
    {
        get { return GetColor(); }
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
        Debug.Log(pickerPos);
        picker.rectTransform.position = pickerPos;
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
        Debug.Log(pickerPos);
        picker.rectTransform.position = pickerPos;
        GetColor();
    }

    Color GetColor()
    {
        Vector2 position = (Vector2)picker.transform.position - (Vector2)pallete.transform.position + sizeOfPallete / 2;

        Vector2 normalized = new Vector2(
            (position.x / (pallete.GetComponent<RectTransform>().rect.width)),
            (position.y / (pallete.GetComponent<RectTransform>().rect.height)));

        Texture2D texture = pallete.mainTexture as Texture2D;
        Color circularSelectedColor = texture.GetPixelBilinear(normalized.x, normalized.y);
        selectedColor = circularSelectedColor;
        return circularSelectedColor;
    }
}
