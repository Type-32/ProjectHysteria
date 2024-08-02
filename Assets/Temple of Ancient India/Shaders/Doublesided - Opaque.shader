// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Bhasker Dee/Doublesided - Opaque"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_Color("Color", Color) = (0.8,0.8,0.8,1)
		_Albedo("Albedo", 2D) = "white" {}
		_NormalMap("Normal Map", 2D) = "bump" {}
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Roughness("Roughness", Range( 0 , 1)) = 0.2
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Off
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
		uniform float4 _Color;
		uniform float _Metallic;
		uniform float _Roughness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_NormalMap = i.uv_texcoord * _NormalMap_ST.xy + _NormalMap_ST.zw;
			o.Normal = UnpackNormal( tex2D( _NormalMap,uv_NormalMap) );
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			o.Albedo = ( tex2D( _Albedo,uv_Albedo) * _Color ).rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Roughness;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=6103
60;452;1325;574;912.5;254.9;1;True;True
Node;AmplifyShaderEditor.ColorNode;8;-440.5,-63.90002;Float;False;Property;_Color;Color;0;0;0.8,0.8,0.8,1;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;2;-466.1001,-251.5;Float;True;Property;_Albedo;Albedo;1;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-112.5,-155.9;Float;False;0;COLOR;0.0;False;1;COLOR;0,0,0,0;False;COLOR
Node;AmplifyShaderEditor.SamplerNode;3;-409.6002,94.69995;Float;True;Property;_NormalMap;Normal Map;2;0;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;6;-412.5002,382.1999;Float;False;Property;_Roughness;Roughness;4;0;0.2;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;5;-402.5002,297.1999;Float;False;Property;_Metallic;Metallic;3;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;Standard;Bhasker Dee/Doublesided - Opaque;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;Relative;0;;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;OBJECT;0.0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False
WireConnection;7;0;2;0
WireConnection;7;1;8;0
WireConnection;0;0;7;0
WireConnection;0;1;3;0
WireConnection;0;3;5;0
WireConnection;0;4;6;0
ASEEND*/
//CHKSM=8CA0A7A57055C7F85FB8EF346241664A3C1FD429