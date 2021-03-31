using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManagement : MonoBehaviour
{
    public GameObject shopPanel; 
    public GameObject pausePanel;

    public static bool isPaused = false;
    public static bool isShopOpen = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

            if (isShopOpen)
            {
                Resume();
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isShopOpen)
            {
                Resume();
            }
            else
            {
                OpenShop();
            }
        }
    }
    public void Resume()
    {
        if (isPaused)
        {
            pausePanel.SetActive(false);
            isPaused = false;
        }
        if (isShopOpen)
        {
            shopPanel.SetActive(false);
            isShopOpen = false;
        }
        Time.timeScale = 1f;
    }
    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void OpenShop()
    {
        isShopOpen = true;
        Time.timeScale = 0f;
        shopPanel.SetActive(true);
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        //SceneManager.LoadScene("Menu");
        Debug.Log("Going to Menu...");
    }

    public void Quit()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
