Shader "ToonShader"
{
    Properties
    {
        _Color("Color", Color) = (1, 0.65, 1, 1)
        _MainTex("BaseMap", 2D) = "white" {}

        [HDR]
        _AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)	

        [HDR]
        _SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1)
        _Glossiness("Glossiness", Float) = 32
    }
    SubShader
    {
        Tags
        {
            "LightMode" = "UniversalForwardOnly"
            "RenderPipeline" = "UniversalPipeline" 
        }
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

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
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            float4 _MainTex_ST;
            
            Varyings vert (appdata v)
            {
                Varyings o = (Varyings)0;
                o.pos = TransformObjectToHClip(v.vertex);              
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal =  mul( v.normal, (float3x3)unity_ObjectToWorld);
                o.viewDir = GetWorldSpaceViewDir(v.vertex);

                return o;
            }
            
            float4 _Color;
            float4 _AmbientColor;
            float _Glossiness;
            float4 _SpecularColor;

            float4 frag (Varyings i) : SV_Target
            {
                float3 normal = normalize(i.worldNormal);

                float NdotL = dot(_MainLightPosition, normal);
                float lightIntensity = smoothstep(0, 0.01, NdotL);
                float4 light = lightIntensity * _MainLightColor;

                float4 sample = _MainTex.Sample(sampler_MainTex, i.uv);

                return _Color * sample * (_AmbientColor + light);
            }
            ENDHLSL
        }
    }
}