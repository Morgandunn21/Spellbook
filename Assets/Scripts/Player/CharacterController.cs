using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public GameObject camTarget;
    public float camRangeMax;
    public float camRangeMin;
    public float moveSpeed;
    public float camSpeed;
    public float zoomSpeed;
    public float minAngle;
    public float maxAngle;

    private float camDist;
    private float speed;


    // Start is called before the first frame update
    void Start()
    {
        camDist = camRangeMin + (camRangeMax - camRangeMin) / 2;
        setCamPos();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        getInput();
    }

    private void getInput()
    {
        var hInput = Input.GetAxis("Horizontal");
        var vInput = Input.GetAxis("Vertical");

        if (hInput != 0 || vInput != 0)
        {
            moveCharacter(hInput, vInput);
        }

        zoomCamera(Input.GetAxis("Mouse ScrollWheel"));
        moveCamera(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    private void moveCharacter(float hInput, float vInput)
    {
        Vector3 delta = new Vector3(hInput, 0, vInput);

        if(delta.magnitude > 1)
        {
            delta.Normalize();
        }

        delta *= moveSpeed * Time.fixedDeltaTime;

        transform.Translate(delta);
    }

    private void zoomCamera(float zoomInput)
    {
        if(zoomInput != 0)
        {
            camDist -= zoomInput * zoomSpeed * Time.fixedDeltaTime;

            if(camDist > camRangeMax)
            {
                camDist = camRangeMax;
            }
            else if(camDist < camRangeMin)
            {
                camDist = camRangeMin;
            }

            Debug.Log(camDist);
            setCamPos();
        }
    }

    private void moveCamera(float mInputX, float mInputY)
    {
        if(mInputX != 0)
        {
            transform.Rotate(new Vector3(0, mInputX * camSpeed * Time.fixedDeltaTime, 0));
        }

        if(mInputY != 0)
        {
            Camera.main.transform.Rotate(new Vector3(-mInputY * camSpeed * Time.deltaTime, 0, 0));

            /*if(Camera.main.transform.rotation.eulerAngles.x > maxAngle)
            {
                Camera.main.transform.Rotate(maxAngle - Camera.main.transform.rotation.eulerAngles.x, 0, 0);
            }
            else if(Camera.main.transform.rotation.eulerAngles.x < minAngle)
            {
                Camera.main.transform.Rotate(minAngle - Camera.main.transform.rotation.eulerAngles.x, 0, 0);
            }*/
            
            //TODO: Dont Let Cam Go Through Walls
            setCamPos();
        }
    }

    private void setCamPos()
    {
        Camera.main.transform.position = camTarget.transform.position;
        Camera.main.transform.Translate(0, 0, -camDist);
    }
}
