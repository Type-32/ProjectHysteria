// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Bhasker Dee/Water"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_WaterNormal("Water Normal", 2D) = "bump" {}
		_Color("Color", Color) = (0.6838235,0.6838235,0.6838235,0)
		_NormalScale("Normal Scale", Float) = 0
		_Distortion("Distortion", Float) = 0.5
		_WaterSpecular("Water Specular", Range( 0 , 1)) = 0
		_WaterSmoothness("Water Smoothness", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Transparent+0" }
		Cull Back
		GrabPass{ "_WaterGrab" }
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf StandardSpecular keepalpha  
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPos;
		};

		uniform half _NormalScale;
		uniform sampler2D _WaterNormal;
		uniform float4 _WaterNormal_ST;
		uniform sampler2D _WaterGrab;
		uniform half _Distortion;
		uniform half4 _Color;
		uniform half _WaterSpecular;
		uniform half _WaterSmoothness;

		void surf( Input i , inout SurfaceOutputStandardSpecular o )
		{
			float2 uv_WaterNormal = i.uv_texcoord * _WaterNormal_ST.xy + _WaterNormal_ST.zw;
			float3 temp_output_24_0 = BlendNormals( UnpackScaleNormal( tex2D( _WaterNormal,(abs( uv_WaterNormal+_Time[1] * float2(-0.03,0 )))) ,_NormalScale ) , UnpackScaleNormal( tex2D( _WaterNormal,(abs( uv_WaterNormal+_Time[1] * float2(0.04,0.04 )))) ,_NormalScale ) );
			o.Normal = temp_output_24_0;
			float2 appendResult67 = float2( i.screenPos.x , i.screenPos.y );
			o.Albedo = ( tex2D( _WaterGrab, ( half3( ( appendResult67 / i.screenPos.w ) ,  0.0 ) + ( temp_output_24_0 * _Distortion ) ).xy ) * _Color ).rgb;
			half3 temp_cast_3 = _WaterSpecular;
			o.Specular = temp_cast_3;
			o.Smoothness = _WaterSmoothness;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=6103
60;452;1325;574;254.2692;1380.024;1.9;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;21;-932.2321,-454.4167;Float;False;0;17;2;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;48;-606.2062,-460.8069;Float;False;Property;_NormalScale;Normal Scale;1;0;0;0;0;FLOAT
Node;AmplifyShaderEditor.PannerNode;22;-901.4587,-589.8104;Float;False;-0.03;0;0;FLOAT2;0,0;False;1;FLOAT;0.0;False;FLOAT2
Node;AmplifyShaderEditor.PannerNode;19;-623.7745,-595.7008;Float;False;0.04;0.04;0;FLOAT2;0,0;False;1;FLOAT;0.0;False;FLOAT2
Node;AmplifyShaderEditor.SamplerNode;23;-302.6639,-771.3746;Float;True;Property;_Normal2;Normal2;-1;0;None;True;0;True;bump;Auto;True;Instance;17;Auto;Texture2D;0;SAMPLER2D;0,0;False;1;FLOAT2;1.0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;17;-289.7633,-572.3585;Float;True;Property;_WaterNormal;Water Normal;3;0;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;0;SAMPLER2D;0,0;False;1;FLOAT2;1.0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.BlendNormalsNode;24;88.33908,-926.011;Float;False;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;FLOAT3
Node;AmplifyShaderEditor.ScreenPosInputsNode;66;481.5959,-1451.783;Float;False;1;False;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;97;635.296,-1016.183;Float;False;Property;_Distortion;Distortion;2;0;0.5;0;0;FLOAT
Node;AmplifyShaderEditor.AppendNode;67;735.795,-1416.684;Float;False;FLOAT2;0;0;0;0;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;FLOAT2
Node;AmplifyShaderEditor.WireNode;149;351.0941,-1274.682;Float;False;0;FLOAT3;0.0,0,0;False;FLOAT3
Node;AmplifyShaderEditor.SimpleDivideOpNode;68;907.8967,-1220.483;Float;False;0;FLOAT2;0.0,0;False;1;FLOAT;0,0;False;FLOAT2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;98;892.5974,-1108.182;Float;False;0;FLOAT3;0.0,0,0;False;1;FLOAT;0,0,0;False;FLOAT3
Node;AmplifyShaderEditor.SimpleAddOpNode;96;1074.296,-1225.683;Float;False;0;FLOAT2;0.0,0;False;1;FLOAT3;0,0;False;FLOAT3
Node;AmplifyShaderEditor.ColorNode;164;1162.709,-952.5616;Float;False;Property;_Color;Color;0;0;0.6838235,0.6838235,0.6838235,0;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ScreenColorNode;65;1228.397,-1170.083;Float;False;Global;_WaterGrab;WaterGrab;-1;0;Object;-1;0;FLOAT2;0,0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;165;1545.315,-891.4669;Float;False;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;COLOR
Node;AmplifyShaderEditor.RangedFloatNode;26;937.9591,-411.4224;Float;False;Property;_WaterSmoothness;Water Smoothness;5;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;104;940.3286,-493.1503;Float;False;Property;_WaterSpecular;Water Specular;4;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1838.601,-748.1998;Half;False;True;2;Half;ASEMaterialInspector;0;StandardSpecular;Bhasker Dee/Water;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;3;False;0;0;Translucent;0.5;True;False;0;False;Opaque;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;Relative;0;;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0.0,0,0;False;7;FLOAT3;0.0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0.0,0,0;False;12;FLOAT3;0,0,0;False;13;OBJECT;0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False
WireConnection;22;0;21;0
WireConnection;19;0;21;0
WireConnection;23;1;22;0
WireConnection;23;5;48;0
WireConnection;17;1;19;0
WireConnection;17;5;48;0
WireConnection;24;0;23;0
WireConnection;24;1;17;0
WireConnection;67;0;66;1
WireConnection;67;1;66;2
WireConnection;149;0;24;0
WireConnection;68;0;67;0
WireConnection;68;1;66;4
WireConnection;98;0;149;0
WireConnection;98;1;97;0
WireConnection;96;0;68;0
WireConnection;96;1;98;0
WireConnection;65;0;96;0
WireConnection;165;0;65;0
WireConnection;165;1;164;0
WireConnection;0;0;165;0
WireConnection;0;1;24;0
WireConnection;0;3;104;0
WireConnection;0;4;26;0
ASEEND*/
//CHKSM=BBDFE86DFAC75380C5DCF3935BF0CE055E47808E