Shader "Graph/Point"
{
    Properties{
        _Smoothness("Smoothness",Range(0,1)) = 0.5
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200
        
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
    float _Smoothness;
        struct Input
        {
            float3 worldPos;
        };



        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = IN.worldPos * 0.5  + 0.5;
            o.Smoothness = _Smoothness;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
