Shader "WaterShader"
{
	Properties
	{
		_MainTex ("Main Texture", 2D) = "white" {}
		_Color ("Color", Color) = (0,0,0,0)
		_WaveSpeed ("Wave Speed", float) = 1
		_WaveHeight ("Wave Height", float) = 0.1
		_WaveFrequency ("Wave Frequency", float) = 1
		_SurfaceWaveStrength ("Surface Wave Strength", float) = 0
		_WaveImpactExtent ("Wave Impact Extent", range(0,1)) = 0.5
		_WaveCollisionPoint ("Wave Collision Point", range(0,1)) = 0
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

			#define PI 3.14

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
				float step : TEXCOORD2;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			float _WaveSpeed;
			float _WaveHeight;
			float _WaveFrequency;
			float _WaveImpactExtent;
			float _SurfaceWaveStrength;
			float _Alpha;
			float _WaveCollisionPoint;
			float4 _Color;

			float invLerp(float a, float b, float value)
			{
				return (value - a) / (b - a);
			}

			v2f vert(appdata v)
			{
				v2f o;

				float direction = 1;

				float period = 1/_WaveFrequency * 0.5;
				float step = smoothstep(_WaveCollisionPoint - period, _WaveCollisionPoint, v.uv.x);
				step *= smoothstep(_WaveCollisionPoint + period, _WaveCollisionPoint , v.uv.x);

				if(v.uv.x >= _WaveCollisionPoint + period)
				direction*= -1;

				float sinValue1 = v.uv.x * _WaveFrequency * 2 * PI + _Time.y * _WaveSpeed;
				float sinValue2 = v.uv.x * _WaveFrequency * -2 * PI + _Time.y * _WaveSpeed;
				float tt1 = smoothstep(_WaveCollisionPoint + period, 1, v.uv.x);
				float tt2 = smoothstep(_WaveCollisionPoint - period, 0, v.uv.x);

				float expValue = -3 * abs(v.uv.x - _WaveCollisionPoint);

				//float offsetY = exp(expValue) * sin(sinValue)* _WaveHeight * (step + 0.1);
				float offsetY = _WaveHeight * step * sin(_Time.y * _WaveSpeed) + sin(sinValue1) * _WaveHeight/2 * (1-tt1) + sin(sinValue2) * _WaveHeight/2 * (1-tt2);

				float t = invLerp(_WaveImpactExtent,1,1-v.uv.y);
				if (t > 0) t =0;

				offsetY = lerp(0, offsetY, t);

				float2 distortedVertex = v.vertex.xy + float2(0, offsetY);

				o.vertex = UnityObjectToClipPos(float4(distortedVertex, v.vertex.zw));

				o.uv_base = v.uv;
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.step = step;

				return o;
			}

			float4 frag(v2f i) : SV_Target
			{
				return float4(i.step,i.step,0,1);

				float4 col = tex2D(_MainTex, i.uv);
				col.a *= _Alpha;

				return col * _Color;
			}

			ENDCG
		}
	}
}
