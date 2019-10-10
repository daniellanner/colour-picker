Shader "Unlit/ColourChannelPreviewH"
{
	//Why are we passing HSV values and not color?
	//because we are fussy and the RGB conversion has branches we don't like
	Properties
	{
		_MainTex("Base (RGB), Alpha (A)", 2D) = "white" {}
		_CenterColor("Hue", Color) = (0,0,0)
		_LeftColor("Saturation", Color) = (1,0,0)
		_RightColor("Value", Color) = (0,0,1)
		_Ease("Ease", Float) = .75
	}

    SubShader
    {
        Tags
		{ 
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
		}

		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off
		Lighting Off
		ZWrite Off

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
                float4 vertex : SV_POSITION;
				float2 screenPos : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float4 _CenterColor;
			float4 _LeftColor;
			float4 _RightColor;
			float _Ease;

			float LargerThan(float a, float b) {
				return (sign(a - b) + 1.) / 2.;
			}

			float Ease(float x, float a) {
				return pow(x, a) / (pow(x, a) + pow(1 - x, a));
			}

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.screenPos = ComputeScreenPos(o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
				
			//float leftDelta = Ease(saturate(i.screenPos.x * 2), _Ease);
			//float4 leftcolor = lerp(_LeftColor, _CenterColor, leftDelta);

			//float rightDelta = Ease(saturate(i.screenPos.x - .5) * 2, _Ease);
			//float4 rightcolor = lerp(_CenterColor, _RightColor, rightDelta);
			//	
			//col.rgb = rightcolor * LargerThan(i.screenPos.x, 0.5) + leftcolor * LargerThan(0.5, i.screenPos.x);


			//UV
			float leftDelta = Ease(saturate(i.uv.x * 2), _Ease);
			float4 leftcolor = lerp(_LeftColor, _CenterColor, leftDelta);

			float rightDelta = Ease(saturate(i.uv.x - .5) * 2, _Ease);
			float4 rightcolor = lerp(_CenterColor, _RightColor, rightDelta);

			col.rgb = rightcolor * LargerThan(i.uv.x, 0.5) + leftcolor * LargerThan(0.5, i.uv.x);
				return col;
            }
            ENDCG
        }
    }

}
