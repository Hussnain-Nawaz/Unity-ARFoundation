using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectsManager : MonoBehaviour
{
    [SerializeField]
    private Button toggleLightButton;
    [SerializeField]
    private Button toggleShadowButton;
    [SerializeField]
    private Light defaultLight;
    // Start is called before the first frame update
    void Start()
    {
        if(toggleLightButton==null || toggleShadowButton==null)
        {
            Debug.LogError("Must Bind Both Buttons");
            enabled = false;
            return;
        }
        if (defaultLight==null)
        {
            Debug.LogError("Must set Lights");
            enabled = false;
            return;
        }
        toggleLightButton.onClick.AddListener(ToggleLights);
        toggleShadowButton.onClick.AddListener(ToggleShadows);
    }
    void ToggleLights()
    {
        defaultLight.enabled = !defaultLight.enabled;
    }
    void ToggleShadows()
    {
        if (defaultLight.enabled)
        {
            defaultLight.shadowStrength = defaultLight.shadowStrength>0?1:0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
