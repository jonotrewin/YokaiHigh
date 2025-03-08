using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    [SerializeField] float speed = 1f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Vector3.right * Input.GetAxis("Horizontal") * speed, ForceMode.VelocityChange);
        rb.AddForce(Vector3.forward * Input.GetAxis("Vertical") * speed, ForceMode.VelocityChange);


    }
}
