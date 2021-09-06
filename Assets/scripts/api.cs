using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;


[Serializable]
public class Root
{
    public List<objects> objects;
}

[Serializable]
public class objects
{
    public string url;
    public int width;
    public int height;
}
public class api: MonoBehaviour
{
    public getTiles getTiles;
    private Root root;
    private string tilesFolderPath;
    private void Start()
    {
        getTiles.SetDefaultMaterial();
        tilesFolderPath = Application.persistentDataPath + "/tiles/";
        StartCoroutine(GetRequest("https://quicklook.orientbell.com/Task/gettiles.php"));
    }

    

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    //Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
                    root = JsonUtility.FromJson<Root>("{ \"objects\" :" + webRequest.downloadHandler.text + "}");
                    for(int i = 0; i < root.objects.Count; i++)
                    {
                        string imageName = root.objects[i].url.Substring(root.objects[i].url.LastIndexOf("/") + 1);
                        string imagePath = tilesFolderPath + imageName;
                        if (File.Exists(imagePath))
                        {
                            byte[] bytes = System.IO.File.ReadAllBytes(tilesFolderPath + imageName);
                            getTiles.placeTileInMenu(bytes);
                            continue;
                        }
                        using (UnityWebRequest webRequestTexture = UnityWebRequestTexture.GetTexture(root.objects[i].url))
                        {
                            yield return webRequestTexture.SendWebRequest();

                            switch (webRequestTexture.result)
                            {
                                case UnityWebRequest.Result.ConnectionError:
                                case UnityWebRequest.Result.DataProcessingError:
                                    Debug.LogError(": Error: " + webRequestTexture.error);
                                    break;
                                case UnityWebRequest.Result.ProtocolError:
                                    Debug.LogError(": HTTP Error: " + webRequestTexture.error);
                                    break;
                                case UnityWebRequest.Result.Success:
                                        DownloadHandlerTexture downloadHandlerTexture = webRequestTexture.downloadHandler as DownloadHandlerTexture;
                                        byte[] bytes;
                                        bytes = downloadHandlerTexture.texture.EncodeToPNG();
                                        if (!Directory.Exists(tilesFolderPath))
                                        {
                                            Directory.CreateDirectory(tilesFolderPath);
                                        }
                                        System.IO.File.WriteAllBytes(tilesFolderPath + imageName, bytes);
                                    break;
                            }
                        }
                    }
                    break;
            }
        }
    }

    
}
