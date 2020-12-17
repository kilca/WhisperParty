using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class MenuScript : MonoBehaviour {

    public TextMeshProUGUI credit;


	// Use this for initialization
	void Start () {

        credit.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void actionCredit() {

        credit.enabled = !credit.enabled;

    }

    public void actionQuit() {


    }

    public void actionPlay() {



    }
}
