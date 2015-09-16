Shader "Custom/Glow Shader" {
    Properties {
        _MainTex ("Texture (alpha)", 2D) = "white" {}
        _TintColor ("Tint", color) = (1, 1, 1, 1)
        _Intensity ("Intensity", float) = 1
    }
    SubShader {
    	Tags {
    	    "Queue" = "Transparent"
    		"PreviewType" = "Plane"
		}
    	
    	Pass {
//    		Tags { "LightMode" = "ForwardBase" }
    		
            // BlendOp BlendOp
            // 
            // Add Add source and destination together.
            // Sub Subtract destination from source.
            // RevSub  Subtract source from destination.
            // Min Use the smaller of source and destination.
            // Max Use the larger of source and destination.
            BlendOp Add

                        
            // Blend SrcFactor DstFactor, SrcFactorA DstFactorA
            // 
            // One The value of one - use this to let either the source or the destination color come through fully.
            // Zero  The value zero - use this to remove either the source or the destination values.
            // SrcColor  The value of this stage is multiplied by the source color value.
            // SrcAlpha  The value of this stage is multiplied by the source alpha value.
            // DstColor  The value of this stage is multiplied by frame buffer source color value.
            // DstAlpha  The value of this stage is multiplied by frame buffer source alpha value.
            // OneMinusSrcColor  The value of this stage is multiplied by (1 - source color).
            // OneMinusSrcAlpha  The value of this stage is multiplied by (1 - source alpha).
            // OneMinusDstColor  The value of this stage is multiplied by (1 - destination color).
            // OneMinusDstAlpha  The value of this stage is multiplied by (1 - destination alpha).    		
            Blend SrcAlpha One
            
        	CGPROGRAM

  	      	#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			
			// appdata_base: vertex consists of position, normal and one texture coordinate.
			// appdata_tan: vertex consists of position, tangent, normal and one texture coordinate.
			// appdata_full: vertex consists of position, tangent, normal, four texture coordinates and color.
			
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform float4 _TintColor;
			uniform float _Intensity;

	      	      
			// POSITION is the vertex position, typically a float4.
			// NORMAL is the vertex normal, typically a float3.
			// TEXCOORD0 is the first UV coordinate, typically a float2..float4.
			// TEXCOORD1 .. TEXCOORD3 are the 2nd..4th UV coordinates.
			// TANGENT is the tangent vector (used for normal mapping), typically a float4.
			// COLOR is the per-vertex color, typically a float4.
	      
            struct vertexInput {
                float4 texcoord0 : TEXCOORD0;
                float4 vertex : POSITION;
//                float4 normal : NORMAL;
            };

            struct v2f {
                float4 texcoord0 : TEXCOORD0;
                float4 pos : SV_POSITION;
//      			fixed4 color : COLOR;
            };
            
			v2f vert (vertexInput i) {
				v2f o;
				o.texcoord0 = i.texcoord0;
				o.pos = mul (UNITY_MATRIX_MVP, i.vertex);
				return o;
			}

			fixed4 frag (v2f i) : SV_Target {
				fixed4 texcol = tex2D(_MainTex, i.texcoord0);
				texcol *= _TintColor;
				texcol.a *= _Intensity;
				return texcol;
			}
      
    	    ENDCG
		}            
    }
//    FallBack "Diffuse"
}