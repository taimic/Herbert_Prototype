using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FontChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    // The text to change
    private Text text;
    // The previous color of the text
    private Color previousColor;

    // Use this for initialization
    void Start() {
        text = transform.FindChild("Text").GetComponent<Text>();
        previousColor = text.color;
    }

    // Color the font
    public void OnPointerEnter(PointerEventData eventData){
        text.color = Color.black;
    }

    // Re-do the coloring
    public void OnPointerExit(PointerEventData eventData){
        text.color = previousColor;
    }
}