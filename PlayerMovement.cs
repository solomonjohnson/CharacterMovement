using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 0.0f;
    Animator animator;
    Rigidbody rigidbody;
    Vector3 movement;
    LayerMask floor;
	// Use this for initialization
	void Awake ()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        floor = LayerMask.GetMask("Floor");
	}
	
	// Update is called once per frame
	void Update ()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Animating(h, v);
        Turning();
	}

    void Move(float h, float v)
    {
        //movement.Set(h*speed*Time.deltaTime, 0, v*speed*Time.deltaTime);

        movement.Set(h, 0, v);
        movement = movement.normalized * speed * Time.deltaTime;
        rigidbody.MovePosition(transform.position + movement);
    }

    void Animating(float h, float v)
    {

        bool isWalking;
        if (h !=0.0f || v != 0.0f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        animator.SetBool("isWalking", isWalking);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitPoint;
        float rayLength = 100.0f;
        if(Physics.Raycast(camRay,out hitPoint,rayLength,floor))
        {
            Vector3 rotation = hitPoint.point - transform.position;
            rotation.y = 0.0f;
            Quaternion pointToRotate = Quaternion.LookRotation(rotation);
            rigidbody.MoveRotation(pointToRotate);
            //transform.rotation = Quaternion.EulerAngles(rotation.normalized);

        }
    }
}
