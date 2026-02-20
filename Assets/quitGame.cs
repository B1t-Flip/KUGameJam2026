using UnityEngine;

public class quitGame : MonoBehaviour
{
    void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
        //Just to make sure its working
    }
}
