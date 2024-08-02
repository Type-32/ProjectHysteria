// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Bhasker Dee/Mobile - Emission"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_MaskClipValue( "Mask Clip Value", Float ) = 0.5
		_Color("Color", Color) = (0.5514706,0.5514706,0.5514706,1)
		_Albedo("Albedo", 2D) = "white" {}
		_Emission("Emission", 2D) = "white" {}
		_NormalMap("Normal Map", 2D) = "bump" {}
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Roughness("Roughness", Range( 0 , 1)) = 0.2
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _NormalMap;
		uniform float4 _NormalMap_ST;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform half4 _Color;
		uniform sampler2D _Emission;
		uniform float4 _Emission_ST;
		uniform half _Metallic;
		uniform half _Roughness;
		uniform float _MaskClipValue = 0.5;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_NormalMap = i.uv_texcoord * _NormalMap_ST.xy + _NormalMap_ST.zw;
			o.Normal = UnpackNormal( tex2D( _NormalMap,uv_NormalMap) );
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			half4 tex2DNode2 = tex2D( _Albedo,uv_Albedo);
			o.Albedo = ( tex2DNode2 * _Color ).rgb;
			float2 uv_Emission = i.uv_texcoord * _Emission_ST.xy + _Emission_ST.zw;
			o.Emission = tex2D( _Emission,uv_Emission).rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Roughness;
			o.Alpha = 1;
			clip( tex2DNode2.a - _MaskClipValue );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=6103
60;452;1325;574;1107.359;467.0785;1.3;True;True
Node;AmplifyShaderEditor.ColorNode;8;-446.7,-380.4;Float;False;Property;_Color;Color;0;0;0.5514706,0.5514706,0.5514706,1;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;2;-433.5,-566;Float;True;Property;_Albedo;Albedo;1;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;5;-415.5002,237.1999;Float;False;Property;_Metallic;Metallic;4;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;6;-412.5002,339.1999;Float;False;Property;_Roughness;Roughness;5;0;0.2;0;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;7;-439.0999,-167.4;Float;True;Property;_Emission;Emission;2;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-102.7,-348.4;Float;False;0;COLOR;0,0,0,0;False;1;COLOR;0.0;False;COLOR
Node;AmplifyShaderEditor.SamplerNode;3;-431.6002,33.69995;Float;True;Property;_NormalMap;Normal Map;3;0;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Half;False;True;2;Half;ASEMaterialInspector;0;Standard;Bhasker Dee/Mobile - Emission;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Custom;0.5;True;True;0;True;TransparentCutout;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;Relative;0;;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;OBJECT;0.0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False
WireConnection;9;0;2;0
WireConnection;9;1;8;0
WireConnection;0;0;9;0
WireConnection;0;1;3;0
WireConnection;0;2;7;0
WireConnection;0;3;5;0
WireConnection;0;4;6;0
WireConnection;0;10;2;4
ASEEND*/
//CHKSM=6BA1985E7A1505D78B2B1197CAEE048E14559CB3