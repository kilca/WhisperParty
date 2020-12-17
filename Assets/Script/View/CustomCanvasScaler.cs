using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CustomCanvasScaler : MonoBehaviour
{

    private CanvasScaler scaler;
    // Start is called before the first frame update
    void Start()
    {
        scaler = GetComponent<CanvasScaler>();

        float scale = PlayerPrefs.GetFloat("scale");

        scaler.referenceResolution = scaler.referenceResolution * scale;

    }

}
