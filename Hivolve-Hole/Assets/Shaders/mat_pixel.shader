Shader "mwanga/mat_pixel"
{
	Properties
	{
		_diff("Cena difusa", Color) = (1,1,1,1)
		_spec("Cena especular", Color) = (1,1,1,1)
		_sh("Cena shiny", Range(1,32)) = 1
	}
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			uniform float4 _diff;
			uniform float4 _spec;
			uniform float _sh;

			struct vertexIn
			{
				float4 pos : POSITION;
				float3 n : NORMAL;
			};

			struct vertexOut
			{
				float4 pos : SV_POSITION;
				float3 n : NORMAL;
				float3 pm : TEXCOORD0;
			};

			vertexOut vert(vertexIn i)
			{
				vertexOut o;

				float3 pm = mul(UNITY_MATRIX_M, i.pos);
				float3 n = normalize(
					mul(float4(i.n, 0), unity_WorldToObject).xyz
				);
				o.pos = UnityObjectToClipPos(i.pos);
				o.n = n;
				o.pm = pm;
				return o;
			}

			float4 frag(vertexOut pos) : COLOR
			{
				float3 n = normalize(pos.n);
				float3 cam = _WorldSpaceCameraPos.xyz;
				float3 look = normalize(pos.pm - cam);
				float3 ldir = normalize(_WorldSpaceLightPos0.xyz);
				float3 r = reflect(ldir, n);
				float idiff = max(0,dot(n,ldir));
				float ispec = 0;
				if (idiff > 0) 
				{ 
					ispec = pow(max(0, dot(r, look)),_sh);
				}
				return _diff*idiff + _spec*ispec;
			}
            ENDCG
        }
    }
}
