using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataUtils : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

// Create a scriptable class to configure line renderer settings
public static class LineRendererSettings
{
    private const float LINE_WIDTH = 0.1f;
    private const float TOLERANCE = 0.01f;
    private const bool SHADOWS = false;
    private const bool DYNAMIC_OCCLUSION = true;
    private const bool USE_LIGHTING = false;

    public static void Configure(LineRenderer lineRenderer)
    {
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer is null");
            return;
        }
        lineRenderer.startWidth = LINE_WIDTH;
        lineRenderer.endWidth = LINE_WIDTH;
        lineRenderer.shadowCastingMode = SHADOWS ? UnityEngine.Rendering.ShadowCastingMode.On : UnityEngine.Rendering.ShadowCastingMode.Off;
        lineRenderer.receiveShadows = SHADOWS;
        lineRenderer.allowOcclusionWhenDynamic = DYNAMIC_OCCLUSION;
        lineRenderer.lightProbeUsage = USE_LIGHTING ? UnityEngine.Rendering.LightProbeUsage.BlendProbes : UnityEngine.Rendering.LightProbeUsage.Off;
        lineRenderer.reflectionProbeUsage = USE_LIGHTING ? UnityEngine.Rendering.ReflectionProbeUsage.BlendProbes : UnityEngine.Rendering.ReflectionProbeUsage.Off;
        lineRenderer.Simplify(TOLERANCE);
    }
}