using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animations;
    Rigidbody2D phisicsPlayer;
    public Collider2D ground;

    float velocidad = 1f;
    float fuerza = 100f; // Fuerza en las piernas para saltar


    void Start()
    {
        
    }


    void FixedUpdate()
    {
        float movement = Input.GetAxis("Horizontal");
        animations = GetComponent<Animator>();
        phisicsPlayer = GetComponent<Rigidbody2D>();

        
        // Verifica si el colider del personaje esta o no tocando el suelo
        if (GetComponent<Collider2D>().IsTouching(ground))
        {
            animations.SetBool("IsGrounded", true);

            if (Input.GetButton("Jump") && GetComponent<Collider2D>().IsTouching(ground))
            {
                phisicsPlayer.AddForce(transform.up * fuerza);
            }

            if (Input.GetButton("Horizontal"))
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
        }        
        else
        {
            animations.SetBool("IsGrounded", false);            
        }

        // Salto   
        animations.SetFloat("MoveSpeed", Mathf.Abs(movement));

        // El blender tree de las animaciones solo se activa cuando el colider del personaje no esta tocando el suelo
        animations.SetFloat("VerticalVelocity", GetComponent<Rigidbody2D>().velocity.y);
    }
}
