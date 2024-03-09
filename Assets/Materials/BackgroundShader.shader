Shader "Eraby/BackgroundShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        // degree of greyscale
        _SkyColor ("SkyColor", Color) = (.25, .5, .5, 1)
    }
    SubShader
    {
     
      Tags{"Queue" = "Transparent"}
  Blend SrcAlpha OneMinusSrcAlpha
  ZTest Off 
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
            float4 _SkyColor;
            float4 _MainTex_ST;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv =  TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // float4 two = (float4)2;
                // float4 r = (1-2*_SkyColor)*pow(col, two) + 2*_SkyColor*col;
                float3 r = col.rgb  + _SkyColor.rgb ;
                float grey = dot(_SkyColor.rgb, float3(0.299, 0.587, 0.114));
                if(grey > 0.5){
                    r = r -1;
                }
                r = clamp(r, (float3)0, (float3) 1);

                return float4(r, col.a);
            }
            ENDCG
        }
    }
}
