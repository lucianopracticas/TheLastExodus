using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private float levelLoadDelay = 1f;
    private float restartLevelDelay = 1.5f;

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You are a friendly person!");

                break;
            case "Finish":
                Debug.Log("You WIN!");
                StartLoadNextSequence();

                break;
            case "Fuel":
                Debug.Log("Refuel");

                break;
            default:
                StartCrushSequence();

                break;
        }
    }

    private void StartLoadNextSequence()
    {
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextScene", levelLoadDelay);
    }

    private void StartCrushSequence()
    {
        // TODO add SFX upon crash
        // TODO add particle effect upon crush

        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", restartLevelDelay);
    }

    private void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("You WIN the Level!!!");
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("You just SAVE the Human race!!!");
            SceneManager.LoadScene(0);
        }
    }
}
