using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovment : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private FixedJoystick fj;
    [SerializeField] private float movmentSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector3(fj.Horizontal * movmentSpeed, rb.velocity.y, fj.Vertical * movmentSpeed);
    }
}
