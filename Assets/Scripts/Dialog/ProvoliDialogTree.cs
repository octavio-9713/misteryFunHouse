using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ProvoliDialogTree : MonoBehaviour
{
    public Text dialog;

    [Header("Dialog")]
    public List<Dialog> dialogTree2;

    [Header("Speed of text writting")]
    public float typeWritterSpeed = 0.05f;

    [Header("Provoli")]
    public Provoli provoli;
    private Animator _provoliAnim;

    [Header("Dialog Event")]
    public UnityEvent dialogEvent = new UnityEvent();

    private bool _isTalking = false;

    [Header ("Game Ender")]
    public bool isEnder = false;

    private Dialog _currentDialog;
    private string _currentLine;

    public void Start()
    {
        _provoliAnim = provoli.GetComponent<Animator>();
        NextLine();
    }

    public void NextLine()
    {
        if (dialogTree2.Count > 0)
        {
            _currentDialog = dialogTree2[0];
            dialogTree2.RemoveAt(0);

            _currentLine = _currentDialog.dialog;
            dialog.text = "";

            if (_currentDialog.firesEvent)
                dialogEvent.Invoke();

            StopAllCoroutines();
            StartCoroutine(TypeWritterEffect(_currentLine));
        }

        else
            FinishTalk();
    }

    public void FinishTalk()
    {

        if (dialogTree2.Count > 0)
        {
            Dialog firer = dialogTree2.Find(dialog => dialog.firesEvent);
            if (firer != null)
                dialogEvent.Invoke();
        }

        _provoliAnim.SetTrigger("fade");
        GetComponent<Animator>().SetTrigger("fade");

        if (isEnder)
            GameManager.Instance.NextRoom();
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

        _isTalking = false;
    }

    public void OnDash()
    {
        FinishTalk();
    }

    void OnShoot(InputValue value)
    {
        if (_isTalking)
        {
            StopAllCoroutines();
            dialog.text = _currentLine;
            _isTalking = false;
        }

        else
            NextLine();
    }
}
