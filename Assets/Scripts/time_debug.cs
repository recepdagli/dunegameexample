using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class time_debug : MonoBehaviour
{
    public float time;
    public bool print_game_second = true;

    void Start()
    {
        
    }

    void Update()
    {
        if(PlayerPrefs.GetInt("gameover", 0) == 0 && PlayerPrefs.GetInt("gamestart", 0) == 1)
        {
            time += Time.deltaTime;
        }
        if(PlayerPrefs.GetInt("gameover", 0) == 1 && print_game_second)
        {
            Debug.Log("Level has finished in "+((int)time).ToString()+" seconds");
            print_game_second = false;
        }
    }
    void OnApplicationQuit()
    {
        Debug.Log("Game has finished in "+((int)Time.time).ToString()+" seconds.");
    }

}
