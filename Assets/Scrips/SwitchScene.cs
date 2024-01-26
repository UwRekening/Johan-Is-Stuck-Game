using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{

    //Checkt als een button word geclickt start
    public void Switch()
    {
        SceneManager.LoadScene("LoadingScreen");
    }

    //Checkt als een button word geclickt quit
    public void Click()
    {
        Application.Quit();
    }
}
