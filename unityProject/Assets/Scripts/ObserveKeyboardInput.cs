using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObserveKeyboardInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // load the menu upon hitting the escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadMenu();
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadSceneAsync("menuScene");

        // only unload the scene when mainScene has been loaded
        if (SceneManager.GetSceneByName("menuScene").isLoaded)
            SceneManager.UnloadSceneAsync("mainScene");
    }
}
