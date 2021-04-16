using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{

    public TextMeshProUGUI dialog;

    public List<string> _dialogTree2;

    [Header("Speed of text writting")]
    public float typeWritterSpeed = 0.05f;

    public float waitNextLine = 1f;

    private bool _isTalking = false;

    private string _currentLine;

    public void Start()
    {
        string[] dialogs = { "Thanks for playing...", "This was a hard and fun experience, see you next time...", "Your final time was " + GameManager.Instance.ActualTime() };
        _dialogTree2 = new List<string>(dialogs);
        NextLine();
    }

    private void Update()
    {
        if (!_isTalking)
        {
            StopAllCoroutines();
            dialog.text = _currentLine;
            NextLine();
        }
    }

    public void NextLine()
    {
        if (_dialogTree2.Count > 0)
        {
            _currentLine = _dialogTree2[0];
            _dialogTree2.RemoveAt(0);

            dialog.text = "";

            StopAllCoroutines();
            StartCoroutine(TypeWritterEffect(_currentLine));
        }

        else
            FinishTalk();
    }

    public void FinishTalk()
    {
        Destroy(this);
    }

    public void OnFinishAnimation()
    {
        GameManager.Instance.player.EnableMovement();
        Destroy(gameObject);
    }

    IEnumerator TypeWritterEffect(string text)
    {
        _isTalking = true;
        yield return new WaitForSeconds(.3f);
        foreach (char character in text.ToCharArray())
        {
            dialog.text += character;
            yield return new WaitForSeconds(typeWritterSpeed);
        }

        yield return new WaitForSeconds(waitNextLine);
        _isTalking = false;
    }
}
