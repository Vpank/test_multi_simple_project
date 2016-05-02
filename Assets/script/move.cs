using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {

    public Transform player;
    public float speed;
    public float jumping;
    int i;
    private Rigidbody2D juping;
    // Use this for initialization
    void Start () {

        //transform.position = Vector3.zero;
       // player.position = transform.position;
       juping = gameObject.GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update() {

        float h = Input.GetAxis("Horizontal");
        juping.AddForce((Vector2.right * speed) * h);
      
        if (Input.GetButtonDown("Jump")) { 

            juping.AddForce(Vector2.up * jumping);
        }
    }
}
