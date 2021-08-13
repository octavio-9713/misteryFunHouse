using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mira : MonoBehaviour
{
    private Vector3 rightStick;
    public float sensibility = 10f;
    public PlayerInput playerInput;

    private void Start()
    {
        playerInput = GetComponentInParent<PlayerInput>();
    }

    void Update()
    {
        if (Gamepad.current != null && playerInput.currentControlScheme.Equals("Gamepad"))
            this.transform.position += rightStick * Time.deltaTime * sensibility;

        else
            DetectMouse();
    }

    public void DetectMouse()
    {
        this.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            -Camera.main.transform.position.z
         ));
    }

    void OnCamera(InputValue value)
    {
        Debug.Log("Here:" + value.Get<Vector2>());
        rightStick = value.Get<Vector2>();
    }
}
