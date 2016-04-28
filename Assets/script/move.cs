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
        
        player.position = new Vector3(0, 0, 0);
        juping = gameObject.GetComponent<Rigidbody2D>();


    }
	
	// Update is called once per frame
	void Update () {
       
            float h = Input.GetAxis("Horizontal");
            juping.AddForce((Vector2.right * speed) * h);
        
        
        if (Input.GetButtonDown("Jump")) { 

            juping.AddForce(Vector2.up * jumping);
        }
    }
}
