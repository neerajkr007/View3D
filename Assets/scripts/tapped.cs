using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class tapped : MonoBehaviour
{
    public void MenuTileImageTapped()
    {
        for(int i = 0; i < transform.parent.childCount; i++)
        {
            transform.parent.GetChild(i).GetComponent<Outline>().enabled = false;
        }
        GetComponent<Outline>().enabled = true;
    }
}
