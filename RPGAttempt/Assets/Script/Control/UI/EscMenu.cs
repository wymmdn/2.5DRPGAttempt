using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscMenu : MonoBehaviour
{
    [SerializeField] private GameObject escMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (escMenu.activeSelf)
            {
                continueGame();
            }
            else { 
                GameManager.instance.pauseGame();
                escMenu.SetActive(true);
            }
        }
    }

    public void continueGame()
    {
        GameManager.instance.cancelPause();
        escMenu.SetActive(false);
    }
    public void backMenu()
    {
        StartCoroutine(GameManager.instance.loadMenuScene(sceneName.magicValley, sceneName.menuScene));
    }
    public void exitGame()
    { 
    
    }
}
