using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDPlayerInput : MonoBehaviour {
    public float speed = 6f;            // The speed that the player will move at.

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.
    public UnityEngine.Transform firePoint;
    void Awake()
    {
        // Create a layer mask for the floor layer.
        floorMask = LayerMask.GetMask("Floor");

        // Set up references.
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Store the input axes.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        bool c = Input.GetButton("Crouch");
        bool a = Input.GetButton("Aim");
        // Move the player around the scene.
        Move(h, v, c, a);

        // Turn the player to face the mouse cursor.
        Turning();

        // Animate the player.
        Animating(h, v, c, a);
    }

    void Move(float h, float v, bool c, bool a)
    {
        // Set the movement vector based on the axis input.
        movement.Set(h, 0f, v);
        if(c == true || a ==true)
        {
            movement = movement.normalized * (speed/2) * Time.deltaTime;
        }
        else
        {
            movement = movement.normalized * speed * Time.deltaTime;
        }

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v, bool c, bool a)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        
        if(c == true)
        {
            anim.SetBool("IsCrouching", true);
        }
        else
        {
            anim.SetBool("IsCrouching", false);
        }

        if (a == true)
        {
            anim.SetBool("IsAiming", true);
        }
        else
        {
            anim.SetBool("IsAiming", false);
        }

        anim.SetBool("IsMoving", walking);
    }
    void Aiming()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit rayHit;
    
        if (Input.GetMouseButton(1))
        {
            //Debug.Log("Mouse 1");
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, transform.position.y, Input.mousePosition.z);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out rayHit, 100, floorMask))
            {
                var lookPos = rayHit.point - transform.position;
                lookPos.y = 0;
                Quaternion rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = rotation;
                Vector3 hitPoint = rayHit.point;
                Vector3 vector = new Vector3(0, 1, 0);
                var newPoint = hitPoint + vector;
                firePoint.transform.LookAt(newPoint);
            }
        }
    }
}
