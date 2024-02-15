using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private float levelLoadDelay = 1f;
    private float restartLevelDelay = 1.5f;
    
    private AudioSource audioSource;
    [SerializeField] private AudioClip obstacleColision;
    [SerializeField] private AudioClip finishSound;

    [SerializeField] private ParticleSystem obstacleColisionPSys;
    [SerializeField] private ParticleSystem finishPSys;

    private bool isTransitioning = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning) return;
        
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
                Debug.Log("You CRUSH!");
                StartCrushSequence();

                break;
        }
    }

    private void StartLoadNextSequence()
    {
        isTransitioning = true;
        audioSource.Stop();

        // SFX upon crash and particle effect upon next level
        finishPSys.Play();
        audioSource.PlayOneShot(finishSound);

        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextScene", levelLoadDelay);
    }

    private void StartCrushSequence()
    {
        isTransitioning = true;
        audioSource.Stop();

        // SFX upon crash and particle effect upon crush
        obstacleColisionPSys.Play();
        audioSource.PlayOneShot(obstacleColision);
        
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
