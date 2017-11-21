// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Unlit/NewUnlitShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	}
		SubShader
	{
		Tags{ "RenderType" = "Opaque" "LightMode" = "ForwardBase" }
		LOD 100

		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
		// make fog work

#pragma multi_compile_fog

#include "UnityCG.cginc"
#include "UnityLightingCommon.cginc" // for _LightColor0
#include "Lighting.cginc"



		struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;

		float3 normal : NORMAL;


	};

	struct v2f
	{
		float2 uv : TEXCOORD0;
		UNITY_FOG_COORDS(1)
		float4 vertex : SV_POSITION;
		float4 color : COLOR1;
		float4 diff: COLOR0;
	};

	sampler2D _MainTex;
	float4 _MainTex_ST;

	v2f vert(appdata_base v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		//o.uv = TRANSFORM_TEX(v.uv, _MainTex);
		o.uv = v.normal;
		
		half3 worldNormal = UnityObjectToWorldNormal(v.normal);

		half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
		// factor in the light color
		o.diff = nl * _LightColor0;


		o.color = float4(v.vertex.x/10, v.vertex.y/10, v.vertex.z/10,0.4f);


		o.diff.rgb += ShadeSH9(half4(worldNormal, 1));

		o.color *= o.diff;








		//UNITY_TRANSFER_FOG(o,o.vertex);
		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{
		// sample the texture
		fixed4 col = i.color;
		//fixed4 col = tex2D(_MainTex, i.uv);
	// apply fog
	UNITY_APPLY_FOG(i.fogCoord, col);
	return col;
	}
		ENDCG
	}
	}
}