using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay;
    [SerializeField] List<AudioClip> audioClips;
    [SerializeField] AudioSource audioSource;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crushParticles;
    

    bool isTransitioning = false;
    bool collisionDisabled = false;

    private void OnCollisionEnter(Collision collision)
    {
        
            if (isTransitioning || collisionDisabled)
            {
                return; // Burada return bu kod bloðunda hiçbirþey yaptýrmadan sadece return ettirir.Aðaðýdaki kodlar çalýþmaz.
            }
            switch (collision.gameObject.tag)
            {

                case "Friendly":
                    Debug.Log("Friendly");
                    break;
                case "Finish":
                    StartSuccessSequence();
                    break;
                default:
                    StartCrushSequence();
                    break;
            }
        
       
    }

    void Update()
    {

        RespondToDebugKeys();

    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;//toggle collison (Eðer collisonDisable true ise false, false ise true yapar.)
        }
    }

    void StartCrushSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(audioClips[0]);

        crushParticles.Play();
        gameObject.GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);//Ýlerde bunun yerine couroutine ler tercih edilecek.
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(audioClips[1]);

        successParticles.Play();
        gameObject.GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
        gameObject.GetComponent<Movement>().enabled = true;
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        gameObject.GetComponent<Movement>().enabled = true;
    }


}
