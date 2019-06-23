using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update

    public float Speed;

    public GameScriptableObject gm;

    Vector2 joystickPosition;
    Vector3 mvmntVector;

    public Transform topLeftConstrain;
    public Transform bottomRightConstrain;

    void Start()
    {
        PowerupSystem.ChangePowerup(PowerupSystem.Powerups.None);

        joystickPosition = new Vector2(
            Screen.width / 2, Screen.height / 8
        );

        Physics.autoSimulation = true;
        gm.paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.paused)
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
                InsideConstraints();
            }
            else
            {
                mvmntVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Speed * Time.deltaTime;
                transform.position += mvmntVector;
                InsideConstraints();
            }
        }
    }

    void InsideConstraints()
    {
        Vector3 np = this.transform.position;
        if (np.x < topLeftConstrain.position.x)
        {
            np.x = topLeftConstrain.position.x;
        }
        else if (np.x > bottomRightConstrain.position.x)
        {
            np.x = bottomRightConstrain.position.x;
        }

        if (np.z > topLeftConstrain.position.z)
        {
            np.z = topLeftConstrain.position.z;
        }
        else if (np.z < bottomRightConstrain.position.z)
        {
            np.z = bottomRightConstrain.position.z;
        }
        this.transform.position = np;
    }
}
