using UnityEngine;
using System.Collections;

public class plymove : MonoBehaviour {

    public float plySpeed = 5;
    public float jumpHeight = 3.0F;

    private int groundCount = 0;
    private Rigidbody rigidBody;
    private float jumpVel = 0.0F;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        //max h = Vi^2 / (2g)
        //Vi = sqrt(2gh)
        jumpVel = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * jumpHeight);
    }

	// Update is called once per physics "frame"
	void FixedUpdate () {
        // x, z
        Vector3 moveVelocity = Vector3.zero;
        float x = Input.GetAxis("Horizontal");
        float y = 0.0F;
        float z = Input.GetAxis("Vertical");
        bool jump = Input.GetButtonDown("Jump");

        Vector3 direction = new Vector3(x, y, z).normalized;
        moveVelocity = direction * plySpeed;

        //Jump
        if (groundCount > 0 && jump)
        {
            //y = jumpVel / Time.fixedDeltaTime;
            //y = jumpVel / Time.fixedDeltaTime;
            rigidBody.velocity += Vector3.up * jumpVel;
        }

        transform.position += moveVelocity * Time.fixedDeltaTime;

        //We *should* probs find a better place for this
        //Its going here for now
        //Dirty floor update
        GameObject[] grounds = GameObject.FindGameObjectsWithTag("Ground");
        foreach (GameObject ground in grounds)
        {
            Material mat = ground.GetComponent<Renderer>().material;
            mat.SetVector("_Center", transform.position);
            mat.SetFloat("_Time2", Time.fixedTime);
        }

	}

    //Grounded detection
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundCount++;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundCount--;
        }
    }
}
