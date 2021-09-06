Shader "Stencil/Outline" {
	Properties {
		_Color ("Color", Vector) = (1,1,0,1)
		_Thickness ("Thickness", Float) = 7
	}
	SubShader {
		Tags { "IGNOREPROJECTOR" = "true" "QUEUE" = "Geometry" "RenderType" = "Transparent" }
		Pass {
			Tags { "IGNOREPROJECTOR" = "true" "QUEUE" = "Geometry" "RenderType" = "Transparent" }
			Blend SrcAlpha OneMinusSrcAlpha, SrcAlpha OneMinusSrcAlpha
			Stencil {
				Ref 1
				Comp Always
				Pass Replace
				Fail Keep
				ZFail Keep
			}
			GpuProgramID 18155
			Program "vp" {
				SubProgram "d3d11 " {
					"vs_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
					#if HLSLCC_ENABLE_UNIFORM_BUFFERS
					#define UNITY_UNIFORM
					#else
					#define UNITY_UNIFORM uniform
					#endif
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					layout(std140) uniform UnityPerCamera {
						vec4 unused_0_0[4];
						vec3 _WorldSpaceCameraPos;
						vec4 unused_0_2[4];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						mat4x4 unity_WorldToObject;
						vec4 unused_1_2[3];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec3 vs_TANGENT0;
					out vec3 vs_NORMAL0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    u_xlat0.xyz = _WorldSpaceCameraPos.yyy * unity_WorldToObject[1].xyz;
					    u_xlat0.xyz = unity_WorldToObject[0].xyz * _WorldSpaceCameraPos.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_WorldToObject[2].xyz * _WorldSpaceCameraPos.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + unity_WorldToObject[3].xyz;
					    vs_TANGENT0.xyz = u_xlat0.xyz + (-in_POSITION0.xyz);
					    vs_NORMAL0.xyz = in_NORMAL0.xyz;
					    return;
					}"
				}
			}
			Program "fp" {
				SubProgram "d3d11 " {
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					layout(location = 0) out vec4 SV_Target0;
					void main()
					{
					    SV_Target0 = vec4(0.0, 0.0, 0.0, 0.0);
					    return;
					}"
				}
			}
		}
		Pass {
			Tags { "IGNOREPROJECTOR" = "true" "QUEUE" = "Geometry" "RenderType" = "Transparent" }
			Blend SrcAlpha OneMinusSrcAlpha, SrcAlpha OneMinusSrcAlpha
			Stencil {
				Comp Equal
				Pass Keep
				Fail Keep
				ZFail Keep
			}
			GpuProgramID 117458
			Program "vp" {
				SubProgram "d3d11 " {
					"vs_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
					#if HLSLCC_ENABLE_UNIFORM_BUFFERS
					#define UNITY_UNIFORM
					#else
					#define UNITY_UNIFORM uniform
					#endif
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					layout(std140) uniform UnityPerCamera {
						vec4 unused_0_0[4];
						vec3 _WorldSpaceCameraPos;
						vec4 unused_0_2[4];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						mat4x4 unity_WorldToObject;
						vec4 unused_1_2[3];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec3 vs_TANGENT0;
					out vec3 vs_NORMAL0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    u_xlat0.xyz = _WorldSpaceCameraPos.yyy * unity_WorldToObject[1].xyz;
					    u_xlat0.xyz = unity_WorldToObject[0].xyz * _WorldSpaceCameraPos.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_WorldToObject[2].xyz * _WorldSpaceCameraPos.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + unity_WorldToObject[3].xyz;
					    vs_TANGENT0.xyz = u_xlat0.xyz + (-in_POSITION0.xyz);
					    vs_NORMAL0.xyz = in_NORMAL0.xyz;
					    return;
					}"
				}
			}
			Program "fp" {
				SubProgram "d3d11 " {
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
					#if HLSLCC_ENABLE_UNIFORM_BUFFERS
					#define UNITY_UNIFORM
					#else
					#define UNITY_UNIFORM uniform
					#endif
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					layout(std140) uniform PGlobals {
						vec4 unused_0_0[2];
						vec4 _Color;
						vec4 unused_0_2;
					};
					layout(location = 0) out vec4 SV_Target0;
					void main()
					{
					    SV_Target0.xyz = _Color.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
			}
			Program "gp" {
				SubProgram "d3d11 " {
					"gs_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
					#if HLSLCC_ENABLE_UNIFORM_BUFFERS
					#define UNITY_UNIFORM
					#else
					#define UNITY_UNIFORM uniform
					#endif
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					layout(std140) uniform GGlobals {
						vec4 unused_0_0[3];
						float _Thickness;
					};
					in  vec2 vs_TEXCOORD0 [3];
					in  vec3 vs_TANGENT0 [3];
					in  vec3 vs_NORMAL0 [3];
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec2 u_xlat6;
					float u_xlat16;
					layout(triangles) in;
					layout(triangle_strip) out;
					out vec2 gs_TEXCOORD0;
					out vec3 gs_TANGENT0;
					out vec3 gs_NORMAL0;
					layout(max_vertices = 12) out;
					void main()
					{
					    u_xlat0 = (-gl_in[0].gl_Position) + gl_in[1].gl_Position;
					    u_xlat1.x = _Thickness * 0.00999999978;
					    u_xlat6.xy = u_xlat0.xy * u_xlat1.xx;
					    u_xlat2.xy = u_xlat6.yx * vec2(1.0, -1.0);
					    u_xlat16 = dot(u_xlat2.xy, u_xlat2.xy);
					    u_xlat16 = inversesqrt(u_xlat16);
					    u_xlat2.x = u_xlat16 * u_xlat6.y;
					    u_xlat2.y = u_xlat16 * (-u_xlat6.x);
					    u_xlat2.xy = u_xlat1.xx * u_xlat2.xy;
					    u_xlat2.z = float(0.0);
					    u_xlat2.w = float(0.0);
					    u_xlat3 = (-u_xlat0) * u_xlat1.xxxx + gl_in[0].gl_Position;
					    u_xlat0 = u_xlat0 * u_xlat1.xxxx + gl_in[1].gl_Position;
					    u_xlat4 = (-u_xlat2.xyww) + u_xlat3;
					    u_xlat3 = u_xlat2 + u_xlat3;
					    gl_Position = u_xlat4;
					    gs_TEXCOORD0.xy = vs_TEXCOORD0[0].xy;
					    gs_TANGENT0.xyz = vs_TANGENT0[0].xyz;
					    gs_NORMAL0.xyz = vs_NORMAL0[0].xyz;
					    EmitVertex();
					    gl_Position = u_xlat3;
					    gs_TEXCOORD0.xy = vs_TEXCOORD0[0].xy;
					    gs_TANGENT0.xyz = vs_TANGENT0[0].xyz;
					    gs_NORMAL0.xyz = vs_NORMAL0[0].xyz;
					    EmitVertex();
					    u_xlat3 = (-u_xlat2.xyww) + u_xlat0;
					    u_xlat0 = u_xlat2 + u_xlat0;
					    gl_Position = u_xlat3;
					    gs_TEXCOORD0.xy = vs_TEXCOORD0[1].xy;
					    gs_TANGENT0.xyz = vs_TANGENT0[1].xyz;
					    gs_NORMAL0.xyz = vs_NORMAL0[1].xyz;
					    EmitVertex();
					    gl_Position = u_xlat0;
					    gs_TEXCOORD0.xy = vs_TEXCOORD0[1].xy;
					    gs_TANGENT0.xyz = vs_TANGENT0[1].xyz;
					    gs_NORMAL0.xyz = vs_NORMAL0[1].xyz;
					    EmitVertex();
					    u_xlat0 = (-gl_in[1].gl_Position) + gl_in[2].gl_Position;
					    u_xlat6.xy = u_xlat1.xx * u_xlat0.xy;
					    u_xlat2.xy = u_xlat6.yx * vec2(1.0, -1.0);
					    u_xlat16 = dot(u_xlat2.xy, u_xlat2.xy);
					    u_xlat16 = inversesqrt(u_xlat16);
					    u_xlat2.x = u_xlat16 * u_xlat6.y;
					    u_xlat2.y = u_xlat16 * (-u_xlat6.x);
					    u_xlat2.xy = u_xlat1.xx * u_xlat2.xy;
					    u_xlat2.z = float(0.0);
					    u_xlat2.w = float(0.0);
					    u_xlat3 = (-u_xlat0) * u_xlat1.xxxx + gl_in[1].gl_Position;
					    u_xlat0 = u_xlat0 * u_xlat1.xxxx + gl_in[2].gl_Position;
					    u_xlat4 = (-u_xlat2.xyww) + u_xlat3;
					    u_xlat3 = u_xlat2 + u_xlat3;
					    gl_Position = u_xlat4;
					    gs_TEXCOORD0.xy = vs_TEXCOORD0[1].xy;
					    gs_TANGENT0.xyz = vs_TANGENT0[1].xyz;
					    gs_NORMAL0.xyz = vs_NORMAL0[1].xyz;
					    EmitVertex();
					    gl_Position = u_xlat3;
					    gs_TEXCOORD0.xy = vs_TEXCOORD0[1].xy;
					    gs_TANGENT0.xyz = vs_TANGENT0[1].xyz;
					    gs_NORMAL0.xyz = vs_NORMAL0[1].xyz;
					    EmitVertex();
					    u_xlat3 = (-u_xlat2.xyww) + u_xlat0;
					    u_xlat0 = u_xlat2 + u_xlat0;
					    gl_Position = u_xlat3;
					    gs_TEXCOORD0.xy = vs_TEXCOORD0[2].xy;
					    gs_TANGENT0.xyz = vs_TANGENT0[2].xyz;
					    gs_NORMAL0.xyz = vs_NORMAL0[2].xyz;
					    EmitVertex();
					    gl_Position = u_xlat0;
					    gs_TEXCOORD0.xy = vs_TEXCOORD0[2].xy;
					    gs_TANGENT0.xyz = vs_TANGENT0[2].xyz;
					    gs_NORMAL0.xyz = vs_NORMAL0[2].xyz;
					    EmitVertex();
					    u_xlat0 = (-gl_in[2].gl_Position) + gl_in[0].gl_Position;
					    u_xlat6.xy = u_xlat1.xx * u_xlat0.xy;
					    u_xlat2.xy = u_xlat6.yx * vec2(1.0, -1.0);
					    u_xlat16 = dot(u_xlat2.xy, u_xlat2.xy);
					    u_xlat16 = inversesqrt(u_xlat16);
					    u_xlat2.x = u_xlat16 * u_xlat6.y;
					    u_xlat2.y = u_xlat16 * (-u_xlat6.x);
					    u_xlat2.xy = u_xlat1.xx * u_xlat2.xy;
					    u_xlat3 = (-u_xlat0) * u_xlat1.xxxx + gl_in[2].gl_Position;
					    u_xlat0 = u_xlat0 * u_xlat1.xxxx + gl_in[0].gl_Position;
					    u_xlat2.z = float(0.0);
					    u_xlat2.w = float(0.0);
					    u_xlat1 = (-u_xlat2.xyww) + u_xlat3;
					    u_xlat3 = u_xlat2 + u_xlat3;
					    gl_Position = u_xlat1;
					    gs_TEXCOORD0.xy = vs_TEXCOORD0[2].xy;
					    gs_TANGENT0.xyz = vs_TANGENT0[2].xyz;
					    gs_NORMAL0.xyz = vs_NORMAL0[2].xyz;
					    EmitVertex();
					    gl_Position = u_xlat3;
					    gs_TEXCOORD0.xy = vs_TEXCOORD0[2].xy;
					    gs_TANGENT0.xyz = vs_TANGENT0[2].xyz;
					    gs_NORMAL0.xyz = vs_NORMAL0[2].xyz;
					    EmitVertex();
					    u_xlat1 = u_xlat0 + (-u_xlat2.xyww);
					    u_xlat0 = u_xlat0 + u_xlat2;
					    gl_Position = u_xlat1;
					    gs_TEXCOORD0.xy = vs_TEXCOORD0[0].xy;
					    gs_TANGENT0.xyz = vs_TANGENT0[0].xyz;
					    gs_NORMAL0.xyz = vs_NORMAL0[0].xyz;
					    EmitVertex();
					    gl_Position = u_xlat0;
					    gs_TEXCOORD0.xy = vs_TEXCOORD0[0].xy;
					    gs_TANGENT0.xyz = vs_TANGENT0[0].xyz;
					    gs_NORMAL0.xyz = vs_NORMAL0[0].xyz;
					    EmitVertex();
					    return;
					}"
				}
			}
		}
	}
	Fallback "Diffuse"
}