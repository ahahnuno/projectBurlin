Shader "Unlit/shaderGradient"
{
    Properties
    {
        _top ("Top Color", Color) = (0.0, 0.0, 0.0, 0.0)
        _bottom ("Bottom Color", Color) = (0.0, 0.0, 0.0, 0.0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float4 _top, _bottom;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 screenPos : TEXCOORD4;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = (_top * i.screenPos.y) + (_bottom * (1.0 - i.screenPos.y)) + (i.screenPos.z / 100);
                return col;
            }

            ENDCG
        }
    }
}
