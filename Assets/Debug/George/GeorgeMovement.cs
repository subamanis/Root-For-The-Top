using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GeorgeMovement : MonoBehaviour
{

    private float speed = 0f;

    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        //var a = gameObject.GetComponent<Rigidbody2D>();
        //var b = FindObjectOfType<GeorgeTimer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector2(rb.velocity.x, speed));
    }

}
