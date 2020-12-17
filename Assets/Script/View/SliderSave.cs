using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderSave : MonoBehaviour
{
    private Slider _slider;
    public void SetMax(int i) {
        _slider = GetComponent<Slider>();
        _slider.maxValue = i;
    }

    void Start() {
        _slider = GetComponent<Slider>();
    }

    public void ChangeValue() {
        for (int i = 1; i < transform.parent.childCount; i++) {
            if (i == _slider.value)
            {
                transform.parent.GetChild(i).gameObject.SetActive(true);
            }
            else {
                transform.parent.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

}
