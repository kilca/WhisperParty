using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ConnectionMenu : MonoBehaviour {

    public InputField nickField;

    public void OnChange() {
        PlayerPrefs.SetString("Nickname", nickField.text);
    }

    void Start() {
        string s = PlayerPrefs.GetString("Nickname");
        if (s == null)
            s = "";

        nickField.text = s;
    }
	
}
