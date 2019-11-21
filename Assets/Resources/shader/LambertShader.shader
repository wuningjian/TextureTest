// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Unlit/LambertShader"
{
	Properties
	{
		 _MainTex ("Base (RGB)", 2D) = "white" {}
		_DiffusePower("Diffuse Power", Float) = 1.0
	}
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL; // 
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float3 normalDir : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _DiffusePower;
			
			v2f vert (appdata v)
			{
				v2f o;
				// o.vertex = mul(UNITY_MATRIX_MVP, v.vertex); // UnityObjectToClipPos原理-MVP矩阵和顶点矩阵相乘
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				
				// o.normalDir = mul(v.normal, (float3x3)unity_WorldToObject); //UnityObjectToWorldNormal原理-法线坐标系转换
				o.normalDir = UnityObjectToWorldNormal(v.normal); //顶点法线
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float3 normalDirection = normalize(i.normalDir);  //顶点归一化向量
				float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz); //灯光方向
				float3 lightColor = _LightColor0.rgb; //灯光颜色（光照强度）

				// 计算灯光衰减
				float attenuation = LIGHT_ATTENUATION(i);
				float3 attenColor = attenuation * _LightColor0.xyz;

				// 基于兰伯特模型计算灯光 _DiffusePower是高光反射的光泽度系数
				float3 NdotL = max(0.0, dot(normalDirection, lightDirection));
				// 方向光
				float3 directionDiffuse = NdotL * attenColor;
				// 环境光
				float3 inDirectionDiffuse = float3(0,0,0) + UNITY_LIGHTMODEL_AMBIENT.rgb;
				//float3 inDirectionDiffuse = (0,0,0);

				// sample the texture
				float4 texCol = tex2D(_MainTex, i.uv);
				texCol.rgb = texCol.rgb * (directionDiffuse + inDirectionDiffuse);
			
				//return fixed4(inDirectionDiffuse.x, inDirectionDiffuse.y, inDirectionDiffuse.z, 1);
				return texCol;
			}
			ENDCG
		}
	}
	Fallback "Shader/Obj-Default"

}
