Shader "WaterShader"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _WaveSpeed ("Wave Speed", float) = 1
        _WaveHeight ("Wave Height", float) = 0.1
        _WaveFrequency ("Wave Frequency", float) = 1
        _SurfaceWaveStrength ("Surface Wave Strength", float) = 0
        _WaveImpactExtent ("Wave Impact Extent", range(0,1)) = 0.5
        _WaveCollisionPoint ("Wave Collision Point", Vector) = (0,0,0,0)
        _Alpha("Alpha", range(0,1)) = 1
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;

            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float2 uv_base : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float _WaveSpeed;
            float _WaveHeight;
            float _WaveFrequency;
            float _WaveImpactExtent;
            float _SurfaceWaveStrength;
            float _Alpha;
            Vector _WaveCollisionPoint;

            // surfaceWaveStrength
            // waveOriginUv
            // waveImpactExtent

            v2f vert(appdata v)
            {
                v2f o;
                float sinValue = v.vertex.x * _WaveFrequency + _Time.y * _WaveSpeed - _WaveCollisionPoint.y;
                float expValue = -0.5 * abs(v.vertex.x - _WaveCollisionPoint);

               // float offsetY = exp(expValue) * sin(sinValue)* _WaveHeight;
                float offsetY = expValue * sin(v.vertex.x * _WaveFrequency + _Time.y * _WaveSpeed) * _WaveHeight;
                offsetY = lerp(offsetY, 0, 1-v.uv.y);
                float2 distortedVertex = v.vertex.xy + float2(0, offsetY);

                // call a function that is going to generate the surface wave
                // the surface wave will affect all vertices. However, this offset gets smaller as uv.y gets smaller. This lerp is remapped from 1 to waveImpactExtent


                o.vertex = UnityObjectToClipPos(float4(distortedVertex, v.vertex.zw));
              //  o.vertex = UnityObjectToClipPos(float4(v.vertex));
                o.uv_base = v.uv;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uv);
                col.a *= _Alpha;

                return col;
            }
            ENDCG
        }
    }
}
