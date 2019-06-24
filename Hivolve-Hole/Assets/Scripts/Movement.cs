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

    public GameObject mesh;
    private Bounds bounds;

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

        bounds = mesh.GetComponent<MeshRenderer>().bounds;
    }

    // Update is called once per frame
    void Update()
    {
        bounds = mesh.GetComponent<MeshRenderer>().bounds;
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

        if (np.x - bounds.extents.x < topLeftConstrain.position.x)
        {
            Debug.Log("A");
            np.x = topLeftConstrain.position.x + bounds.extents.x;
        }
        else if (np.x + bounds.extents.x > bottomRightConstrain.position.x)
        {
            Debug.Log("B");
            np.x = bottomRightConstrain.position.x - bounds.extents.x;
        }

        if (np.z + bounds.extents.z > topLeftConstrain.position.z)
        {
            Debug.Log("C");
            np.z = topLeftConstrain.position.z - bounds.extents.z;
        }
        else if (np.z - bounds.extents.z < bottomRightConstrain.position.z)
        {
            Debug.Log("D");
            np.z = bottomRightConstrain.position.z + bounds.extents.z;
        }
        this.transform.position = np;
    }
}
