using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaqueteController : MonoBehaviour
{

    private Transform raquete;
    private float veloc = 10f;
    private float myLimit = 3.5f;
    public bool isPlayer1;
    public bool isAutomatic = false;
    public Transform transfBall;
    private float varLerp;


    void Start()
    {
        raquete = gameObject.GetComponent<Transform>();
    }

    void Update()
    {
        if (!isAutomatic)
        {
            if (isPlayer1)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    raquete.transform.position = new Vector3(transform.position.x,
                                                            transform.position.y + veloc * Time.deltaTime,
                                                            transform.position.z);
                }

                if (Input.GetKey(KeyCode.S))
                {
                    raquete.transform.position = new Vector3(transform.position.x,
                                                            transform.position.y - veloc * Time.deltaTime,
                                                            transform.position.z);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    isAutomatic = true;
                }

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    raquete.transform.position = new Vector3(transform.position.x,
                                                            transform.position.y + veloc * Time.deltaTime,
                                                            transform.position.z);
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    raquete.transform.position = new Vector3(transform.position.x,
                                                            transform.position.y - veloc * Time.deltaTime,
                                                            transform.position.z);
                }
            }

        }
        else
        {
            // varLerp = Mathf.Lerp(transform.position.y, transfBall.position.y, 0.025f);
            varLerp = Mathf.Lerp(transform.position.y, transfBall.position.y, 0.1f);
            transform.position = new Vector3(transform.position.x, varLerp, transform.position.z);

            if (Input.GetKey(KeyCode.UpArrow) || (Input.GetKey(KeyCode.DownArrow)))
            {
                isAutomatic = false;
            }
        }

        if (transform.position.y > myLimit)
        {
            raquete.transform.position = new Vector3(transform.position.x, myLimit, transform.position.z);
        }

        if (transform.position.y < -myLimit)
        {
            raquete.transform.position = new Vector3(transform.position.x, -myLimit, transform.position.z);
        }
    }

}

