Shader "Custom/Hole"
{
    Properties
    {
        _Color("Cor", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags{"Queue" = "Geometry-2" "RenderType" = "Opaque" "RenderPipeline" = "LightweightPipeline" "IgnoreProjector" = "True"}
        LOD 300

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            uniform float4 _Color;

            struct vertexIn
            {
                float4 pos : POSITION; 
                float4 color : COLOR; 
            };
            
            struct vertexOut
            {
                float4 pos : SV_POSITION; 
                float4 color : COLOR; 
            };
            
            vertexOut vert(vertexIn i)
            {
                vertexOut o;

                o.pos = UnityObjectToClipPos(i.pos);
                o.color = i.color;

                return o;
            }
            
            float4 frag(vertexOut pos) : COLOR
            {
                return _Color;
            }
            ENDCG
        }
    }
}
