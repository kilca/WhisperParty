using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdeaSaved : MonoBehaviour
{

    Transform par;
    bool isBig = false;

    RectTransform trans;

    int index;

    void Start() {
        par = transform.parent;
        trans = GetComponent<RectTransform>();
        index = transform.GetSiblingIndex();
    }

    public void SetBig() {
        isBig = true;

        par.gameObject.SetActive(false);

        transform.SetParent(par.parent);
        trans.localPosition = new Vector3(0, 0, 0);
        trans.localScale = new Vector3(3, 3, 3);
    }

    public void SetLittle() {

        par.gameObject.SetActive(true);
        isBig = false;
        transform.SetParent(par);
        trans.localScale = new Vector3(1, 1, 1);
        transform.SetSiblingIndex(index);
    }

    public void Click() {
        if (isBig)
            SetLittle();
        else
            SetBig();
    }

}
