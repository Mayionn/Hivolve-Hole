using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update

    public float Speed;

    Vector2 joystickPosition;
    Vector3 mvmntVector;

    GameObject obj;
    bool spawned;

    void Start()
    {
        joystickPosition = new Vector2(
            Screen.width / 2, Screen.height / 8
        );
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touchInput = Input.GetTouch(0);

            if (touchInput.position.y < joystickPosition.y * 2)
            {
                if (touchInput.phase == TouchPhase.Began)
                {
                    Debug.Log("Tapped");
                }
                else if (touchInput.phase == TouchPhase.Moved)
                {
                    mvmntVector = touchInput.position - joystickPosition;
                    mvmntVector.Normalize();

                    mvmntVector.z = mvmntVector.y;
                    mvmntVector.y = 0;
                }
            }

            this.transform.position += (mvmntVector * Speed) * Time.deltaTime;
        }


        transform.position += new Vector3(Speed, 0, 0) * Input.GetAxis("Horizontal") * Time.deltaTime;
        transform.position += new Vector3(0, 0, Speed) * Input.GetAxis("Vertical") * Time.deltaTime;

    }
}
