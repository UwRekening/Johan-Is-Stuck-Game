using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameinstance : MonoBehaviour
{
    //Zoekt naar text component
    [SerializeField] TextMeshProUGUI scoringText;
    public int score = 0;

    //Als er geclickt word op een button
    public void Click()
    {
        SceneManager.LoadScene("Level01");
    }
    private void Start()
    {
        //set score naar 0
        score = 0;
    }
    //Update de score altijd naar scoringText
    private void Update()
    {
        scoringText.text = score.ToString();
    }
}
