using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    //Make sure cursor is usable in the menu
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
