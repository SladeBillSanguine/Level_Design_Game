using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    public void LoadTutorial()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadLevel1()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadLevel2()
    {
        SceneManager.LoadScene(3);
    }
    public void LoadLevel3()
    {
        SceneManager.LoadScene(4);
    }
    public void LoadLevel4()
    {
        SceneManager.LoadScene(5);
    }
}
