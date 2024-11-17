using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    public static void chooseLevel()
    {
        SceneManager.LoadScene("chooseLevel");
    }

    public static void EndScene()
    {
        SceneManager.LoadScene("EndScene");
    }

    public static void GenerateRandomScene()
    {
        SceneManager.LoadScene("RandomScene");
    }

    public static void LevelScene(int index)
    {
        SceneManager.LoadScene("Level" + index);
    }

    public static void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    
}
