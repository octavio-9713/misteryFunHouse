using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnPickedItem : MonoBehaviour
{
    public Text textName;
    public Text textDesc;

    [Header ("Speed of text writting")]
    public float typeWritterSpeed = 0.05f;

    //TODO: Agregar Escritura lenta de letras...

    //TODO: Terminar pickups...
    public void SetText(string name, string desc)
    {
        //textName.text = name;
        textDesc.text = "";

        StopAllCoroutines();
        StartCoroutine(TypeWritterEffect(desc));
    }

    public void OnFinishAnimation()
    {
        Destroy(gameObject);
    }

    IEnumerator TypeWritterEffect(string text)
    {
        yield return new WaitForSeconds(.3f);
        foreach (char character in text.ToCharArray())
        {
            textDesc.text += character;
            yield return new WaitForSeconds(typeWritterSpeed);
        }

        GetComponent<Animator>().SetBool("fade", true);
    }
}
