using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PosterBehaviour : MonoBehaviour
{
    private MeshRenderer posterRenderer;
    public List<string> imatges = new List<string>();
    UnityWebRequest httpClient;
    bool downloading = false;

    public Vector3 posterScale;

    public Image imageInCanvasWS;
    
    void Start()
    {
        imatges.Add("https://spdvisa.blob.core.windows.net/largeblobs/heic1501a.jpg");
        imatges.Add("https://spdvisa.blob.core.windows.net/largeblobs/heic2007a.jpg");
        imatges.Add("https://spdvisa.blob.core.windows.net/largeblobs/opo0501a.jpg");
        imatges.Add("https://spdvisa.blob.core.windows.net/largeblobs/heic0515a.jpg");
        imatges.Add("https://spdvisa.blob.core.windows.net/largeblobs/opo0006a.jpg");

        posterRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        posterScale = transform.localScale;

        if(Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(GetTexture(imatges[3]));
            //GetTextureBlocking(imatges[4]);
        }

        if (downloading)
        {
            Debug.Log(httpClient.downloadProgress * 100 + "% (Bytes downloaded: " + httpClient.downloadedBytes / 1024 + " KB");

        }
    }

    private void GetTextureBlocking(string url)
    {
        httpClient = UnityWebRequestTexture.GetTexture(url);
        //        www.SetRequestHeader("Authorization", "bearer " + token);
        httpClient.SendWebRequest();

        // Es penja. isDone may arriba a true
        //while(!httpClient.isDone)
        //{
        //    Thread.Sleep(5);
        //    Debug.Log(httpClient.downloadProgress * 100 + "% (Bytes downloaded: " + httpClient.downloadedBytes / 1024 + " KB");
        //}


        // I així dona httpClient.error = null
        while (httpClient.downloadProgress < 1f)
        {
            Thread.Sleep(1);
            Debug.Log(httpClient.downloadProgress * 100 + "% (Bytes downloaded: " + httpClient.downloadedBytes / 1024 + " KB");
        }

        // A Unity no li dona temps a actualitzar httpClient.isDone per estar dormint?
        // Canviam Thread.Sleep a 1 ms a veure si li dona temps
        // while (!httpClient.isDone) ;
        // Es penja igual

        //if (httpClient.result != UnityWebRequest.Result.Success)
        //{
        //    Debug.Log(httpClient.error);
        //}
        //else
        //{
        //    Texture2D myTexture = ((DownloadHandlerTexture)httpClient.downloadHandler).texture;
        //    Sprite mySprite = Sprite.Create(myTexture, new Rect(0.0f, 0.0f, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
        //    //playerProfileImage.sprite = mySprite;
        //    posterRenderer.material.SetTexture(myTexture.name, myTexture);
        //}


        // Intentam carregar la imatge a lo brut
        // Error texture has not finished downloading
        //Texture2D myTexture = ((DownloadHandlerTexture)httpClient.downloadHandler).texture;
        //Sprite mySprite = Sprite.Create(myTexture, new Rect(0.0f, 0.0f, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
        ////playerProfileImage.sprite = mySprite;
        //posterRenderer.material.SetTexture(myTexture.name, myTexture);
    }


    IEnumerator GetTexture(string url)
    {
        httpClient = UnityWebRequestTexture.GetTexture(url);
        //        www.SetRequestHeader("Authorization", "bearer " + token);
        downloading = true;
        yield return httpClient.SendWebRequest();
        downloading = false;
        if (httpClient.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(httpClient.error);
        }
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)httpClient.downloadHandler).texture;
            Debug.Log("Texture dimensions: " + texture.width + "x" + texture.height);
            Debug.Log("Quad dimensions: " + posterRenderer.transform.localScale.x + "x" + posterRenderer.transform.localScale.y);

            //RenderMaterial(ref posterRenderer.material, new Vector2Int())
            // Resize to fit image - Not working.
            // Try resizing the texture instead
            // https://forum.unity.com/threads/how-to-resize-scale-down-texture-without-losing-quality.976965/
            float textureAspectRatio = texture.width * 1.0f / texture.height;
            if (textureAspectRatio > 1.0f)
            {
                // Width > Height. Keep postRenderer Width = 4 and adapt height
                //transform.localScale = new Vector3(1, transform.localScale.y / textureAspectRatio, 0);
                posterRenderer.material.mainTextureScale = new Vector2(posterRenderer.material.mainTextureScale.x
                    , transform.localScale.y / textureAspectRatio);
            }
            else
            {
                // Width < Height. Keep postRenderer Height = 4 and adapt width
                //transform.localScale = new Vector3(transform.localScale.x * textureAspectRatio, 1, 0);
                posterRenderer.material.mainTextureScale = new Vector2(posterRenderer.material.mainTextureScale.x * textureAspectRatio
                    , posterRenderer.material.mainTextureScale.y);
            }

            posterRenderer.material.mainTexture = texture;
            Debug.Log("posterRenderer.material.mainTextureScale: " + posterRenderer.material.mainTextureScale);
            //posterRenderer.material.mainTexture = mySprite.texture;
            //GameObject.CreatePrimitive(PrimitiveType.Quad);

            Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height),
                new Vector2(0.5f, 0.5f), 100.0f);
            imageInCanvasWS.sprite = sprite;
        }

        httpClient.Dispose();
    }

    public static Texture2D RenderMaterial(ref Material material, Vector2Int resolution, string filename = "")
    {
        RenderTexture renderTexture = RenderTexture.GetTemporary(resolution.x, resolution.y);
        Graphics.Blit(null, renderTexture, material);

        Texture2D texture = new Texture2D(resolution.x, resolution.y, TextureFormat.ARGB32, false);
        texture.filterMode = FilterMode.Bilinear;
        texture.wrapMode = TextureWrapMode.Clamp;
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(Vector2.zero, resolution), 0, 0);
//#if UNITY_EDITOR
//        // optional, if you want to save it:
//        if (filename.Length != 0)
//        {
//            byte[] png = texture.EncodeToPNG();
//            File.WriteAllBytes(filename, png);
//            AssetDatabase.Refresh();
//        }
//#endif
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(renderTexture);

        return texture;
    }

}
