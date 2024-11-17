using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    // Start is called before the first frame update
    public static void v1scene()
    {
        SceneManager.LoadScene("V1");
    }

    public static void Menuscene()
    {
        SceneManager.LoadScene("Menu");
    }

    public static void DeadScene()
    {
        SceneManager.LoadScene("DeadScreen");
    }
}
