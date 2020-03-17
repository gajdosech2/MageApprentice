using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class MaterialExt
{
    public static void MakeOpaque(this Material material)
    {
        material.SetOverrideTag("RenderType", "");
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        material.SetInt("_ZWrite", 1);
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = -1;
    }

    public static void MakeTransparent(this Material material)
    {
        material.SetOverrideTag("RenderType", "Transparent");
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
    }
}
public class CameraController : MonoBehaviour
{

    public Transform target; // leftover
    public Vector3 offset; // leftover

    public Material fadeMaterial;
    public float fadeStartDistance = 1.0f;
    public float fadeEndDistance = 0.7f;
    public float fadeMinOpacity = 0.1f;

    public float zoomoutSmooth = 0.8f;

    Camera cam;
    float max_distance;
    float current_distance;
    Vector3 local_target;
    Vector2 near_plane_extents = new Vector2();
    bool is_material_opaque = true;

    void Start()
    {
        cam = GetComponent<Camera>();
        // warning: camera rotation must be zero, otherwise script does not work properly
        cam.transform.rotation = new Quaternion();

        local_target = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
        max_distance = current_distance = -transform.localPosition.z;

        near_plane_extents.y = cam.nearClipPlane * Mathf.Tan(Mathf.Deg2Rad * cam.fieldOfView * 0.5f);
        near_plane_extents.x = near_plane_extents.y * cam.aspect;

        MaterialExt.MakeOpaque(fadeMaterial);
    }

    float GetSafeDistance()
    {
        Vector3[] near_clip_plane_extents = new Vector3[4];
        near_clip_plane_extents[0] = transform.up * near_plane_extents.y + transform.right * near_plane_extents.x;
        near_clip_plane_extents[1] = transform.up * near_plane_extents.y - transform.right * near_plane_extents.x;
        near_clip_plane_extents[2] = -transform.up * near_plane_extents.y + transform.right * near_plane_extents.x;
        near_clip_plane_extents[3] = -transform.up * near_plane_extents.y - transform.right * near_plane_extents.x;

        Vector3 eye_position = transform.parent.position;
        Vector3 target = transform.parent.localToWorldMatrix * new Vector4(local_target.x, local_target.y, local_target.z, 1.0f);
        float min_near_distance = max_distance;
        RaycastHit hit;
        foreach (Vector3 extent in near_clip_plane_extents)
        {
            Vector3 near_plane_corner = target + extent;
            if (Physics.Raycast(near_plane_corner, -transform.forward, out hit, max_distance, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
            {
                min_near_distance = Mathf.Min(min_near_distance, hit.distance + cam.nearClipPlane);
                //Debug.DrawLine(near_plane_corner, near_plane_corner - transform.forward * hit.distance);
            }
        }
        return min_near_distance;
    }

    void UpdateMaterialTransparency()
    {
        if (current_distance < fadeStartDistance)
        {
            if (is_material_opaque)
            {
                MaterialExt.MakeTransparent(fadeMaterial);
                is_material_opaque = false;
            }

            float alpha = Mathf.Clamp01((current_distance - fadeEndDistance) / (fadeStartDistance - fadeEndDistance));
            Debug.Log("Alpha: " + alpha);

            Color color = fadeMaterial.color;
            color.a = Mathf.Lerp(fadeMinOpacity, 1.0f, alpha);
            fadeMaterial.color = color;
        }
        else
        {
            if (!is_material_opaque)
            {
                MaterialExt.MakeOpaque(fadeMaterial);
                is_material_opaque = true;
            }
        }
    }

    void LateUpdate()
    {
        float safe_distance = GetSafeDistance();

        current_distance = Mathf.Lerp(safe_distance, current_distance, zoomoutSmooth);
        if (safe_distance < current_distance)
        {
            current_distance = safe_distance;
        }

        transform.localPosition = local_target + new Vector3(0, 0, -current_distance);

        UpdateMaterialTransparency();
    }

    void OnDestroy()
    {
        fadeMaterial.color = Color.white;
    }
}
