using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    void Start()
    {
        //Voeg een delay toe 10s lange delay
        StartCoroutine(SwitchScene(10f));
    }
    IEnumerator SwitchScene(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        //laad de scene Level01
        SceneManager.LoadScene("Level01");
    }
}
