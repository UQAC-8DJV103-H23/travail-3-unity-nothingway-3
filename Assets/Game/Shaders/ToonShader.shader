Shader "ToonShader"
{
    Properties
    {
        [MainColor]
        _Color("Color", Color) = (1, 0.65, 1, 1)
        [MainTexture]
        _MainTex("BaseMap", 2D) = "white" {}

        [HDR]
        _AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)	

        [HDR]
        _SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1)
        _Glossiness("Glossiness", Float) = 32

        [HDR]
        _RimColor("Rim Color", Color) = (1,1,1,1)
        _RimAmount("Rim Amount", Range(0, 1)) = 0.716
        _RimThreshold("Rim Threshold", Range(0, 1)) = 0.1

    }
    SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalPipeline" 
        }
        Pass
        {
            Tags{"LightMode" = "UniversalForwardOnly"}
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/CommonMaterial.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct appdata
            {
                float4 vertex : POSITION;				
                float4 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct Varyings
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldNormal : NORMAL;
                float3 viewDir : TEXCOORD1;
                float4 shadowCoord : TEXCOORD2;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float4 _Color;
                float4 _AmbientColor;
                float _Glossiness;
                float4 _SpecularColor;
                float4 _RimColor;
                float _RimAmount;
                float _RimThreshold;
            CBUFFER_END

            Varyings vert (appdata v)
            {
                Varyings o = (Varyings)0;
                o.pos = TransformObjectToHClip(v.vertex);              
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal =  mul( v.normal, (float3x3)unity_ObjectToWorld);
                o.viewDir = GetWorldSpaceViewDir(v.vertex);
                o.shadowCoord = TransformWorldToShadowCoord(o.pos);
                
                return o;
            }
            


            float4 frag (Varyings i) : SV_Target
            {
                float3 normal = normalize(i.worldNormal);

                float NdotL = dot(_MainLightPosition, normal);
                Light mainLight = GetMainLight();
                float shadow = mainLight.shadowAttenuation;
                float lightIntensity = smoothstep(0, 0.01, NdotL * shadow);
                float4 light = lightIntensity * _MainLightColor;

                float3 viewDir = normalize(i.viewDir);
                float3 halfVector = normalize(_MainLightPosition + viewDir);
                float NdotH = dot(normal, halfVector);

                float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
                float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
                float4 specular = specularIntensitySmooth * _SpecularColor;

                float4 rimDot = 1 - dot(viewDir, normal);
                float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
                rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
                float4 rim = rimIntensity * _RimColor;

                float4 sample = _MainTex.Sample(sampler_MainTex, i.uv);

                return _Color * sample * (_AmbientColor + light + specular + rim);
            }
            ENDHLSL
        }
        
       Pass {
	Name "ShadowCaster"
	Tags { "LightMode"="ShadowCaster" }

	ZWrite On
	ZTest LEqual

	HLSLPROGRAM
	#pragma vertex ShadowPassVertex
	#pragma fragment ShadowPassFragment

	// Material Keywords
	#pragma shader_feature _ALPHATEST_ON
	#pragma shader_feature _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

	// GPU Instancing
	#pragma multi_compile_instancing
	// (Note, this doesn't support instancing for properties though. Same as URP/Lit)
	// #pragma multi_compile _ DOTS_INSTANCING_ON
	// (This was handled by LitInput.hlsl. I don't use DOTS so haven't bothered to support it)

    #include "Packages/com.unity.render-pipelines.universal/Shaders/SimpleLitInput.hlsl"
	#include "Packages/com.unity.render-pipelines.universal/Shaders/ShadowCasterPass.hlsl"
	ENDHLSL
}
    }
}