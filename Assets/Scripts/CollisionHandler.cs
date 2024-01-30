using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You are a friendly person!");
                break;
            case "Finish":
                Debug.Log("You WIN!");
                break;
            case "Fuel":
                Debug.Log("Refuel");
                break;
            default:
                Debug.Log("We under attack!");
                break;
        }
    }
}
