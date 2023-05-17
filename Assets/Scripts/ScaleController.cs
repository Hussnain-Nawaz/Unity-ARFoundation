using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScaleController : MonoBehaviour
{
    private bool check = false;
    public Slider slider;
    private GameObject objectToScale;
    public TextMeshProUGUI text;
    private Vector3 initialScale;

    public void OnSliderValueChanged(float value)
    {
        text.text = "Changing";
        objectToScale.transform.localScale = initialScale * value;
    }


    // Update is called once per frame
    void Update()
    {
        if (check == false) { 
            objectToScale = GameObject.FindGameObjectWithTag("Dragon");
            if (objectToScale != null)
            {
                text.text = "Found";
                Initialvalue();
                check = true;
            }
        }

    }
    public void Initialvalue()
    {
        initialScale = objectToScale.transform.localScale;
    }
}
