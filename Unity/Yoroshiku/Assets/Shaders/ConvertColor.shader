//仅限MainTex的颜色区分不大都为一个色系
Shader "Yoroshiku/ConvertColor"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _TColor ("Templete", Color) = (1,1,1,1) 
		_AColor ("Additive", Color) = (1,1,1,1)
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
            fixed4 _AColor;

            fixed4 frag (appdata i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
				if(col.a == 0.0)
				{
                    discard;
                }
                float gray = col.r/_TColor.r * 0.333 + col.g/_TColor.g * 0.333 + col.b/_TColor.b * 0.333;
                gray *= col.a;
    			return fixed4(gray*_AColor.r, gray*_AColor.g ,gray*_AColor.b, col.a); 
            }
            ENDCG
        }
    }
}
