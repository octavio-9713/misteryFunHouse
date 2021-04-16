using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Dialog
{
    [Header("Dialog")]
    public String dialog;

    [Header("Character Face")]
    public Sprite faceSprite;

    [Header("Activates Event")]
    public bool firesEvent;
}
