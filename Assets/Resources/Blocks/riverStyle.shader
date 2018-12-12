// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

<<<<<<< HEAD:Assets/Resources/Blocks/riverStyle.shader
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
=======
Shader "Unlit/riverStyle"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        _BaseColor ("Base color", COLOR)  = ( .54, .95, .99, 0.5) 
        _Color("Colour", Color) = (0,1,0,1)
        _AnimationSpeed("Animation speed", Range(0,3)) = 0
        _waveAmplitude("Amp range", Range(0,10)) = 0
        _Transparency("Transparency", Range(0.0,1)) = 0.99
       
	}
	SubShader
	{
		Tags {"Queue"="Transparent" "RenderType"="Transparent" }
		LOD 100
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"
            uniform float4 _BaseColor;  

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
                float4 tangent : TANGENT;  
                float3 normal : NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
>>>>>>> waterEffect2:Assets/Resources/Materials/Blocks/riverStyle.shader
				float4 position : SV_POSITION;
                float3 normal : TEXCOORD1;
                float4 viewToPixel : TEXCOORD2;
                float4 normalInterpolator : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float4 posWorld : TEXCOORD5;
                float displ : TEXCOORD6;
                
<<<<<<< HEAD:Assets/Resources/Blocks/riverStyle.shader
                
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
=======
                
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
            float _Color;
            float _AnimationSpeed;
            float _waveAmplitude;
            float _Transparency;
            
            

			
			v2f vert (appdata v)
			{   
				v2f o;
                float3 objectOrigin = mul(unity_ObjectToWorld, float4(0.0,0.0,0.0,1.0) );
                float3 worldPos = mul (unity_ObjectToWorld, v.vertex).xyz;
                half3   offsets = half3(0,0,0);
                
                o.viewToPixel.w = saturate(offsets.y);
                o.viewToPixel.xyz = worldPos - _WorldSpaceCameraPos;
                o.normalInterpolator.xyz = half3(0,1,0);
                o.normalInterpolator.w = 1; 
                
                //this is what offsets the water
                //the step makes sure that only the top of the water i affected
                float displacement = step(0.0, worldPos.y-objectOrigin.y)*((_waveAmplitude*sin(_Time.y*_AnimationSpeed + (worldPos.x+worldPos.z)*1000)/2300+_waveAmplitude*sin(_Time.y*_AnimationSpeed + (worldPos.x-worldPos.z)*500)/1000)-0.002);
                
                //this is to calc Tangent and bit tangent to new pos - so we can find new normal for the light
                //http://diary.conewars.com/vertex-displacement-shader/ ?

                // TangentDisplacement = 
                 float displacementx = step(0.0, worldPos.y-objectOrigin.y)*((_waveAmplitude*sin(_Time.y*_AnimationSpeed + v.vertex.y*1000)/3000+_waveAmplitude*sin(_Time.y*_AnimationSpeed + (v.vertex.x+0.1)*1000)/1000)-0.002);
                
                
                float3 newTangent = (float3(0,0.1,displacementx)); // leaves just 'tangent'
                float3 newBitangent = (float3(0.1,0,displacementx)); // leaves just 'bitangent'
                float3 newNormal = cross( newTangent, newBitangent);
                //v.normal = newNormal.xyz;
                //this makes the normal negative if the dot is bellow the origin.
                //this makes the normal negative if the dot is bellow the origin.
                float y = 0.5+(1-step(0.0, worldPos.y-objectOrigin.y))*-1.0;
                newNormal=float3(0,y,0);
                v.normal = newNormal.xyz;
                //add the displacement
                v.vertex.y += displacement;
                //change it to ??
				o.position = UnityObjectToClipPos(v.vertex);
                
                o.displ = displacement;
                //?? to change the normal to object?
                o.normal = mul(
                    transpose((float3x3)unity_WorldToObject),
                    v.normal);

				return o;
			}
            
            //inspired by  https://github.com/danielzeller/Lowpoly-Water-Unity/blob/master/Assets/Shaders/FlatShadedWaterWithEdgeBlend.shader
         half4 calculateBaseColor(v2f input)  
         {
            float3 normalDirection = normalize(input.normalDir);
 
            float3 viewDirection = normalize(
               _WorldSpaceCameraPos - input.posWorld.xyz);
            float3 lightDirection;
            float attenuation;
 
            if (0.0 == _WorldSpaceLightPos0.w) // directional light?
            {
               attenuation = 1.0; // no attenuation
               lightDirection = normalize(_WorldSpaceLightPos0.xyz);
            } 
            else // point or spot light
            {
               float3 vertexToLightSource = 
                  _WorldSpaceLightPos0.xyz - input.posWorld.xyz;
               float distance = length(vertexToLightSource);
               attenuation = 1.0 / distance; // linear attenuation 
               lightDirection = normalize(vertexToLightSource);
            }
 
            float3 ambientLighting = 
               UNITY_LIGHTMODEL_AMBIENT.rgb * _BaseColor.rgb;
            float3 _LightColor0 = (0,0,0);
            float3 _SpecColor = (0,0,0);
            float3 diffuseReflection = 
               attenuation * _LightColor0.rgb * _BaseColor.rgb
               * max(0.0, dot(normalDirection, lightDirection));
            ambientLighting = float3(0.2,0.6+60*input.displ,0.8+(20*input.displ));
            //float3 specularReflection;
            //if (dot(normalDirection, lightDirection) < 0.0) 
            //   // light source on the wrong side?
            //{
            //   specularReflection = float3(0.0, 0.0, 0.0); 
            //      // no specular reflection
            //}
            //else  
            //{
            //   specularReflection = attenuation * _LightColor0.rgb  * _SpecColor.rgb * pow(max(0.0, dot(reflect(-lightDirection, normalDirection), viewDirection)), _Shininess);
            //}

            return half4(ambientLighting, .9);
         }
			
			fixed4 frag (v2f i) : SV_Target
			{
                //fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 col = calculateBaseColor(i);
                col.a = step(0.0,i.normal.y)*_Transparency;
                return col;
                
                //return calculateBaseColor(i);
				// sample the texture
				//fixed4 col = fixed4(0,1,sqrt(i.position.z),0);
                
               // float3 worldPos = mul (unity_ObjectToWorld, i.position).xyz;
                //return saturate(dot(float3(0, 1, 0), i.normal));
               // return float4(0,0.2,1,1);
                
				// apply fog

			}
			ENDCG
		}
	}
}
>>>>>>> waterEffect2:Assets/Resources/Materials/Blocks/riverStyle.shader
