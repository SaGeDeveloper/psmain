using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalRenderer : MonoBehaviour
{
    public Camera portalCamera;
    public int maxRecursions = 2;
    
    public int debugTotalRenderCount;

    private Camera mainCamera;
    private PortalRecursionController[] allPortals;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        allPortals = FindObjectsOfType<PortalRecursionController>();
    }

    private void OnPreRender()
    {
        debugTotalRenderCount = 0;

        foreach (var portal in allPortals)
        {
            // TODO: Render portal here
        }
    }
    private void OnPostRender()
    {
        RenderTexturePool.Instance.ReleaseAllTextures();
    }
    // Update is called once per frame
}
