//将MainTex根据指定颜色进行权重计算(以亮度为权重则输出灰度图)
Shader "Yoroshiku/WeightColor"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _TColor ("Templete", Color) = (0.299,0.587,0.114,1) //默认灰度图
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
        	CGPROGRAM
            #pragma vertex SpriteVert
            #pragma fragment frag
            #pragma target 2.0
            #pragma multi_compile_instancing
            #pragma multi_compile _ PIXELSNAP_ON
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
            #include "UnitySprites.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            fixed4 _TColor;

            fixed4 frag (appdata i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
				if(col.a == 0.0)
				{
                    discard;
                }
                //float gray = dot(col.rgb, float3(0.299, 0.587, 0.114));
                float gray = dot(col.rgb, _TColor.rgb);
				gray *= col.a; //大雾
    			return fixed4(gray,gray,gray,col.a);
            }
            ENDCG
        }
    }
}
