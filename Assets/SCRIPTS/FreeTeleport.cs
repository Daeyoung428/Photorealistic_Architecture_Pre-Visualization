using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeTeleport : MonoBehaviour
{
    private float rotateSpeed = 30f;//speed at which the player rotates
    private static int numBezierPoints = 50;
    private Vector3[] bezierPoints = new Vector3[numBezierPoints];
    private LineRenderer lineRenderer;
    private Vector3 bezierP0, bezierP1, bezierP2;

    public OVRInput.Controller controller;
    public Transform controllerTransform;
    public OVRInput.Button button;

    [Header("--Steps to use the FreeTeleport script--")]
    //[Header("1. Add the script to the OVRCameraRig")]
    [Header("1. Add the 'TeleportMarker' prefab to ")]
    [Header("    the Teleport Marker field")]
    [Header("2. Drag the RightHandAnchor or ")]
    [Header("    LeftHandAnchor into the Controller ")]
    [Header("    Transform field above")]
    [Header("3. Set the Controller and Button to your ")]
    [Header("    desired choices (R touch and Primary ")]
    [Header("    Index Trigger or standard)")]
    [Space(20)]
    [Header("--Using teleport in the Editor (non VR)-- ")]
    //[Header("")]
    [Header("    Left mouse click -> teleport")]
    [Header("    'A' key -> rotate left")]
    [Header("    'D' key -> rotate right")]
    [Space(10)]

    public GameObject teleportMarker;

    private GameObject currTeleportMarker;
    private RaycastHit hit;
    private Transform mainCamera;


    // Use this for initialization
    void Start()
    {
        currTeleportMarker = null;
        mainCamera = GameObject.FindWithTag("MainCamera").transform;
        if(gameObject.GetComponent<LineRenderer>() == null){
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        else{
            lineRenderer = gameObject.GetComponent<LineRenderer>();
        }
        lineRenderer.positionCount = 2;//numBezierPoints;
        lineRenderer.material = new Material(Shader.Find("Standard"));
        lineRenderer.material.color = Color.green;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        HandleInEditorMouseInput();
#else
        HandleOVRInput();
#endif

        UpdateLineRenderer();

    }

    private void UpdateLineRenderer()
    {
        if (currTeleportMarker == null){
            lineRenderer.enabled = false;
            return;
        }
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, controllerTransform.position);
        lineRenderer.SetPosition(1, currTeleportMarker.transform.position);
    }


private void HandleOVRInput()
{
   if (OVRInput.Get(button, controller))
   {
       //cast ray forward to ground 

       Debug.DrawLine(controllerTransform.position, controllerTransform.forward * 10f);

            if (Physics.Raycast(controllerTransform.position, controllerTransform.forward, out hit))
            {

                if (currTeleportMarker == null)
                {
                    //spawn marker
                    currTeleportMarker = Instantiate(teleportMarker, hit.point, transform.rotation);
                }
                //Update cube
                currTeleportMarker.transform.position = hit.point;
            }
            else
            {
                Destroy(currTeleportMarker);
                currTeleportMarker = null;
            }
   }
   else
   {
       if (currTeleportMarker != null)
       {
           //set transform position
           transform.position = new Vector3(currTeleportMarker.transform.position.x, 1.58f, currTeleportMarker.transform.position.z);
           transform.rotation = currTeleportMarker.transform.rotation;
           Destroy(currTeleportMarker);

           //set marker to null
           currTeleportMarker = null;
       }

   }
        Vector2 currTouchPosition = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);

        if (OVRInput.GetDown(OVRInput.Button.Right,controller))
        {
            RotatePointOrPlayer(Vector3.up);
        }
        if (OVRInput.GetDown(OVRInput.Button.Left,controller))
        {
            RotatePointOrPlayer(Vector3.down);
        }
}

    private void HandleInEditorMouseInput()
    {
        if (Input.GetMouseButton(0))
        {
            //cast ray forward to ground 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (currTeleportMarker == null)
                {
                    //spawn marker
                    currTeleportMarker = Instantiate(teleportMarker, hit.point, transform.rotation);
                }
                //Update cube
                currTeleportMarker.transform.position = hit.point;
            }
            else{
                Destroy(currTeleportMarker);
                currTeleportMarker = null;
            }
        }
        else
        {
            if (currTeleportMarker != null)
            {
                //set transform position
                transform.position = new Vector3(currTeleportMarker.transform.position.x, 1.58f, currTeleportMarker.transform.position.z);
                transform.rotation = currTeleportMarker.transform.rotation;
                Destroy(currTeleportMarker);

                //set marker to null
                currTeleportMarker = null;
            }

        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            RotatePointOrPlayer(Vector3.up);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            RotatePointOrPlayer(Vector3.down);
        }
    }

    private void RotatePointOrPlayer(Vector3 axisOfRotation){
        if (currTeleportMarker == null)
        {
            transform.Rotate(axisOfRotation * rotateSpeed);
        }
        else
        {
            currTeleportMarker.transform.Rotate(axisOfRotation * rotateSpeed);
        }
    }

    public void DrawQuadraticCurve()
    {
        for (int i = 1; i <= numBezierPoints; i++){
            float t = i / (float)numBezierPoints;
            bezierPoints[i - 1] = CalculateQuadraticPoint(t, bezierP0, bezierP1, bezierP2);
        }
        lineRenderer.SetPositions(bezierPoints);
    }

    private Vector3 CalculateQuadraticPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = (uu * p0) + (2 * u * t * p1) + (tt * p2);
        return p;
    }
}

