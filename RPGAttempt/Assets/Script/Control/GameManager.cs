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
    public PlayerController playerController;
    [SerializeField]private Canvas menuCanvas;
    [SerializeField]private Camera menuCamera;
    [SerializeField]private Canvas escCanvas;

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
        //resetManager("");
    }

    public void resetManager(string scene)
    {
        mBushs = new List<MagicBush>();
        bushNum = 0;
        bushTriggerNum = 0;
        if (scene != sceneName.menuScene)
        {
            playerController = GameObject.FindGameObjectWithTag(tagtag.player).GetComponent<PlayerController>();
            escCanvas.worldCamera = GameObject.FindGameObjectWithTag(tagtag.gameCamera).GetComponent<Camera>();
            StoryManager.instance.resetStory();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (escCanvas.gameObject.activeSelf)
            {
                continueGame();
            }
            else
            {
                GameManager.instance.pauseGame();
                escCanvas.gameObject.SetActive(true);
            }
        }
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
    public void GetWin()
    {
        Debug.Log("you win");
        resetManager(sceneName.menuScene);
        StartCoroutine(loadMenuScene(sceneName.magicValley, sceneName.menuScene));        
    }
    public void GameOver()
    {
        Debug.Log("game over");
        resetManager(sceneName.magicValley);
        SceneManager.LoadScene(playerController.gameObject.scene.name);
    }
    public IEnumerator loadGameScene(string menu, string game)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(menu);
        while (!ao.isDone)
        {
            yield return null;
            //Debug.Log(ao.progress);
        }
        menuCanvas.gameObject.SetActive(false);
        menuCamera.gameObject.SetActive(false);
        yield return SceneManager.LoadSceneAsync(game,LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(game));
        resetManager(game);
    }
    public IEnumerator loadMenuScene(string game, string menu)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(game);
        while (!ao.isDone)
        {
            yield return null;
            //Debug.Log(ao.progress);
        }
        menuCanvas.gameObject.SetActive(true);
        menuCamera.gameObject.SetActive(true);
        yield return SceneManager.LoadSceneAsync(menu, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(menu));
        resetManager(menu);
    }

    public void startGame()
    {
        StartCoroutine(loadGameScene(sceneName.menuScene, sceneName.magicValley));
        //StartCoroutine(GameManager.instance.debugfunc());
    }
    public void exitGame()
    {

    }
    public void continueGame()
    {
        GameManager.instance.cancelPause();
        escCanvas.gameObject.SetActive(false);
    }
    public void backMenu()
    {
        continueGame();
        StartCoroutine(GameManager.instance.loadMenuScene(sceneName.magicValley, sceneName.menuScene));
    }
    public void pauseGame()
    {
        Time.timeScale = 0;
        playerController.isTalking = true;
    }
    public void cancelPause()
    {
        Time.timeScale = 1f;
        playerController.isTalking = false;
    }

    public void OnApplicationPause(bool pause)
    {

    }
    private void readData()
    {

    }
}
