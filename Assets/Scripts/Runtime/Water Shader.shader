Shader "Custom/2DWaterShader_Texture"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _WaveSpeed ("Wave Speed", float) = 1
        _WaveHeight ("Wave Height", float) = 0.1
        _WaveFrequency ("Wave Frequency", float) = 1
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
            };

            sampler2D _MainTex;
            float _WaveSpeed;
            float _WaveHeight;
            float _WaveFrequency;

            v2f vert(appdata v)
            {
                v2f o;

                float2 offset = float2(sin(v.vertex.y * _WaveFrequency + _Time.y * _WaveSpeed), 0) * _WaveHeight;
                float2 distortedVertex = v.vertex.xy + offset;
                o.vertex = UnityObjectToClipPos(float4(distortedVertex, v.vertex.zw));
                o.uv = v.uv;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                return col;
            }
            ENDCG
        }
    }
}