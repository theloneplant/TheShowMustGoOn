Shader "UnlitAlpha"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB) Trans. (Alpha)", 2D) = "white" { }
    }

    Category
    {   
        ZWrite On
        Alphatest Greater 0.5
        Lighting On
        SubShader
        {
            Pass
            {
            	Cull Off
                Lighting Off
                SetTexture [_MainTex]
                {
                    constantColor [_Color]
                    Combine texture * constant, texture * constant 
                } 
            }
	        } 
	    SubShader {
			Tags { "RenderType" = "Opaque" }
			CGPROGRAM
			#pragma surface surf Lambert

			struct Input {
				float2 uv_MainTex;
			};

			sampler2D _MainTex;

			void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
			}
			ENDCG
	    }
	    Fallback "VertexLit"
    }
}