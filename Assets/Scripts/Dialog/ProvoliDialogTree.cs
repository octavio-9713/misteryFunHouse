using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProvoliDialogTree : MonoBehaviour
{
    public Text dialog;

    [Header("")]
    public string[] dialogTree;
    private List<string> _dialogList;

    [Header("Speed of text writting")]
    public float typeWritterSpeed = 0.05f;

    [Header("Provoli")]
    public Provoli provoli;
    private Animator _provoliAnim;

    private bool _isTalking = false;

    private string _currentLine;

    public void Start()
    {
        _provoliAnim = provoli.GetComponent<Animator>();

        _dialogList = new List<string>(dialogTree);
        NextLine();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
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

        if (Input.GetButtonDown("Fire2"))
            FinishTalk();
    }

    public void NextLine()
    {
        if (_dialogList.Count > 0)
        {
            _currentLine = _dialogList[0];
            _dialogList.RemoveAt(0);
            dialog.text = "";

            StopAllCoroutines();
            StartCoroutine(TypeWritterEffect(_currentLine));
        }

        else
            FinishTalk();
    }

    public void FinishTalk()
    {
        _provoliAnim.SetTrigger("fade");
        GetComponent<Animator>().SetTrigger("fade");
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
}
