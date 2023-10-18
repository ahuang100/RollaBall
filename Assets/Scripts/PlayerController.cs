using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
 //inf jump boolean 
 private bool onGround; 
 // Rigidbody of the player.
 private Rigidbody rb; 
private int count;
 // Movement along X and Y axes.
 private float movementX;
 private float movementY;

 // Speed at which the player moves.
 public float speed = 0; 

 public TextMeshProUGUI countText;

 // Start is called before the first frame update.
 void Start()
    {
 // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();
        count = 0; 
        SetCountText();
        onGround = false; 
    }
 
 // This function is called when a move input is detected.
 void OnMove(InputValue val)
    {
 // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = val.Get<Vector2>();

         Debug.Log("move"); 
 // Store the X and Y components of the movement.
        movementX = movementVector.x; 
        movementY = movementVector.y; 
    }

   void OnJump(InputValue val)
   {
      Debug.Log("jumped");
      if(!onGround)
      {
      rb.velocity = new Vector3(rb.velocity.x, 5, rb.velocity.z); 
      }
   }

   void SetCountText() 
   {
       countText.text =  "Count: " + count.ToString();
   }

 // FixedUpdate is called once per fixed frame-rate frame.
 private void FixedUpdate() 
    {
 // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
 // Apply force to the Rigidbody to move the player.
        rb.AddForce(movement * speed); 
    }
      void OnTriggerEnter(Collider other) 
   {
          if (other.gameObject.CompareTag("PickUp")) 
       {
           other.gameObject.SetActive(false);
           count = count + 1;
           SetCountText();
       }
        if(other.gameObject.CompareTag("Mesh"))
        {
         onGround = true; 
        }
   }
   void OnTriggerExit(Collider other) {
        Debug.Log(other);
        if(other.gameObject.CompareTag("Mesh"))
        onGround = false;
    }
}