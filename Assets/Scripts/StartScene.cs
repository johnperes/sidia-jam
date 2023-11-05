using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayGamePVP()
    {
        GameConfig.Instance.cpuGame = false;
        SceneManager.LoadScene("Game");
    }
    public void PlayGameCPU()
    {
        GameConfig.Instance.cpuGame = true;
        GameConfig.Instance.cpuLevel = 0;
        SceneManager.LoadScene("Game");
    }
    public void PlayGameImpossibleCPU()
    {
        GameConfig.Instance.cpuGame = true;
        GameConfig.Instance.cpuLevel = 1;
        SceneManager.LoadScene("Game");
    }
}
