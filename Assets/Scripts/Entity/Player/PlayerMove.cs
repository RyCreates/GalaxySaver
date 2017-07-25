using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    public float distanceToCamera = 5f;
    private float actualDistanceToCamera;
    public float shipSpeed = 5f;
    public bool useInitialCameraDistance = false;
    private bool canMove = false;

	void Start ()
    {
	    if(useInitialCameraDistance)
        {
            Vector3 camToObjectVector = transform.position - Camera.main.transform.position;
            Vector3 linearDistanceToCameraVector = Vector3.Project(camToObjectVector, Camera.main.transform.forward);
            actualDistanceToCamera = linearDistanceToCameraVector.magnitude;
        }
        else
        {
            actualDistanceToCamera = distanceToCamera;
        }
	}
	
	

	void Update ()
    {
	    if((!canMove && Input.touchCount==1)||(!canMove && Input.GetButton("Fire1")))
        {
            canMove = true;
        }
        if(canMove)
        {
            Vector3 touchPosition = Input.mousePosition;
            touchPosition.z = actualDistanceToCamera;
            Vector3 direction = Camera.main.ScreenToWorldPoint(touchPosition) - this.transform.position;
            float speed = shipSpeed * Time.deltaTime;
            if (direction.magnitude > speed) { transform.Translate(direction.normalized * speed); }
            if (direction.magnitude <= speed) { canMove = false;}
        }
	}
}
