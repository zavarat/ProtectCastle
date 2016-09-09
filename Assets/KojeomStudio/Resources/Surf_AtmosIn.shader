//
// Etherea1 for Unity3D
// Written by Vander 'imerso' Nunes -- imerso@imersiva.com
//

Shader "Surf_AtmosIn"
{
	Properties
	{
		_planetRadius ("planetRadius", Float) = 16.0
		_atmosRadius ("atmosRadius", Float) = 17.0
		_sunDir ("sunDir", Vector) = (0, 1, 0)
		_sunIntensity ("sunIntensity", Float) = 1.0
		_camPos ("camPos", Vector) = (0, 0, 0)				// The camera's current position
		_color1 ("color1", Color) = (0, 0.1, 0.5, 1.0)
		_color2 ("color2", Color) = (0.5, 0.3, 0, 1.0)
		_horizonHeight ("horzHeight", Float) = 0.55
		_horizonIntensity ("horzInt", Float) = 2.0
		_horizonPower ("horzPower", Float) = 5.0
		_minAlpha ("minAlpha", Float) = 0.3
	}

	SubShader
	{
			Tags { "Queue"="Transparent" "RenderType"="Transparent" }
			LOD 300
			Blend One One Cull Front ZWrite Off

			CGPROGRAM

			#pragma surface surf Lambert vertex:vert

			uniform float _planetRadius;
			uniform float _atmosRadius;
			uniform float3 _sunDir;
			uniform float _sunIntensity;
			uniform float3 _camPos;
			uniform float3 _color1;
			uniform float3 _color2;
			uniform float _horizonHeight;
			uniform float _horizonIntensity;
			uniform float _horizonPower;
			uniform float _minAlpha;

			struct Input
			{
				float3 normal;
				float3 vertexPos;
			};

			void vert(inout appdata_full v, out Input o)
			{
				o.normal = normalize(v.vertex.xyz);
				o.vertexPos = v.vertex.xyz;
			}

			// ---

			void surf(Input IN, inout SurfaceOutput o)
			{
#if 1

				float camHeight = max(0.1, length(_camPos) - _planetRadius);
				float atmosHeight = _atmosRadius - _planetRadius;

				// height factor is 0 when camera is out (above) the atmosphere,
				// and 1 when the camera is in (below) the atmosphere
				float heightFactor = pow(clamp(atmosHeight / (camHeight * 2.0), 0, 1), 2);

				float3 eyeDir = normalize(_camPos - IN.vertexPos);

				float dotView = dot(IN.normal, eyeDir);
				float dotSun = dot(IN.normal, _sunDir);
				float dotCam = dot(normalize(_camPos), _sunDir);
				float dotSunSquared = pow(1-dotCam, 2);

				float horzHeight = _horizonHeight - max(0, dotCam) * 0.25;
				float dotNormal2Normal = pow( clamp( horzHeight + dot(normalize(_camPos), eyeDir), 0, 1 ), _horizonPower);
				dotNormal2Normal *= _horizonIntensity;

				float tint = clamp(dotSunSquared, 0.0, 1.0);
				float3 color = lerp(_color1, _color2, tint);

				float dotViewFadeIn = clamp(1+dotView, 0, 1);					// center of the sphere
				float dotSunFadeOut = max(0, dotSun);

				float alpha = clamp(dotViewFadeIn, _minAlpha, 1.0) * dotSunFadeOut;
				color *= (_sunIntensity + dotNormal2Normal);
				color *= alpha;

				o.Albedo = color * heightFactor;
				//o.Emission = color;

#else

				float dotSun = max(0, dot(IN.normal, _sunDir));
				float dotSunFadeOut = dotSun;
				o.Albedo = float3(5,5,0) * dotSunFadeOut;

#endif
			}

			ENDCG

	}

	FallBack "Diffuse"
}
