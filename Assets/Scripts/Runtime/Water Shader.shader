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

			float invLerp(float a, float b, float value)
			{
				return (value - a) / (b - a);
			}

			v2f vert(appdata v)
			{
				v2f o;
				float direction = 1;

				float sinValue;

				if(v.uv.x < _WaveCollisionPoint.x)
				sinValue = v.uv.x * _WaveFrequency * 6.28 + _Time.y * _WaveSpeed * direction;// - _WaveCollisionPoint.x;
				else
				sinValue = -v.uv.x * _WaveFrequency * 6.28 + _Time.y * _WaveSpeed * direction;// - _WaveCollisionPoint.x;


				float expValue = -3 * abs(v.uv.x - _WaveCollisionPoint.x);

				float offsetY = exp(expValue) * sin(sinValue)* _WaveHeight;
			   //float offsetY = expValue * sin(v.vertex.x * _WaveFrequency + _Time.y * _WaveSpeed) * _WaveHeight;

				float t = invLerp(_WaveImpactExtent,1,1-v.uv.y);
				if (t > 0) t =0;
				//float t = smoothstep(0,_WaveImpactExtent, v.uv.y);
				offsetY = lerp(0, offsetY, t);

			   //offsetY = clamp(0, _WaveImpactExtent, offsetY);
				float2 distortedVertex = v.vertex.xy + float2(0, offsetY);

				// call a function that is going to generate the surface wave
				// the surface wave will affect all vertices. However, this offset gets smaller as uv.y gets smaller. This lerp is remapped from 1 to waveImpactExtent


				o.vertex = UnityObjectToClipPos(float4(distortedVertex, v.vertex.zw));
				//o.vertex = UnityObjectToClipPos(float4(v.vertex));
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
