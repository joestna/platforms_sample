using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float velocidad = 1f;
    float fuerza = 30f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButton("Horizontal"))
        {
            Rigidbody2D movPlayer = GetComponent<Rigidbody2D>();
            float movement = Input.GetAxis("Horizontal");
            

            //transform.position += velocity * Vector3.right * 0.0001f * Time.deltaTime;

            if (movement > 0)
            {
                if(transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
                }               

                movPlayer.velocity = (transform.right * velocidad * movement);
            }
            else
            {
                if (transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
                }

                movPlayer.velocity = (transform.right * velocidad * movement);
                //movPlayer.transform.localScale = new Vector3(0f, movPlayer.transform.localScale.y * -1f, 0f);
            }

        }

        if (Input.GetButton("Jump"))
        {
            //float movement = Input.GetAxis("Jump");

            Rigidbody2D movPlayer = GetComponent<Rigidbody2D>();
            movPlayer.AddForce(transform.up * fuerza);

            //transform.position += velocity * Vector3.right * 0.0001f * Time.deltaTime;
        }
    }
}
