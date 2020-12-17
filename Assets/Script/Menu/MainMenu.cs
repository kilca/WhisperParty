using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Slider sliderScale;

    public Text textScale;

    public Text infoText;

    private CanvasScaler scaler;

    public GameObject sliderParent;

    private Vector2 baseScale;

    void Start() {
        scaler = GetComponent<CanvasScaler>();

        float scale = PlayerPrefs.GetFloat("scale");

        baseScale = scaler.referenceResolution;
        if (!PlayerPrefs.HasKey("scale"))
        {
            PlayerPrefs.SetFloat("scale", 1.0f);
            scale = 1;
            sliderScale.value = scale;
            textScale.text = "" + sliderScale.value;
        }
        else {
            sliderScale.value = scale;
            ChangeValue();
        }

    }

    public void ChangeValue() {

        textScale.text = ""+sliderScale.value;

        //scaler.referenceResolution = scaler.referenceResolution * sliderScale.value;
        scaler.referenceResolution = baseScale * sliderScale.value;
        //Debug.Log(scaler.referenceResolution);
        PlayerPrefs.SetFloat("scale",sliderScale.value);

    }

    public void ClickChangeRes() {
        infoText.gameObject.SetActive(!infoText.gameObject.activeSelf);
        sliderParent.SetActive(!sliderParent.activeSelf);
    }

    public void LoadScene() {
        SceneManager.LoadScene("Connection", LoadSceneMode.Single);
    }

}
