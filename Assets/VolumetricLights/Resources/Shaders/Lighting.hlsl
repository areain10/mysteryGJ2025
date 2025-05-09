#if VL_CAST_DIRECT_LIGHT

void AddDirectLighting(float3 wpos, float2 uv, inout half4 sum) {
    #if VL_CAST_DIRECT_LIGHT_BLEND
        VolumetricGBufferData gbuffer;
        GetGBufferData(uv, gbuffer);
	    half3 color = (gbuffer.albedo + gbuffer.specular) * _LightColor.rgb;
        half3 normal = gbuffer.normal;
    #else
        half3 color = _LightColor.rgb;
        half3 normal = SampleSceneNormals(uv);
    #endif
    half3 atten = GetShadowAttenWS_Soft(wpos) * DistanceAttenuation(wpos);
    half3 lambert = LightingLambert(color, _ToLightDir.xyz, normal);
    half3 directLight = lambert * atten * DIRECT_LIGHT_MULTIPLIER;
    sum.rgb += directLight * (1.0 - sum.a);
}

#endif