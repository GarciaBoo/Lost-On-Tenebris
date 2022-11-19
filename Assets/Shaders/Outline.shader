Shader "Hidden/Custom/Outline"
{
    CGINCLUDE
        #include "UnityCG.cginc"
        #pragma  vertex vert
        #pragma fragment frag
    
        struct appdata
        {
            float4 vertex : POSITION;
            float2 uv : TEXCOORD0;
        };

        struct v2f
        {
            float2 uv : TEXCOORD0;
            float4 vertex : SV_POSITION;
        };

        sampler2D _MainTex;
        float4 _MainTex_ST;
        float4 _MainTex_TexelSize;

        v2f vert(appdata v)
        {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv = TRANSFORM_TEX(v.uv, _MainTex);
            return o;
        }
    ENDCG
    
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _KernelSize ("Kernel size", Float) = 1
        _OutlineColor("Outline Color", Color) = (1, 1, 1, 1)
        _OutlineAlpha("Outline Alpha", Range(0, 1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        
        // Silhouette
        Pass
        {
            CGPROGRAM

            float4 _OutlineColor;

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                return fixed4(_OutlineColor.rgb, step(col.a, 0));
            }
            
            ENDCG
        }
        
        // Blur vertical
        Pass
        {
            CGPROGRAM
            float _KernelSize;

            fixed4 frag (v2f i) : SV_Target
            {
                half4 sum = 0;
                const int samples = 2 * _KernelSize + 1;
                for (float y = 0; y < samples; y++)
                {
                    const float2 offset = float2(0, y - _KernelSize);
                    sum += tex2D(_MainTex, i.uv + offset * _MainTex_TexelSize.xy);
                }
                return sum / samples;
            }
            ENDCG
        }
        
        // Blur horizontal
        Pass
        {
            CGPROGRAM

            float _KernelSize;
            
            fixed4 frag (v2f i) : SV_Target
            {
                half4 sum = 0;
                int samples = 2 * _KernelSize + 1;
                for (float x = 0; x < samples; x++)
                {
                    float2 offset = float2(x - _KernelSize, 0);
                    half4 col = tex2D(_MainTex, i.uv + offset * _MainTex_TexelSize.xy);
                    sum += col;
                }

                return sum / samples;
            }
            ENDCG
        }
        
        // Mask
        Pass
        {
            CGPROGRAM

            sampler2D tmp_silhouette;
            float _OutlineAlpha;
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                half mask = tex2D(tmp_silhouette, i.uv).a;
                fixed4 res = col * (1 - mask);
                res.a *= _OutlineAlpha;
                return res;
            }
            ENDCG
        }
        
    }
}
