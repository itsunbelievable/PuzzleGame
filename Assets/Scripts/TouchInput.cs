using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    Camera mainCam;
    void Start()
    {
        mainCam = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = mainCam.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject reciever = hit.transform.gameObject;
                reciever.SendMessage("Click", reciever.GetComponent<Tile>());
            }

            if (touch.phase == TouchPhase.Ended)
            {
                
            }
        }
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject reciever = hit.transform.gameObject;
                reciever.SendMessage("Click", reciever.GetComponent<Tile>());
            }

        }
#endif
    }
}
