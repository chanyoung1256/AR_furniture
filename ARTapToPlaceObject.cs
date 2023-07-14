using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;


[RequireComponent(typeof(ARRaycastManager))]
public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject[] copyTarget = new GameObject[3];

    private GameObject selectedTarget;
    private GameObject spwandObject;
    private ARRaycastManager _arRaycastManager;
    private Vector2 touchPosition;

    private float rotateY = 0;
    private float scaleFactor = 1;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        selectedTarget = copyTarget[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(!TryGetTouchPosition(out Vector2 touchPosition))
        {
            return;
        }

        Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit raycastHit;
        if (Physics.Raycast(raycast, out raycastHit))
        {
            Vector3 hitPose = raycastHit.point;

            if(raycastHit.transform.tag == "object")
            {
                
            }
            else
            {
                if (spwandObject == null)
                {
                    Quaternion e = Quaternion.Euler(0, 0, 0);
                    spwandObject = Instantiate(selectedTarget, hitPose, e);
                }
                else
                {
                    spwandObject.transform.position = hitPose;
                }
            }
       
        }

    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        //if(Input.touchCount > 0)
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    public void changeObject(int i)
    {
        selectedTarget = copyTarget[i];
        if(spwandObject) Destroy(spwandObject);
        spwandObject = null;
    }

    public void changeRotateY(Slider s)
    {
        rotateY = s.value;
        if(spwandObject) spwandObject.transform.eulerAngles = new Vector3(0, rotateY, 0);
    }

    public void changeScaleFactor(Slider s)
    {
        scaleFactor = s.value;
        if (spwandObject) spwandObject.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }
}
