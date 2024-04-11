using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelDeactivator : MonoBehaviour
{
    private bool hasDeactivated = false;

    void Update()
    {
        if (!hasDeactivated)
        {
            gameObject.SetActive(false);
            hasDeactivated = true;
        }
    }
}
