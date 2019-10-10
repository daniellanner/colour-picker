Shader "Unlit/MouseTouch"
{
	//Why are we passing HSV values and not color?
	//because we are fussy and the RGB conversion has branches we don't like
	Properties
	{
		_MainTex("Base (RGB), Alpha (A)", 2D) = "white" {}
		_CenterColor("Hue", Color) = (0,0,0)
		_Radius("Radius", Float) = .25
		_Thickness("Thickness", Float) = .1
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
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float4 _CenterColor;
			float _Radius;
			float _Thickness;

			float GreaterThan(float a, float b) {
				return (sign(a - b) + 1.) / 2.;
			}

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				float2 distVector = float2(.5,.5) - i.uv.xy;
				float sqrDistance = distVector.x * distVector.x + distVector.y * distVector.y;

				float outer = _Radius * _Radius;
				float inner = outer - _Thickness * _Thickness;

				float4 outColor = _CenterColor * GreaterThan(outer, sqrDistance) * GreaterThan(sqrDistance, inner) * saturate(_Thickness / .1);
				return outColor;
            }
            ENDCG
        }
    }

}
