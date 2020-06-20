using UnityEngine;
using UnityEngine.SceneManagement;

public class endPoint : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If player hits end point go to new floor
        if (collision.gameObject.tag == "Player")
        {
            GameManager.floor++;
            Debug.LogError(GameManager.floor);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
