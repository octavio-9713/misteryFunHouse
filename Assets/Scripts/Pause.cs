using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

    private bool _pressedPause = false;
    private bool _onPause = false;
    public MenuPause panelPause;
    public Player player;
    
    public GameObject SonidoClick;

    public void PauseGame()
    {
        Time.timeScale = 0;
        _onPause = true;
        _pressedPause = true;

        if (!GameManager.Instance.provoliTalking)
            player.DisableMovement();

        panelPause.gameObject.SetActive(true);
        panelPause.disableSelection = false;
    }

    public void Resume()
    {
        panelPause.gameObject.GetComponent<Animator>().SetTrigger("Closing");
    }

    public void OnClickResume()
    {
        Resume();
    }

    public void EndPause()
    {
        Time.timeScale = 1;

        if (!GameManager.Instance.provoliTalking)
            player.EnableMovement();

        _onPause = false;
        _pressedPause = false;

        panelPause.gameObject.SetActive(false);
        Debug.Log("Setting to false");
    }


    void OnPause()
    {
        if (!_onPause)
            PauseGame();
        else
            Resume();
    }
}
