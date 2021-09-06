using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class applyTileToWall : MonoBehaviour
{
    public Material wallMaterial;
    public GameObject alert;

    Transform MenuTilesParent;
    MeshRenderer wallToCustomize;

    private void Start()
    {
        MenuTilesParent = transform.GetChild(0);
    }

    public void getWall(Transform transform)
    {
        wallToCustomize = transform.gameObject.GetComponent<MeshRenderer>();
    }

    public void tryProceed()
    {
        for(int i = 0; i < MenuTilesParent.childCount; i++)
        {
            if(MenuTilesParent.GetChild(i).GetComponent<Outline>().enabled)
            {
                proceed(i);
                return;
            }
        }
        alert.SetActive(true);

        StartCoroutine(waitForASec());
    }

    IEnumerator waitForASec()
    {
        yield return new WaitForSeconds(2f);
        alert.SetActive(false);
    }

    void proceed(int index)
    {
        transform.parent.gameObject.SetActive(false);
        transform.parent.parent.GetChild(2).gameObject.SetActive(true); 
        Cursor.lockState = CursorLockMode.Locked;
        wallMaterial.SetTexture("_MainTex", MenuTilesParent.GetChild(index).GetComponent<Image>().mainTexture);
        if(wallToCustomize.gameObject.name == "Plane062")
        {
            wallMaterial.SetTextureScale("_MainTex", new Vector2(8f, 8f));
        }
        else if(wallToCustomize.gameObject.name == "Plane059")
        {
            wallMaterial.SetTextureScale("_MainTex", new Vector2(4f, 8f));
        }
        else if (wallToCustomize.gameObject.name == "Plane060")
        {
            wallMaterial.SetTextureScale("_MainTex", new Vector2(12f, 8f));
        }
        wallToCustomize.material.CopyPropertiesFromMaterial(wallMaterial);
    }
}
