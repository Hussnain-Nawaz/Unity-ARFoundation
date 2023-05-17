using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlacementObject : MonoBehaviour
{
    [SerializeField]
    private bool IsSelected;

    [SerializeField]
    private TextMeshProUGUI OverlayText;

    [SerializeField]
    private string OverlayDisplayText;

    public bool Selected { get { return this.IsSelected; }
        set {
            IsSelected = value;
        }
    }

    private void Awake()
    {
        OverlayText = GetComponentInChildren<TextMeshProUGUI>();
        if(OverlayText!=null)
        {
            OverlayText.gameObject.SetActive(false);
        }
    }
    public void ShowOverlay()
    {
        OverlayText.gameObject.SetActive(IsSelected);
        OverlayText.text = OverlayDisplayText;
    }
    
}
