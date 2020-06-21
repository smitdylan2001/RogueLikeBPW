using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
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
