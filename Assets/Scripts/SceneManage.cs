using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    //go to first scene
    public void Begin()
    {
        SceneManager.LoadScene(1);
    }

    //exit game
    public void Exit()
    {
        Application.Quit();
    }
}
