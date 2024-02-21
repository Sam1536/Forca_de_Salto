using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveLeft : MonoBehaviour
{
    public float moveSpeed = 20f;
    private PlayerController playerController;
    private Rigidbody rig;

    public float leftBound = -15;

    // Start is called before the first frame update
    void Start()
    {
       playerController = FindObjectOfType<PlayerController>();
        rig = GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.startGame == true)
        {
            if (playerController.gameOver == false)
            {
                if (playerController.isDashing == true)
                {
                    transform.Translate(Vector3.left * Time.deltaTime * moveSpeed * playerController.dash);
                }
                else
                {
                    transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                }

                if (transform.position.x < leftBound && gameObject.CompareTag("Obstacles"))
                {
                    Destroy(gameObject);
                }
            }
          

        }
        else if (playerController.startGame == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * 6);
        }

    }
}
