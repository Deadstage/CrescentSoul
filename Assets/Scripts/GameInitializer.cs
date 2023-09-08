using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInitializer : MonoBehaviour
{
    private static bool gameJustStarted = true;

    void Start()
    {
        if (gameJustStarted)
        {
            // Load the main menu scene as soon as the game scene is loaded
            SceneManager.LoadScene("MainMenu"); // Replace with the actual name of your main menu scene
            gameJustStarted = false; // Set the flag to false so that we don't transition to the main menu again
        }
    }
}