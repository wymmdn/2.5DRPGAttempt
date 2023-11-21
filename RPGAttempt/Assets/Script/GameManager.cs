using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    List<MagicBush> mBushs;
    public int bushNum;
    public int bushTriggerNum;

    public void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
        DontDestroyOnLoad(this);

        mBushs = new List<MagicBush>();
        bushNum = 0;
        bushTriggerNum = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bushTriggerNum == bushNum)
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
    }
}
