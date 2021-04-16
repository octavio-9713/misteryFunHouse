using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingText : MonoBehaviour
{
    public Text dialog;

    private string _line = "LOADING . . .";

    private bool _isTalking = false;

    [Header("Speed of text writting")]
    public float typeWritterSpeed = 0.05f;

    // Update is called once per frame
    void Update()
    {
        if (!_isTalking)
        {
            _isTalking = true;
            dialog.text = "";
            StopAllCoroutines();
            StartCoroutine(TypeWritterEffect());
        }
    }


    IEnumerator TypeWritterEffect()
    {
        yield return new WaitForSeconds(.3f);
        foreach (char character in _line.ToCharArray())
        {
            dialog.text += character;
            yield return new WaitForSeconds(typeWritterSpeed);
        }

        _isTalking = false;
    }
}
