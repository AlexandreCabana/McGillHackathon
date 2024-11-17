using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWin : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        changeScene.WinScene();
    }
}
