using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public Button button;
    public Sprite selectImage;
    public Sprite normalImage;

    public void SelectButton()
    {
        button.image.sprite = selectImage;
    }

    public void DeselectButton()
    {
        button.image.sprite = normalImage;
    }
}
