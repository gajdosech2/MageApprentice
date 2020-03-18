using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialFadeByCameraDistance : MonoBehaviour
{
    public Material material;
    new public Camera camera;
    public float fadeStartDistance = 1.0f;
    public float fadeEndDistance = 0.7f;
    public float minAlpha = 0.1f;
    public float maxAlpha = 1.0f;

    bool is_material_opaque = true;

    // Start is called before the first frame update
    void Start()
    {
        MaterialExt.MakeOpaque(material);
    }

    // Update is called once per frame
    void Update()
    {
        float camera_distance = camera.transform.localPosition.magnitude;

        if (camera_distance < fadeStartDistance)
        {
            if (is_material_opaque)
            {
                MaterialExt.MakeTransparent(material);
                is_material_opaque = false;
            }

            float alpha = Mathf.Clamp01((camera_distance - fadeEndDistance) / (fadeStartDistance - fadeEndDistance));

            Color color = material.color;
            color.a = Mathf.Lerp(minAlpha, maxAlpha, alpha);
            material.color = color;
        }
        else
        {
            if (!is_material_opaque)
            {
                MaterialExt.MakeOpaque(material);
                is_material_opaque = true;
            }
        }
    }

    void OnDestroy()
    {
        material.color = Color.white;
    }
}
