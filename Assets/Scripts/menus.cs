using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menus : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public float sensitivity;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void enterGame(int value)
    {
        SceneManager.LoadScene(value);
    }
    public void exitGame()
    {
        Application.Quit();
    }
    public void settings()
    {
        //load PauseMenu
        Instantiate(pauseMenu);
 
    }
    public void resume()
    {
        Destroy(gameObject);
    }
    public void sensitvity(float sen)
    {
        try
        {
            sensitivity = GameObject.FindGameObjectWithTag("sense").GetComponent<Slider>().value;
        }
        catch { }
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
