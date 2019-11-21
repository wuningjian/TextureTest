Shader "Unlit/RippleShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_F("F",Range(0,29))      = 10           //周期
		_A("A",Range(0,0.1))     = 0.01         //振幅
		_R("R",Range(0,1))       = 0.2          //水波范围
	}
	SubShader
	{

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			
			#include "UnityCG.cginc"
			#define PI 3.14159

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
			float _F;
			float _A;
			float _R;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				//水波实现
				//i.uv += _A * sin(i.uv*PI*_F + _Time.y); //_Time (x,y,z,w) = (t/20, t, t*2, t*3)

				//水波荡漾发散
				float dis = distance(i.uv, float2(0.5f,0.5f));//固定点距离
				float f = saturate(1 - dis / _R);//限制value在0,1之间并返回value。如果value小于0，返回0。如果value大于1,返回1，否则返回value 
				i.uv += f * _A * sin(-dis * 3.14 * _F + _Time.y);

				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				return col;
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}
