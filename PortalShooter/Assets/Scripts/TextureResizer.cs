using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureResizer : MonoBehaviour
{
    public Camera camera;
    public Material cameraMaterial;
    public RenderTexture texture;
    // Start is called before the first frame update
    void Start()
    {
        if (camera.targetTexture != null)
		{
			camera.targetTexture.Release();
		}
		camera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
		texture = camera.targetTexture;

    }

    // Update is called once per frame
    
}
