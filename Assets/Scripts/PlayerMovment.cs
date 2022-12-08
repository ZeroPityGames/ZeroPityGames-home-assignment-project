using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovment : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private Animator anim;
    [SerializeField] private FixedJoystick fj;
    public float movmentSpeed;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private float rotationSpeed;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (new Vector3(fj.Horizontal,0, fj.Vertical) != Vector3.zero)
        {
            //Debug.Log("LOOKING");
            anim.SetBool("IsMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(new Vector3(fj.Horizontal, 0, fj.Vertical), Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector3(fj.Horizontal * movmentSpeed, rb.velocity.y, fj.Vertical * movmentSpeed);
    }

    

}
