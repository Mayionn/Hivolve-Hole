Shader "Custom/Wobble"
{
    Properties
    {
        _Colour ("Colour", Color) = (1,1,1,1)
        _FillAmount ("Fill Amount", Range(0,1)) = 0.0
		_WobbleX ("WobbleX", Range(-1,1)) = 0.0
		_WobbleZ ("WobbleZ", Range(-1,1)) = 0.0
        _TopColor ("Top Color", Color) = (1,1,1,1)
        _FoamColor ("Foam Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags{ 
            "Queue" = "Geometry-2" 
            "RenderType" = "Opaque" 
            "RenderPipeline" = "LightweightPipeline" 
            "IgnoreProjector" = "True"
            "PassFlags" = "OnlyDirectional"
            }
        LOD 300


        Pass
        {
            Zwrite On
            Cull Off
            AlphaToMask On

            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"


            struct vertexIn
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
               
            };
            
            struct vertexOut
            {
                float4 pos : SV_POSITION; 
                float2 uv : TEXCOORD0;
                float fill : TEXCOORD2;  
                float3 normal : NORMAL;
                SHADOW_COORDS(2)
            };
            
            float _FillAmount;
            float _WobbleX;
            float _WobbleZ;
            float4 _TopColor;

            float4 _FoamColor;
            float4 _Colour;
            
            float4 RotateAroundYInDegrees (float4 vertex, float degrees)
            {
                float alpha = degrees * UNITY_PI / 180;
                float sina, cosa;
                sincos(alpha, sina, cosa); 
                float2x2 m = float2x2(cosa, sina, -sina, cosa);
                return float4(vertex.yz , mul(m, vertex.xz)).xzyw ;
            }

            vertexOut vert(vertexIn i)
            {
                vertexOut o;

                o.pos = UnityObjectToClipPos(i.pos);

                float3 worldPos = mul (unity_ObjectToWorld, i.pos.xyz);
			    float3 worldPosX= RotateAroundYInDegrees(float4(worldPos,0),360);
			    float3 worldPosZ = float3 (worldPosX.y, worldPosX.z, worldPosX.x);
			    float3 worldPosAdjusted = worldPos + (worldPosX  * _WobbleX)+ (worldPosZ * _WobbleZ); 

			    o.fill =  worldPosAdjusted.y + _FillAmount;
                o.uv = i.uv;
                o.normal = UnityObjectToWorldNormal(i.normal);
                TRANSFER_SHADOW(o);

                return o;
            }
            
            float4 frag(vertexOut i, fixed facing : VFACE ) : SV_TARGET
            {
		        float4 result = step(i.fill, 0.5) ;
                float4 resultColored = result * _Colour;
		        float4 topColor = _TopColor *  result;
		        return facing > 0 ? resultColored : topColor;
            }
            ENDCG
        }

        UsePass "Lightweight Render Pipeline/Lit/ShadowCaster"
}
}