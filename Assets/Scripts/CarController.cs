using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 10f; // Geschwindigkeit des Autos
    public float rotationSpeed = 100f; // Drehgeschwindigkeit des Autos

    private float moveInput;
    private float steerInput;

    private AudioSource startEngineSound;
    private AudioSource driveForwardSound;
    private AudioSource driveBackwardSound;
    private AudioSource turnSound;

    private bool isMovingForward;
    private bool isMovingBackward;
    private bool isTurning;

    void Start()
    {
        // Finde die Audioquellen in der Szene
        startEngineSound = GameObject.Find("StartEngineSound").GetComponent<AudioSource>();
        driveForwardSound = GameObject.Find("DriveForwardSound").GetComponent<AudioSource>();
        driveBackwardSound = GameObject.Find("DriveBackwardSound").GetComponent<AudioSource>();
        turnSound = GameObject.Find("TurnSound").GetComponent<AudioSource>();

        if (startEngineSound != null)
        {
            startEngineSound.Play();
        }
    }

    void Update()
    {
        // Bewegung des Autos in Fahrtrichtung
        transform.Translate(Vector3.forward * moveInput * speed * Time.deltaTime);
        // Drehung des Autos
        transform.Rotate(Vector3.up, steerInput * rotationSpeed * Time.deltaTime);

        HandleEngineSounds();
    }

    public void Move(float input)
    {
        moveInput = input;
    }

    public void Steer(float input)
    {
        steerInput = input;
    }

    private void HandleEngineSounds()
    {
        if (moveInput > 0)
        {
            if (!isMovingForward)
            {
                driveForwardSound.Play();
                driveBackwardSound.Stop();
                isMovingForward = true;
                isMovingBackward = false;
            }
        }
        else if (moveInput < 0)
        {
            if (!isMovingBackward)
            {
                driveBackwardSound.Play();
                driveForwardSound.Stop();
                isMovingBackward = true;
                isMovingForward = false;
            }
        }
        else
        {
            driveForwardSound.Stop();
            driveBackwardSound.Stop();
            isMovingForward = false;
            isMovingBackward = false;
        }

        if (steerInput != 0)
        {
            if (!isTurning)
            {
                turnSound.Play();
                isTurning = true;
            }
        }
        else
        {
            turnSound.Stop();
            isTurning = false;
        }
    }
}
