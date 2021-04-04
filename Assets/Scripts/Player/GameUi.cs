using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUi : MonoBehaviour
{ 
    public Sprite panel;

    public Life lifeControl;

    private Image _mainImage;

    public void Start()
    {
        _mainImage = GetComponent<Image>();
    }

    public void ChangePanel(Sprite sprite)
    {
        if (_mainImage == null)
            _mainImage = GetComponent<Image>();

        _mainImage.sprite = sprite;
    }
}
