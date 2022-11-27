Shader "Unlit/SphereDistortion"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PlaneDistance ("PlaneDistance", Float) = 0.5
        _CircleSize ("CircleSize", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }

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
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _PlaneDistance;
            float _CircleSize;

            float3 pointOnPlane(float2 uv);
            float2 projectOntoCircle(float2 uv);

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = projectOntoCircle(i.uv - float2(0.5, 0.5)) + float2(0.5, 0.5);
                fixed4 col = tex2D(_MainTex, uv);
                
                col.a = min(step(0, uv.x) * step(uv.x, 1) * step(0, uv.y) * step(uv.y, 1), col.a);
                
                return col;
            }

            float3 pointOnPlane(float2 uv)
            {
                return float3(uv.xy, _PlaneDistance);
            }

            float2 projectOntoCircle(float2 uv)
            {
                float3 dir = normalize(pointOnPlane(uv)) * _CircleSize;
                return dir.xy; 
            }
            ENDCG
        }
    }
}
