Shader "Draw"
{
    Properties{
        _MainTex("texture", 2D) = "white"{}
    }

    SubShader{
        LOD 100

        Pass{
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            fixed4 _MainTex_ST;

            struct vIn{
                half4 vertex:POSITION;
                float2 texcoord:TEXCOORD0;
                fixed4 color:COLOR;
            };

            struct vOut{
                half4 pos:SV_POSITION;
                float2 uv:TEXCOORD0;
                fixed4 color:COLOR;
            };

            vOut vert(vIn v){
                vOut o;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.color = v.color;
                return o;
            }

            fixed4 frag(vOut i):COLOR{
                fixed4 tex = tex2D(_MainTex, i.uv);
                return tex * i.color;
            }
            ENDCG
        }
    }
}