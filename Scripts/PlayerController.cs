using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator runAnimation;

    float velocidad = 1f;
    float fuerza = 30f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float movement = Input.GetAxis("Horizontal");
        runAnimation = GetComponent<Animator>();

        if (Input.GetButton("Horizontal"))
        {
            Rigidbody2D movPlayer = GetComponent<Rigidbody2D>();            

            if (movement > 0)
            {
                if(transform.localScale.x < 0)
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

            movPlayer.velocity = (transform.right * velocidad * movement);
            // Mathf.Abs => si el valor de la variable que se pasa por parametro es negativo se pasa a positivo
            
        }

        if (Input.GetButton("Jump"))
        {
            //float movement = Input.GetAxis("Jump");

            Rigidbody2D movPlayer = GetComponent<Rigidbody2D>();
            movPlayer.AddForce(transform.up * fuerza);

            //transform.position += velocity * Vector3.right * 0.0001f * Time.deltaTime;
        }

        runAnimation.SetFloat("MoveSpeed", Mathf.Abs(movement));
        Debug.Log(movement);
    }
}
