using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
public class FeatureSupported : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI feature;

    [SerializeField]
    private ARFaceManager arFaceManager;

    [SerializeField]
    private ARHumanBodyManager arHumanBodyManager;

    [SerializeField]
    private ARPointCloudManager arPointCloudManager;

    private ARSession arSession;
    private ARHumanBodyManager arBodyManager;

    // Start is called before the first frame update
    void Start()
    {
        if (/*arHumanBodyManager.subsystem != null ||*/ arPointCloudManager.subsystem != null || arFaceManager.subsystem != null)
        {
            //BodySupport Check
            //bool supportsHumanBody2D = arHumanBodyManager.subsystem.subsystemDescriptor.supportsHumanBody2D;
            //bool supportsHumanBody3D = arHumanBodyManager.subsystem.subsystemDescriptor.supportsHumanBody3D;

            //PointCloud Check
            bool supportConfidence = arPointCloudManager.subsystem.subsystemDescriptor.supportsConfidence;
            bool supportFeaturePoints = arPointCloudManager.subsystem.subsystemDescriptor.supportsFeaturePoints;

            //FaceSupport Check
            bool supportEyeTracking = arFaceManager.subsystem.subsystemDescriptor.supportsEyeTracking;
            bool supportFacePose = arFaceManager.subsystem.subsystemDescriptor.supportsFacePose;
            bool supportFaceMesh = arFaceManager.subsystem.subsystemDescriptor.supportsFaceMeshVerticesAndIndices;
            feature.text = $"SupportEyeTracking: {supportEyeTracking}\n" +
            $"SupportFacePose: {supportFacePose}\n" +
            $"SupportFaceMesh: {supportFaceMesh}\n" +
            //$"SupportHumanBody2D: {supportsHumanBody2D}\n" +
            //$"SupportHumanBody3D: {supportsHumanBody3D}\n" +
            $"SupportConfidence: {supportConfidence}\n" +
            $"SupportFeaturePoints: {supportFeaturePoints}\n";
        }
        else
        {
            feature.text = $"SupportEyeTracking: Not Detected\n" +
            $"SupportFacePose: Not Detected\n" +
            $"SupportFaceMesh: Not Detected\n" +
            $"SupportHumanBody2D: Not Detected\n" +
            $"SupportHumanBody3D: Not Detected\n" +
            $"SupportConfidence: Not Detected\n" +
            $"SupportFeaturePoints: Not Detected\n";
        }
        
    }

}
