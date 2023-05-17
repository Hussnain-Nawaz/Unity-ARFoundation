using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class PlacementController : MonoBehaviour
{
    [SerializeField]
    private GameObject placedPrefab;
    private bool objectPlacement=false;
    public Button toggleButton;
    private ARPlaneManager arPlaneManager;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public GameObject PlacedPrefab
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
    private ARRaycastManager arRayCastManager;


    void Awake()
    {
        arRayCastManager = GetComponent<ARRaycastManager>();
        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(ToggleDetetcion);
        }
        arPlaneManager = GetComponent<ARPlaneManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if(Input.touchCount>0)
        {
            touchPosition = Input.GetTouch(0).position;
            if(Input.GetTouch(0).phase==TouchPhase.Began)
                return true;
        }
        touchPosition = default;
        return false;
    }
    private void ToggleDetetcion()
    {
        arPlaneManager.enabled = !arPlaneManager.enabled;
        foreach (ARPlane plane in arPlaneManager.trackables)
        {
            plane.gameObject.SetActive(arPlaneManager.enabled);
        }
        toggleButton.GetComponentInChildren<Text>().text = arPlaneManager.enabled ? "Disabled" : "Enabled";
    }
    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
        {
            return;
        }
        if(arRayCastManager.Raycast(touchPosition,hits,UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            var rotation = Quaternion.FromToRotation(new Vector3(0,0,90), hitPose.up);
            if(objectPlacement==false)
                Instantiate(placedPrefab, hitPose.position,rotation);
                objectPlacement = true;
        }
    }
    
}
