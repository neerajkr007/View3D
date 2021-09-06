using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getTiles : MonoBehaviour
{
    public GameObject MenuTileImage;
    public Material defaultWallMaterial;
    public MeshRenderer[] walls;
    public void placeTileInMenu(byte[] bytes)
    {
        Texture2D texture = new Texture2D(16, 16, TextureFormat.RGBA64, false);
        texture.LoadImage(bytes);
        Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        GameObject menuTileImage = Instantiate(MenuTileImage);
        menuTileImage.GetComponent<Image>().sprite = sprite;
        menuTileImage.transform.SetParent(transform.GetChild(0), false);
    }

    public void SetDefaultMaterial()
    {
        for(int i = 0; i < walls.Length; i++)
        {
            walls[i].material = defaultWallMaterial;
        }
    }

}
