using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    [SerializeField] GameObject flashlightGround;
    [SerializeField] GameObject flashlightPlayer;
    [SerializeField] GameObject pickupText;
    Animator animator;
    Gameinstance scores;
    private void Start()
    {
        //Zoekt naar animator
        animator = GetComponent<Animator>();
        //Zoekt naar Gameinstance
        scores = FindObjectOfType<Gameinstance>();
        flashlightPlayer.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Checkt als collision de tag "Pickable" heeft.
        if (collision.tag == "Pickable")
        {
            //Checkt als E of controler knop is geklikt
            if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.JoystickButton2))
            {
                Debug.Log("Click");
                //Verwijderd flashlight op de grond
                Destroy(flashlightGround);
                //Verander animatie van de player
                animator.SetBool("FlashlightOn", true);
                //Doe de flashlight aan van de player
                flashlightPlayer.SetActive(true);
                //Text van de pickup word gefadeout animatie
                pickupText.GetComponent<Animator>().SetBool("FadeActive", true);
                //Wacht 1s voor dat er wat gebeurd
                Invoke("DestroyPickup", 1.0f);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            //Zet de score naar +2
            scores.score += 2;
            collision.GetComponent<Animator>().Play("Coin fadeout");
            StartCoroutine(DestroyCoin(collision, 1f));
        }
        if (collision.tag == "Chest")
        {
            collision.GetComponent<ParticleSystem>().Play();
            scores.score += 5;
            Destroy(collision.GetComponent<Collider2D>());
        }
    }
    //Display text word verwijderd
    private void DestroyPickup()
    {
        Destroy(pickupText);
    }
    IEnumerator DestroyCoin(Collider2D collision, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        //Verwijderd collision object uit de scene.
        Destroy(collision.gameObject);
    }
}
