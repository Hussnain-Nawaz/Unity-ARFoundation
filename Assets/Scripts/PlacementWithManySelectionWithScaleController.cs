using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class PlacementWithManySelectionWithScaleController : MonoBehaviour
{
    [SerializeField]
    private GameObject placedPrefab;
    [SerializeField]
    private Slider scaleSlider;
    [SerializeField]
    private Camera arCamera;


    public TextMeshProUGUI text;
    public TextMeshProUGUI text2;
    private GameObject placedObject;
    private Vector2 touchPosition = default;
    private ARRaycastManager arRaycastManager;
    private bool onTouchHold = false;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private PlacementObject lastSelectedObject;

    private GameObject PlacedPrefab
    {
        get
        {
            return placedPrefab;
        }
        set
        {
            placedPrefab = value;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        scaleSlider.onValueChanged.AddListener(ScaleChanged);
    }
    private void ScaleChanged(float newValue)
    {
        if(lastSelectedObject!=null && lastSelectedObject.Selected)
        {
            text.text = "Increasing";
            lastSelectedObject.transform.parent.localScale = Vector3.one * newValue;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount>0)
        {
            Touch touch = Input.GetTouch(0);
            if(EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                return;
            }
            touchPosition = touch.position;
            if(touch.phase==TouchPhase.Began){
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if(Physics.Raycast(ray,out hitObject))
                {
                    
                    lastSelectedObject = hitObject.transform.GetComponent<PlacementObject>();
                    if(lastSelectedObject!=null)
                    {
                        PlacementObject[] allOtherObjects = FindObjectsOfType<PlacementObject>();
                        foreach (PlacementObject placementObject in allOtherObjects)
                        {
                            if(placementObject != lastSelectedObject)
                            {
                                placementObject.Selected = false;
                            }
                            else
                            {
                                placementObject.Selected = true;
                                text.text = "Found";
                            }
                        }
                    }
                }
                if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;
                    if(lastSelectedObject==null)
                    {
                        var rotation = Quaternion.FromToRotation(new Vector3(0, 0, 90), hitPose.up);
                        lastSelectedObject = Instantiate(placedPrefab,hitPose.position,rotation).GetComponent<PlacementObject>();
                    }
                }
            }
            if(touch.phase==TouchPhase.Moved)
            {
                if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;
                    if (lastSelectedObject != null && lastSelectedObject.Selected)
                    {
                        lastSelectedObject.transform.parent.position = hitPose.position;
                        lastSelectedObject.transform.parent.rotation = hitPose.rotation;
                    }
                }
            }
        }
    }
}
