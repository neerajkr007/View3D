using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crossHair : MonoBehaviour
{
    public GameObject customizePanel;
    public GameObject tilesPanel;
    public applyTileToWall applyTileToWall;
    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit possibleWall;
        if(Physics.Raycast(ray, out possibleWall))
        {
            if(possibleWall.transform.CompareTag("customizableWall") && !tilesPanel.activeInHierarchy)
            {
                customizePanel.SetActive(true);
                if(Input.GetMouseButtonDown(0))
                {
                    tilesPanel.SetActive(true);
                    applyTileToWall.getWall(possibleWall.transform);
                    tilesPanel.transform.parent.GetChild(2).gameObject.SetActive(false);
                    Cursor.lockState = CursorLockMode.None;
                }
            }
            else
            {
                customizePanel.SetActive(false);
            }
        }
    }
}
