using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager _instance = null;

    private GameObject pauseMenu;


    public static GameManager Instance
    {
        get { return _instance; }
    }


    private void Awake()
    {
        if (_instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
    }
    // called first
    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
        pauseMenu.SetActive(false);
    }

    // called third
    

    // called when the game is terminated
    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!pauseMenu)
            {
                pauseMenu = GameObject.Find("PauseMenu");
            }


            if (pauseMenu)
            {
                if (pauseMenu.activeSelf)
                {
                    pauseMenu.SetActive(false);
                    Time.timeScale = 1;
                }
                else
                {
                    pauseMenu.SetActive(true);
                    Time.timeScale = 0;
                }


            }


            //else
            //{
            //    Time.timeScale = 1.0f;
            //    FindObjectOfType<PauseMenu>().ShowMenu();
            //}
        }

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    Application.Quit();
        //}
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    SceneManager.LoadScene(0);
        //}
        //if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    SceneManager.LoadScene("Level 1");
        //}
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
        
    }
    public void QuitGame()
    {
        Debug.Log("quit application");
        Application.Quit();
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}

