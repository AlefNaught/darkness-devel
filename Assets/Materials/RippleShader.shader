Shader "Custom/RippleShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_Center("Center", Vector) = (0,0,0)
		_Radius("Radius", Float) = 3.0
		_Speed("Speed of 'sound' (units per second)", Float) = 0.5
		_Time2("Current time (seconds)", Float) = 0
		_PulseWidth("Circular length of pulses (wavelength/2)", Float) = 0.125
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		float3 _Center;
		float _Radius;
		float _Speed;
		float _Time2;
		float _PulseWidth;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;

			//====
			float CalcR = min(_Radius, _Speed * _Time2);
			float PointD = distance(_Center, IN.worldPos);
			float PointDPercent = PointD / CalcR;

			if (PointDPercent <= 1.0) {
				int div = (int)ceil(abs(CalcR - PointD) / _PulseWidth);
				if (div % 2 == 0) {
					float r1 = CalcR - div * _PulseWidth;
					float r2 = min(r1 + _PulseWidth, CalcR);
					if (r1 <= PointD && PointD <= r2) {
						o.Emission = c * (1 - PointDPercent); //Linear dropoff
					}
				}
			}

			//====

			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
			//o.Alpha = 1.0;
			//o.Specular = 1.0;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
