using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialController : MonoBehaviour
{
    [Header("Move Buttons")]
    public GameObject moveController;
    public GameObject moveKeyboard;

    [Header("Dash Buttons")]
    public GameObject dashController;
    public GameObject dashKeyboard;

    [Header("Shoot Buttons")]
    public GameObject shootController;
    public GameObject shootKeyboard;

    private PlayerInput _input;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    { 
        _input = GameManager.Instance.PlayerInput();

        if (_input.currentControlScheme != null && _input.currentControlScheme.Equals("Gamepad") && !moveController.activeInHierarchy)
        {
            ChangeKeyboardActive(false);
            ChangeControllerActive(true);
        }
        else if (!moveKeyboard.activeInHierarchy)
        {
            ChangeControllerActive(false);
            ChangeKeyboardActive(true);
        }
    }

    private void ChangeControllerActive(bool active)
    {
        moveController.SetActive(active);
        dashController.SetActive(active);
        shootController.SetActive(active);
    }

    private void ChangeKeyboardActive(bool active)
    {
        moveKeyboard.SetActive(active);
        dashKeyboard.SetActive(active);
        shootKeyboard.SetActive(active);
    }
}
