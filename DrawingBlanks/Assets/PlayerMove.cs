using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Camera mainCam;
    private Rigidbody myRB;

    private Vector3 myDir;
    private float curSpeed = 0.0f;
    public float topSpeed = 1.0f;


	// Use this for initialization
	void Start ()
    {
        myRB = GetComponent<Rigidbody>();
        mainCam = Camera.main;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        myDir = DirInput();

        ApplyInput();
    }

    private void ApplyInput()
    {
        //Get mouse inputs
        float x = 25 * Input.GetAxis("Mouse X");
        float y = ClampingCamLook(5 * Input.GetAxis("Mouse Y"));

        //Apply rotation to body and camera
        transform.Rotate(0, x, 0);
        mainCam.transform.localRotation *= Quaternion.AngleAxis(y, Vector3.left);

        //Moved based on input direction and rotation
        myRB.position += myDir.z * transform.forward * Time.deltaTime * curSpeed;
        myRB.position += myDir.x * transform.right * Time.deltaTime * curSpeed;
    }

    private float ClampingCamLook(float myC_)
    {
        Quaternion tempRot = mainCam.transform.localRotation * Quaternion.AngleAxis(myC_, Vector3.left);

        //Increase eulers slightly to prevent gimble lock
        if(tempRot.eulerAngles.x < 360 && tempRot.eulerAngles.x > 285)
        {
            myC_ += 0.01f;
        }
        else if(tempRot.eulerAngles.x < 30 && tempRot.eulerAngles.x > 0)
        {
            myC_ += -0.01f;
        }
        else
        {
            myC_ = 0;
        }

        //Clamp returned value to prevent exceeding stated amounts
        return Mathf.Clamp(myC_, -9.0f, 9.0f);
    }

    private Vector3 DirInput()
    {
        Vector3 newDir = new Vector3(0, 0, 0);

        //left, right movement
        if(Input.GetKey(KeyCode.D))
        {
            newDir += new Vector3(1, 0, 0);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            newDir += new Vector3(-1, 0, 0);
        }

        //forward, backward movement
        if(Input.GetKey(KeyCode.W))
        {
            newDir += new Vector3(0, 0, 1);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            newDir += new Vector3(0, 0, -1);
        }

        //Check for speed
        if(newDir != Vector3.zero)
        {
            curSpeed = Mathf.Lerp(curSpeed, topSpeed, 0.05f);
        }
        else
        {
            curSpeed = Mathf.Lerp(curSpeed, 0, 0.01f);
        }

        return newDir * curSpeed;
    }
}
