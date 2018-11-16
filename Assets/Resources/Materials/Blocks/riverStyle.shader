// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/riverStyle"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        _Color("Colour", Color) = (1,1,1,1)
        _AnimationSpeed("Animation speed", Range(0,3)) = 0
        _waveAmplitude("Amp range", Range(0,10)) = 0
        _height("y-value", float) = 0
       
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
                float4 tangent : TANGENT;  
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 position : SV_POSITION;
                
                
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
            float _Color;
            float _AnimationSpeed;
            float _waveAmplitude;
			
			v2f vert (appdata v)
			{   
				v2f o;
                float3 worldPos = mul (unity_ObjectToWorld, v.vertex).xyz;
                v.vertex.z += sin(_Time.y*_AnimationSpeed + v.vertex.y*_waveAmplitude)/1000;
                v.vertex.z += sin(_Time.x*_AnimationSpeed + v.vertex.x*_waveAmplitude)/1000; 
				o.position = UnityObjectToClipPos(v.vertex);
				//o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				//UNITY_TRANSFER_FOG(o,o.vertex);
                o.uv = worldPos.y;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col =_Color;
				// apply fog
				return col;
			}
			ENDCG
		}
	}
}
