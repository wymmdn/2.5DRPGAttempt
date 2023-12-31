using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    List<MagicBush> mBushs;
    private int bushNum;
    private int bushTriggerNum;
    private PlayerController playerController;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        { 
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
        mBushs = new List<MagicBush>();
        bushNum = 0;
        bushTriggerNum = 0;
        playerController = GameObject.FindGameObjectWithTag(tagtag.player).GetComponent<PlayerController>();
    }

    private void resetManager()
    {
        mBushs = new List<MagicBush>();
        bushNum = 0;
        bushTriggerNum = 0;
        playerController = GameObject.FindGameObjectWithTag(tagtag.player).GetComponent<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (bushNum != 0 && bushTriggerNum == bushNum)
        {
            GetWin(); 
        }
    }

    public static void RegisterBush(MagicBush bush)
    {
        if (!instance.mBushs.Contains(bush))
        {
            instance.mBushs.Add(bush);
        }
        instance.bushNum = instance.mBushs.Count;
    }
    public static void TriggerBush()
    {
        instance.bushTriggerNum = 0;
        foreach (MagicBush bush in instance.mBushs)
        {
            if (bush.triggered == true)
            { 
                instance.bushTriggerNum++;
            }
        }
    }
    public void pauseGame()
    {
        Time.timeScale = 0;
    }

    public void OnApplicationPause(bool pause)
    {
        
    }
    private void readData()
    { 
        
    }
    public void GetWin()
    {
        Debug.Log("you win");
        resetManager();
        SceneManager.LoadScene(playerController.gameObject.scene.name);
    }
    public void GameOver()
    {
        Debug.Log("game over");
        resetManager();
        SceneManager.LoadScene(playerController.gameObject.scene.name);
    }
}
