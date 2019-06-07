Shader "Custom/Dissolve"
{
    Properties
    {
        _MainTex("Texture2D", 2D) = "white" {}

        _AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)

        _SliceGuide("Slice Guide (RGB)", 2D) = "white" {}
        _SliceAmount("Slice Amount", Range(0.0, 1.0)) = 0
 
        _BurnSize("Burn Size", Range(0.0, 1.0)) = 0.15
        _BurnRamp("Burn Ramp (RGB)", 2D) = "white" {}
        _BurnColor("Burn Color", Color) = (1,1,1,1)
 
        _EmissionAmount("Emission amount", float) = 2.0
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
                float3 viewDir : TEXCOORD1;  
                float3 normal : NORMAL;
                SHADOW_COORDS(2)
            };
            
            uniform sampler2D _MainTex;
            float4 _AmbientColor;
            sampler2D _SliceGuide;
            sampler2D _BumpMap;
            sampler2D _BurnRamp;
            fixed4 _BurnColor;
            float _BurnSize;
            float _SliceAmount;
            float _EmissionAmount;
            
            vertexOut vert(vertexIn i)
            {
                vertexOut o;

                o.pos = UnityObjectToClipPos(i.pos);
                o.uv = i.uv;
                o.normal = UnityObjectToWorldNormal(i.normal);
                o.viewDir = WorldSpaceViewDir(i.pos);
                TRANSFER_SHADOW(o);

                return o;
            }
            
            float4 frag(vertexOut i) : COLOR
            {
                float3 normal = normalize(i.normal);
                float lightdot = dot(_WorldSpaceLightPos0, normal);
               
                float shadow = SHADOW_ATTENUATION(i);

                float lightIntensity = smoothstep(0, 0.01, lightdot * shadow);

                float4 light = lightIntensity * _LightColor0;

                float4 cor = tex2D(_MainTex,float2(i.uv.x, i.uv.y));
                float4 a = (1,1,1,1);
                
                half test = tex2D(_SliceGuide, float2(i.uv.x, i.uv.y)).rgb - _SliceAmount;

                clip(test);
             
                if (test < _BurnSize && _SliceAmount > 0) {
                    a = tex2D(_BurnRamp, float2(test * (1 / _BurnSize), 0)) * _BurnColor * _EmissionAmount;
                }          
                
                return cor * a * (_AmbientColor + light);
            }
            ENDCG
        }

        UsePass "Lightweight Render Pipeline/Lit/ShadowCaster"
}
}