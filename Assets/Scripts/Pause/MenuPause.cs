using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{
    public MenuButton selectedButton;

    public List<MenuButton> buttons;

    public Image image;

    public bool disableSelection = false;
    private int actualIndex = 0;

    void Start()
    {
        SelectAButton(selectedButton);
    }

    private void SelectAButton(MenuButton newSelectedButton)
    {
        this.selectedButton = newSelectedButton;
        image.sprite = this.selectedButton.selectImage;
    }

    public void StartResumeAnim()
    {
        GameManager.Instance.ResumeGame();
    }

    public void CloseMenu()
    {
        GameManager.Instance.EndResumeAnim();
    }

    void OnUp()
    {
        if (!disableSelection && actualIndex > 0)
        {
            actualIndex--;
            SelectAButton(buttons[actualIndex]);
        }
    }
    void OnDown()
    {
        if (!disableSelection && actualIndex < buttons.Count - 1)
        {
            actualIndex++;
            SelectAButton(buttons[actualIndex]);
        }
    }
    void OnAcept()
    {
        selectedButton.button.onClick.Invoke();

        disableSelection = true;
    }
}
