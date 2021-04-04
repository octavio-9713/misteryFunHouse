using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnPickedItem : MonoBehaviour
{
    public Text textName;
    public Text textDesc;

    public void SetText(string name, string desc)
    {
        this.textName.text = name;
        this.textDesc.text = desc;
    }
}
