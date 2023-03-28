using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 50f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem leftThrusterParticles, rightThrusterParticles, mainEngineParticles;

    Rigidbody rb;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

   

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrust();
        }
        else
        {
            StopThrusting();
        }

    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void StartThrust()
    {
        //rb.AddRelativeForce(0, thrustValue*Time.deltaTime, 0);
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime); //(0,1,0) demektir.
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    void StopThrusting()
    {
        mainEngineParticles.Stop();
        audioSource.Stop();
    }

    

    void RotateLeft()
    {
        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }


        ApplyRotation(rotationThrust);
    }

    void RotateRight()
    {
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }

        ApplyRotation(-rotationThrust);
    }
    void StopRotating()
    {
        leftThrusterParticles.Stop();
        rightThrusterParticles.Stop();
    }
    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;//rigidbody rotasyonu dondurur.
        gameObject.transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);//(0,0,1) demektir.
        rb.freezeRotation = false;
    }
}
