using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject paintPrefab;

    public Transform shooterBody;
    public Transform shooterCam;

    private int ammoTotal = 6;
    private int ammoCurrent = 6;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		if(Input.GetMouseButtonDown(0))
        {
            FirePaint();
        }
	}

    private void FirePaint()
    {
        var myShot = Instantiate(paintPrefab, shooterBody.position, shooterBody.rotation);

        myShot.GetComponent<Rigidbody>().velocity = myShot.transform.forward * 45;

        // Destroy the bullet after 2 seconds
        Destroy(myShot, 5f);
    }
}
