using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalRenderer : MonoBehaviour
{
    public Camera portalCamera;
    public int maxRecursions = 2;
    
    public int debugTotalRenderCount;

    private Camera mainCamera;
    private PortalVisualiser[] allPortals;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        allPortals = FindObjectsOfType<PortalVisualiser>();
    }
    


    private void LateUpdate()
    {
        Debug.Log("PRERENDER");
        debugTotalRenderCount = 0;

        foreach (var portal in allPortals)
        {
            portal.DeepRender(
            mainCamera.transform.position,
            mainCamera.transform.rotation,
            out _,
            out _,
            out var renderCount,
            portalCamera,
            0,
            maxRecursions);

        debugTotalRenderCount += renderCount;
        }
    }
    private void Update()
    {
        RenderTexturePool.Instance.ReleaseAllTextures();
    }
    // Update is called once per frame
}
