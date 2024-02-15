using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    private AudioSource audioSource;

    [SerializeField] private ParticleSystem thrustPS;
    [SerializeField] private ParticleSystem leftBoosterPS;
    [SerializeField] private ParticleSystem rightBoosterPS;

    [SerializeField] private float rocketThrust = 1000f;
    [SerializeField] private float rotationThrust = 10f;
    [SerializeField] private AudioClip mainEngine;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!thrustPS.isPlaying)
            {
                Debug.Log("Is palying PS");
                thrustPS.Play();
            }
            rb.AddRelativeForce(Vector3.up * rocketThrust * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
        }
        else
        {
            audioSource.Stop();
            thrustPS.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            ApplyRotation(rotationThrust);
            leftBoosterPS.Play();
        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            ApplyRotation(-rotationThrust);
            rightBoosterPS.Play();
        }
    }

    private void ApplyRotation(float rotationValue)
    {
        rb.freezeRotation = true; // Freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationValue * Time.deltaTime);
        rb.freezeRotation = false; // Unfreezinf rotation so the phisycs system can take over
    }
}
