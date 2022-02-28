using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animations;
    Rigidbody2D phisicsPlayer;
    public Collider2D ground;
    bool isGrounded = false;

    float velocidad = 2f; // Velocidad a la que va a correr el personaje
    float fuerza = 5f; // Fuerza en las piernas para saltar


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

        // Pone la variable is grounded a true cuando toca el suelo // Se utiliza raycast para verificar que toca el suelo evitando que se active la animacion de landing cuando toca un collider con la cabeza por ejemplo
        if (hit.distance < 0.4 && isGrounded == false && phisicsPlayer.velocity.y < 0 && hit.collider.tag == "Ground")
        {
            //Debug.Log("tocando " + phisicsPlayer.velocity.y);
            isGrounded = true;
        }

        // Si el personaje esta tocando el suelo podra moverse y saltar
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
                // Se utiliza para aplicar una fuerza con potencia simulando un impulso
                phisicsPlayer.AddForce(transform.up * fuerza, ForceMode2D.Impulse);

                isGrounded = false;
            }
        }        
        else
        {
            animations.SetBool("IsGrounded", false);            
        }

        // Variable animator para que se active la animacion de run 
        animations.SetFloat("MoveSpeed", Mathf.Abs(movement));

        // Variable animator para indicar cuando se activara cada una de las animaciones del blender tree
        // El blender tree de las animaciones solo se activa cuando el colider del personaje no esta tocando el suelo
        animations.SetFloat("VerticalVelocity", GetComponent<Rigidbody2D>().velocity.y);
    }
}
