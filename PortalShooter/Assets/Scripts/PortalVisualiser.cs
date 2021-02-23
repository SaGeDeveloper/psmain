using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalVisualiser : MonoBehaviour
{
    public Transform normalOut;
    public Transform normalIn;
    public PortalRecursionController recursionController;
    public Renderer viewthroughRenderer;
    public Renderer cubeViewthroughRenderer;
    private Material viewthroughMaterial;
    private Material cubeViewthroughMaterial;
    private Camera mainCamera;
    private RenderTexture viewthroughRenderTexture;
    public Camera portalCamera;
    private Vector4 vectorPlane;
    public static Vector3 TransformPositionBetweenPortals(PortalVisualiser sender, PortalVisualiser target, Vector3 position)
    {
        return
            target.normalIn.TransformPoint(
                sender.normalOut.InverseTransformPoint(position));
    }

    public static Quaternion TransformRotationBetweenPortals(PortalVisualiser sender, PortalVisualiser target, Quaternion rotation)
    {
        return
            target.normalIn.rotation *
            Quaternion.Inverse(sender.normalOut.rotation) *
            rotation;
    }

    private void Start()
    {
        // Create render texture
        
        viewthroughRenderTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.DefaultHDR);
        viewthroughRenderTexture.Create();
        mainCamera = Camera.main;
        // Generate bounding plane

        var plane = new Plane(normalOut.forward, transform.position);
        vectorPlane = new Vector4(plane.normal.x, plane.normal.y, plane.normal.z, plane.distance);
         //Assign render texture to portal material (cloned)

        viewthroughMaterial = viewthroughRenderer.material;
        viewthroughMaterial.mainTexture = viewthroughRenderTexture;
        // and to cube material
        cubeViewthroughMaterial = cubeViewthroughRenderer.material;
        cubeViewthroughMaterial.mainTexture = viewthroughRenderTexture;
     
        // Assign render texture to portal camera

        portalCamera.targetTexture = viewthroughRenderTexture;
        

    }

    void LateUpdate()
    {
        //Debug.Log(mainCamera);
        var virtualPosition = TransformPositionBetweenPortals(this, recursionController.OtherPortal.gameObject.GetComponent<PortalVisualiser>(), mainCamera.transform.position);
        var virtualRotation = TransformRotationBetweenPortals(this, recursionController.OtherPortal.gameObject.GetComponent<PortalVisualiser>(), mainCamera.transform.rotation);
        // Position camera
        portalCamera.transform.SetPositionAndRotation(virtualPosition, virtualRotation);
        // Calculate projection matrix
    
        var clipThroughSpace = Matrix4x4.Transpose(Matrix4x4.Inverse(portalCamera.worldToCameraMatrix)) * recursionController.OtherPortal.gameObject.GetComponent<PortalVisualiser>().vectorPlane;
        
        // Set portal camera projection matrix to clip walls between target portal and portal camera
        // Inherits main camera near/far clip plane and FOV settings
        
        var obliqueProjectionMatrix = mainCamera.CalculateObliqueMatrix(clipThroughSpace);
        portalCamera.projectionMatrix = obliqueProjectionMatrix;
        
    }

    private void OnDestroy()
    {
        // Release render texture from GPU
        
        viewthroughRenderTexture.Release();

        // Destroy cloned material and render texture
        
        Destroy(viewthroughMaterial);
        Destroy(viewthroughRenderTexture);
    }
}
