using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    Rigidbody rb;
    [SerializeField] private int speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x < -1)
        {
            rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);
        }
        else if (this.transform.position.x > 1)
        {
            rb.velocity = new Vector3(-1 * speed, rb.velocity.y, rb.velocity.z);
        }
    }
}
