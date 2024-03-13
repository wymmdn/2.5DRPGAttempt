using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    

    public void startGame()
    {
        StartCoroutine(GameManager.instance.loadGameScene(sceneName.menuScene, sceneName.magicValley));
        //StartCoroutine(GameManager.instance.debugfunc());
    }
    public void exitGame()
    { 
    
    }

    
}
