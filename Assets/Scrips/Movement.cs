using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D box;
    private Animator animator;
    private TimerScript timer;

    [SerializeField] GameObject particleSystem;
    [SerializeField] GameObject Canvas;
    [SerializeField] float speed = 2.0f;
    [SerializeField] int jump = 40;
    [SerializeField] LayerMask ground;
    [SerializeField] GameObject FlashlightPlayer;
    [SerializeField] GameObject Gameinstance;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        timer = Gameinstance.GetComponent<TimerScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float move;

        //Checkt als ik horizontal loop.
        move = Input.GetAxisRaw("Horizontal") * speed;

        //Als ik naar rechts ga
        if (move > 0f)
        {
            //Laat hem lopen.
            rb.velocity = new Vector2(move, rb.velocity.y);
            //Verander de scale van de player zodat hij naar rechts kijkt
            transform.localScale = new Vector2(9.62f, 9.62f);
        }
        //Als ik naar links ga
        else if (move < 0f)
        {
            //Laat hem lopen
            rb.velocity = new Vector2(move, rb.velocity.y);
            //Verander de scale van de player zodat hij naar links kijkt
            transform.localScale = new Vector2(-9.62f, 9.62f);
        }
        //Als ik niet loop
        else
        {
            //Character staat stil loopt niet
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        //Als ik spring en ik de grond raak
        if (Input.GetAxisRaw("Jump") > 0 && isGrounded())
        {
            //Voeg een force toe zodat ik spring
            rb.AddForce(transform.up * jump, ForceMode2D.Force);
            timer.stopWatch = true;
        }

        //Laat de walk animation zien.
        animator.SetFloat("Speed", move);
    }

    //Check als ik de grond aanraak.
    private bool isGrounded()
    {
        float extraHeight = 0.1f;
        //Tekend een lijn vanaf het midden naar de voeten om te kijken als ik wat aanraak
        RaycastHit2D raycastHit2D = Physics2D.Raycast(box.bounds.center, Vector2.down, box.bounds.extents.y + extraHeight, ground);
        Color rayColor;

        //Als de raycast niet null is
        if (raycastHit2D.collider != null)
        {
            //Verander hem naar groen
            rayColor = Color.green;
        }
        else
        {
            //Verander hem naar rood
            rayColor = Color.red;
        }

        //Laat hem tekenen/zien via scene
        Debug.DrawRay(box.bounds.center, Vector2.down * (box.bounds.extents.y + extraHeight), rayColor);
        return raycastHit2D.collider != null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Spikes")
        {
            //Stop de timer
            timer.stopWatch = false;
            Debug.Log("Auw");
            //Laat bloed zien via particleSystem.
            particleSystem.GetComponent<ParticleSystem>().Play();
            //Speelt een animatie af.
            animator.Play("Die");
            Destroy(FlashlightPlayer);
            Destroy(this);
            //Laat de gameover screen zien
            Canvas.SetActive(true);
        }
        if (collision.tag == "End")
        {
            timer.stopWatch = false;
            //Verander de scene naar MainMenu
            SceneManager.LoadScene("MainMenu");
        }
    }
}
