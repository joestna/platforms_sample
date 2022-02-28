using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animations;
    Rigidbody2D phisicsPlayer;
    public Collider2D ground;
    bool isGrounded = false;

    float velocidad = 1f;
    float fuerza = 4f; // Fuerza en las piernas para saltar


    void Start()
    {
        Physics2D.queriesStartInColliders = false;
    }


    void FixedUpdate()
    {
        float movement = Input.GetAxis("Horizontal");
        animations = GetComponent<Animator>();
        phisicsPlayer = GetComponent<Rigidbody2D>();

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
        //Debug.Log(hit.collider.name);
        //Debug.Log(isGrounded);

        if (hit.distance < 0.4 && isGrounded == false && phisicsPlayer.velocity.y < 0 && hit.collider.tag == "Ground")
        {
            Debug.Log("tocando " + phisicsPlayer.velocity.y);
            isGrounded = true;
        }

        // Verifica si el colider del personaje esta o no tocando el suelo
        if (isGrounded == true)
        {            
            animations.SetBool("IsGrounded", true);

            if (Input.GetButton("Horizontal")  && isGrounded == true)
            {
                // Invertir el sprite segun el movimiento - derecha izquierda
                if (movement > 0)
                {
                    if (transform.localScale.x < 0)
                    {
                        transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
                    }
                }
                else
                {
                    if (transform.localScale.x > 0)
                    {
                        transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
                    }
                }

                phisicsPlayer.velocity = (transform.right * velocidad * movement);
            }

            if (Input.GetButton("Jump") && isGrounded == true)
            {
                //phisicsPlayer.AddForce(transform.up * fuerza);
                phisicsPlayer.AddForce(transform.up * fuerza, ForceMode2D.Impulse);

                isGrounded = false;
            }
        }        
        else
        {
            animations.SetBool("IsGrounded", false);            
        }

        

        

        // Salto   
        animations.SetFloat("MoveSpeed", Mathf.Abs(movement));

        // El blender tree de las animaciones solo se activa cuando el colider del personaje no esta tocando el suelo
        animations.SetFloat("VerticalVelocity", GetComponent<Rigidbody2D>().velocity.y);
        //Debug.Log(GetComponent<Rigidbody2D>().velocity.y);
    }
}
