using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public MenuButton selectedButton;

    public List<MenuButton> buttons;

    private bool _disableSelection = false;
    private int actualIndex = 0;

    void Start()
    {
        SelectAButton(selectedButton);
    }
    private void SelectAButton(MenuButton newSelectedButton)
    {
        this.selectedButton.button.image.sprite = this.selectedButton.normalImage;

        this.selectedButton = newSelectedButton;
        this.selectedButton.button.image.sprite = this.selectedButton.selectImage;
    }

    public void CloseMenu()
    {
        GameManager.Instance.ResumeGame();
    }

    void OnUp()
    {
        if (!_disableSelection && actualIndex > 0)
        {
            actualIndex--;
            SelectAButton(buttons[actualIndex]);
        }
    }
    void OnDown()
    {
        if (!_disableSelection && actualIndex < buttons.Count - 1)
        {
            actualIndex++;
            SelectAButton(buttons[actualIndex]);
        }
    }
    void OnAccept()
    {
        selectedButton.button.onClick.Invoke();

        _disableSelection = true;
    }
}
