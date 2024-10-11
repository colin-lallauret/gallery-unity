using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LifeManager : MonoBehaviour
{
    public Image[] hearts;
    public Sprite heartFull;
    public Sprite heartEmpty;
    public int maxLives = 3;
    private int currentLives;

    public Transform destination;

    private CharacterController playerController;

    void Start()
    {
        InitializeLives();
        playerController = GetComponent<CharacterController>();
    }

    void InitializeLives()
    {
        currentLives = maxLives;
        UpdateHearts();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("damage"))
        {
            LoseLife();
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("out of damage");
    }

    public void LoseLife()
    {
        if (currentLives > 0)
        {
            currentLives--;
            UpdateHearts();

            if (destination != null)
            {
                TeleportPlayer();
            }

            if (currentLives <= 0)
            {
                GameOver();
            }
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentLives)
            {
                hearts[i].sprite = heartFull;
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].sprite = heartEmpty;
                hearts[i].enabled = true;
            }
        }
    }

    void TeleportPlayer()
    {
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        transform.position = destination.position;

        if (playerController != null)
        {
            playerController.enabled = true;
        }

        Debug.Log("Le joueur a été téléporté à la destination.");
    }

    void GameOver()
    {
        StartCoroutine(LoadMainMenu());
    }

    IEnumerator LoadMainMenu()
    {
        yield return new WaitForSeconds(0);
        SceneManager.LoadScene("MainMenu");
    }
}
