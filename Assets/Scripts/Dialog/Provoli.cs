using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Provoli : MonoBehaviour
{
    public ProvoliDialogTree dialogTree;

    public void Start()
    {
        GameManager.Instance.player.DisableMovement();
        GameManager.Instance.provoliTalking = true;
    }

    public void StartTalking()
    {
        dialogTree.gameObject.SetActive(true);
    }

    public void DestroyProvoli()
    {
        GameManager.Instance.player.EnableMovement();
        GameManager.Instance.provoliTalking = false;
        Destroy(transform.parent.gameObject);
    }


}
