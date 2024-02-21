using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rig;
    

    public float dash = 1f;
    public bool isDashing = false;
    private float animSpeed;    

    public float jump;
    public bool isGround = false;
    private bool canJump = true;

    public float gravity;
    public bool gameOver = false;
    public bool startGame = false;

    private Animator anim;

    public ParticleSystem particle; 
    public ParticleSystem dirtParticle;

    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource audioSource;    

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        Physics.gravity *= gravity;

        StartCoroutine(WalkThenRun());
    }
     
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.Space) && isGround && !gameOver)
        //{
        //    rig.AddForce(Vector3.up * jump, ForceMode.Impulse);
        //    isGround = false;
        //    anim.SetTrigger("Jump_trig");
        //    dirtParticle.Stop();
        //    audioSource.PlayOneShot(jumpSound, 1f);
        //}

        if (Input.GetKey(KeyCode.Space) && isGround && !gameOver)
        {
            Jump();
           
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isGround && !canJump && !gameOver)
        {
            rig.AddForce(Vector3.up * jump * 1.5f, ForceMode.Impulse);
            audioSource.PlayOneShot(jumpSound, 0.5f);
            canJump = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animSpeed = anim.GetFloat("Speed_f") * dash;
            anim.SetFloat("Speed_f", animSpeed);
            isDashing = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.SetFloat("Speed_f", animSpeed / dash);
            isDashing = false;
        }
    }

    void Jump()
    {
        rig.AddForce(Vector3.up * jump, ForceMode.Impulse);
        isGround = false;  
        anim.SetTrigger("Jump_trig");
        dirtParticle.Stop();
        audioSource.PlayOneShot(jumpSound, 1f);
    }

    private void OnCollisionEnter(Collision collision) 
    { 
        if (collision.gameObject.CompareTag("Grounded"))
        {
            isGround = true;
            canJump = false;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacles"))
        {
            gameOver = true;
            anim.SetBool("Death_b", true);
            anim.SetInteger("DeathType_int", 1);
            particle.Play();
            dirtParticle.Stop();
            audioSource.PlayOneShot(crashSound, 1f);
            //StartCoroutine(SpawnTime());
            Debug.Log("GamerOver");
        }   
    }
  
    IEnumerator WalkThenRun()
    {
        WalkIn();
        yield return new WaitForSeconds(1.5f);
        Run();
    }

    void WalkIn()
    {
        anim.SetFloat("Speed_f", 0.4f);
    }

    void Run()
    {
        anim.SetFloat("Speed_f", 1f);
        startGame = true;
    }

    IEnumerator SpawnTime()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
        
    }
}
