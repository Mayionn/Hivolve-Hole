// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "raymarch/raymarch"
{
    Properties
    {
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct vertex_in
            {
            	float4 pos : POSITION;
            	float2 uv : TEXCOORD0;
            };

            struct frag_in
            {
            	float4 pos : SV_POSITION;
            	float2 uv : TEXCOORD0;
            	float3 ray_dir : TEXCOORD1;
            };

            frag_in vert(vertex_in i)
            {
            	frag_in o;
            	o.pos = UnityObjectToClipPos(i.pos);
            	o.uv = i.uv;
            	//o.ray_dir = normalize(	mul(UNITY_MATRIX_M, i.pos).xyz - _WorldSpaceCameraPos.xyz);
            	o.ray_dir = mul(UNITY_MATRIX_M, i.pos).xyz;
            	return o;
            }

            float4 frag(frag_in i) : COLOR
            {
             	//esfera - centro + raio
             	float3 centre = float3(0,0,0);
             	float radius = 0.05f;
             	float3 cor = float3(0,0,0);
            	float3 ray_origin = _WorldSpaceCameraPos.xyz;
            	float3 ray_dir = -normalize(i.ray_dir-ray_origin);
            	//raymarch
            	//dist max
            	float max_dist = 100.0f;
            	//samples
            	int samples = 1000;
            	float passo = max_dist/(float)samples;
            	for(int k=0; k<samples; k++)
            	{
            		float spos = ray_origin + ray_dir*passo*k;
            		//teste
            		float test = length(spos-centre);
            		if(test < radius)
            		{
            			cor += float3(0.1,0,0.01);
            		}
            	}
            	return float4(cor, 1);
            }
            
            ENDCG
        }
    }
}
