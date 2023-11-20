Shader "Custom/TestShader02"
{
    Properties
    {
        _Depth("Depth", Range(0, 1)) = 0.2
        _DepthFalloff("DepthFalloff", Float) = 0.1
        _ShoreColor("ShoreColor", Color) = (1, 1, 1, 0)
        _Color("Color", Color) = (0, 0.5964391, 1, 0)
        _FoamShoreWidth("FoamShoreWidth", Float) = 0
        _FoamColor("FoamColor", Color) = (0, 0, 0, 0)
        _FoamDepth("FoamDepth", Float) = 0
        _FoamFalloff("FoamFalloff", Float) = 1
        _WaveIntensity("WaveIntensity", Float) = 0
        _WaveSpeed("WaveSpeed", Float) = 0
        _Float("Float", Float) = 8
        _Metal("Metal", Float) = 0
        _NormalTexture("NormalTexture", 2D) = "white" {}
        _NormalStrenght("NormalStrenght", Float) = 5
        [HideInInspector]_BUILTIN_Surface("Float", Float) = 1
        [HideInInspector]_BUILTIN_Blend("Float", Float) = 0
        [HideInInspector]_BUILTIN_AlphaClip("Float", Float) = 1
        [HideInInspector]_BUILTIN_SrcBlend("Float", Float) = 1
        [HideInInspector]_BUILTIN_DstBlend("Float", Float) = 0
        [HideInInspector]_BUILTIN_ZWrite("Float", Float) = 0
        [HideInInspector]_BUILTIN_ZWriteControl("Float", Float) = 0
        [HideInInspector]_BUILTIN_ZTest("Float", Float) = 4
        [HideInInspector]_BUILTIN_CullMode("Float", Float) = 2
        [HideInInspector]_BUILTIN_QueueOffset("Float", Float) = 0
        [HideInInspector]_BUILTIN_QueueControl("Float", Float) = -1
    }
    SubShader
    {
        Tags
        {
            // RenderPipeline: <None>
            "RenderType"="Transparent"
            "BuiltInMaterialType" = "Lit"
            "Queue"="Transparent"
            "ShaderGraphShader"="true"
            "ShaderGraphTargetId"="BuiltInLitSubTarget"
        }
        Pass
        {
            Name "BuiltIn Forward"
            Tags
            {
                "LightMode" = "ForwardBase"
            }
        
        // Render State
        Cull [_BUILTIN_CullMode]
        Blend [_BUILTIN_SrcBlend] [_BUILTIN_DstBlend]
        ZTest [_BUILTIN_ZTest]
        ZWrite [_BUILTIN_ZWrite]
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 3.0
        #pragma multi_compile_instancing
        #pragma multi_compile_fog
        #pragma multi_compile_fwdbase
        #pragma vertex vert
        #pragma fragment frag
        
        // DotsInstancingOptions: <None>
        // HybridV1InjectedBuiltinProperties: <None>
        
        // Keywords
        #pragma multi_compile _ _SCREEN_SPACE_OCCLUSION
        #pragma multi_compile _ LIGHTMAP_ON
        #pragma multi_compile _ DIRLIGHTMAP_COMBINED
        #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
        #pragma multi_compile _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS _ADDITIONAL_OFF
        #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
        #pragma multi_compile _ _SHADOWS_SOFT
        #pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
        #pragma multi_compile _ SHADOWS_SHADOWMASK
        #pragma shader_feature_local_fragment _ _BUILTIN_SURFACE_TYPE_TRANSPARENT
        #pragma shader_feature_local_fragment _ _BUILTIN_ALPHAPREMULTIPLY_ON
        #pragma shader_feature_local_fragment _ _BUILTIN_AlphaClip
        #pragma shader_feature_local_fragment _ _BUILTIN_ALPHATEST_ON
        // GraphKeywords: <None>
        
        // Defines
        #define _NORMALMAP 1
        #define _NORMAL_DROPOFF_WS 1
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define ATTRIBUTES_NEED_TEXCOORD1
        #define VARYINGS_NEED_POSITION_WS
        #define VARYINGS_NEED_NORMAL_WS
        #define VARYINGS_NEED_TANGENT_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define VARYINGS_NEED_TEXCOORD1
        #define VARYINGS_NEED_VIEWDIRECTION_WS
        #define VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_FORWARD
        #define BUILTIN_TARGET_API 1
        #define REQUIRE_DEPTH_TEXTURE
        #define REQUIRE_OPAQUE_TEXTURE
        /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
        #ifdef _BUILTIN_SURFACE_TYPE_TRANSPARENT
        #define _SURFACE_TYPE_TRANSPARENT _BUILTIN_SURFACE_TYPE_TRANSPARENT
        #endif
        #ifdef _BUILTIN_ALPHATEST_ON
        #define _ALPHATEST_ON _BUILTIN_ALPHATEST_ON
        #endif
        #ifdef _BUILTIN_AlphaClip
        #define _AlphaClip _BUILTIN_AlphaClip
        #endif
        #ifdef _BUILTIN_ALPHAPREMULTIPLY_ON
        #define _ALPHAPREMULTIPLY_ON _BUILTIN_ALPHAPREMULTIPLY_ON
        #endif
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Shim/Shims.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/LegacySurfaceVertex.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/ShaderGraphFunctions.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
             float4 uv1 : TEXCOORD1;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 positionWS;
             float3 normalWS;
             float4 tangentWS;
             float4 texCoord0;
             float4 texCoord1;
             float3 viewDirectionWS;
            #if defined(LIGHTMAP_ON)
             float2 lightmapUV;
            #endif
            #if !defined(LIGHTMAP_ON)
             float3 sh;
            #endif
             float4 fogFactorAndVertexLight;
             float4 shadowCoord;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float3 WorldSpaceNormal;
             float3 WorldSpacePosition;
             float4 ScreenPosition;
             float4 uv0;
             float4 uv1;
             float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
             float3 WorldSpacePosition;
             float3 TimeParameters;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float3 interp0 : INTERP0;
             float3 interp1 : INTERP1;
             float4 interp2 : INTERP2;
             float4 interp3 : INTERP3;
             float4 interp4 : INTERP4;
             float3 interp5 : INTERP5;
             float2 interp6 : INTERP6;
             float3 interp7 : INTERP7;
             float4 interp8 : INTERP8;
             float4 interp9 : INTERP9;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.interp0.xyz =  input.positionWS;
            output.interp1.xyz =  input.normalWS;
            output.interp2.xyzw =  input.tangentWS;
            output.interp3.xyzw =  input.texCoord0;
            output.interp4.xyzw =  input.texCoord1;
            output.interp5.xyz =  input.viewDirectionWS;
            #if defined(LIGHTMAP_ON)
            output.interp6.xy =  input.lightmapUV;
            #endif
            #if !defined(LIGHTMAP_ON)
            output.interp7.xyz =  input.sh;
            #endif
            output.interp8.xyzw =  input.fogFactorAndVertexLight;
            output.interp9.xyzw =  input.shadowCoord;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.positionWS = input.interp0.xyz;
            output.normalWS = input.interp1.xyz;
            output.tangentWS = input.interp2.xyzw;
            output.texCoord0 = input.interp3.xyzw;
            output.texCoord1 = input.interp4.xyzw;
            output.viewDirectionWS = input.interp5.xyz;
            #if defined(LIGHTMAP_ON)
            output.lightmapUV = input.interp6.xy;
            #endif
            #if !defined(LIGHTMAP_ON)
            output.sh = input.interp7.xyz;
            #endif
            output.fogFactorAndVertexLight = input.interp8.xyzw;
            output.shadowCoord = input.interp9.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _Depth;
        float _DepthFalloff;
        float4 _ShoreColor;
        float4 _Color;
        float _FoamShoreWidth;
        float4 _FoamColor;
        float _FoamDepth;
        float _FoamFalloff;
        float _WaveIntensity;
        float _WaveSpeed;
        float _Float;
        float _Metal;
        float4 _NormalTexture_TexelSize;
        float4 _NormalTexture_ST;
        float _NormalStrenght;
        CBUFFER_END
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_NormalTexture);
        SAMPLER(sampler_NormalTexture);
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Includes
        // GraphIncludes: <None>
        
        // Graph Functions
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float(float A, float B, out float Out)
        {
            Out = A + B;
        }
        
        void Unity_Sine_float(float In, out float Out)
        {
            Out = sin(In);
        }
        
        void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A + B;
        }
        
        void Unity_SceneDepth_Eye_float(float4 UV, out float Out)
        {
            if (unity_OrthoParams.w == 1.0)
            {
                Out = LinearEyeDepth(ComputeWorldSpacePosition(UV.xy, SHADERGRAPH_SAMPLE_SCENE_DEPTH(UV.xy), UNITY_MATRIX_I_VP), UNITY_MATRIX_V);
            }
            else
            {
                Out = LinearEyeDepth(SHADERGRAPH_SAMPLE_SCENE_DEPTH(UV.xy), _ZBufferParams);
            }
        }
        
        void Unity_Subtract_float(float A, float B, out float Out)
        {
            Out = A - B;
        }
        
        void Unity_Divide_float(float A, float B, out float Out)
        {
            Out = A / B;
        }
        
        void Unity_OneMinus_float(float In, out float Out)
        {
            Out = 1 - In;
        }
        
        void Unity_Saturate_float(float In, out float Out)
        {
            Out = saturate(In);
        }
        
        void Unity_Power_float(float A, float B, out float Out)
        {
            Out = pow(A, B);
        }
        
        struct Bindings_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float
        {
        float4 ScreenPosition;
        };
        
        void SG_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float(float _Depth, float _DepthFalloff, Bindings_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float IN, out float OutVector1_1)
        {
        float _SceneDepth_4398a40afb5444a98bdb88f05ff37be7_Out_1;
        Unity_SceneDepth_Eye_float(float4(IN.ScreenPosition.xy / IN.ScreenPosition.w, 0, 0), _SceneDepth_4398a40afb5444a98bdb88f05ff37be7_Out_1);
        float4 _ScreenPosition_78861b0c174847048fb96d1b9be441c4_Out_0 = IN.ScreenPosition;
        float _Split_9aecda2f9d2945b9b0a54de06a3a9d48_R_1 = _ScreenPosition_78861b0c174847048fb96d1b9be441c4_Out_0[0];
        float _Split_9aecda2f9d2945b9b0a54de06a3a9d48_G_2 = _ScreenPosition_78861b0c174847048fb96d1b9be441c4_Out_0[1];
        float _Split_9aecda2f9d2945b9b0a54de06a3a9d48_B_3 = _ScreenPosition_78861b0c174847048fb96d1b9be441c4_Out_0[2];
        float _Split_9aecda2f9d2945b9b0a54de06a3a9d48_A_4 = _ScreenPosition_78861b0c174847048fb96d1b9be441c4_Out_0[3];
        float _Subtract_0ef5acf5e9ec4f599d608aed9d014d88_Out_2;
        Unity_Subtract_float(_SceneDepth_4398a40afb5444a98bdb88f05ff37be7_Out_1, _Split_9aecda2f9d2945b9b0a54de06a3a9d48_A_4, _Subtract_0ef5acf5e9ec4f599d608aed9d014d88_Out_2);
        float _Property_5702a0604e9c425f9f28a8e389f7d6e8_Out_0 = _Depth;
        float _Divide_230152a01c7a4ab691e1a20a1fbf597f_Out_2;
        Unity_Divide_float(_Subtract_0ef5acf5e9ec4f599d608aed9d014d88_Out_2, _Property_5702a0604e9c425f9f28a8e389f7d6e8_Out_0, _Divide_230152a01c7a4ab691e1a20a1fbf597f_Out_2);
        float _OneMinus_4edb44dcaf8a4df5974bc2b0bfc1a39d_Out_1;
        Unity_OneMinus_float(_Divide_230152a01c7a4ab691e1a20a1fbf597f_Out_2, _OneMinus_4edb44dcaf8a4df5974bc2b0bfc1a39d_Out_1);
        float _Saturate_c574ae0f849b4335bdfce5762b7b4760_Out_1;
        Unity_Saturate_float(_OneMinus_4edb44dcaf8a4df5974bc2b0bfc1a39d_Out_1, _Saturate_c574ae0f849b4335bdfce5762b7b4760_Out_1);
        float _Property_8b3812fa4cec4943b82bfecf45cd931a_Out_0 = _DepthFalloff;
        float _Power_494a422a08904cbbb881363a1d49b985_Out_2;
        Unity_Power_float(_Saturate_c574ae0f849b4335bdfce5762b7b4760_Out_1, _Property_8b3812fa4cec4943b82bfecf45cd931a_Out_0, _Power_494a422a08904cbbb881363a1d49b985_Out_2);
        OutVector1_1 = _Power_494a422a08904cbbb881363a1d49b985_Out_2;
        }
        
        void Unity_Ceiling_float(float In, out float Out)
        {
            Out = ceil(In);
        }
        
        struct Bindings_Cutout_719ac7b9c41a99e4fa65ee4e7f3e2847_float
        {
        };
        
        void SG_Cutout_719ac7b9c41a99e4fa65ee4e7f3e2847_float(float _Alpha, float _Input, Bindings_Cutout_719ac7b9c41a99e4fa65ee4e7f3e2847_float IN, out float Output_0)
        {
        float _Property_c3987da974dc4f0485a61aed8135469c_Out_0 = _Input;
        float _Property_1427757994b04881a478a38d221123de_Out_0 = _Alpha;
        float _Saturate_35fc4a94f39247569cd864728c7400af_Out_1;
        Unity_Saturate_float(_Property_1427757994b04881a478a38d221123de_Out_0, _Saturate_35fc4a94f39247569cd864728c7400af_Out_1);
        float _Subtract_7135ced8bffe4d64949f974ca6083fdf_Out_2;
        Unity_Subtract_float(_Property_c3987da974dc4f0485a61aed8135469c_Out_0, _Saturate_35fc4a94f39247569cd864728c7400af_Out_1, _Subtract_7135ced8bffe4d64949f974ca6083fdf_Out_2);
        float _Ceiling_46ae28079db64bdc9dfb591ddb2c6194_Out_1;
        Unity_Ceiling_float(_Subtract_7135ced8bffe4d64949f974ca6083fdf_Out_2, _Ceiling_46ae28079db64bdc9dfb591ddb2c6194_Out_1);
        Output_0 = _Ceiling_46ae28079db64bdc9dfb591ddb2c6194_Out_1;
        }
        
        void Unity_RadialShear_float(float2 UV, float2 Center, float2 Strength, float2 Offset, out float2 Out)
        {
            float2 delta = UV - Center;
            float delta2 = dot(delta.xy, delta.xy);
            float2 delta_offset = delta2 * Strength;
            Out = UV + float2(delta.y, -delta.x) * delta_offset + Offset;
        }
        
        
        inline float2 Unity_Voronoi_RandomVector_float (float2 UV, float offset)
        {
            float2x2 m = float2x2(15.27, 47.63, 99.41, 89.98);
            UV = frac(sin(mul(UV, m)));
            return float2(sin(UV.y*+offset)*0.5+0.5, cos(UV.x*offset)*0.5+0.5);
        }
        
        void Unity_Voronoi_float(float2 UV, float AngleOffset, float CellDensity, out float Out, out float Cells)
        {
            float2 g = floor(UV * CellDensity);
            float2 f = frac(UV * CellDensity);
            float t = 8.0;
            float3 res = float3(8.0, 0.0, 0.0);
        
            for(int y=-1; y<=1; y++)
            {
                for(int x=-1; x<=1; x++)
                {
                    float2 lattice = float2(x,y);
                    float2 offset = Unity_Voronoi_RandomVector_float(lattice + g, AngleOffset);
                    float d = distance(lattice + offset, f);
        
                    if(d < res.x)
                    {
                        res = float3(d, offset.x, offset.y);
                        Out = res.x;
                        Cells = res.y;
                    }
                }
            }
        }
        
        void Unity_Multiply_float4_float4(float4 A, float4 B, out float4 Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float4(float4 A, float4 B, out float4 Out)
        {
            Out = A + B;
        }
        
        void Unity_SceneColor_float(float4 UV, out float3 Out)
        {
            Out = SHADERGRAPH_SAMPLE_SCENE_COLOR(UV.xy);
        }
        
        void Unity_NormalFromTexture_float(TEXTURE2D_PARAM(Texture, Sampler), float2 UV, float Offset, float Strength, out float3 Out)
        {
            Offset = pow(Offset, 3) * 0.1;
            float2 offsetU = float2(UV.x + Offset, UV.y);
            float2 offsetV = float2(UV.x, UV.y + Offset);
            float normalSample = SAMPLE_TEXTURE2D(Texture, Sampler, UV);
            float uSample = SAMPLE_TEXTURE2D(Texture, Sampler, offsetU);
            float vSample = SAMPLE_TEXTURE2D(Texture, Sampler, offsetV);
            float3 va = float3(1, 0, (uSample - normalSample) * Strength);
            float3 vb = float3(0, 1, (vSample - normalSample) * Strength);
            Out = normalize(cross(va, vb));
        }
        
        void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
        {
            Out = UV * Tiling + Offset;
        }
        
        
        inline float Unity_SimpleNoise_RandomValue_float (float2 uv)
        {
            float angle = dot(uv, float2(12.9898, 78.233));
            #if defined(SHADER_API_MOBILE) && (defined(SHADER_API_GLES) || defined(SHADER_API_GLES3) || defined(SHADER_API_VULKAN))
                // 'sin()' has bad precision on Mali GPUs for inputs > 10000
                angle = fmod(angle, TWO_PI); // Avoid large inputs to sin()
            #endif
            return frac(sin(angle)*43758.5453);
        }
        
        inline float Unity_SimpleNnoise_Interpolate_float (float a, float b, float t)
        {
            return (1.0-t)*a + (t*b);
        }
        
        
        inline float Unity_SimpleNoise_ValueNoise_float (float2 uv)
        {
            float2 i = floor(uv);
            float2 f = frac(uv);
            f = f * f * (3.0 - 2.0 * f);
        
            uv = abs(frac(uv) - 0.5);
            float2 c0 = i + float2(0.0, 0.0);
            float2 c1 = i + float2(1.0, 0.0);
            float2 c2 = i + float2(0.0, 1.0);
            float2 c3 = i + float2(1.0, 1.0);
            float r0 = Unity_SimpleNoise_RandomValue_float(c0);
            float r1 = Unity_SimpleNoise_RandomValue_float(c1);
            float r2 = Unity_SimpleNoise_RandomValue_float(c2);
            float r3 = Unity_SimpleNoise_RandomValue_float(c3);
        
            float bottomOfGrid = Unity_SimpleNnoise_Interpolate_float(r0, r1, f.x);
            float topOfGrid = Unity_SimpleNnoise_Interpolate_float(r2, r3, f.x);
            float t = Unity_SimpleNnoise_Interpolate_float(bottomOfGrid, topOfGrid, f.y);
            return t;
        }
        void Unity_SimpleNoise_float(float2 UV, float Scale, out float Out)
        {
            float t = 0.0;
        
            float freq = pow(2.0, float(0));
            float amp = pow(0.5, float(3-0));
            t += Unity_SimpleNoise_ValueNoise_float(float2(UV.x*Scale/freq, UV.y*Scale/freq))*amp;
        
            freq = pow(2.0, float(1));
            amp = pow(0.5, float(3-1));
            t += Unity_SimpleNoise_ValueNoise_float(float2(UV.x*Scale/freq, UV.y*Scale/freq))*amp;
        
            freq = pow(2.0, float(2));
            amp = pow(0.5, float(3-2));
            t += Unity_SimpleNoise_ValueNoise_float(float2(UV.x*Scale/freq, UV.y*Scale/freq))*amp;
        
            Out = t;
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            float _Property_3e04952c468843ab8933b2692cf6bacd_Out_0 = _WaveIntensity;
            float3 _Vector3_a18246eb0d944cbe92ba8ab4df244f74_Out_0 = float3(0, _Property_3e04952c468843ab8933b2692cf6bacd_Out_0, 0);
            float _Property_e189c9961bde4d4a80a0c20b6a92503b_Out_0 = _WaveSpeed;
            float _Multiply_c13c13771f8e4aa3a00a44ac4ff11002_Out_2;
            Unity_Multiply_float_float(_Property_e189c9961bde4d4a80a0c20b6a92503b_Out_0, IN.TimeParameters.x, _Multiply_c13c13771f8e4aa3a00a44ac4ff11002_Out_2);
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_R_1 = IN.WorldSpacePosition[0];
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_G_2 = IN.WorldSpacePosition[1];
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_B_3 = IN.WorldSpacePosition[2];
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_A_4 = 0;
            float _Add_f6078872ccf44f37bfbcc10e7e23224f_Out_2;
            Unity_Add_float(_Split_9172b35d396b4f6da3213c0bcd4ecb96_R_1, _Split_9172b35d396b4f6da3213c0bcd4ecb96_B_3, _Add_f6078872ccf44f37bfbcc10e7e23224f_Out_2);
            float _Add_6e66df41b5444def8731ecb95ab6afe3_Out_2;
            Unity_Add_float(_Multiply_c13c13771f8e4aa3a00a44ac4ff11002_Out_2, _Add_f6078872ccf44f37bfbcc10e7e23224f_Out_2, _Add_6e66df41b5444def8731ecb95ab6afe3_Out_2);
            float _Sine_1dfa4e4673a14844bdcb53c2ff481fe7_Out_1;
            Unity_Sine_float(_Add_6e66df41b5444def8731ecb95ab6afe3_Out_2, _Sine_1dfa4e4673a14844bdcb53c2ff481fe7_Out_1);
            float3 _Multiply_1fd709eee6bd43a5adf15627c9e1556b_Out_2;
            Unity_Multiply_float3_float3(_Vector3_a18246eb0d944cbe92ba8ab4df244f74_Out_0, (_Sine_1dfa4e4673a14844bdcb53c2ff481fe7_Out_1.xxx), _Multiply_1fd709eee6bd43a5adf15627c9e1556b_Out_2);
            float3 _Add_c55b332417574d1495a47eee31203ddc_Out_2;
            Unity_Add_float3(IN.ObjectSpacePosition, _Multiply_1fd709eee6bd43a5adf15627c9e1556b_Out_2, _Add_c55b332417574d1495a47eee31203ddc_Out_2);
            description.Position = _Add_c55b332417574d1495a47eee31203ddc_Out_2;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float3 BaseColor;
            float3 NormalWS;
            float3 Emission;
            float Metallic;
            float3 Specular;
            float Smoothness;
            float Occlusion;
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float4 _ScreenPosition_3b330ecd9d44487b9c002f9cc7f91cb6_Out_0 = float4(IN.ScreenPosition.xy / IN.ScreenPosition.w, 0, 0);
            float4 _Property_e7752889f9aa4c45930eda630af8bfa0_Out_0 = _FoamColor;
            float _Property_26d00366155a4444be65770ec1a4521d_Out_0 = _FoamShoreWidth;
            Bindings_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float _sSWaterDepth_e1942db5fec045e6b9a624763ed0ac66;
            _sSWaterDepth_e1942db5fec045e6b9a624763ed0ac66.ScreenPosition = IN.ScreenPosition;
            float _sSWaterDepth_e1942db5fec045e6b9a624763ed0ac66_OutVector1_1;
            SG_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float(1, 1, _sSWaterDepth_e1942db5fec045e6b9a624763ed0ac66, _sSWaterDepth_e1942db5fec045e6b9a624763ed0ac66_OutVector1_1);
            Bindings_Cutout_719ac7b9c41a99e4fa65ee4e7f3e2847_float _Cutout_923fce825c4c4c3b9bef9bbb02abb2de;
            float _Cutout_923fce825c4c4c3b9bef9bbb02abb2de_Output_0;
            SG_Cutout_719ac7b9c41a99e4fa65ee4e7f3e2847_float(_Property_26d00366155a4444be65770ec1a4521d_Out_0, _sSWaterDepth_e1942db5fec045e6b9a624763ed0ac66_OutVector1_1, _Cutout_923fce825c4c4c3b9bef9bbb02abb2de, _Cutout_923fce825c4c4c3b9bef9bbb02abb2de_Output_0);
            float2 _RadialShear_9df83a3a335746848643024aa5b7c9e0_Out_4;
            Unity_RadialShear_float(IN.uv0.xy, float2 (0.5, 0.5), float2 (5, 5), float2 (0, 0), _RadialShear_9df83a3a335746848643024aa5b7c9e0_Out_4);
            float _Voronoi_1a7e03c0bcbd4dc29b26257ce21caa1d_Out_3;
            float _Voronoi_1a7e03c0bcbd4dc29b26257ce21caa1d_Cells_4;
            Unity_Voronoi_float(_RadialShear_9df83a3a335746848643024aa5b7c9e0_Out_4, IN.TimeParameters.x, 8, _Voronoi_1a7e03c0bcbd4dc29b26257ce21caa1d_Out_3, _Voronoi_1a7e03c0bcbd4dc29b26257ce21caa1d_Cells_4);
            float _Property_636c9004ffcb4e39863cd54e7352d8e6_Out_0 = _Float;
            float _Power_fe5432d360554233a7cc6f909009a36a_Out_2;
            Unity_Power_float(_Voronoi_1a7e03c0bcbd4dc29b26257ce21caa1d_Out_3, _Property_636c9004ffcb4e39863cd54e7352d8e6_Out_0, _Power_fe5432d360554233a7cc6f909009a36a_Out_2);
            float _Property_f3b23a2533b640a4851a42b69fe171e4_Out_0 = _FoamDepth;
            float _Property_7c6c40f2b3564d36b4001b8efb2af0ea_Out_0 = _FoamFalloff;
            Bindings_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float _sSWaterDepth_bb1b5652101c44a1919b6541defe8462;
            _sSWaterDepth_bb1b5652101c44a1919b6541defe8462.ScreenPosition = IN.ScreenPosition;
            float _sSWaterDepth_bb1b5652101c44a1919b6541defe8462_OutVector1_1;
            SG_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float(_Property_f3b23a2533b640a4851a42b69fe171e4_Out_0, _Property_7c6c40f2b3564d36b4001b8efb2af0ea_Out_0, _sSWaterDepth_bb1b5652101c44a1919b6541defe8462, _sSWaterDepth_bb1b5652101c44a1919b6541defe8462_OutVector1_1);
            float _Multiply_ccdc528bed5d4c18b6ab85511e54d5a6_Out_2;
            Unity_Multiply_float_float(_Power_fe5432d360554233a7cc6f909009a36a_Out_2, _sSWaterDepth_bb1b5652101c44a1919b6541defe8462_OutVector1_1, _Multiply_ccdc528bed5d4c18b6ab85511e54d5a6_Out_2);
            float _Add_4191fc0bec74415aa999b2a33166d88a_Out_2;
            Unity_Add_float(_Cutout_923fce825c4c4c3b9bef9bbb02abb2de_Output_0, _Multiply_ccdc528bed5d4c18b6ab85511e54d5a6_Out_2, _Add_4191fc0bec74415aa999b2a33166d88a_Out_2);
            float _Saturate_b910c8272d1a4155a7173705fb331898_Out_1;
            Unity_Saturate_float(_Add_4191fc0bec74415aa999b2a33166d88a_Out_2, _Saturate_b910c8272d1a4155a7173705fb331898_Out_1);
            float4 _Multiply_ff39ce7dfb524ce481784d5917bc83a8_Out_2;
            Unity_Multiply_float4_float4(_Property_e7752889f9aa4c45930eda630af8bfa0_Out_0, (_Saturate_b910c8272d1a4155a7173705fb331898_Out_1.xxxx), _Multiply_ff39ce7dfb524ce481784d5917bc83a8_Out_2);
            float _OneMinus_894809b879cc48b7ba2261fac43e00cd_Out_1;
            Unity_OneMinus_float(_Saturate_b910c8272d1a4155a7173705fb331898_Out_1, _OneMinus_894809b879cc48b7ba2261fac43e00cd_Out_1);
            float4 _Property_e0ba34c22e694f58ba492f038826fde6_Out_0 = _ShoreColor;
            float _Property_375ab85e259d4db39b617839ff4c5008_Out_0 = _Depth;
            float _Property_75e9248b4fc9458e99caeb3bf19b1908_Out_0 = _DepthFalloff;
            Bindings_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float _sSWaterDepth_558ab96888514e11b58182e01c6b6fa2;
            _sSWaterDepth_558ab96888514e11b58182e01c6b6fa2.ScreenPosition = IN.ScreenPosition;
            float _sSWaterDepth_558ab96888514e11b58182e01c6b6fa2_OutVector1_1;
            SG_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float(_Property_375ab85e259d4db39b617839ff4c5008_Out_0, _Property_75e9248b4fc9458e99caeb3bf19b1908_Out_0, _sSWaterDepth_558ab96888514e11b58182e01c6b6fa2, _sSWaterDepth_558ab96888514e11b58182e01c6b6fa2_OutVector1_1);
            float4 _Multiply_d0c0d2997ae6412ea37dadf952efd293_Out_2;
            Unity_Multiply_float4_float4(_Property_e0ba34c22e694f58ba492f038826fde6_Out_0, (_sSWaterDepth_558ab96888514e11b58182e01c6b6fa2_OutVector1_1.xxxx), _Multiply_d0c0d2997ae6412ea37dadf952efd293_Out_2);
            float _OneMinus_c0858fd22bc54bd59887f99335d6311f_Out_1;
            Unity_OneMinus_float(_sSWaterDepth_558ab96888514e11b58182e01c6b6fa2_OutVector1_1, _OneMinus_c0858fd22bc54bd59887f99335d6311f_Out_1);
            float4 _Property_6435d5dfbd6e4beebf3719823fe9ba99_Out_0 = _Color;
            float4 _Multiply_edf9a4ef75bc4ac4ad6c88cb52c69d3d_Out_2;
            Unity_Multiply_float4_float4((_OneMinus_c0858fd22bc54bd59887f99335d6311f_Out_1.xxxx), _Property_6435d5dfbd6e4beebf3719823fe9ba99_Out_0, _Multiply_edf9a4ef75bc4ac4ad6c88cb52c69d3d_Out_2);
            float4 _Add_3817e0ebbc6e44d594f3d07bde8e7ce6_Out_2;
            Unity_Add_float4(_Multiply_d0c0d2997ae6412ea37dadf952efd293_Out_2, _Multiply_edf9a4ef75bc4ac4ad6c88cb52c69d3d_Out_2, _Add_3817e0ebbc6e44d594f3d07bde8e7ce6_Out_2);
            float4 _Multiply_85d7a273de444dcb983c32751eeace29_Out_2;
            Unity_Multiply_float4_float4((_OneMinus_894809b879cc48b7ba2261fac43e00cd_Out_1.xxxx), _Add_3817e0ebbc6e44d594f3d07bde8e7ce6_Out_2, _Multiply_85d7a273de444dcb983c32751eeace29_Out_2);
            float4 _Add_7b0f7efb411c4c598bcc28966dd51c15_Out_2;
            Unity_Add_float4(_Multiply_ff39ce7dfb524ce481784d5917bc83a8_Out_2, _Multiply_85d7a273de444dcb983c32751eeace29_Out_2, _Add_7b0f7efb411c4c598bcc28966dd51c15_Out_2);
            float4 _Multiply_e0820bab8d5d461eaa9c157e8bce8884_Out_2;
            Unity_Multiply_float4_float4(_Add_7b0f7efb411c4c598bcc28966dd51c15_Out_2, float4(2, 2, 2, 2), _Multiply_e0820bab8d5d461eaa9c157e8bce8884_Out_2);
            float4 _Add_e74668030b0b498385a1a9468b8dbd24_Out_2;
            Unity_Add_float4(_ScreenPosition_3b330ecd9d44487b9c002f9cc7f91cb6_Out_0, _Multiply_e0820bab8d5d461eaa9c157e8bce8884_Out_2, _Add_e74668030b0b498385a1a9468b8dbd24_Out_2);
            float3 _SceneColor_ef9de574552a4340b580a3d43b107025_Out_1;
            Unity_SceneColor_float(_Add_e74668030b0b498385a1a9468b8dbd24_Out_2, _SceneColor_ef9de574552a4340b580a3d43b107025_Out_1);
            UnityTexture2D _Property_5578bd8013004991ad2902cb855a6d0b_Out_0 = UnityBuildTexture2DStruct(_NormalTexture);
            float2 _RadialShear_bccb9a215bd44eafbe040c49c8000278_Out_4;
            Unity_RadialShear_float(IN.uv0.xy, float2 (0.5, 0.5), float2 (5, 5), float2 (0, 0), _RadialShear_bccb9a215bd44eafbe040c49c8000278_Out_4);
            float _Voronoi_1065fa35ce7b4200924784cef13d4b39_Out_3;
            float _Voronoi_1065fa35ce7b4200924784cef13d4b39_Cells_4;
            Unity_Voronoi_float(_RadialShear_bccb9a215bd44eafbe040c49c8000278_Out_4, IN.TimeParameters.x, 8, _Voronoi_1065fa35ce7b4200924784cef13d4b39_Out_3, _Voronoi_1065fa35ce7b4200924784cef13d4b39_Cells_4);
            float _Property_73ca0a3ef1f542c591eac06bebfdfc62_Out_0 = _NormalStrenght;
            float3 _NormalFromTexture_1fc976b5873840429e4ac72c33aa2d0a_Out_5;
            Unity_NormalFromTexture_float(TEXTURE2D_ARGS(_Property_5578bd8013004991ad2902cb855a6d0b_Out_0.tex, _Property_5578bd8013004991ad2902cb855a6d0b_Out_0.samplerstate), _Property_5578bd8013004991ad2902cb855a6d0b_Out_0.GetTransformedUV(IN.uv1.xy), _Voronoi_1065fa35ce7b4200924784cef13d4b39_Out_3, _Property_73ca0a3ef1f542c591eac06bebfdfc62_Out_0, _NormalFromTexture_1fc976b5873840429e4ac72c33aa2d0a_Out_5);
            float _Multiply_9103c4b0ed7b41dab55d0c48769d043f_Out_2;
            Unity_Multiply_float_float(IN.TimeParameters.x, 0.01, _Multiply_9103c4b0ed7b41dab55d0c48769d043f_Out_2);
            float2 _TilingAndOffset_a8ae8dd4702740cd81c535e72ab6869c_Out_3;
            Unity_TilingAndOffset_float(_RadialShear_9df83a3a335746848643024aa5b7c9e0_Out_4, float2 (0.5, 0.5), (_Multiply_9103c4b0ed7b41dab55d0c48769d043f_Out_2.xx), _TilingAndOffset_a8ae8dd4702740cd81c535e72ab6869c_Out_3);
            float _SimpleNoise_b317b9dbecde4771a32418318aa6bf2e_Out_2;
            Unity_SimpleNoise_float(_TilingAndOffset_a8ae8dd4702740cd81c535e72ab6869c_Out_3, 300, _SimpleNoise_b317b9dbecde4771a32418318aa6bf2e_Out_2);
            float _Power_e0b197c557384350a2d2f0d8f2cce02c_Out_2;
            Unity_Power_float(_SimpleNoise_b317b9dbecde4771a32418318aa6bf2e_Out_2, 10, _Power_e0b197c557384350a2d2f0d8f2cce02c_Out_2);
            float _Add_4f955775a32a4e90962988b710db2973_Out_2;
            Unity_Add_float(_Power_e0b197c557384350a2d2f0d8f2cce02c_Out_2, _Power_e0b197c557384350a2d2f0d8f2cce02c_Out_2, _Add_4f955775a32a4e90962988b710db2973_Out_2);
            float _Add_34f816e43a2146619e527cf77b5a2829_Out_2;
            Unity_Add_float(_Power_fe5432d360554233a7cc6f909009a36a_Out_2, _Add_4f955775a32a4e90962988b710db2973_Out_2, _Add_34f816e43a2146619e527cf77b5a2829_Out_2);
            float4 _Add_45316f2f4573425b8fb3629669a020c7_Out_2;
            Unity_Add_float4((_Add_34f816e43a2146619e527cf77b5a2829_Out_2.xxxx), _Add_3817e0ebbc6e44d594f3d07bde8e7ce6_Out_2, _Add_45316f2f4573425b8fb3629669a020c7_Out_2);
            float4 _Add_bc4e82f988a84312bc336ac7809f548c_Out_2;
            Unity_Add_float4(_Add_7b0f7efb411c4c598bcc28966dd51c15_Out_2, _Add_45316f2f4573425b8fb3629669a020c7_Out_2, _Add_bc4e82f988a84312bc336ac7809f548c_Out_2);
            float _Property_4990610322e74243844d3db91469cce8_Out_0 = _Metal;
            surface.BaseColor = _SceneColor_ef9de574552a4340b580a3d43b107025_Out_1;
            surface.NormalWS = _NormalFromTexture_1fc976b5873840429e4ac72c33aa2d0a_Out_5;
            surface.Emission = (_Add_bc4e82f988a84312bc336ac7809f548c_Out_2.xyz);
            surface.Metallic = _Property_4990610322e74243844d3db91469cce8_Out_0;
            surface.Specular = IsGammaSpace() ? float3(0.5, 0.5, 0.5) : SRGBToLinear(float3(0.5, 0.5, 0.5));
            surface.Smoothness = 1;
            surface.Occlusion = 1;
            surface.Alpha = 1;
            surface.AlphaClipThreshold = 0.5;
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
            output.WorldSpacePosition =                         TransformObjectToWorld(input.positionOS);
            output.TimeParameters =                             _TimeParameters.xyz;
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
            
        
            // must use interpolated tangent, bitangent and normal before they are normalized in the pixel shader.
            float3 unnormalizedNormalWS = input.normalWS;
            const float renormFactor = 1.0 / length(unnormalizedNormalWS);
        
        
            output.WorldSpaceNormal = renormFactor * input.normalWS.xyz;      // we want a unit length Normal Vector node in shader graph
        
        
            output.WorldSpacePosition = input.positionWS;
            output.ScreenPosition = ComputeScreenPos(TransformWorldToHClip(input.positionWS), _ProjectionParams.x);
            output.uv0 = input.texCoord0;
            output.uv1 = input.texCoord1;
            output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        void BuildAppDataFull(Attributes attributes, VertexDescription vertexDescription, inout appdata_full result)
        {
            result.vertex     = float4(attributes.positionOS, 1);
            result.tangent    = attributes.tangentOS;
            result.normal     = attributes.normalOS;
            result.texcoord   = attributes.uv0;
            result.texcoord1  = attributes.uv1;
            result.vertex     = float4(vertexDescription.Position, 1);
            result.normal     = vertexDescription.Normal;
            result.tangent    = float4(vertexDescription.Tangent, 0);
            #if UNITY_ANY_INSTANCING_ENABLED
            #endif
        }
        
        void VaryingsToSurfaceVertex(Varyings varyings, inout v2f_surf result)
        {
            result.pos = varyings.positionCS;
            result.worldPos = varyings.positionWS;
            result.worldNormal = varyings.normalWS;
            result.viewDir = varyings.viewDirectionWS;
            // World Tangent isn't an available input on v2f_surf
        
            result._ShadowCoord = varyings.shadowCoord;
        
            #if UNITY_ANY_INSTANCING_ENABLED
            #endif
            #if !defined(LIGHTMAP_ON)
            #if UNITY_SHOULD_SAMPLE_SH
            result.sh = varyings.sh;
            #endif
            #endif
            #if defined(LIGHTMAP_ON)
            result.lmap.xy = varyings.lightmapUV;
            #endif
            #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
                result.fogCoord = varyings.fogFactorAndVertexLight.x;
                COPY_TO_LIGHT_COORDS(result, varyings.fogFactorAndVertexLight.yzw);
            #endif
        
            DEFAULT_UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(varyings, result);
        }
        
        void SurfaceVertexToVaryings(v2f_surf surfVertex, inout Varyings result)
        {
            result.positionCS = surfVertex.pos;
            result.positionWS = surfVertex.worldPos;
            result.normalWS = surfVertex.worldNormal;
            // viewDirectionWS is never filled out in the legacy pass' function. Always use the value computed by SRP
            // World Tangent isn't an available input on v2f_surf
            result.shadowCoord = surfVertex._ShadowCoord;
        
            #if UNITY_ANY_INSTANCING_ENABLED
            #endif
            #if !defined(LIGHTMAP_ON)
            #if UNITY_SHOULD_SAMPLE_SH
            result.sh = surfVertex.sh;
            #endif
            #endif
            #if defined(LIGHTMAP_ON)
            result.lightmapUV = surfVertex.lmap.xy;
            #endif
            #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
                result.fogFactorAndVertexLight.x = surfVertex.fogCoord;
                COPY_FROM_LIGHT_COORDS(result.fogFactorAndVertexLight.yzw, surfVertex);
            #endif
        
            DEFAULT_UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(surfVertex, result);
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/PBRForwardPass.hlsl"
        
        ENDHLSL
        }
        Pass
        {
            Name "BuiltIn ForwardAdd"
            Tags
            {
                "LightMode" = "ForwardAdd"
            }
        
        // Render State
        Blend SrcAlpha One
        ZWrite Off
        ColorMask RGB
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 3.0
        #pragma multi_compile_instancing
        #pragma multi_compile_fog
        #pragma multi_compile_fwdadd_fullshadows
        #pragma vertex vert
        #pragma fragment frag
        
        // DotsInstancingOptions: <None>
        // HybridV1InjectedBuiltinProperties: <None>
        
        // Keywords
        #pragma multi_compile _ _SCREEN_SPACE_OCCLUSION
        #pragma multi_compile _ LIGHTMAP_ON
        #pragma multi_compile _ DIRLIGHTMAP_COMBINED
        #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
        #pragma multi_compile _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS _ADDITIONAL_OFF
        #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
        #pragma multi_compile _ _SHADOWS_SOFT
        #pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
        #pragma multi_compile _ SHADOWS_SHADOWMASK
        #pragma shader_feature_local_fragment _ _BUILTIN_SURFACE_TYPE_TRANSPARENT
        #pragma shader_feature_local_fragment _ _BUILTIN_AlphaClip
        #pragma shader_feature_local_fragment _ _BUILTIN_ALPHATEST_ON
        // GraphKeywords: <None>
        
        // Defines
        #define _NORMALMAP 1
        #define _NORMAL_DROPOFF_WS 1
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define ATTRIBUTES_NEED_TEXCOORD1
        #define VARYINGS_NEED_POSITION_WS
        #define VARYINGS_NEED_NORMAL_WS
        #define VARYINGS_NEED_TANGENT_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define VARYINGS_NEED_TEXCOORD1
        #define VARYINGS_NEED_VIEWDIRECTION_WS
        #define VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_FORWARD_ADD
        #define BUILTIN_TARGET_API 1
        #define REQUIRE_DEPTH_TEXTURE
        #define REQUIRE_OPAQUE_TEXTURE
        /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
        #ifdef _BUILTIN_SURFACE_TYPE_TRANSPARENT
        #define _SURFACE_TYPE_TRANSPARENT _BUILTIN_SURFACE_TYPE_TRANSPARENT
        #endif
        #ifdef _BUILTIN_ALPHATEST_ON
        #define _ALPHATEST_ON _BUILTIN_ALPHATEST_ON
        #endif
        #ifdef _BUILTIN_AlphaClip
        #define _AlphaClip _BUILTIN_AlphaClip
        #endif
        #ifdef _BUILTIN_ALPHAPREMULTIPLY_ON
        #define _ALPHAPREMULTIPLY_ON _BUILTIN_ALPHAPREMULTIPLY_ON
        #endif
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Shim/Shims.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/LegacySurfaceVertex.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/ShaderGraphFunctions.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
             float4 uv1 : TEXCOORD1;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 positionWS;
             float3 normalWS;
             float4 tangentWS;
             float4 texCoord0;
             float4 texCoord1;
             float3 viewDirectionWS;
            #if defined(LIGHTMAP_ON)
             float2 lightmapUV;
            #endif
            #if !defined(LIGHTMAP_ON)
             float3 sh;
            #endif
             float4 fogFactorAndVertexLight;
             float4 shadowCoord;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float3 WorldSpaceNormal;
             float3 WorldSpacePosition;
             float4 ScreenPosition;
             float4 uv0;
             float4 uv1;
             float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
             float3 WorldSpacePosition;
             float3 TimeParameters;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float3 interp0 : INTERP0;
             float3 interp1 : INTERP1;
             float4 interp2 : INTERP2;
             float4 interp3 : INTERP3;
             float4 interp4 : INTERP4;
             float3 interp5 : INTERP5;
             float2 interp6 : INTERP6;
             float3 interp7 : INTERP7;
             float4 interp8 : INTERP8;
             float4 interp9 : INTERP9;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.interp0.xyz =  input.positionWS;
            output.interp1.xyz =  input.normalWS;
            output.interp2.xyzw =  input.tangentWS;
            output.interp3.xyzw =  input.texCoord0;
            output.interp4.xyzw =  input.texCoord1;
            output.interp5.xyz =  input.viewDirectionWS;
            #if defined(LIGHTMAP_ON)
            output.interp6.xy =  input.lightmapUV;
            #endif
            #if !defined(LIGHTMAP_ON)
            output.interp7.xyz =  input.sh;
            #endif
            output.interp8.xyzw =  input.fogFactorAndVertexLight;
            output.interp9.xyzw =  input.shadowCoord;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.positionWS = input.interp0.xyz;
            output.normalWS = input.interp1.xyz;
            output.tangentWS = input.interp2.xyzw;
            output.texCoord0 = input.interp3.xyzw;
            output.texCoord1 = input.interp4.xyzw;
            output.viewDirectionWS = input.interp5.xyz;
            #if defined(LIGHTMAP_ON)
            output.lightmapUV = input.interp6.xy;
            #endif
            #if !defined(LIGHTMAP_ON)
            output.sh = input.interp7.xyz;
            #endif
            output.fogFactorAndVertexLight = input.interp8.xyzw;
            output.shadowCoord = input.interp9.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _Depth;
        float _DepthFalloff;
        float4 _ShoreColor;
        float4 _Color;
        float _FoamShoreWidth;
        float4 _FoamColor;
        float _FoamDepth;
        float _FoamFalloff;
        float _WaveIntensity;
        float _WaveSpeed;
        float _Float;
        float _Metal;
        float4 _NormalTexture_TexelSize;
        float4 _NormalTexture_ST;
        float _NormalStrenght;
        CBUFFER_END
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_NormalTexture);
        SAMPLER(sampler_NormalTexture);
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Includes
        // GraphIncludes: <None>
        
        // Graph Functions
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float(float A, float B, out float Out)
        {
            Out = A + B;
        }
        
        void Unity_Sine_float(float In, out float Out)
        {
            Out = sin(In);
        }
        
        void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A + B;
        }
        
        void Unity_SceneDepth_Eye_float(float4 UV, out float Out)
        {
            if (unity_OrthoParams.w == 1.0)
            {
                Out = LinearEyeDepth(ComputeWorldSpacePosition(UV.xy, SHADERGRAPH_SAMPLE_SCENE_DEPTH(UV.xy), UNITY_MATRIX_I_VP), UNITY_MATRIX_V);
            }
            else
            {
                Out = LinearEyeDepth(SHADERGRAPH_SAMPLE_SCENE_DEPTH(UV.xy), _ZBufferParams);
            }
        }
        
        void Unity_Subtract_float(float A, float B, out float Out)
        {
            Out = A - B;
        }
        
        void Unity_Divide_float(float A, float B, out float Out)
        {
            Out = A / B;
        }
        
        void Unity_OneMinus_float(float In, out float Out)
        {
            Out = 1 - In;
        }
        
        void Unity_Saturate_float(float In, out float Out)
        {
            Out = saturate(In);
        }
        
        void Unity_Power_float(float A, float B, out float Out)
        {
            Out = pow(A, B);
        }
        
        struct Bindings_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float
        {
        float4 ScreenPosition;
        };
        
        void SG_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float(float _Depth, float _DepthFalloff, Bindings_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float IN, out float OutVector1_1)
        {
        float _SceneDepth_4398a40afb5444a98bdb88f05ff37be7_Out_1;
        Unity_SceneDepth_Eye_float(float4(IN.ScreenPosition.xy / IN.ScreenPosition.w, 0, 0), _SceneDepth_4398a40afb5444a98bdb88f05ff37be7_Out_1);
        float4 _ScreenPosition_78861b0c174847048fb96d1b9be441c4_Out_0 = IN.ScreenPosition;
        float _Split_9aecda2f9d2945b9b0a54de06a3a9d48_R_1 = _ScreenPosition_78861b0c174847048fb96d1b9be441c4_Out_0[0];
        float _Split_9aecda2f9d2945b9b0a54de06a3a9d48_G_2 = _ScreenPosition_78861b0c174847048fb96d1b9be441c4_Out_0[1];
        float _Split_9aecda2f9d2945b9b0a54de06a3a9d48_B_3 = _ScreenPosition_78861b0c174847048fb96d1b9be441c4_Out_0[2];
        float _Split_9aecda2f9d2945b9b0a54de06a3a9d48_A_4 = _ScreenPosition_78861b0c174847048fb96d1b9be441c4_Out_0[3];
        float _Subtract_0ef5acf5e9ec4f599d608aed9d014d88_Out_2;
        Unity_Subtract_float(_SceneDepth_4398a40afb5444a98bdb88f05ff37be7_Out_1, _Split_9aecda2f9d2945b9b0a54de06a3a9d48_A_4, _Subtract_0ef5acf5e9ec4f599d608aed9d014d88_Out_2);
        float _Property_5702a0604e9c425f9f28a8e389f7d6e8_Out_0 = _Depth;
        float _Divide_230152a01c7a4ab691e1a20a1fbf597f_Out_2;
        Unity_Divide_float(_Subtract_0ef5acf5e9ec4f599d608aed9d014d88_Out_2, _Property_5702a0604e9c425f9f28a8e389f7d6e8_Out_0, _Divide_230152a01c7a4ab691e1a20a1fbf597f_Out_2);
        float _OneMinus_4edb44dcaf8a4df5974bc2b0bfc1a39d_Out_1;
        Unity_OneMinus_float(_Divide_230152a01c7a4ab691e1a20a1fbf597f_Out_2, _OneMinus_4edb44dcaf8a4df5974bc2b0bfc1a39d_Out_1);
        float _Saturate_c574ae0f849b4335bdfce5762b7b4760_Out_1;
        Unity_Saturate_float(_OneMinus_4edb44dcaf8a4df5974bc2b0bfc1a39d_Out_1, _Saturate_c574ae0f849b4335bdfce5762b7b4760_Out_1);
        float _Property_8b3812fa4cec4943b82bfecf45cd931a_Out_0 = _DepthFalloff;
        float _Power_494a422a08904cbbb881363a1d49b985_Out_2;
        Unity_Power_float(_Saturate_c574ae0f849b4335bdfce5762b7b4760_Out_1, _Property_8b3812fa4cec4943b82bfecf45cd931a_Out_0, _Power_494a422a08904cbbb881363a1d49b985_Out_2);
        OutVector1_1 = _Power_494a422a08904cbbb881363a1d49b985_Out_2;
        }
        
        void Unity_Ceiling_float(float In, out float Out)
        {
            Out = ceil(In);
        }
        
        struct Bindings_Cutout_719ac7b9c41a99e4fa65ee4e7f3e2847_float
        {
        };
        
        void SG_Cutout_719ac7b9c41a99e4fa65ee4e7f3e2847_float(float _Alpha, float _Input, Bindings_Cutout_719ac7b9c41a99e4fa65ee4e7f3e2847_float IN, out float Output_0)
        {
        float _Property_c3987da974dc4f0485a61aed8135469c_Out_0 = _Input;
        float _Property_1427757994b04881a478a38d221123de_Out_0 = _Alpha;
        float _Saturate_35fc4a94f39247569cd864728c7400af_Out_1;
        Unity_Saturate_float(_Property_1427757994b04881a478a38d221123de_Out_0, _Saturate_35fc4a94f39247569cd864728c7400af_Out_1);
        float _Subtract_7135ced8bffe4d64949f974ca6083fdf_Out_2;
        Unity_Subtract_float(_Property_c3987da974dc4f0485a61aed8135469c_Out_0, _Saturate_35fc4a94f39247569cd864728c7400af_Out_1, _Subtract_7135ced8bffe4d64949f974ca6083fdf_Out_2);
        float _Ceiling_46ae28079db64bdc9dfb591ddb2c6194_Out_1;
        Unity_Ceiling_float(_Subtract_7135ced8bffe4d64949f974ca6083fdf_Out_2, _Ceiling_46ae28079db64bdc9dfb591ddb2c6194_Out_1);
        Output_0 = _Ceiling_46ae28079db64bdc9dfb591ddb2c6194_Out_1;
        }
        
        void Unity_RadialShear_float(float2 UV, float2 Center, float2 Strength, float2 Offset, out float2 Out)
        {
            float2 delta = UV - Center;
            float delta2 = dot(delta.xy, delta.xy);
            float2 delta_offset = delta2 * Strength;
            Out = UV + float2(delta.y, -delta.x) * delta_offset + Offset;
        }
        
        
        inline float2 Unity_Voronoi_RandomVector_float (float2 UV, float offset)
        {
            float2x2 m = float2x2(15.27, 47.63, 99.41, 89.98);
            UV = frac(sin(mul(UV, m)));
            return float2(sin(UV.y*+offset)*0.5+0.5, cos(UV.x*offset)*0.5+0.5);
        }
        
        void Unity_Voronoi_float(float2 UV, float AngleOffset, float CellDensity, out float Out, out float Cells)
        {
            float2 g = floor(UV * CellDensity);
            float2 f = frac(UV * CellDensity);
            float t = 8.0;
            float3 res = float3(8.0, 0.0, 0.0);
        
            for(int y=-1; y<=1; y++)
            {
                for(int x=-1; x<=1; x++)
                {
                    float2 lattice = float2(x,y);
                    float2 offset = Unity_Voronoi_RandomVector_float(lattice + g, AngleOffset);
                    float d = distance(lattice + offset, f);
        
                    if(d < res.x)
                    {
                        res = float3(d, offset.x, offset.y);
                        Out = res.x;
                        Cells = res.y;
                    }
                }
            }
        }
        
        void Unity_Multiply_float4_float4(float4 A, float4 B, out float4 Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float4(float4 A, float4 B, out float4 Out)
        {
            Out = A + B;
        }
        
        void Unity_SceneColor_float(float4 UV, out float3 Out)
        {
            Out = SHADERGRAPH_SAMPLE_SCENE_COLOR(UV.xy);
        }
        
        void Unity_NormalFromTexture_float(TEXTURE2D_PARAM(Texture, Sampler), float2 UV, float Offset, float Strength, out float3 Out)
        {
            Offset = pow(Offset, 3) * 0.1;
            float2 offsetU = float2(UV.x + Offset, UV.y);
            float2 offsetV = float2(UV.x, UV.y + Offset);
            float normalSample = SAMPLE_TEXTURE2D(Texture, Sampler, UV);
            float uSample = SAMPLE_TEXTURE2D(Texture, Sampler, offsetU);
            float vSample = SAMPLE_TEXTURE2D(Texture, Sampler, offsetV);
            float3 va = float3(1, 0, (uSample - normalSample) * Strength);
            float3 vb = float3(0, 1, (vSample - normalSample) * Strength);
            Out = normalize(cross(va, vb));
        }
        
        void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
        {
            Out = UV * Tiling + Offset;
        }
        
        
        inline float Unity_SimpleNoise_RandomValue_float (float2 uv)
        {
            float angle = dot(uv, float2(12.9898, 78.233));
            #if defined(SHADER_API_MOBILE) && (defined(SHADER_API_GLES) || defined(SHADER_API_GLES3) || defined(SHADER_API_VULKAN))
                // 'sin()' has bad precision on Mali GPUs for inputs > 10000
                angle = fmod(angle, TWO_PI); // Avoid large inputs to sin()
            #endif
            return frac(sin(angle)*43758.5453);
        }
        
        inline float Unity_SimpleNnoise_Interpolate_float (float a, float b, float t)
        {
            return (1.0-t)*a + (t*b);
        }
        
        
        inline float Unity_SimpleNoise_ValueNoise_float (float2 uv)
        {
            float2 i = floor(uv);
            float2 f = frac(uv);
            f = f * f * (3.0 - 2.0 * f);
        
            uv = abs(frac(uv) - 0.5);
            float2 c0 = i + float2(0.0, 0.0);
            float2 c1 = i + float2(1.0, 0.0);
            float2 c2 = i + float2(0.0, 1.0);
            float2 c3 = i + float2(1.0, 1.0);
            float r0 = Unity_SimpleNoise_RandomValue_float(c0);
            float r1 = Unity_SimpleNoise_RandomValue_float(c1);
            float r2 = Unity_SimpleNoise_RandomValue_float(c2);
            float r3 = Unity_SimpleNoise_RandomValue_float(c3);
        
            float bottomOfGrid = Unity_SimpleNnoise_Interpolate_float(r0, r1, f.x);
            float topOfGrid = Unity_SimpleNnoise_Interpolate_float(r2, r3, f.x);
            float t = Unity_SimpleNnoise_Interpolate_float(bottomOfGrid, topOfGrid, f.y);
            return t;
        }
        void Unity_SimpleNoise_float(float2 UV, float Scale, out float Out)
        {
            float t = 0.0;
        
            float freq = pow(2.0, float(0));
            float amp = pow(0.5, float(3-0));
            t += Unity_SimpleNoise_ValueNoise_float(float2(UV.x*Scale/freq, UV.y*Scale/freq))*amp;
        
            freq = pow(2.0, float(1));
            amp = pow(0.5, float(3-1));
            t += Unity_SimpleNoise_ValueNoise_float(float2(UV.x*Scale/freq, UV.y*Scale/freq))*amp;
        
            freq = pow(2.0, float(2));
            amp = pow(0.5, float(3-2));
            t += Unity_SimpleNoise_ValueNoise_float(float2(UV.x*Scale/freq, UV.y*Scale/freq))*amp;
        
            Out = t;
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            float _Property_3e04952c468843ab8933b2692cf6bacd_Out_0 = _WaveIntensity;
            float3 _Vector3_a18246eb0d944cbe92ba8ab4df244f74_Out_0 = float3(0, _Property_3e04952c468843ab8933b2692cf6bacd_Out_0, 0);
            float _Property_e189c9961bde4d4a80a0c20b6a92503b_Out_0 = _WaveSpeed;
            float _Multiply_c13c13771f8e4aa3a00a44ac4ff11002_Out_2;
            Unity_Multiply_float_float(_Property_e189c9961bde4d4a80a0c20b6a92503b_Out_0, IN.TimeParameters.x, _Multiply_c13c13771f8e4aa3a00a44ac4ff11002_Out_2);
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_R_1 = IN.WorldSpacePosition[0];
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_G_2 = IN.WorldSpacePosition[1];
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_B_3 = IN.WorldSpacePosition[2];
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_A_4 = 0;
            float _Add_f6078872ccf44f37bfbcc10e7e23224f_Out_2;
            Unity_Add_float(_Split_9172b35d396b4f6da3213c0bcd4ecb96_R_1, _Split_9172b35d396b4f6da3213c0bcd4ecb96_B_3, _Add_f6078872ccf44f37bfbcc10e7e23224f_Out_2);
            float _Add_6e66df41b5444def8731ecb95ab6afe3_Out_2;
            Unity_Add_float(_Multiply_c13c13771f8e4aa3a00a44ac4ff11002_Out_2, _Add_f6078872ccf44f37bfbcc10e7e23224f_Out_2, _Add_6e66df41b5444def8731ecb95ab6afe3_Out_2);
            float _Sine_1dfa4e4673a14844bdcb53c2ff481fe7_Out_1;
            Unity_Sine_float(_Add_6e66df41b5444def8731ecb95ab6afe3_Out_2, _Sine_1dfa4e4673a14844bdcb53c2ff481fe7_Out_1);
            float3 _Multiply_1fd709eee6bd43a5adf15627c9e1556b_Out_2;
            Unity_Multiply_float3_float3(_Vector3_a18246eb0d944cbe92ba8ab4df244f74_Out_0, (_Sine_1dfa4e4673a14844bdcb53c2ff481fe7_Out_1.xxx), _Multiply_1fd709eee6bd43a5adf15627c9e1556b_Out_2);
            float3 _Add_c55b332417574d1495a47eee31203ddc_Out_2;
            Unity_Add_float3(IN.ObjectSpacePosition, _Multiply_1fd709eee6bd43a5adf15627c9e1556b_Out_2, _Add_c55b332417574d1495a47eee31203ddc_Out_2);
            description.Position = _Add_c55b332417574d1495a47eee31203ddc_Out_2;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float3 BaseColor;
            float3 NormalWS;
            float3 Emission;
            float Metallic;
            float3 Specular;
            float Smoothness;
            float Occlusion;
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float4 _ScreenPosition_3b330ecd9d44487b9c002f9cc7f91cb6_Out_0 = float4(IN.ScreenPosition.xy / IN.ScreenPosition.w, 0, 0);
            float4 _Property_e7752889f9aa4c45930eda630af8bfa0_Out_0 = _FoamColor;
            float _Property_26d00366155a4444be65770ec1a4521d_Out_0 = _FoamShoreWidth;
            Bindings_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float _sSWaterDepth_e1942db5fec045e6b9a624763ed0ac66;
            _sSWaterDepth_e1942db5fec045e6b9a624763ed0ac66.ScreenPosition = IN.ScreenPosition;
            float _sSWaterDepth_e1942db5fec045e6b9a624763ed0ac66_OutVector1_1;
            SG_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float(1, 1, _sSWaterDepth_e1942db5fec045e6b9a624763ed0ac66, _sSWaterDepth_e1942db5fec045e6b9a624763ed0ac66_OutVector1_1);
            Bindings_Cutout_719ac7b9c41a99e4fa65ee4e7f3e2847_float _Cutout_923fce825c4c4c3b9bef9bbb02abb2de;
            float _Cutout_923fce825c4c4c3b9bef9bbb02abb2de_Output_0;
            SG_Cutout_719ac7b9c41a99e4fa65ee4e7f3e2847_float(_Property_26d00366155a4444be65770ec1a4521d_Out_0, _sSWaterDepth_e1942db5fec045e6b9a624763ed0ac66_OutVector1_1, _Cutout_923fce825c4c4c3b9bef9bbb02abb2de, _Cutout_923fce825c4c4c3b9bef9bbb02abb2de_Output_0);
            float2 _RadialShear_9df83a3a335746848643024aa5b7c9e0_Out_4;
            Unity_RadialShear_float(IN.uv0.xy, float2 (0.5, 0.5), float2 (5, 5), float2 (0, 0), _RadialShear_9df83a3a335746848643024aa5b7c9e0_Out_4);
            float _Voronoi_1a7e03c0bcbd4dc29b26257ce21caa1d_Out_3;
            float _Voronoi_1a7e03c0bcbd4dc29b26257ce21caa1d_Cells_4;
            Unity_Voronoi_float(_RadialShear_9df83a3a335746848643024aa5b7c9e0_Out_4, IN.TimeParameters.x, 8, _Voronoi_1a7e03c0bcbd4dc29b26257ce21caa1d_Out_3, _Voronoi_1a7e03c0bcbd4dc29b26257ce21caa1d_Cells_4);
            float _Property_636c9004ffcb4e39863cd54e7352d8e6_Out_0 = _Float;
            float _Power_fe5432d360554233a7cc6f909009a36a_Out_2;
            Unity_Power_float(_Voronoi_1a7e03c0bcbd4dc29b26257ce21caa1d_Out_3, _Property_636c9004ffcb4e39863cd54e7352d8e6_Out_0, _Power_fe5432d360554233a7cc6f909009a36a_Out_2);
            float _Property_f3b23a2533b640a4851a42b69fe171e4_Out_0 = _FoamDepth;
            float _Property_7c6c40f2b3564d36b4001b8efb2af0ea_Out_0 = _FoamFalloff;
            Bindings_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float _sSWaterDepth_bb1b5652101c44a1919b6541defe8462;
            _sSWaterDepth_bb1b5652101c44a1919b6541defe8462.ScreenPosition = IN.ScreenPosition;
            float _sSWaterDepth_bb1b5652101c44a1919b6541defe8462_OutVector1_1;
            SG_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float(_Property_f3b23a2533b640a4851a42b69fe171e4_Out_0, _Property_7c6c40f2b3564d36b4001b8efb2af0ea_Out_0, _sSWaterDepth_bb1b5652101c44a1919b6541defe8462, _sSWaterDepth_bb1b5652101c44a1919b6541defe8462_OutVector1_1);
            float _Multiply_ccdc528bed5d4c18b6ab85511e54d5a6_Out_2;
            Unity_Multiply_float_float(_Power_fe5432d360554233a7cc6f909009a36a_Out_2, _sSWaterDepth_bb1b5652101c44a1919b6541defe8462_OutVector1_1, _Multiply_ccdc528bed5d4c18b6ab85511e54d5a6_Out_2);
            float _Add_4191fc0bec74415aa999b2a33166d88a_Out_2;
            Unity_Add_float(_Cutout_923fce825c4c4c3b9bef9bbb02abb2de_Output_0, _Multiply_ccdc528bed5d4c18b6ab85511e54d5a6_Out_2, _Add_4191fc0bec74415aa999b2a33166d88a_Out_2);
            float _Saturate_b910c8272d1a4155a7173705fb331898_Out_1;
            Unity_Saturate_float(_Add_4191fc0bec74415aa999b2a33166d88a_Out_2, _Saturate_b910c8272d1a4155a7173705fb331898_Out_1);
            float4 _Multiply_ff39ce7dfb524ce481784d5917bc83a8_Out_2;
            Unity_Multiply_float4_float4(_Property_e7752889f9aa4c45930eda630af8bfa0_Out_0, (_Saturate_b910c8272d1a4155a7173705fb331898_Out_1.xxxx), _Multiply_ff39ce7dfb524ce481784d5917bc83a8_Out_2);
            float _OneMinus_894809b879cc48b7ba2261fac43e00cd_Out_1;
            Unity_OneMinus_float(_Saturate_b910c8272d1a4155a7173705fb331898_Out_1, _OneMinus_894809b879cc48b7ba2261fac43e00cd_Out_1);
            float4 _Property_e0ba34c22e694f58ba492f038826fde6_Out_0 = _ShoreColor;
            float _Property_375ab85e259d4db39b617839ff4c5008_Out_0 = _Depth;
            float _Property_75e9248b4fc9458e99caeb3bf19b1908_Out_0 = _DepthFalloff;
            Bindings_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float _sSWaterDepth_558ab96888514e11b58182e01c6b6fa2;
            _sSWaterDepth_558ab96888514e11b58182e01c6b6fa2.ScreenPosition = IN.ScreenPosition;
            float _sSWaterDepth_558ab96888514e11b58182e01c6b6fa2_OutVector1_1;
            SG_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float(_Property_375ab85e259d4db39b617839ff4c5008_Out_0, _Property_75e9248b4fc9458e99caeb3bf19b1908_Out_0, _sSWaterDepth_558ab96888514e11b58182e01c6b6fa2, _sSWaterDepth_558ab96888514e11b58182e01c6b6fa2_OutVector1_1);
            float4 _Multiply_d0c0d2997ae6412ea37dadf952efd293_Out_2;
            Unity_Multiply_float4_float4(_Property_e0ba34c22e694f58ba492f038826fde6_Out_0, (_sSWaterDepth_558ab96888514e11b58182e01c6b6fa2_OutVector1_1.xxxx), _Multiply_d0c0d2997ae6412ea37dadf952efd293_Out_2);
            float _OneMinus_c0858fd22bc54bd59887f99335d6311f_Out_1;
            Unity_OneMinus_float(_sSWaterDepth_558ab96888514e11b58182e01c6b6fa2_OutVector1_1, _OneMinus_c0858fd22bc54bd59887f99335d6311f_Out_1);
            float4 _Property_6435d5dfbd6e4beebf3719823fe9ba99_Out_0 = _Color;
            float4 _Multiply_edf9a4ef75bc4ac4ad6c88cb52c69d3d_Out_2;
            Unity_Multiply_float4_float4((_OneMinus_c0858fd22bc54bd59887f99335d6311f_Out_1.xxxx), _Property_6435d5dfbd6e4beebf3719823fe9ba99_Out_0, _Multiply_edf9a4ef75bc4ac4ad6c88cb52c69d3d_Out_2);
            float4 _Add_3817e0ebbc6e44d594f3d07bde8e7ce6_Out_2;
            Unity_Add_float4(_Multiply_d0c0d2997ae6412ea37dadf952efd293_Out_2, _Multiply_edf9a4ef75bc4ac4ad6c88cb52c69d3d_Out_2, _Add_3817e0ebbc6e44d594f3d07bde8e7ce6_Out_2);
            float4 _Multiply_85d7a273de444dcb983c32751eeace29_Out_2;
            Unity_Multiply_float4_float4((_OneMinus_894809b879cc48b7ba2261fac43e00cd_Out_1.xxxx), _Add_3817e0ebbc6e44d594f3d07bde8e7ce6_Out_2, _Multiply_85d7a273de444dcb983c32751eeace29_Out_2);
            float4 _Add_7b0f7efb411c4c598bcc28966dd51c15_Out_2;
            Unity_Add_float4(_Multiply_ff39ce7dfb524ce481784d5917bc83a8_Out_2, _Multiply_85d7a273de444dcb983c32751eeace29_Out_2, _Add_7b0f7efb411c4c598bcc28966dd51c15_Out_2);
            float4 _Multiply_e0820bab8d5d461eaa9c157e8bce8884_Out_2;
            Unity_Multiply_float4_float4(_Add_7b0f7efb411c4c598bcc28966dd51c15_Out_2, float4(2, 2, 2, 2), _Multiply_e0820bab8d5d461eaa9c157e8bce8884_Out_2);
            float4 _Add_e74668030b0b498385a1a9468b8dbd24_Out_2;
            Unity_Add_float4(_ScreenPosition_3b330ecd9d44487b9c002f9cc7f91cb6_Out_0, _Multiply_e0820bab8d5d461eaa9c157e8bce8884_Out_2, _Add_e74668030b0b498385a1a9468b8dbd24_Out_2);
            float3 _SceneColor_ef9de574552a4340b580a3d43b107025_Out_1;
            Unity_SceneColor_float(_Add_e74668030b0b498385a1a9468b8dbd24_Out_2, _SceneColor_ef9de574552a4340b580a3d43b107025_Out_1);
            UnityTexture2D _Property_5578bd8013004991ad2902cb855a6d0b_Out_0 = UnityBuildTexture2DStruct(_NormalTexture);
            float2 _RadialShear_bccb9a215bd44eafbe040c49c8000278_Out_4;
            Unity_RadialShear_float(IN.uv0.xy, float2 (0.5, 0.5), float2 (5, 5), float2 (0, 0), _RadialShear_bccb9a215bd44eafbe040c49c8000278_Out_4);
            float _Voronoi_1065fa35ce7b4200924784cef13d4b39_Out_3;
            float _Voronoi_1065fa35ce7b4200924784cef13d4b39_Cells_4;
            Unity_Voronoi_float(_RadialShear_bccb9a215bd44eafbe040c49c8000278_Out_4, IN.TimeParameters.x, 8, _Voronoi_1065fa35ce7b4200924784cef13d4b39_Out_3, _Voronoi_1065fa35ce7b4200924784cef13d4b39_Cells_4);
            float _Property_73ca0a3ef1f542c591eac06bebfdfc62_Out_0 = _NormalStrenght;
            float3 _NormalFromTexture_1fc976b5873840429e4ac72c33aa2d0a_Out_5;
            Unity_NormalFromTexture_float(TEXTURE2D_ARGS(_Property_5578bd8013004991ad2902cb855a6d0b_Out_0.tex, _Property_5578bd8013004991ad2902cb855a6d0b_Out_0.samplerstate), _Property_5578bd8013004991ad2902cb855a6d0b_Out_0.GetTransformedUV(IN.uv1.xy), _Voronoi_1065fa35ce7b4200924784cef13d4b39_Out_3, _Property_73ca0a3ef1f542c591eac06bebfdfc62_Out_0, _NormalFromTexture_1fc976b5873840429e4ac72c33aa2d0a_Out_5);
            float _Multiply_9103c4b0ed7b41dab55d0c48769d043f_Out_2;
            Unity_Multiply_float_float(IN.TimeParameters.x, 0.01, _Multiply_9103c4b0ed7b41dab55d0c48769d043f_Out_2);
            float2 _TilingAndOffset_a8ae8dd4702740cd81c535e72ab6869c_Out_3;
            Unity_TilingAndOffset_float(_RadialShear_9df83a3a335746848643024aa5b7c9e0_Out_4, float2 (0.5, 0.5), (_Multiply_9103c4b0ed7b41dab55d0c48769d043f_Out_2.xx), _TilingAndOffset_a8ae8dd4702740cd81c535e72ab6869c_Out_3);
            float _SimpleNoise_b317b9dbecde4771a32418318aa6bf2e_Out_2;
            Unity_SimpleNoise_float(_TilingAndOffset_a8ae8dd4702740cd81c535e72ab6869c_Out_3, 300, _SimpleNoise_b317b9dbecde4771a32418318aa6bf2e_Out_2);
            float _Power_e0b197c557384350a2d2f0d8f2cce02c_Out_2;
            Unity_Power_float(_SimpleNoise_b317b9dbecde4771a32418318aa6bf2e_Out_2, 10, _Power_e0b197c557384350a2d2f0d8f2cce02c_Out_2);
            float _Add_4f955775a32a4e90962988b710db2973_Out_2;
            Unity_Add_float(_Power_e0b197c557384350a2d2f0d8f2cce02c_Out_2, _Power_e0b197c557384350a2d2f0d8f2cce02c_Out_2, _Add_4f955775a32a4e90962988b710db2973_Out_2);
            float _Add_34f816e43a2146619e527cf77b5a2829_Out_2;
            Unity_Add_float(_Power_fe5432d360554233a7cc6f909009a36a_Out_2, _Add_4f955775a32a4e90962988b710db2973_Out_2, _Add_34f816e43a2146619e527cf77b5a2829_Out_2);
            float4 _Add_45316f2f4573425b8fb3629669a020c7_Out_2;
            Unity_Add_float4((_Add_34f816e43a2146619e527cf77b5a2829_Out_2.xxxx), _Add_3817e0ebbc6e44d594f3d07bde8e7ce6_Out_2, _Add_45316f2f4573425b8fb3629669a020c7_Out_2);
            float4 _Add_bc4e82f988a84312bc336ac7809f548c_Out_2;
            Unity_Add_float4(_Add_7b0f7efb411c4c598bcc28966dd51c15_Out_2, _Add_45316f2f4573425b8fb3629669a020c7_Out_2, _Add_bc4e82f988a84312bc336ac7809f548c_Out_2);
            float _Property_4990610322e74243844d3db91469cce8_Out_0 = _Metal;
            surface.BaseColor = _SceneColor_ef9de574552a4340b580a3d43b107025_Out_1;
            surface.NormalWS = _NormalFromTexture_1fc976b5873840429e4ac72c33aa2d0a_Out_5;
            surface.Emission = (_Add_bc4e82f988a84312bc336ac7809f548c_Out_2.xyz);
            surface.Metallic = _Property_4990610322e74243844d3db91469cce8_Out_0;
            surface.Specular = IsGammaSpace() ? float3(0.5, 0.5, 0.5) : SRGBToLinear(float3(0.5, 0.5, 0.5));
            surface.Smoothness = 1;
            surface.Occlusion = 1;
            surface.Alpha = 1;
            surface.AlphaClipThreshold = 0.5;
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
            output.WorldSpacePosition =                         TransformObjectToWorld(input.positionOS);
            output.TimeParameters =                             _TimeParameters.xyz;
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
            
        
            // must use interpolated tangent, bitangent and normal before they are normalized in the pixel shader.
            float3 unnormalizedNormalWS = input.normalWS;
            const float renormFactor = 1.0 / length(unnormalizedNormalWS);
        
        
            output.WorldSpaceNormal = renormFactor * input.normalWS.xyz;      // we want a unit length Normal Vector node in shader graph
        
        
            output.WorldSpacePosition = input.positionWS;
            output.ScreenPosition = ComputeScreenPos(TransformWorldToHClip(input.positionWS), _ProjectionParams.x);
            output.uv0 = input.texCoord0;
            output.uv1 = input.texCoord1;
            output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        void BuildAppDataFull(Attributes attributes, VertexDescription vertexDescription, inout appdata_full result)
        {
            result.vertex     = float4(attributes.positionOS, 1);
            result.tangent    = attributes.tangentOS;
            result.normal     = attributes.normalOS;
            result.texcoord   = attributes.uv0;
            result.texcoord1  = attributes.uv1;
            result.vertex     = float4(vertexDescription.Position, 1);
            result.normal     = vertexDescription.Normal;
            result.tangent    = float4(vertexDescription.Tangent, 0);
            #if UNITY_ANY_INSTANCING_ENABLED
            #endif
        }
        
        void VaryingsToSurfaceVertex(Varyings varyings, inout v2f_surf result)
        {
            result.pos = varyings.positionCS;
            result.worldPos = varyings.positionWS;
            result.worldNormal = varyings.normalWS;
            result.viewDir = varyings.viewDirectionWS;
            // World Tangent isn't an available input on v2f_surf
        
            result._ShadowCoord = varyings.shadowCoord;
        
            #if UNITY_ANY_INSTANCING_ENABLED
            #endif
            #if !defined(LIGHTMAP_ON)
            #if UNITY_SHOULD_SAMPLE_SH
            result.sh = varyings.sh;
            #endif
            #endif
            #if defined(LIGHTMAP_ON)
            result.lmap.xy = varyings.lightmapUV;
            #endif
            #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
                result.fogCoord = varyings.fogFactorAndVertexLight.x;
                COPY_TO_LIGHT_COORDS(result, varyings.fogFactorAndVertexLight.yzw);
            #endif
        
            DEFAULT_UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(varyings, result);
        }
        
        void SurfaceVertexToVaryings(v2f_surf surfVertex, inout Varyings result)
        {
            result.positionCS = surfVertex.pos;
            result.positionWS = surfVertex.worldPos;
            result.normalWS = surfVertex.worldNormal;
            // viewDirectionWS is never filled out in the legacy pass' function. Always use the value computed by SRP
            // World Tangent isn't an available input on v2f_surf
            result.shadowCoord = surfVertex._ShadowCoord;
        
            #if UNITY_ANY_INSTANCING_ENABLED
            #endif
            #if !defined(LIGHTMAP_ON)
            #if UNITY_SHOULD_SAMPLE_SH
            result.sh = surfVertex.sh;
            #endif
            #endif
            #if defined(LIGHTMAP_ON)
            result.lightmapUV = surfVertex.lmap.xy;
            #endif
            #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
                result.fogFactorAndVertexLight.x = surfVertex.fogCoord;
                COPY_FROM_LIGHT_COORDS(result.fogFactorAndVertexLight.yzw, surfVertex);
            #endif
        
            DEFAULT_UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(surfVertex, result);
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/PBRForwardAddPass.hlsl"
        
        ENDHLSL
        }
        Pass
        {
            Name "BuiltIn Deferred"
            Tags
            {
                "LightMode" = "Deferred"
            }
        
        // Render State
        Cull [_BUILTIN_CullMode]
        Blend [_BUILTIN_SrcBlend] [_BUILTIN_DstBlend]
        ZTest [_BUILTIN_ZTest]
        ZWrite [_BUILTIN_ZWrite]
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 4.5
        #pragma multi_compile_instancing
        #pragma exclude_renderers nomrt
        #pragma multi_compile_prepassfinal
        #pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
        #pragma vertex vert
        #pragma fragment frag
        
        // DotsInstancingOptions: <None>
        // HybridV1InjectedBuiltinProperties: <None>
        
        // Keywords
        #pragma multi_compile _ LIGHTMAP_ON
        #pragma multi_compile _ DIRLIGHTMAP_COMBINED
        #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
        #pragma multi_compile _ _SHADOWS_SOFT
        #pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
        #pragma multi_compile _ _MIXED_LIGHTING_SUBTRACTIVE
        #pragma multi_compile _ _GBUFFER_NORMALS_OCT
        #pragma shader_feature_local_fragment _ _BUILTIN_SURFACE_TYPE_TRANSPARENT
        #pragma shader_feature_local_fragment _ _BUILTIN_ALPHAPREMULTIPLY_ON
        #pragma shader_feature_local_fragment _ _BUILTIN_AlphaClip
        #pragma shader_feature_local_fragment _ _BUILTIN_ALPHATEST_ON
        // GraphKeywords: <None>
        
        // Defines
        #define _NORMALMAP 1
        #define _NORMAL_DROPOFF_WS 1
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define ATTRIBUTES_NEED_TEXCOORD1
        #define VARYINGS_NEED_POSITION_WS
        #define VARYINGS_NEED_NORMAL_WS
        #define VARYINGS_NEED_TANGENT_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define VARYINGS_NEED_TEXCOORD1
        #define VARYINGS_NEED_VIEWDIRECTION_WS
        #define VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_DEFERRED
        #define BUILTIN_TARGET_API 1
        #define REQUIRE_DEPTH_TEXTURE
        #define REQUIRE_OPAQUE_TEXTURE
        /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
        #ifdef _BUILTIN_SURFACE_TYPE_TRANSPARENT
        #define _SURFACE_TYPE_TRANSPARENT _BUILTIN_SURFACE_TYPE_TRANSPARENT
        #endif
        #ifdef _BUILTIN_ALPHATEST_ON
        #define _ALPHATEST_ON _BUILTIN_ALPHATEST_ON
        #endif
        #ifdef _BUILTIN_AlphaClip
        #define _AlphaClip _BUILTIN_AlphaClip
        #endif
        #ifdef _BUILTIN_ALPHAPREMULTIPLY_ON
        #define _ALPHAPREMULTIPLY_ON _BUILTIN_ALPHAPREMULTIPLY_ON
        #endif
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Shim/Shims.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/LegacySurfaceVertex.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/ShaderGraphFunctions.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
             float4 uv1 : TEXCOORD1;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 positionWS;
             float3 normalWS;
             float4 tangentWS;
             float4 texCoord0;
             float4 texCoord1;
             float3 viewDirectionWS;
            #if defined(LIGHTMAP_ON)
             float2 lightmapUV;
            #endif
            #if !defined(LIGHTMAP_ON)
             float3 sh;
            #endif
             float4 fogFactorAndVertexLight;
             float4 shadowCoord;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float3 WorldSpaceNormal;
             float3 WorldSpacePosition;
             float4 ScreenPosition;
             float4 uv0;
             float4 uv1;
             float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
             float3 WorldSpacePosition;
             float3 TimeParameters;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float3 interp0 : INTERP0;
             float3 interp1 : INTERP1;
             float4 interp2 : INTERP2;
             float4 interp3 : INTERP3;
             float4 interp4 : INTERP4;
             float3 interp5 : INTERP5;
             float2 interp6 : INTERP6;
             float3 interp7 : INTERP7;
             float4 interp8 : INTERP8;
             float4 interp9 : INTERP9;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.interp0.xyz =  input.positionWS;
            output.interp1.xyz =  input.normalWS;
            output.interp2.xyzw =  input.tangentWS;
            output.interp3.xyzw =  input.texCoord0;
            output.interp4.xyzw =  input.texCoord1;
            output.interp5.xyz =  input.viewDirectionWS;
            #if defined(LIGHTMAP_ON)
            output.interp6.xy =  input.lightmapUV;
            #endif
            #if !defined(LIGHTMAP_ON)
            output.interp7.xyz =  input.sh;
            #endif
            output.interp8.xyzw =  input.fogFactorAndVertexLight;
            output.interp9.xyzw =  input.shadowCoord;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.positionWS = input.interp0.xyz;
            output.normalWS = input.interp1.xyz;
            output.tangentWS = input.interp2.xyzw;
            output.texCoord0 = input.interp3.xyzw;
            output.texCoord1 = input.interp4.xyzw;
            output.viewDirectionWS = input.interp5.xyz;
            #if defined(LIGHTMAP_ON)
            output.lightmapUV = input.interp6.xy;
            #endif
            #if !defined(LIGHTMAP_ON)
            output.sh = input.interp7.xyz;
            #endif
            output.fogFactorAndVertexLight = input.interp8.xyzw;
            output.shadowCoord = input.interp9.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _Depth;
        float _DepthFalloff;
        float4 _ShoreColor;
        float4 _Color;
        float _FoamShoreWidth;
        float4 _FoamColor;
        float _FoamDepth;
        float _FoamFalloff;
        float _WaveIntensity;
        float _WaveSpeed;
        float _Float;
        float _Metal;
        float4 _NormalTexture_TexelSize;
        float4 _NormalTexture_ST;
        float _NormalStrenght;
        CBUFFER_END
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_NormalTexture);
        SAMPLER(sampler_NormalTexture);
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Includes
        // GraphIncludes: <None>
        
        // Graph Functions
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float(float A, float B, out float Out)
        {
            Out = A + B;
        }
        
        void Unity_Sine_float(float In, out float Out)
        {
            Out = sin(In);
        }
        
        void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A + B;
        }
        
        void Unity_SceneDepth_Eye_float(float4 UV, out float Out)
        {
            if (unity_OrthoParams.w == 1.0)
            {
                Out = LinearEyeDepth(ComputeWorldSpacePosition(UV.xy, SHADERGRAPH_SAMPLE_SCENE_DEPTH(UV.xy), UNITY_MATRIX_I_VP), UNITY_MATRIX_V);
            }
            else
            {
                Out = LinearEyeDepth(SHADERGRAPH_SAMPLE_SCENE_DEPTH(UV.xy), _ZBufferParams);
            }
        }
        
        void Unity_Subtract_float(float A, float B, out float Out)
        {
            Out = A - B;
        }
        
        void Unity_Divide_float(float A, float B, out float Out)
        {
            Out = A / B;
        }
        
        void Unity_OneMinus_float(float In, out float Out)
        {
            Out = 1 - In;
        }
        
        void Unity_Saturate_float(float In, out float Out)
        {
            Out = saturate(In);
        }
        
        void Unity_Power_float(float A, float B, out float Out)
        {
            Out = pow(A, B);
        }
        
        struct Bindings_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float
        {
        float4 ScreenPosition;
        };
        
        void SG_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float(float _Depth, float _DepthFalloff, Bindings_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float IN, out float OutVector1_1)
        {
        float _SceneDepth_4398a40afb5444a98bdb88f05ff37be7_Out_1;
        Unity_SceneDepth_Eye_float(float4(IN.ScreenPosition.xy / IN.ScreenPosition.w, 0, 0), _SceneDepth_4398a40afb5444a98bdb88f05ff37be7_Out_1);
        float4 _ScreenPosition_78861b0c174847048fb96d1b9be441c4_Out_0 = IN.ScreenPosition;
        float _Split_9aecda2f9d2945b9b0a54de06a3a9d48_R_1 = _ScreenPosition_78861b0c174847048fb96d1b9be441c4_Out_0[0];
        float _Split_9aecda2f9d2945b9b0a54de06a3a9d48_G_2 = _ScreenPosition_78861b0c174847048fb96d1b9be441c4_Out_0[1];
        float _Split_9aecda2f9d2945b9b0a54de06a3a9d48_B_3 = _ScreenPosition_78861b0c174847048fb96d1b9be441c4_Out_0[2];
        float _Split_9aecda2f9d2945b9b0a54de06a3a9d48_A_4 = _ScreenPosition_78861b0c174847048fb96d1b9be441c4_Out_0[3];
        float _Subtract_0ef5acf5e9ec4f599d608aed9d014d88_Out_2;
        Unity_Subtract_float(_SceneDepth_4398a40afb5444a98bdb88f05ff37be7_Out_1, _Split_9aecda2f9d2945b9b0a54de06a3a9d48_A_4, _Subtract_0ef5acf5e9ec4f599d608aed9d014d88_Out_2);
        float _Property_5702a0604e9c425f9f28a8e389f7d6e8_Out_0 = _Depth;
        float _Divide_230152a01c7a4ab691e1a20a1fbf597f_Out_2;
        Unity_Divide_float(_Subtract_0ef5acf5e9ec4f599d608aed9d014d88_Out_2, _Property_5702a0604e9c425f9f28a8e389f7d6e8_Out_0, _Divide_230152a01c7a4ab691e1a20a1fbf597f_Out_2);
        float _OneMinus_4edb44dcaf8a4df5974bc2b0bfc1a39d_Out_1;
        Unity_OneMinus_float(_Divide_230152a01c7a4ab691e1a20a1fbf597f_Out_2, _OneMinus_4edb44dcaf8a4df5974bc2b0bfc1a39d_Out_1);
        float _Saturate_c574ae0f849b4335bdfce5762b7b4760_Out_1;
        Unity_Saturate_float(_OneMinus_4edb44dcaf8a4df5974bc2b0bfc1a39d_Out_1, _Saturate_c574ae0f849b4335bdfce5762b7b4760_Out_1);
        float _Property_8b3812fa4cec4943b82bfecf45cd931a_Out_0 = _DepthFalloff;
        float _Power_494a422a08904cbbb881363a1d49b985_Out_2;
        Unity_Power_float(_Saturate_c574ae0f849b4335bdfce5762b7b4760_Out_1, _Property_8b3812fa4cec4943b82bfecf45cd931a_Out_0, _Power_494a422a08904cbbb881363a1d49b985_Out_2);
        OutVector1_1 = _Power_494a422a08904cbbb881363a1d49b985_Out_2;
        }
        
        void Unity_Ceiling_float(float In, out float Out)
        {
            Out = ceil(In);
        }
        
        struct Bindings_Cutout_719ac7b9c41a99e4fa65ee4e7f3e2847_float
        {
        };
        
        void SG_Cutout_719ac7b9c41a99e4fa65ee4e7f3e2847_float(float _Alpha, float _Input, Bindings_Cutout_719ac7b9c41a99e4fa65ee4e7f3e2847_float IN, out float Output_0)
        {
        float _Property_c3987da974dc4f0485a61aed8135469c_Out_0 = _Input;
        float _Property_1427757994b04881a478a38d221123de_Out_0 = _Alpha;
        float _Saturate_35fc4a94f39247569cd864728c7400af_Out_1;
        Unity_Saturate_float(_Property_1427757994b04881a478a38d221123de_Out_0, _Saturate_35fc4a94f39247569cd864728c7400af_Out_1);
        float _Subtract_7135ced8bffe4d64949f974ca6083fdf_Out_2;
        Unity_Subtract_float(_Property_c3987da974dc4f0485a61aed8135469c_Out_0, _Saturate_35fc4a94f39247569cd864728c7400af_Out_1, _Subtract_7135ced8bffe4d64949f974ca6083fdf_Out_2);
        float _Ceiling_46ae28079db64bdc9dfb591ddb2c6194_Out_1;
        Unity_Ceiling_float(_Subtract_7135ced8bffe4d64949f974ca6083fdf_Out_2, _Ceiling_46ae28079db64bdc9dfb591ddb2c6194_Out_1);
        Output_0 = _Ceiling_46ae28079db64bdc9dfb591ddb2c6194_Out_1;
        }
        
        void Unity_RadialShear_float(float2 UV, float2 Center, float2 Strength, float2 Offset, out float2 Out)
        {
            float2 delta = UV - Center;
            float delta2 = dot(delta.xy, delta.xy);
            float2 delta_offset = delta2 * Strength;
            Out = UV + float2(delta.y, -delta.x) * delta_offset + Offset;
        }
        
        
        inline float2 Unity_Voronoi_RandomVector_float (float2 UV, float offset)
        {
            float2x2 m = float2x2(15.27, 47.63, 99.41, 89.98);
            UV = frac(sin(mul(UV, m)));
            return float2(sin(UV.y*+offset)*0.5+0.5, cos(UV.x*offset)*0.5+0.5);
        }
        
        void Unity_Voronoi_float(float2 UV, float AngleOffset, float CellDensity, out float Out, out float Cells)
        {
            float2 g = floor(UV * CellDensity);
            float2 f = frac(UV * CellDensity);
            float t = 8.0;
            float3 res = float3(8.0, 0.0, 0.0);
        
            for(int y=-1; y<=1; y++)
            {
                for(int x=-1; x<=1; x++)
                {
                    float2 lattice = float2(x,y);
                    float2 offset = Unity_Voronoi_RandomVector_float(lattice + g, AngleOffset);
                    float d = distance(lattice + offset, f);
        
                    if(d < res.x)
                    {
                        res = float3(d, offset.x, offset.y);
                        Out = res.x;
                        Cells = res.y;
                    }
                }
            }
        }
        
        void Unity_Multiply_float4_float4(float4 A, float4 B, out float4 Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float4(float4 A, float4 B, out float4 Out)
        {
            Out = A + B;
        }
        
        void Unity_SceneColor_float(float4 UV, out float3 Out)
        {
            Out = SHADERGRAPH_SAMPLE_SCENE_COLOR(UV.xy);
        }
        
        void Unity_NormalFromTexture_float(TEXTURE2D_PARAM(Texture, Sampler), float2 UV, float Offset, float Strength, out float3 Out)
        {
            Offset = pow(Offset, 3) * 0.1;
            float2 offsetU = float2(UV.x + Offset, UV.y);
            float2 offsetV = float2(UV.x, UV.y + Offset);
            float normalSample = SAMPLE_TEXTURE2D(Texture, Sampler, UV);
            float uSample = SAMPLE_TEXTURE2D(Texture, Sampler, offsetU);
            float vSample = SAMPLE_TEXTURE2D(Texture, Sampler, offsetV);
            float3 va = float3(1, 0, (uSample - normalSample) * Strength);
            float3 vb = float3(0, 1, (vSample - normalSample) * Strength);
            Out = normalize(cross(va, vb));
        }
        
        void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
        {
            Out = UV * Tiling + Offset;
        }
        
        
        inline float Unity_SimpleNoise_RandomValue_float (float2 uv)
        {
            float angle = dot(uv, float2(12.9898, 78.233));
            #if defined(SHADER_API_MOBILE) && (defined(SHADER_API_GLES) || defined(SHADER_API_GLES3) || defined(SHADER_API_VULKAN))
                // 'sin()' has bad precision on Mali GPUs for inputs > 10000
                angle = fmod(angle, TWO_PI); // Avoid large inputs to sin()
            #endif
            return frac(sin(angle)*43758.5453);
        }
        
        inline float Unity_SimpleNnoise_Interpolate_float (float a, float b, float t)
        {
            return (1.0-t)*a + (t*b);
        }
        
        
        inline float Unity_SimpleNoise_ValueNoise_float (float2 uv)
        {
            float2 i = floor(uv);
            float2 f = frac(uv);
            f = f * f * (3.0 - 2.0 * f);
        
            uv = abs(frac(uv) - 0.5);
            float2 c0 = i + float2(0.0, 0.0);
            float2 c1 = i + float2(1.0, 0.0);
            float2 c2 = i + float2(0.0, 1.0);
            float2 c3 = i + float2(1.0, 1.0);
            float r0 = Unity_SimpleNoise_RandomValue_float(c0);
            float r1 = Unity_SimpleNoise_RandomValue_float(c1);
            float r2 = Unity_SimpleNoise_RandomValue_float(c2);
            float r3 = Unity_SimpleNoise_RandomValue_float(c3);
        
            float bottomOfGrid = Unity_SimpleNnoise_Interpolate_float(r0, r1, f.x);
            float topOfGrid = Unity_SimpleNnoise_Interpolate_float(r2, r3, f.x);
            float t = Unity_SimpleNnoise_Interpolate_float(bottomOfGrid, topOfGrid, f.y);
            return t;
        }
        void Unity_SimpleNoise_float(float2 UV, float Scale, out float Out)
        {
            float t = 0.0;
        
            float freq = pow(2.0, float(0));
            float amp = pow(0.5, float(3-0));
            t += Unity_SimpleNoise_ValueNoise_float(float2(UV.x*Scale/freq, UV.y*Scale/freq))*amp;
        
            freq = pow(2.0, float(1));
            amp = pow(0.5, float(3-1));
            t += Unity_SimpleNoise_ValueNoise_float(float2(UV.x*Scale/freq, UV.y*Scale/freq))*amp;
        
            freq = pow(2.0, float(2));
            amp = pow(0.5, float(3-2));
            t += Unity_SimpleNoise_ValueNoise_float(float2(UV.x*Scale/freq, UV.y*Scale/freq))*amp;
        
            Out = t;
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            float _Property_3e04952c468843ab8933b2692cf6bacd_Out_0 = _WaveIntensity;
            float3 _Vector3_a18246eb0d944cbe92ba8ab4df244f74_Out_0 = float3(0, _Property_3e04952c468843ab8933b2692cf6bacd_Out_0, 0);
            float _Property_e189c9961bde4d4a80a0c20b6a92503b_Out_0 = _WaveSpeed;
            float _Multiply_c13c13771f8e4aa3a00a44ac4ff11002_Out_2;
            Unity_Multiply_float_float(_Property_e189c9961bde4d4a80a0c20b6a92503b_Out_0, IN.TimeParameters.x, _Multiply_c13c13771f8e4aa3a00a44ac4ff11002_Out_2);
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_R_1 = IN.WorldSpacePosition[0];
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_G_2 = IN.WorldSpacePosition[1];
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_B_3 = IN.WorldSpacePosition[2];
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_A_4 = 0;
            float _Add_f6078872ccf44f37bfbcc10e7e23224f_Out_2;
            Unity_Add_float(_Split_9172b35d396b4f6da3213c0bcd4ecb96_R_1, _Split_9172b35d396b4f6da3213c0bcd4ecb96_B_3, _Add_f6078872ccf44f37bfbcc10e7e23224f_Out_2);
            float _Add_6e66df41b5444def8731ecb95ab6afe3_Out_2;
            Unity_Add_float(_Multiply_c13c13771f8e4aa3a00a44ac4ff11002_Out_2, _Add_f6078872ccf44f37bfbcc10e7e23224f_Out_2, _Add_6e66df41b5444def8731ecb95ab6afe3_Out_2);
            float _Sine_1dfa4e4673a14844bdcb53c2ff481fe7_Out_1;
            Unity_Sine_float(_Add_6e66df41b5444def8731ecb95ab6afe3_Out_2, _Sine_1dfa4e4673a14844bdcb53c2ff481fe7_Out_1);
            float3 _Multiply_1fd709eee6bd43a5adf15627c9e1556b_Out_2;
            Unity_Multiply_float3_float3(_Vector3_a18246eb0d944cbe92ba8ab4df244f74_Out_0, (_Sine_1dfa4e4673a14844bdcb53c2ff481fe7_Out_1.xxx), _Multiply_1fd709eee6bd43a5adf15627c9e1556b_Out_2);
            float3 _Add_c55b332417574d1495a47eee31203ddc_Out_2;
            Unity_Add_float3(IN.ObjectSpacePosition, _Multiply_1fd709eee6bd43a5adf15627c9e1556b_Out_2, _Add_c55b332417574d1495a47eee31203ddc_Out_2);
            description.Position = _Add_c55b332417574d1495a47eee31203ddc_Out_2;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float3 BaseColor;
            float3 NormalWS;
            float3 Emission;
            float Metallic;
            float3 Specular;
            float Smoothness;
            float Occlusion;
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float4 _ScreenPosition_3b330ecd9d44487b9c002f9cc7f91cb6_Out_0 = float4(IN.ScreenPosition.xy / IN.ScreenPosition.w, 0, 0);
            float4 _Property_e7752889f9aa4c45930eda630af8bfa0_Out_0 = _FoamColor;
            float _Property_26d00366155a4444be65770ec1a4521d_Out_0 = _FoamShoreWidth;
            Bindings_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float _sSWaterDepth_e1942db5fec045e6b9a624763ed0ac66;
            _sSWaterDepth_e1942db5fec045e6b9a624763ed0ac66.ScreenPosition = IN.ScreenPosition;
            float _sSWaterDepth_e1942db5fec045e6b9a624763ed0ac66_OutVector1_1;
            SG_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float(1, 1, _sSWaterDepth_e1942db5fec045e6b9a624763ed0ac66, _sSWaterDepth_e1942db5fec045e6b9a624763ed0ac66_OutVector1_1);
            Bindings_Cutout_719ac7b9c41a99e4fa65ee4e7f3e2847_float _Cutout_923fce825c4c4c3b9bef9bbb02abb2de;
            float _Cutout_923fce825c4c4c3b9bef9bbb02abb2de_Output_0;
            SG_Cutout_719ac7b9c41a99e4fa65ee4e7f3e2847_float(_Property_26d00366155a4444be65770ec1a4521d_Out_0, _sSWaterDepth_e1942db5fec045e6b9a624763ed0ac66_OutVector1_1, _Cutout_923fce825c4c4c3b9bef9bbb02abb2de, _Cutout_923fce825c4c4c3b9bef9bbb02abb2de_Output_0);
            float2 _RadialShear_9df83a3a335746848643024aa5b7c9e0_Out_4;
            Unity_RadialShear_float(IN.uv0.xy, float2 (0.5, 0.5), float2 (5, 5), float2 (0, 0), _RadialShear_9df83a3a335746848643024aa5b7c9e0_Out_4);
            float _Voronoi_1a7e03c0bcbd4dc29b26257ce21caa1d_Out_3;
            float _Voronoi_1a7e03c0bcbd4dc29b26257ce21caa1d_Cells_4;
            Unity_Voronoi_float(_RadialShear_9df83a3a335746848643024aa5b7c9e0_Out_4, IN.TimeParameters.x, 8, _Voronoi_1a7e03c0bcbd4dc29b26257ce21caa1d_Out_3, _Voronoi_1a7e03c0bcbd4dc29b26257ce21caa1d_Cells_4);
            float _Property_636c9004ffcb4e39863cd54e7352d8e6_Out_0 = _Float;
            float _Power_fe5432d360554233a7cc6f909009a36a_Out_2;
            Unity_Power_float(_Voronoi_1a7e03c0bcbd4dc29b26257ce21caa1d_Out_3, _Property_636c9004ffcb4e39863cd54e7352d8e6_Out_0, _Power_fe5432d360554233a7cc6f909009a36a_Out_2);
            float _Property_f3b23a2533b640a4851a42b69fe171e4_Out_0 = _FoamDepth;
            float _Property_7c6c40f2b3564d36b4001b8efb2af0ea_Out_0 = _FoamFalloff;
            Bindings_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float _sSWaterDepth_bb1b5652101c44a1919b6541defe8462;
            _sSWaterDepth_bb1b5652101c44a1919b6541defe8462.ScreenPosition = IN.ScreenPosition;
            float _sSWaterDepth_bb1b5652101c44a1919b6541defe8462_OutVector1_1;
            SG_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float(_Property_f3b23a2533b640a4851a42b69fe171e4_Out_0, _Property_7c6c40f2b3564d36b4001b8efb2af0ea_Out_0, _sSWaterDepth_bb1b5652101c44a1919b6541defe8462, _sSWaterDepth_bb1b5652101c44a1919b6541defe8462_OutVector1_1);
            float _Multiply_ccdc528bed5d4c18b6ab85511e54d5a6_Out_2;
            Unity_Multiply_float_float(_Power_fe5432d360554233a7cc6f909009a36a_Out_2, _sSWaterDepth_bb1b5652101c44a1919b6541defe8462_OutVector1_1, _Multiply_ccdc528bed5d4c18b6ab85511e54d5a6_Out_2);
            float _Add_4191fc0bec74415aa999b2a33166d88a_Out_2;
            Unity_Add_float(_Cutout_923fce825c4c4c3b9bef9bbb02abb2de_Output_0, _Multiply_ccdc528bed5d4c18b6ab85511e54d5a6_Out_2, _Add_4191fc0bec74415aa999b2a33166d88a_Out_2);
            float _Saturate_b910c8272d1a4155a7173705fb331898_Out_1;
            Unity_Saturate_float(_Add_4191fc0bec74415aa999b2a33166d88a_Out_2, _Saturate_b910c8272d1a4155a7173705fb331898_Out_1);
            float4 _Multiply_ff39ce7dfb524ce481784d5917bc83a8_Out_2;
            Unity_Multiply_float4_float4(_Property_e7752889f9aa4c45930eda630af8bfa0_Out_0, (_Saturate_b910c8272d1a4155a7173705fb331898_Out_1.xxxx), _Multiply_ff39ce7dfb524ce481784d5917bc83a8_Out_2);
            float _OneMinus_894809b879cc48b7ba2261fac43e00cd_Out_1;
            Unity_OneMinus_float(_Saturate_b910c8272d1a4155a7173705fb331898_Out_1, _OneMinus_894809b879cc48b7ba2261fac43e00cd_Out_1);
            float4 _Property_e0ba34c22e694f58ba492f038826fde6_Out_0 = _ShoreColor;
            float _Property_375ab85e259d4db39b617839ff4c5008_Out_0 = _Depth;
            float _Property_75e9248b4fc9458e99caeb3bf19b1908_Out_0 = _DepthFalloff;
            Bindings_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float _sSWaterDepth_558ab96888514e11b58182e01c6b6fa2;
            _sSWaterDepth_558ab96888514e11b58182e01c6b6fa2.ScreenPosition = IN.ScreenPosition;
            float _sSWaterDepth_558ab96888514e11b58182e01c6b6fa2_OutVector1_1;
            SG_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float(_Property_375ab85e259d4db39b617839ff4c5008_Out_0, _Property_75e9248b4fc9458e99caeb3bf19b1908_Out_0, _sSWaterDepth_558ab96888514e11b58182e01c6b6fa2, _sSWaterDepth_558ab96888514e11b58182e01c6b6fa2_OutVector1_1);
            float4 _Multiply_d0c0d2997ae6412ea37dadf952efd293_Out_2;
            Unity_Multiply_float4_float4(_Property_e0ba34c22e694f58ba492f038826fde6_Out_0, (_sSWaterDepth_558ab96888514e11b58182e01c6b6fa2_OutVector1_1.xxxx), _Multiply_d0c0d2997ae6412ea37dadf952efd293_Out_2);
            float _OneMinus_c0858fd22bc54bd59887f99335d6311f_Out_1;
            Unity_OneMinus_float(_sSWaterDepth_558ab96888514e11b58182e01c6b6fa2_OutVector1_1, _OneMinus_c0858fd22bc54bd59887f99335d6311f_Out_1);
            float4 _Property_6435d5dfbd6e4beebf3719823fe9ba99_Out_0 = _Color;
            float4 _Multiply_edf9a4ef75bc4ac4ad6c88cb52c69d3d_Out_2;
            Unity_Multiply_float4_float4((_OneMinus_c0858fd22bc54bd59887f99335d6311f_Out_1.xxxx), _Property_6435d5dfbd6e4beebf3719823fe9ba99_Out_0, _Multiply_edf9a4ef75bc4ac4ad6c88cb52c69d3d_Out_2);
            float4 _Add_3817e0ebbc6e44d594f3d07bde8e7ce6_Out_2;
            Unity_Add_float4(_Multiply_d0c0d2997ae6412ea37dadf952efd293_Out_2, _Multiply_edf9a4ef75bc4ac4ad6c88cb52c69d3d_Out_2, _Add_3817e0ebbc6e44d594f3d07bde8e7ce6_Out_2);
            float4 _Multiply_85d7a273de444dcb983c32751eeace29_Out_2;
            Unity_Multiply_float4_float4((_OneMinus_894809b879cc48b7ba2261fac43e00cd_Out_1.xxxx), _Add_3817e0ebbc6e44d594f3d07bde8e7ce6_Out_2, _Multiply_85d7a273de444dcb983c32751eeace29_Out_2);
            float4 _Add_7b0f7efb411c4c598bcc28966dd51c15_Out_2;
            Unity_Add_float4(_Multiply_ff39ce7dfb524ce481784d5917bc83a8_Out_2, _Multiply_85d7a273de444dcb983c32751eeace29_Out_2, _Add_7b0f7efb411c4c598bcc28966dd51c15_Out_2);
            float4 _Multiply_e0820bab8d5d461eaa9c157e8bce8884_Out_2;
            Unity_Multiply_float4_float4(_Add_7b0f7efb411c4c598bcc28966dd51c15_Out_2, float4(2, 2, 2, 2), _Multiply_e0820bab8d5d461eaa9c157e8bce8884_Out_2);
            float4 _Add_e74668030b0b498385a1a9468b8dbd24_Out_2;
            Unity_Add_float4(_ScreenPosition_3b330ecd9d44487b9c002f9cc7f91cb6_Out_0, _Multiply_e0820bab8d5d461eaa9c157e8bce8884_Out_2, _Add_e74668030b0b498385a1a9468b8dbd24_Out_2);
            float3 _SceneColor_ef9de574552a4340b580a3d43b107025_Out_1;
            Unity_SceneColor_float(_Add_e74668030b0b498385a1a9468b8dbd24_Out_2, _SceneColor_ef9de574552a4340b580a3d43b107025_Out_1);
            UnityTexture2D _Property_5578bd8013004991ad2902cb855a6d0b_Out_0 = UnityBuildTexture2DStruct(_NormalTexture);
            float2 _RadialShear_bccb9a215bd44eafbe040c49c8000278_Out_4;
            Unity_RadialShear_float(IN.uv0.xy, float2 (0.5, 0.5), float2 (5, 5), float2 (0, 0), _RadialShear_bccb9a215bd44eafbe040c49c8000278_Out_4);
            float _Voronoi_1065fa35ce7b4200924784cef13d4b39_Out_3;
            float _Voronoi_1065fa35ce7b4200924784cef13d4b39_Cells_4;
            Unity_Voronoi_float(_RadialShear_bccb9a215bd44eafbe040c49c8000278_Out_4, IN.TimeParameters.x, 8, _Voronoi_1065fa35ce7b4200924784cef13d4b39_Out_3, _Voronoi_1065fa35ce7b4200924784cef13d4b39_Cells_4);
            float _Property_73ca0a3ef1f542c591eac06bebfdfc62_Out_0 = _NormalStrenght;
            float3 _NormalFromTexture_1fc976b5873840429e4ac72c33aa2d0a_Out_5;
            Unity_NormalFromTexture_float(TEXTURE2D_ARGS(_Property_5578bd8013004991ad2902cb855a6d0b_Out_0.tex, _Property_5578bd8013004991ad2902cb855a6d0b_Out_0.samplerstate), _Property_5578bd8013004991ad2902cb855a6d0b_Out_0.GetTransformedUV(IN.uv1.xy), _Voronoi_1065fa35ce7b4200924784cef13d4b39_Out_3, _Property_73ca0a3ef1f542c591eac06bebfdfc62_Out_0, _NormalFromTexture_1fc976b5873840429e4ac72c33aa2d0a_Out_5);
            float _Multiply_9103c4b0ed7b41dab55d0c48769d043f_Out_2;
            Unity_Multiply_float_float(IN.TimeParameters.x, 0.01, _Multiply_9103c4b0ed7b41dab55d0c48769d043f_Out_2);
            float2 _TilingAndOffset_a8ae8dd4702740cd81c535e72ab6869c_Out_3;
            Unity_TilingAndOffset_float(_RadialShear_9df83a3a335746848643024aa5b7c9e0_Out_4, float2 (0.5, 0.5), (_Multiply_9103c4b0ed7b41dab55d0c48769d043f_Out_2.xx), _TilingAndOffset_a8ae8dd4702740cd81c535e72ab6869c_Out_3);
            float _SimpleNoise_b317b9dbecde4771a32418318aa6bf2e_Out_2;
            Unity_SimpleNoise_float(_TilingAndOffset_a8ae8dd4702740cd81c535e72ab6869c_Out_3, 300, _SimpleNoise_b317b9dbecde4771a32418318aa6bf2e_Out_2);
            float _Power_e0b197c557384350a2d2f0d8f2cce02c_Out_2;
            Unity_Power_float(_SimpleNoise_b317b9dbecde4771a32418318aa6bf2e_Out_2, 10, _Power_e0b197c557384350a2d2f0d8f2cce02c_Out_2);
            float _Add_4f955775a32a4e90962988b710db2973_Out_2;
            Unity_Add_float(_Power_e0b197c557384350a2d2f0d8f2cce02c_Out_2, _Power_e0b197c557384350a2d2f0d8f2cce02c_Out_2, _Add_4f955775a32a4e90962988b710db2973_Out_2);
            float _Add_34f816e43a2146619e527cf77b5a2829_Out_2;
            Unity_Add_float(_Power_fe5432d360554233a7cc6f909009a36a_Out_2, _Add_4f955775a32a4e90962988b710db2973_Out_2, _Add_34f816e43a2146619e527cf77b5a2829_Out_2);
            float4 _Add_45316f2f4573425b8fb3629669a020c7_Out_2;
            Unity_Add_float4((_Add_34f816e43a2146619e527cf77b5a2829_Out_2.xxxx), _Add_3817e0ebbc6e44d594f3d07bde8e7ce6_Out_2, _Add_45316f2f4573425b8fb3629669a020c7_Out_2);
            float4 _Add_bc4e82f988a84312bc336ac7809f548c_Out_2;
            Unity_Add_float4(_Add_7b0f7efb411c4c598bcc28966dd51c15_Out_2, _Add_45316f2f4573425b8fb3629669a020c7_Out_2, _Add_bc4e82f988a84312bc336ac7809f548c_Out_2);
            float _Property_4990610322e74243844d3db91469cce8_Out_0 = _Metal;
            surface.BaseColor = _SceneColor_ef9de574552a4340b580a3d43b107025_Out_1;
            surface.NormalWS = _NormalFromTexture_1fc976b5873840429e4ac72c33aa2d0a_Out_5;
            surface.Emission = (_Add_bc4e82f988a84312bc336ac7809f548c_Out_2.xyz);
            surface.Metallic = _Property_4990610322e74243844d3db91469cce8_Out_0;
            surface.Specular = IsGammaSpace() ? float3(0.5, 0.5, 0.5) : SRGBToLinear(float3(0.5, 0.5, 0.5));
            surface.Smoothness = 1;
            surface.Occlusion = 1;
            surface.Alpha = 1;
            surface.AlphaClipThreshold = 0.5;
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
            output.WorldSpacePosition =                         TransformObjectToWorld(input.positionOS);
            output.TimeParameters =                             _TimeParameters.xyz;
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
            
        
            // must use interpolated tangent, bitangent and normal before they are normalized in the pixel shader.
            float3 unnormalizedNormalWS = input.normalWS;
            const float renormFactor = 1.0 / length(unnormalizedNormalWS);
        
        
            output.WorldSpaceNormal = renormFactor * input.normalWS.xyz;      // we want a unit length Normal Vector node in shader graph
        
        
            output.WorldSpacePosition = input.positionWS;
            output.ScreenPosition = ComputeScreenPos(TransformWorldToHClip(input.positionWS), _ProjectionParams.x);
            output.uv0 = input.texCoord0;
            output.uv1 = input.texCoord1;
            output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        void BuildAppDataFull(Attributes attributes, VertexDescription vertexDescription, inout appdata_full result)
        {
            result.vertex     = float4(attributes.positionOS, 1);
            result.tangent    = attributes.tangentOS;
            result.normal     = attributes.normalOS;
            result.texcoord   = attributes.uv0;
            result.texcoord1  = attributes.uv1;
            result.vertex     = float4(vertexDescription.Position, 1);
            result.normal     = vertexDescription.Normal;
            result.tangent    = float4(vertexDescription.Tangent, 0);
            #if UNITY_ANY_INSTANCING_ENABLED
            #endif
        }
        
        void VaryingsToSurfaceVertex(Varyings varyings, inout v2f_surf result)
        {
            result.pos = varyings.positionCS;
            result.worldPos = varyings.positionWS;
            result.worldNormal = varyings.normalWS;
            result.viewDir = varyings.viewDirectionWS;
            // World Tangent isn't an available input on v2f_surf
        
            result._ShadowCoord = varyings.shadowCoord;
        
            #if UNITY_ANY_INSTANCING_ENABLED
            #endif
            #if !defined(LIGHTMAP_ON)
            #if UNITY_SHOULD_SAMPLE_SH
            result.sh = varyings.sh;
            #endif
            #endif
            #if defined(LIGHTMAP_ON)
            result.lmap.xy = varyings.lightmapUV;
            #endif
            #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
                result.fogCoord = varyings.fogFactorAndVertexLight.x;
                COPY_TO_LIGHT_COORDS(result, varyings.fogFactorAndVertexLight.yzw);
            #endif
        
            DEFAULT_UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(varyings, result);
        }
        
        void SurfaceVertexToVaryings(v2f_surf surfVertex, inout Varyings result)
        {
            result.positionCS = surfVertex.pos;
            result.positionWS = surfVertex.worldPos;
            result.normalWS = surfVertex.worldNormal;
            // viewDirectionWS is never filled out in the legacy pass' function. Always use the value computed by SRP
            // World Tangent isn't an available input on v2f_surf
            result.shadowCoord = surfVertex._ShadowCoord;
        
            #if UNITY_ANY_INSTANCING_ENABLED
            #endif
            #if !defined(LIGHTMAP_ON)
            #if UNITY_SHOULD_SAMPLE_SH
            result.sh = surfVertex.sh;
            #endif
            #endif
            #if defined(LIGHTMAP_ON)
            result.lightmapUV = surfVertex.lmap.xy;
            #endif
            #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
                result.fogFactorAndVertexLight.x = surfVertex.fogCoord;
                COPY_FROM_LIGHT_COORDS(result.fogFactorAndVertexLight.yzw, surfVertex);
            #endif
        
            DEFAULT_UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(surfVertex, result);
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/PBRDeferredPass.hlsl"
        
        ENDHLSL
        }
        Pass
        {
            Name "ShadowCaster"
            Tags
            {
                "LightMode" = "ShadowCaster"
            }
        
        // Render State
        Cull [_BUILTIN_CullMode]
        Blend [_BUILTIN_SrcBlend] [_BUILTIN_DstBlend]
        ZTest LEqual
        ZWrite On
        ColorMask 0
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 3.0
        #pragma multi_compile_shadowcaster
        #pragma vertex vert
        #pragma fragment frag
        
        // DotsInstancingOptions: <None>
        // HybridV1InjectedBuiltinProperties: <None>
        
        // Keywords
        #pragma multi_compile _ _CASTING_PUNCTUAL_LIGHT_SHADOW
        #pragma shader_feature_local_fragment _ _BUILTIN_SURFACE_TYPE_TRANSPARENT
        #pragma shader_feature_local_fragment _ _BUILTIN_AlphaClip
        #pragma shader_feature_local_fragment _ _BUILTIN_ALPHATEST_ON
        // GraphKeywords: <None>
        
        // Defines
        #define _NORMALMAP 1
        #define _NORMAL_DROPOFF_WS 1
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_SHADOWCASTER
        #define BUILTIN_TARGET_API 1
        /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
        #ifdef _BUILTIN_SURFACE_TYPE_TRANSPARENT
        #define _SURFACE_TYPE_TRANSPARENT _BUILTIN_SURFACE_TYPE_TRANSPARENT
        #endif
        #ifdef _BUILTIN_ALPHATEST_ON
        #define _ALPHATEST_ON _BUILTIN_ALPHATEST_ON
        #endif
        #ifdef _BUILTIN_AlphaClip
        #define _AlphaClip _BUILTIN_AlphaClip
        #endif
        #ifdef _BUILTIN_ALPHAPREMULTIPLY_ON
        #define _ALPHAPREMULTIPLY_ON _BUILTIN_ALPHAPREMULTIPLY_ON
        #endif
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Shim/Shims.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/LegacySurfaceVertex.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/ShaderGraphFunctions.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
             float3 WorldSpacePosition;
             float3 TimeParameters;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _Depth;
        float _DepthFalloff;
        float4 _ShoreColor;
        float4 _Color;
        float _FoamShoreWidth;
        float4 _FoamColor;
        float _FoamDepth;
        float _FoamFalloff;
        float _WaveIntensity;
        float _WaveSpeed;
        float _Float;
        float _Metal;
        float4 _NormalTexture_TexelSize;
        float4 _NormalTexture_ST;
        float _NormalStrenght;
        CBUFFER_END
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_NormalTexture);
        SAMPLER(sampler_NormalTexture);
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Includes
        // GraphIncludes: <None>
        
        // Graph Functions
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float(float A, float B, out float Out)
        {
            Out = A + B;
        }
        
        void Unity_Sine_float(float In, out float Out)
        {
            Out = sin(In);
        }
        
        void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A + B;
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            float _Property_3e04952c468843ab8933b2692cf6bacd_Out_0 = _WaveIntensity;
            float3 _Vector3_a18246eb0d944cbe92ba8ab4df244f74_Out_0 = float3(0, _Property_3e04952c468843ab8933b2692cf6bacd_Out_0, 0);
            float _Property_e189c9961bde4d4a80a0c20b6a92503b_Out_0 = _WaveSpeed;
            float _Multiply_c13c13771f8e4aa3a00a44ac4ff11002_Out_2;
            Unity_Multiply_float_float(_Property_e189c9961bde4d4a80a0c20b6a92503b_Out_0, IN.TimeParameters.x, _Multiply_c13c13771f8e4aa3a00a44ac4ff11002_Out_2);
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_R_1 = IN.WorldSpacePosition[0];
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_G_2 = IN.WorldSpacePosition[1];
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_B_3 = IN.WorldSpacePosition[2];
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_A_4 = 0;
            float _Add_f6078872ccf44f37bfbcc10e7e23224f_Out_2;
            Unity_Add_float(_Split_9172b35d396b4f6da3213c0bcd4ecb96_R_1, _Split_9172b35d396b4f6da3213c0bcd4ecb96_B_3, _Add_f6078872ccf44f37bfbcc10e7e23224f_Out_2);
            float _Add_6e66df41b5444def8731ecb95ab6afe3_Out_2;
            Unity_Add_float(_Multiply_c13c13771f8e4aa3a00a44ac4ff11002_Out_2, _Add_f6078872ccf44f37bfbcc10e7e23224f_Out_2, _Add_6e66df41b5444def8731ecb95ab6afe3_Out_2);
            float _Sine_1dfa4e4673a14844bdcb53c2ff481fe7_Out_1;
            Unity_Sine_float(_Add_6e66df41b5444def8731ecb95ab6afe3_Out_2, _Sine_1dfa4e4673a14844bdcb53c2ff481fe7_Out_1);
            float3 _Multiply_1fd709eee6bd43a5adf15627c9e1556b_Out_2;
            Unity_Multiply_float3_float3(_Vector3_a18246eb0d944cbe92ba8ab4df244f74_Out_0, (_Sine_1dfa4e4673a14844bdcb53c2ff481fe7_Out_1.xxx), _Multiply_1fd709eee6bd43a5adf15627c9e1556b_Out_2);
            float3 _Add_c55b332417574d1495a47eee31203ddc_Out_2;
            Unity_Add_float3(IN.ObjectSpacePosition, _Multiply_1fd709eee6bd43a5adf15627c9e1556b_Out_2, _Add_c55b332417574d1495a47eee31203ddc_Out_2);
            description.Position = _Add_c55b332417574d1495a47eee31203ddc_Out_2;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            surface.Alpha = 1;
            surface.AlphaClipThreshold = 0.5;
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
            output.WorldSpacePosition =                         TransformObjectToWorld(input.positionOS);
            output.TimeParameters =                             _TimeParameters.xyz;
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
            
        
        
        
        
        
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        void BuildAppDataFull(Attributes attributes, VertexDescription vertexDescription, inout appdata_full result)
        {
            result.vertex     = float4(attributes.positionOS, 1);
            result.tangent    = attributes.tangentOS;
            result.normal     = attributes.normalOS;
            result.vertex     = float4(vertexDescription.Position, 1);
            result.normal     = vertexDescription.Normal;
            result.tangent    = float4(vertexDescription.Tangent, 0);
            #if UNITY_ANY_INSTANCING_ENABLED
            #endif
        }
        
        void VaryingsToSurfaceVertex(Varyings varyings, inout v2f_surf result)
        {
            result.pos = varyings.positionCS;
            // World Tangent isn't an available input on v2f_surf
        
        
            #if UNITY_ANY_INSTANCING_ENABLED
            #endif
            #if !defined(LIGHTMAP_ON)
            #if UNITY_SHOULD_SAMPLE_SH
            #endif
            #endif
            #if defined(LIGHTMAP_ON)
            #endif
            #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
                result.fogCoord = varyings.fogFactorAndVertexLight.x;
                COPY_TO_LIGHT_COORDS(result, varyings.fogFactorAndVertexLight.yzw);
            #endif
        
            DEFAULT_UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(varyings, result);
        }
        
        void SurfaceVertexToVaryings(v2f_surf surfVertex, inout Varyings result)
        {
            result.positionCS = surfVertex.pos;
            // viewDirectionWS is never filled out in the legacy pass' function. Always use the value computed by SRP
            // World Tangent isn't an available input on v2f_surf
        
            #if UNITY_ANY_INSTANCING_ENABLED
            #endif
            #if !defined(LIGHTMAP_ON)
            #if UNITY_SHOULD_SAMPLE_SH
            #endif
            #endif
            #if defined(LIGHTMAP_ON)
            #endif
            #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
                result.fogFactorAndVertexLight.x = surfVertex.fogCoord;
                COPY_FROM_LIGHT_COORDS(result.fogFactorAndVertexLight.yzw, surfVertex);
            #endif
        
            DEFAULT_UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(surfVertex, result);
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/ShadowCasterPass.hlsl"
        
        ENDHLSL
        }
        Pass
        {
            Name "DepthOnly"
            Tags
            {
                "LightMode" = "DepthOnly"
            }
        
        // Render State
        Cull [_BUILTIN_CullMode]
        Blend [_BUILTIN_SrcBlend] [_BUILTIN_DstBlend]
        ZTest LEqual
        ZWrite On
        ColorMask 0
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 3.0
        #pragma multi_compile_instancing
        #pragma vertex vert
        #pragma fragment frag
        
        // DotsInstancingOptions: <None>
        // HybridV1InjectedBuiltinProperties: <None>
        
        // Keywords
        #pragma shader_feature_local_fragment _ _BUILTIN_AlphaClip
        #pragma shader_feature_local_fragment _ _BUILTIN_ALPHATEST_ON
        // GraphKeywords: <None>
        
        // Defines
        #define _NORMALMAP 1
        #define _NORMAL_DROPOFF_WS 1
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_DEPTHONLY
        #define BUILTIN_TARGET_API 1
        /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
        #ifdef _BUILTIN_SURFACE_TYPE_TRANSPARENT
        #define _SURFACE_TYPE_TRANSPARENT _BUILTIN_SURFACE_TYPE_TRANSPARENT
        #endif
        #ifdef _BUILTIN_ALPHATEST_ON
        #define _ALPHATEST_ON _BUILTIN_ALPHATEST_ON
        #endif
        #ifdef _BUILTIN_AlphaClip
        #define _AlphaClip _BUILTIN_AlphaClip
        #endif
        #ifdef _BUILTIN_ALPHAPREMULTIPLY_ON
        #define _ALPHAPREMULTIPLY_ON _BUILTIN_ALPHAPREMULTIPLY_ON
        #endif
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Shim/Shims.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/LegacySurfaceVertex.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/ShaderGraphFunctions.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
             float3 WorldSpacePosition;
             float3 TimeParameters;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _Depth;
        float _DepthFalloff;
        float4 _ShoreColor;
        float4 _Color;
        float _FoamShoreWidth;
        float4 _FoamColor;
        float _FoamDepth;
        float _FoamFalloff;
        float _WaveIntensity;
        float _WaveSpeed;
        float _Float;
        float _Metal;
        float4 _NormalTexture_TexelSize;
        float4 _NormalTexture_ST;
        float _NormalStrenght;
        CBUFFER_END
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_NormalTexture);
        SAMPLER(sampler_NormalTexture);
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Includes
        // GraphIncludes: <None>
        
        // Graph Functions
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float(float A, float B, out float Out)
        {
            Out = A + B;
        }
        
        void Unity_Sine_float(float In, out float Out)
        {
            Out = sin(In);
        }
        
        void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A + B;
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            float _Property_3e04952c468843ab8933b2692cf6bacd_Out_0 = _WaveIntensity;
            float3 _Vector3_a18246eb0d944cbe92ba8ab4df244f74_Out_0 = float3(0, _Property_3e04952c468843ab8933b2692cf6bacd_Out_0, 0);
            float _Property_e189c9961bde4d4a80a0c20b6a92503b_Out_0 = _WaveSpeed;
            float _Multiply_c13c13771f8e4aa3a00a44ac4ff11002_Out_2;
            Unity_Multiply_float_float(_Property_e189c9961bde4d4a80a0c20b6a92503b_Out_0, IN.TimeParameters.x, _Multiply_c13c13771f8e4aa3a00a44ac4ff11002_Out_2);
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_R_1 = IN.WorldSpacePosition[0];
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_G_2 = IN.WorldSpacePosition[1];
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_B_3 = IN.WorldSpacePosition[2];
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_A_4 = 0;
            float _Add_f6078872ccf44f37bfbcc10e7e23224f_Out_2;
            Unity_Add_float(_Split_9172b35d396b4f6da3213c0bcd4ecb96_R_1, _Split_9172b35d396b4f6da3213c0bcd4ecb96_B_3, _Add_f6078872ccf44f37bfbcc10e7e23224f_Out_2);
            float _Add_6e66df41b5444def8731ecb95ab6afe3_Out_2;
            Unity_Add_float(_Multiply_c13c13771f8e4aa3a00a44ac4ff11002_Out_2, _Add_f6078872ccf44f37bfbcc10e7e23224f_Out_2, _Add_6e66df41b5444def8731ecb95ab6afe3_Out_2);
            float _Sine_1dfa4e4673a14844bdcb53c2ff481fe7_Out_1;
            Unity_Sine_float(_Add_6e66df41b5444def8731ecb95ab6afe3_Out_2, _Sine_1dfa4e4673a14844bdcb53c2ff481fe7_Out_1);
            float3 _Multiply_1fd709eee6bd43a5adf15627c9e1556b_Out_2;
            Unity_Multiply_float3_float3(_Vector3_a18246eb0d944cbe92ba8ab4df244f74_Out_0, (_Sine_1dfa4e4673a14844bdcb53c2ff481fe7_Out_1.xxx), _Multiply_1fd709eee6bd43a5adf15627c9e1556b_Out_2);
            float3 _Add_c55b332417574d1495a47eee31203ddc_Out_2;
            Unity_Add_float3(IN.ObjectSpacePosition, _Multiply_1fd709eee6bd43a5adf15627c9e1556b_Out_2, _Add_c55b332417574d1495a47eee31203ddc_Out_2);
            description.Position = _Add_c55b332417574d1495a47eee31203ddc_Out_2;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            surface.Alpha = 1;
            surface.AlphaClipThreshold = 0.5;
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
            output.WorldSpacePosition =                         TransformObjectToWorld(input.positionOS);
            output.TimeParameters =                             _TimeParameters.xyz;
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
            
        
        
        
        
        
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        void BuildAppDataFull(Attributes attributes, VertexDescription vertexDescription, inout appdata_full result)
        {
            result.vertex     = float4(attributes.positionOS, 1);
            result.tangent    = attributes.tangentOS;
            result.normal     = attributes.normalOS;
            result.vertex     = float4(vertexDescription.Position, 1);
            result.normal     = vertexDescription.Normal;
            result.tangent    = float4(vertexDescription.Tangent, 0);
            #if UNITY_ANY_INSTANCING_ENABLED
            #endif
        }
        
        void VaryingsToSurfaceVertex(Varyings varyings, inout v2f_surf result)
        {
            result.pos = varyings.positionCS;
            // World Tangent isn't an available input on v2f_surf
        
        
            #if UNITY_ANY_INSTANCING_ENABLED
            #endif
            #if !defined(LIGHTMAP_ON)
            #if UNITY_SHOULD_SAMPLE_SH
            #endif
            #endif
            #if defined(LIGHTMAP_ON)
            #endif
            #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
                result.fogCoord = varyings.fogFactorAndVertexLight.x;
                COPY_TO_LIGHT_COORDS(result, varyings.fogFactorAndVertexLight.yzw);
            #endif
        
            DEFAULT_UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(varyings, result);
        }
        
        void SurfaceVertexToVaryings(v2f_surf surfVertex, inout Varyings result)
        {
            result.positionCS = surfVertex.pos;
            // viewDirectionWS is never filled out in the legacy pass' function. Always use the value computed by SRP
            // World Tangent isn't an available input on v2f_surf
        
            #if UNITY_ANY_INSTANCING_ENABLED
            #endif
            #if !defined(LIGHTMAP_ON)
            #if UNITY_SHOULD_SAMPLE_SH
            #endif
            #endif
            #if defined(LIGHTMAP_ON)
            #endif
            #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
                result.fogFactorAndVertexLight.x = surfVertex.fogCoord;
                COPY_FROM_LIGHT_COORDS(result.fogFactorAndVertexLight.yzw, surfVertex);
            #endif
        
            DEFAULT_UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(surfVertex, result);
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/DepthOnlyPass.hlsl"
        
        ENDHLSL
        }
        Pass
        {
            Name "Meta"
            Tags
            {
                "LightMode" = "Meta"
            }
        
        // Render State
        Cull Off
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 3.0
        #pragma vertex vert
        #pragma fragment frag
        
        // DotsInstancingOptions: <None>
        // HybridV1InjectedBuiltinProperties: <None>
        
        // Keywords
        #pragma shader_feature _ _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
        #pragma shader_feature_local_fragment _ _BUILTIN_SURFACE_TYPE_TRANSPARENT
        #pragma shader_feature_local_fragment _ _BUILTIN_AlphaClip
        #pragma shader_feature_local_fragment _ _BUILTIN_ALPHATEST_ON
        // GraphKeywords: <None>
        
        // Defines
        #define _NORMALMAP 1
        #define _NORMAL_DROPOFF_WS 1
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define ATTRIBUTES_NEED_TEXCOORD1
        #define ATTRIBUTES_NEED_TEXCOORD2
        #define VARYINGS_NEED_POSITION_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_META
        #define BUILTIN_TARGET_API 1
        #define REQUIRE_DEPTH_TEXTURE
        #define REQUIRE_OPAQUE_TEXTURE
        /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
        #ifdef _BUILTIN_SURFACE_TYPE_TRANSPARENT
        #define _SURFACE_TYPE_TRANSPARENT _BUILTIN_SURFACE_TYPE_TRANSPARENT
        #endif
        #ifdef _BUILTIN_ALPHATEST_ON
        #define _ALPHATEST_ON _BUILTIN_ALPHATEST_ON
        #endif
        #ifdef _BUILTIN_AlphaClip
        #define _AlphaClip _BUILTIN_AlphaClip
        #endif
        #ifdef _BUILTIN_ALPHAPREMULTIPLY_ON
        #define _ALPHAPREMULTIPLY_ON _BUILTIN_ALPHAPREMULTIPLY_ON
        #endif
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Shim/Shims.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/LegacySurfaceVertex.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/ShaderGraphFunctions.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
             float4 uv1 : TEXCOORD1;
             float4 uv2 : TEXCOORD2;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 positionWS;
             float4 texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float3 WorldSpacePosition;
             float4 ScreenPosition;
             float4 uv0;
             float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
             float3 WorldSpacePosition;
             float3 TimeParameters;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float3 interp0 : INTERP0;
             float4 interp1 : INTERP1;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.interp0.xyz =  input.positionWS;
            output.interp1.xyzw =  input.texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.positionWS = input.interp0.xyz;
            output.texCoord0 = input.interp1.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _Depth;
        float _DepthFalloff;
        float4 _ShoreColor;
        float4 _Color;
        float _FoamShoreWidth;
        float4 _FoamColor;
        float _FoamDepth;
        float _FoamFalloff;
        float _WaveIntensity;
        float _WaveSpeed;
        float _Float;
        float _Metal;
        float4 _NormalTexture_TexelSize;
        float4 _NormalTexture_ST;
        float _NormalStrenght;
        CBUFFER_END
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_NormalTexture);
        SAMPLER(sampler_NormalTexture);
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Includes
        // GraphIncludes: <None>
        
        // Graph Functions
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float(float A, float B, out float Out)
        {
            Out = A + B;
        }
        
        void Unity_Sine_float(float In, out float Out)
        {
            Out = sin(In);
        }
        
        void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A + B;
        }
        
        void Unity_SceneDepth_Eye_float(float4 UV, out float Out)
        {
            if (unity_OrthoParams.w == 1.0)
            {
                Out = LinearEyeDepth(ComputeWorldSpacePosition(UV.xy, SHADERGRAPH_SAMPLE_SCENE_DEPTH(UV.xy), UNITY_MATRIX_I_VP), UNITY_MATRIX_V);
            }
            else
            {
                Out = LinearEyeDepth(SHADERGRAPH_SAMPLE_SCENE_DEPTH(UV.xy), _ZBufferParams);
            }
        }
        
        void Unity_Subtract_float(float A, float B, out float Out)
        {
            Out = A - B;
        }
        
        void Unity_Divide_float(float A, float B, out float Out)
        {
            Out = A / B;
        }
        
        void Unity_OneMinus_float(float In, out float Out)
        {
            Out = 1 - In;
        }
        
        void Unity_Saturate_float(float In, out float Out)
        {
            Out = saturate(In);
        }
        
        void Unity_Power_float(float A, float B, out float Out)
        {
            Out = pow(A, B);
        }
        
        struct Bindings_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float
        {
        float4 ScreenPosition;
        };
        
        void SG_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float(float _Depth, float _DepthFalloff, Bindings_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float IN, out float OutVector1_1)
        {
        float _SceneDepth_4398a40afb5444a98bdb88f05ff37be7_Out_1;
        Unity_SceneDepth_Eye_float(float4(IN.ScreenPosition.xy / IN.ScreenPosition.w, 0, 0), _SceneDepth_4398a40afb5444a98bdb88f05ff37be7_Out_1);
        float4 _ScreenPosition_78861b0c174847048fb96d1b9be441c4_Out_0 = IN.ScreenPosition;
        float _Split_9aecda2f9d2945b9b0a54de06a3a9d48_R_1 = _ScreenPosition_78861b0c174847048fb96d1b9be441c4_Out_0[0];
        float _Split_9aecda2f9d2945b9b0a54de06a3a9d48_G_2 = _ScreenPosition_78861b0c174847048fb96d1b9be441c4_Out_0[1];
        float _Split_9aecda2f9d2945b9b0a54de06a3a9d48_B_3 = _ScreenPosition_78861b0c174847048fb96d1b9be441c4_Out_0[2];
        float _Split_9aecda2f9d2945b9b0a54de06a3a9d48_A_4 = _ScreenPosition_78861b0c174847048fb96d1b9be441c4_Out_0[3];
        float _Subtract_0ef5acf5e9ec4f599d608aed9d014d88_Out_2;
        Unity_Subtract_float(_SceneDepth_4398a40afb5444a98bdb88f05ff37be7_Out_1, _Split_9aecda2f9d2945b9b0a54de06a3a9d48_A_4, _Subtract_0ef5acf5e9ec4f599d608aed9d014d88_Out_2);
        float _Property_5702a0604e9c425f9f28a8e389f7d6e8_Out_0 = _Depth;
        float _Divide_230152a01c7a4ab691e1a20a1fbf597f_Out_2;
        Unity_Divide_float(_Subtract_0ef5acf5e9ec4f599d608aed9d014d88_Out_2, _Property_5702a0604e9c425f9f28a8e389f7d6e8_Out_0, _Divide_230152a01c7a4ab691e1a20a1fbf597f_Out_2);
        float _OneMinus_4edb44dcaf8a4df5974bc2b0bfc1a39d_Out_1;
        Unity_OneMinus_float(_Divide_230152a01c7a4ab691e1a20a1fbf597f_Out_2, _OneMinus_4edb44dcaf8a4df5974bc2b0bfc1a39d_Out_1);
        float _Saturate_c574ae0f849b4335bdfce5762b7b4760_Out_1;
        Unity_Saturate_float(_OneMinus_4edb44dcaf8a4df5974bc2b0bfc1a39d_Out_1, _Saturate_c574ae0f849b4335bdfce5762b7b4760_Out_1);
        float _Property_8b3812fa4cec4943b82bfecf45cd931a_Out_0 = _DepthFalloff;
        float _Power_494a422a08904cbbb881363a1d49b985_Out_2;
        Unity_Power_float(_Saturate_c574ae0f849b4335bdfce5762b7b4760_Out_1, _Property_8b3812fa4cec4943b82bfecf45cd931a_Out_0, _Power_494a422a08904cbbb881363a1d49b985_Out_2);
        OutVector1_1 = _Power_494a422a08904cbbb881363a1d49b985_Out_2;
        }
        
        void Unity_Ceiling_float(float In, out float Out)
        {
            Out = ceil(In);
        }
        
        struct Bindings_Cutout_719ac7b9c41a99e4fa65ee4e7f3e2847_float
        {
        };
        
        void SG_Cutout_719ac7b9c41a99e4fa65ee4e7f3e2847_float(float _Alpha, float _Input, Bindings_Cutout_719ac7b9c41a99e4fa65ee4e7f3e2847_float IN, out float Output_0)
        {
        float _Property_c3987da974dc4f0485a61aed8135469c_Out_0 = _Input;
        float _Property_1427757994b04881a478a38d221123de_Out_0 = _Alpha;
        float _Saturate_35fc4a94f39247569cd864728c7400af_Out_1;
        Unity_Saturate_float(_Property_1427757994b04881a478a38d221123de_Out_0, _Saturate_35fc4a94f39247569cd864728c7400af_Out_1);
        float _Subtract_7135ced8bffe4d64949f974ca6083fdf_Out_2;
        Unity_Subtract_float(_Property_c3987da974dc4f0485a61aed8135469c_Out_0, _Saturate_35fc4a94f39247569cd864728c7400af_Out_1, _Subtract_7135ced8bffe4d64949f974ca6083fdf_Out_2);
        float _Ceiling_46ae28079db64bdc9dfb591ddb2c6194_Out_1;
        Unity_Ceiling_float(_Subtract_7135ced8bffe4d64949f974ca6083fdf_Out_2, _Ceiling_46ae28079db64bdc9dfb591ddb2c6194_Out_1);
        Output_0 = _Ceiling_46ae28079db64bdc9dfb591ddb2c6194_Out_1;
        }
        
        void Unity_RadialShear_float(float2 UV, float2 Center, float2 Strength, float2 Offset, out float2 Out)
        {
            float2 delta = UV - Center;
            float delta2 = dot(delta.xy, delta.xy);
            float2 delta_offset = delta2 * Strength;
            Out = UV + float2(delta.y, -delta.x) * delta_offset + Offset;
        }
        
        
        inline float2 Unity_Voronoi_RandomVector_float (float2 UV, float offset)
        {
            float2x2 m = float2x2(15.27, 47.63, 99.41, 89.98);
            UV = frac(sin(mul(UV, m)));
            return float2(sin(UV.y*+offset)*0.5+0.5, cos(UV.x*offset)*0.5+0.5);
        }
        
        void Unity_Voronoi_float(float2 UV, float AngleOffset, float CellDensity, out float Out, out float Cells)
        {
            float2 g = floor(UV * CellDensity);
            float2 f = frac(UV * CellDensity);
            float t = 8.0;
            float3 res = float3(8.0, 0.0, 0.0);
        
            for(int y=-1; y<=1; y++)
            {
                for(int x=-1; x<=1; x++)
                {
                    float2 lattice = float2(x,y);
                    float2 offset = Unity_Voronoi_RandomVector_float(lattice + g, AngleOffset);
                    float d = distance(lattice + offset, f);
        
                    if(d < res.x)
                    {
                        res = float3(d, offset.x, offset.y);
                        Out = res.x;
                        Cells = res.y;
                    }
                }
            }
        }
        
        void Unity_Multiply_float4_float4(float4 A, float4 B, out float4 Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float4(float4 A, float4 B, out float4 Out)
        {
            Out = A + B;
        }
        
        void Unity_SceneColor_float(float4 UV, out float3 Out)
        {
            Out = SHADERGRAPH_SAMPLE_SCENE_COLOR(UV.xy);
        }
        
        void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
        {
            Out = UV * Tiling + Offset;
        }
        
        
        inline float Unity_SimpleNoise_RandomValue_float (float2 uv)
        {
            float angle = dot(uv, float2(12.9898, 78.233));
            #if defined(SHADER_API_MOBILE) && (defined(SHADER_API_GLES) || defined(SHADER_API_GLES3) || defined(SHADER_API_VULKAN))
                // 'sin()' has bad precision on Mali GPUs for inputs > 10000
                angle = fmod(angle, TWO_PI); // Avoid large inputs to sin()
            #endif
            return frac(sin(angle)*43758.5453);
        }
        
        inline float Unity_SimpleNnoise_Interpolate_float (float a, float b, float t)
        {
            return (1.0-t)*a + (t*b);
        }
        
        
        inline float Unity_SimpleNoise_ValueNoise_float (float2 uv)
        {
            float2 i = floor(uv);
            float2 f = frac(uv);
            f = f * f * (3.0 - 2.0 * f);
        
            uv = abs(frac(uv) - 0.5);
            float2 c0 = i + float2(0.0, 0.0);
            float2 c1 = i + float2(1.0, 0.0);
            float2 c2 = i + float2(0.0, 1.0);
            float2 c3 = i + float2(1.0, 1.0);
            float r0 = Unity_SimpleNoise_RandomValue_float(c0);
            float r1 = Unity_SimpleNoise_RandomValue_float(c1);
            float r2 = Unity_SimpleNoise_RandomValue_float(c2);
            float r3 = Unity_SimpleNoise_RandomValue_float(c3);
        
            float bottomOfGrid = Unity_SimpleNnoise_Interpolate_float(r0, r1, f.x);
            float topOfGrid = Unity_SimpleNnoise_Interpolate_float(r2, r3, f.x);
            float t = Unity_SimpleNnoise_Interpolate_float(bottomOfGrid, topOfGrid, f.y);
            return t;
        }
        void Unity_SimpleNoise_float(float2 UV, float Scale, out float Out)
        {
            float t = 0.0;
        
            float freq = pow(2.0, float(0));
            float amp = pow(0.5, float(3-0));
            t += Unity_SimpleNoise_ValueNoise_float(float2(UV.x*Scale/freq, UV.y*Scale/freq))*amp;
        
            freq = pow(2.0, float(1));
            amp = pow(0.5, float(3-1));
            t += Unity_SimpleNoise_ValueNoise_float(float2(UV.x*Scale/freq, UV.y*Scale/freq))*amp;
        
            freq = pow(2.0, float(2));
            amp = pow(0.5, float(3-2));
            t += Unity_SimpleNoise_ValueNoise_float(float2(UV.x*Scale/freq, UV.y*Scale/freq))*amp;
        
            Out = t;
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            float _Property_3e04952c468843ab8933b2692cf6bacd_Out_0 = _WaveIntensity;
            float3 _Vector3_a18246eb0d944cbe92ba8ab4df244f74_Out_0 = float3(0, _Property_3e04952c468843ab8933b2692cf6bacd_Out_0, 0);
            float _Property_e189c9961bde4d4a80a0c20b6a92503b_Out_0 = _WaveSpeed;
            float _Multiply_c13c13771f8e4aa3a00a44ac4ff11002_Out_2;
            Unity_Multiply_float_float(_Property_e189c9961bde4d4a80a0c20b6a92503b_Out_0, IN.TimeParameters.x, _Multiply_c13c13771f8e4aa3a00a44ac4ff11002_Out_2);
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_R_1 = IN.WorldSpacePosition[0];
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_G_2 = IN.WorldSpacePosition[1];
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_B_3 = IN.WorldSpacePosition[2];
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_A_4 = 0;
            float _Add_f6078872ccf44f37bfbcc10e7e23224f_Out_2;
            Unity_Add_float(_Split_9172b35d396b4f6da3213c0bcd4ecb96_R_1, _Split_9172b35d396b4f6da3213c0bcd4ecb96_B_3, _Add_f6078872ccf44f37bfbcc10e7e23224f_Out_2);
            float _Add_6e66df41b5444def8731ecb95ab6afe3_Out_2;
            Unity_Add_float(_Multiply_c13c13771f8e4aa3a00a44ac4ff11002_Out_2, _Add_f6078872ccf44f37bfbcc10e7e23224f_Out_2, _Add_6e66df41b5444def8731ecb95ab6afe3_Out_2);
            float _Sine_1dfa4e4673a14844bdcb53c2ff481fe7_Out_1;
            Unity_Sine_float(_Add_6e66df41b5444def8731ecb95ab6afe3_Out_2, _Sine_1dfa4e4673a14844bdcb53c2ff481fe7_Out_1);
            float3 _Multiply_1fd709eee6bd43a5adf15627c9e1556b_Out_2;
            Unity_Multiply_float3_float3(_Vector3_a18246eb0d944cbe92ba8ab4df244f74_Out_0, (_Sine_1dfa4e4673a14844bdcb53c2ff481fe7_Out_1.xxx), _Multiply_1fd709eee6bd43a5adf15627c9e1556b_Out_2);
            float3 _Add_c55b332417574d1495a47eee31203ddc_Out_2;
            Unity_Add_float3(IN.ObjectSpacePosition, _Multiply_1fd709eee6bd43a5adf15627c9e1556b_Out_2, _Add_c55b332417574d1495a47eee31203ddc_Out_2);
            description.Position = _Add_c55b332417574d1495a47eee31203ddc_Out_2;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float3 BaseColor;
            float3 Emission;
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float4 _ScreenPosition_3b330ecd9d44487b9c002f9cc7f91cb6_Out_0 = float4(IN.ScreenPosition.xy / IN.ScreenPosition.w, 0, 0);
            float4 _Property_e7752889f9aa4c45930eda630af8bfa0_Out_0 = _FoamColor;
            float _Property_26d00366155a4444be65770ec1a4521d_Out_0 = _FoamShoreWidth;
            Bindings_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float _sSWaterDepth_e1942db5fec045e6b9a624763ed0ac66;
            _sSWaterDepth_e1942db5fec045e6b9a624763ed0ac66.ScreenPosition = IN.ScreenPosition;
            float _sSWaterDepth_e1942db5fec045e6b9a624763ed0ac66_OutVector1_1;
            SG_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float(1, 1, _sSWaterDepth_e1942db5fec045e6b9a624763ed0ac66, _sSWaterDepth_e1942db5fec045e6b9a624763ed0ac66_OutVector1_1);
            Bindings_Cutout_719ac7b9c41a99e4fa65ee4e7f3e2847_float _Cutout_923fce825c4c4c3b9bef9bbb02abb2de;
            float _Cutout_923fce825c4c4c3b9bef9bbb02abb2de_Output_0;
            SG_Cutout_719ac7b9c41a99e4fa65ee4e7f3e2847_float(_Property_26d00366155a4444be65770ec1a4521d_Out_0, _sSWaterDepth_e1942db5fec045e6b9a624763ed0ac66_OutVector1_1, _Cutout_923fce825c4c4c3b9bef9bbb02abb2de, _Cutout_923fce825c4c4c3b9bef9bbb02abb2de_Output_0);
            float2 _RadialShear_9df83a3a335746848643024aa5b7c9e0_Out_4;
            Unity_RadialShear_float(IN.uv0.xy, float2 (0.5, 0.5), float2 (5, 5), float2 (0, 0), _RadialShear_9df83a3a335746848643024aa5b7c9e0_Out_4);
            float _Voronoi_1a7e03c0bcbd4dc29b26257ce21caa1d_Out_3;
            float _Voronoi_1a7e03c0bcbd4dc29b26257ce21caa1d_Cells_4;
            Unity_Voronoi_float(_RadialShear_9df83a3a335746848643024aa5b7c9e0_Out_4, IN.TimeParameters.x, 8, _Voronoi_1a7e03c0bcbd4dc29b26257ce21caa1d_Out_3, _Voronoi_1a7e03c0bcbd4dc29b26257ce21caa1d_Cells_4);
            float _Property_636c9004ffcb4e39863cd54e7352d8e6_Out_0 = _Float;
            float _Power_fe5432d360554233a7cc6f909009a36a_Out_2;
            Unity_Power_float(_Voronoi_1a7e03c0bcbd4dc29b26257ce21caa1d_Out_3, _Property_636c9004ffcb4e39863cd54e7352d8e6_Out_0, _Power_fe5432d360554233a7cc6f909009a36a_Out_2);
            float _Property_f3b23a2533b640a4851a42b69fe171e4_Out_0 = _FoamDepth;
            float _Property_7c6c40f2b3564d36b4001b8efb2af0ea_Out_0 = _FoamFalloff;
            Bindings_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float _sSWaterDepth_bb1b5652101c44a1919b6541defe8462;
            _sSWaterDepth_bb1b5652101c44a1919b6541defe8462.ScreenPosition = IN.ScreenPosition;
            float _sSWaterDepth_bb1b5652101c44a1919b6541defe8462_OutVector1_1;
            SG_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float(_Property_f3b23a2533b640a4851a42b69fe171e4_Out_0, _Property_7c6c40f2b3564d36b4001b8efb2af0ea_Out_0, _sSWaterDepth_bb1b5652101c44a1919b6541defe8462, _sSWaterDepth_bb1b5652101c44a1919b6541defe8462_OutVector1_1);
            float _Multiply_ccdc528bed5d4c18b6ab85511e54d5a6_Out_2;
            Unity_Multiply_float_float(_Power_fe5432d360554233a7cc6f909009a36a_Out_2, _sSWaterDepth_bb1b5652101c44a1919b6541defe8462_OutVector1_1, _Multiply_ccdc528bed5d4c18b6ab85511e54d5a6_Out_2);
            float _Add_4191fc0bec74415aa999b2a33166d88a_Out_2;
            Unity_Add_float(_Cutout_923fce825c4c4c3b9bef9bbb02abb2de_Output_0, _Multiply_ccdc528bed5d4c18b6ab85511e54d5a6_Out_2, _Add_4191fc0bec74415aa999b2a33166d88a_Out_2);
            float _Saturate_b910c8272d1a4155a7173705fb331898_Out_1;
            Unity_Saturate_float(_Add_4191fc0bec74415aa999b2a33166d88a_Out_2, _Saturate_b910c8272d1a4155a7173705fb331898_Out_1);
            float4 _Multiply_ff39ce7dfb524ce481784d5917bc83a8_Out_2;
            Unity_Multiply_float4_float4(_Property_e7752889f9aa4c45930eda630af8bfa0_Out_0, (_Saturate_b910c8272d1a4155a7173705fb331898_Out_1.xxxx), _Multiply_ff39ce7dfb524ce481784d5917bc83a8_Out_2);
            float _OneMinus_894809b879cc48b7ba2261fac43e00cd_Out_1;
            Unity_OneMinus_float(_Saturate_b910c8272d1a4155a7173705fb331898_Out_1, _OneMinus_894809b879cc48b7ba2261fac43e00cd_Out_1);
            float4 _Property_e0ba34c22e694f58ba492f038826fde6_Out_0 = _ShoreColor;
            float _Property_375ab85e259d4db39b617839ff4c5008_Out_0 = _Depth;
            float _Property_75e9248b4fc9458e99caeb3bf19b1908_Out_0 = _DepthFalloff;
            Bindings_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float _sSWaterDepth_558ab96888514e11b58182e01c6b6fa2;
            _sSWaterDepth_558ab96888514e11b58182e01c6b6fa2.ScreenPosition = IN.ScreenPosition;
            float _sSWaterDepth_558ab96888514e11b58182e01c6b6fa2_OutVector1_1;
            SG_sSWaterDepth_4f0fd71f20ed1b147a284ec8bf73fc05_float(_Property_375ab85e259d4db39b617839ff4c5008_Out_0, _Property_75e9248b4fc9458e99caeb3bf19b1908_Out_0, _sSWaterDepth_558ab96888514e11b58182e01c6b6fa2, _sSWaterDepth_558ab96888514e11b58182e01c6b6fa2_OutVector1_1);
            float4 _Multiply_d0c0d2997ae6412ea37dadf952efd293_Out_2;
            Unity_Multiply_float4_float4(_Property_e0ba34c22e694f58ba492f038826fde6_Out_0, (_sSWaterDepth_558ab96888514e11b58182e01c6b6fa2_OutVector1_1.xxxx), _Multiply_d0c0d2997ae6412ea37dadf952efd293_Out_2);
            float _OneMinus_c0858fd22bc54bd59887f99335d6311f_Out_1;
            Unity_OneMinus_float(_sSWaterDepth_558ab96888514e11b58182e01c6b6fa2_OutVector1_1, _OneMinus_c0858fd22bc54bd59887f99335d6311f_Out_1);
            float4 _Property_6435d5dfbd6e4beebf3719823fe9ba99_Out_0 = _Color;
            float4 _Multiply_edf9a4ef75bc4ac4ad6c88cb52c69d3d_Out_2;
            Unity_Multiply_float4_float4((_OneMinus_c0858fd22bc54bd59887f99335d6311f_Out_1.xxxx), _Property_6435d5dfbd6e4beebf3719823fe9ba99_Out_0, _Multiply_edf9a4ef75bc4ac4ad6c88cb52c69d3d_Out_2);
            float4 _Add_3817e0ebbc6e44d594f3d07bde8e7ce6_Out_2;
            Unity_Add_float4(_Multiply_d0c0d2997ae6412ea37dadf952efd293_Out_2, _Multiply_edf9a4ef75bc4ac4ad6c88cb52c69d3d_Out_2, _Add_3817e0ebbc6e44d594f3d07bde8e7ce6_Out_2);
            float4 _Multiply_85d7a273de444dcb983c32751eeace29_Out_2;
            Unity_Multiply_float4_float4((_OneMinus_894809b879cc48b7ba2261fac43e00cd_Out_1.xxxx), _Add_3817e0ebbc6e44d594f3d07bde8e7ce6_Out_2, _Multiply_85d7a273de444dcb983c32751eeace29_Out_2);
            float4 _Add_7b0f7efb411c4c598bcc28966dd51c15_Out_2;
            Unity_Add_float4(_Multiply_ff39ce7dfb524ce481784d5917bc83a8_Out_2, _Multiply_85d7a273de444dcb983c32751eeace29_Out_2, _Add_7b0f7efb411c4c598bcc28966dd51c15_Out_2);
            float4 _Multiply_e0820bab8d5d461eaa9c157e8bce8884_Out_2;
            Unity_Multiply_float4_float4(_Add_7b0f7efb411c4c598bcc28966dd51c15_Out_2, float4(2, 2, 2, 2), _Multiply_e0820bab8d5d461eaa9c157e8bce8884_Out_2);
            float4 _Add_e74668030b0b498385a1a9468b8dbd24_Out_2;
            Unity_Add_float4(_ScreenPosition_3b330ecd9d44487b9c002f9cc7f91cb6_Out_0, _Multiply_e0820bab8d5d461eaa9c157e8bce8884_Out_2, _Add_e74668030b0b498385a1a9468b8dbd24_Out_2);
            float3 _SceneColor_ef9de574552a4340b580a3d43b107025_Out_1;
            Unity_SceneColor_float(_Add_e74668030b0b498385a1a9468b8dbd24_Out_2, _SceneColor_ef9de574552a4340b580a3d43b107025_Out_1);
            float _Multiply_9103c4b0ed7b41dab55d0c48769d043f_Out_2;
            Unity_Multiply_float_float(IN.TimeParameters.x, 0.01, _Multiply_9103c4b0ed7b41dab55d0c48769d043f_Out_2);
            float2 _TilingAndOffset_a8ae8dd4702740cd81c535e72ab6869c_Out_3;
            Unity_TilingAndOffset_float(_RadialShear_9df83a3a335746848643024aa5b7c9e0_Out_4, float2 (0.5, 0.5), (_Multiply_9103c4b0ed7b41dab55d0c48769d043f_Out_2.xx), _TilingAndOffset_a8ae8dd4702740cd81c535e72ab6869c_Out_3);
            float _SimpleNoise_b317b9dbecde4771a32418318aa6bf2e_Out_2;
            Unity_SimpleNoise_float(_TilingAndOffset_a8ae8dd4702740cd81c535e72ab6869c_Out_3, 300, _SimpleNoise_b317b9dbecde4771a32418318aa6bf2e_Out_2);
            float _Power_e0b197c557384350a2d2f0d8f2cce02c_Out_2;
            Unity_Power_float(_SimpleNoise_b317b9dbecde4771a32418318aa6bf2e_Out_2, 10, _Power_e0b197c557384350a2d2f0d8f2cce02c_Out_2);
            float _Add_4f955775a32a4e90962988b710db2973_Out_2;
            Unity_Add_float(_Power_e0b197c557384350a2d2f0d8f2cce02c_Out_2, _Power_e0b197c557384350a2d2f0d8f2cce02c_Out_2, _Add_4f955775a32a4e90962988b710db2973_Out_2);
            float _Add_34f816e43a2146619e527cf77b5a2829_Out_2;
            Unity_Add_float(_Power_fe5432d360554233a7cc6f909009a36a_Out_2, _Add_4f955775a32a4e90962988b710db2973_Out_2, _Add_34f816e43a2146619e527cf77b5a2829_Out_2);
            float4 _Add_45316f2f4573425b8fb3629669a020c7_Out_2;
            Unity_Add_float4((_Add_34f816e43a2146619e527cf77b5a2829_Out_2.xxxx), _Add_3817e0ebbc6e44d594f3d07bde8e7ce6_Out_2, _Add_45316f2f4573425b8fb3629669a020c7_Out_2);
            float4 _Add_bc4e82f988a84312bc336ac7809f548c_Out_2;
            Unity_Add_float4(_Add_7b0f7efb411c4c598bcc28966dd51c15_Out_2, _Add_45316f2f4573425b8fb3629669a020c7_Out_2, _Add_bc4e82f988a84312bc336ac7809f548c_Out_2);
            surface.BaseColor = _SceneColor_ef9de574552a4340b580a3d43b107025_Out_1;
            surface.Emission = (_Add_bc4e82f988a84312bc336ac7809f548c_Out_2.xyz);
            surface.Alpha = 1;
            surface.AlphaClipThreshold = 0.5;
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
            output.WorldSpacePosition =                         TransformObjectToWorld(input.positionOS);
            output.TimeParameters =                             _TimeParameters.xyz;
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
            
        
        
        
        
        
            output.WorldSpacePosition = input.positionWS;
            output.ScreenPosition = ComputeScreenPos(TransformWorldToHClip(input.positionWS), _ProjectionParams.x);
            output.uv0 = input.texCoord0;
            output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        void BuildAppDataFull(Attributes attributes, VertexDescription vertexDescription, inout appdata_full result)
        {
            result.vertex     = float4(attributes.positionOS, 1);
            result.tangent    = attributes.tangentOS;
            result.normal     = attributes.normalOS;
            result.texcoord   = attributes.uv0;
            result.texcoord1  = attributes.uv1;
            result.texcoord2  = attributes.uv2;
            result.vertex     = float4(vertexDescription.Position, 1);
            result.normal     = vertexDescription.Normal;
            result.tangent    = float4(vertexDescription.Tangent, 0);
            #if UNITY_ANY_INSTANCING_ENABLED
            #endif
        }
        
        void VaryingsToSurfaceVertex(Varyings varyings, inout v2f_surf result)
        {
            result.pos = varyings.positionCS;
            result.worldPos = varyings.positionWS;
            // World Tangent isn't an available input on v2f_surf
        
        
            #if UNITY_ANY_INSTANCING_ENABLED
            #endif
            #if !defined(LIGHTMAP_ON)
            #if UNITY_SHOULD_SAMPLE_SH
            #endif
            #endif
            #if defined(LIGHTMAP_ON)
            #endif
            #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
                result.fogCoord = varyings.fogFactorAndVertexLight.x;
                COPY_TO_LIGHT_COORDS(result, varyings.fogFactorAndVertexLight.yzw);
            #endif
        
            DEFAULT_UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(varyings, result);
        }
        
        void SurfaceVertexToVaryings(v2f_surf surfVertex, inout Varyings result)
        {
            result.positionCS = surfVertex.pos;
            result.positionWS = surfVertex.worldPos;
            // viewDirectionWS is never filled out in the legacy pass' function. Always use the value computed by SRP
            // World Tangent isn't an available input on v2f_surf
        
            #if UNITY_ANY_INSTANCING_ENABLED
            #endif
            #if !defined(LIGHTMAP_ON)
            #if UNITY_SHOULD_SAMPLE_SH
            #endif
            #endif
            #if defined(LIGHTMAP_ON)
            #endif
            #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
                result.fogFactorAndVertexLight.x = surfVertex.fogCoord;
                COPY_FROM_LIGHT_COORDS(result.fogFactorAndVertexLight.yzw, surfVertex);
            #endif
        
            DEFAULT_UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(surfVertex, result);
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/LightingMetaPass.hlsl"
        
        ENDHLSL
        }
        Pass
        {
            Name "SceneSelectionPass"
            Tags
            {
                "LightMode" = "SceneSelectionPass"
            }
        
        // Render State
        Cull Off
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 3.0
        #pragma multi_compile_instancing
        #pragma vertex vert
        #pragma fragment frag
        
        // DotsInstancingOptions: <None>
        // HybridV1InjectedBuiltinProperties: <None>
        
        // Keywords
        #pragma shader_feature_local_fragment _ _BUILTIN_SURFACE_TYPE_TRANSPARENT
        #pragma shader_feature_local_fragment _ _BUILTIN_AlphaClip
        #pragma shader_feature_local_fragment _ _BUILTIN_ALPHATEST_ON
        // GraphKeywords: <None>
        
        // Defines
        #define _NORMALMAP 1
        #define _NORMAL_DROPOFF_WS 1
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SceneSelectionPass
        #define BUILTIN_TARGET_API 1
        #define SCENESELECTIONPASS 1
        /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
        #ifdef _BUILTIN_SURFACE_TYPE_TRANSPARENT
        #define _SURFACE_TYPE_TRANSPARENT _BUILTIN_SURFACE_TYPE_TRANSPARENT
        #endif
        #ifdef _BUILTIN_ALPHATEST_ON
        #define _ALPHATEST_ON _BUILTIN_ALPHATEST_ON
        #endif
        #ifdef _BUILTIN_AlphaClip
        #define _AlphaClip _BUILTIN_AlphaClip
        #endif
        #ifdef _BUILTIN_ALPHAPREMULTIPLY_ON
        #define _ALPHAPREMULTIPLY_ON _BUILTIN_ALPHAPREMULTIPLY_ON
        #endif
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Shim/Shims.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/LegacySurfaceVertex.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/ShaderGraphFunctions.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
             float3 WorldSpacePosition;
             float3 TimeParameters;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _Depth;
        float _DepthFalloff;
        float4 _ShoreColor;
        float4 _Color;
        float _FoamShoreWidth;
        float4 _FoamColor;
        float _FoamDepth;
        float _FoamFalloff;
        float _WaveIntensity;
        float _WaveSpeed;
        float _Float;
        float _Metal;
        float4 _NormalTexture_TexelSize;
        float4 _NormalTexture_ST;
        float _NormalStrenght;
        CBUFFER_END
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_NormalTexture);
        SAMPLER(sampler_NormalTexture);
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Includes
        // GraphIncludes: <None>
        
        // Graph Functions
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float(float A, float B, out float Out)
        {
            Out = A + B;
        }
        
        void Unity_Sine_float(float In, out float Out)
        {
            Out = sin(In);
        }
        
        void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A + B;
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            float _Property_3e04952c468843ab8933b2692cf6bacd_Out_0 = _WaveIntensity;
            float3 _Vector3_a18246eb0d944cbe92ba8ab4df244f74_Out_0 = float3(0, _Property_3e04952c468843ab8933b2692cf6bacd_Out_0, 0);
            float _Property_e189c9961bde4d4a80a0c20b6a92503b_Out_0 = _WaveSpeed;
            float _Multiply_c13c13771f8e4aa3a00a44ac4ff11002_Out_2;
            Unity_Multiply_float_float(_Property_e189c9961bde4d4a80a0c20b6a92503b_Out_0, IN.TimeParameters.x, _Multiply_c13c13771f8e4aa3a00a44ac4ff11002_Out_2);
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_R_1 = IN.WorldSpacePosition[0];
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_G_2 = IN.WorldSpacePosition[1];
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_B_3 = IN.WorldSpacePosition[2];
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_A_4 = 0;
            float _Add_f6078872ccf44f37bfbcc10e7e23224f_Out_2;
            Unity_Add_float(_Split_9172b35d396b4f6da3213c0bcd4ecb96_R_1, _Split_9172b35d396b4f6da3213c0bcd4ecb96_B_3, _Add_f6078872ccf44f37bfbcc10e7e23224f_Out_2);
            float _Add_6e66df41b5444def8731ecb95ab6afe3_Out_2;
            Unity_Add_float(_Multiply_c13c13771f8e4aa3a00a44ac4ff11002_Out_2, _Add_f6078872ccf44f37bfbcc10e7e23224f_Out_2, _Add_6e66df41b5444def8731ecb95ab6afe3_Out_2);
            float _Sine_1dfa4e4673a14844bdcb53c2ff481fe7_Out_1;
            Unity_Sine_float(_Add_6e66df41b5444def8731ecb95ab6afe3_Out_2, _Sine_1dfa4e4673a14844bdcb53c2ff481fe7_Out_1);
            float3 _Multiply_1fd709eee6bd43a5adf15627c9e1556b_Out_2;
            Unity_Multiply_float3_float3(_Vector3_a18246eb0d944cbe92ba8ab4df244f74_Out_0, (_Sine_1dfa4e4673a14844bdcb53c2ff481fe7_Out_1.xxx), _Multiply_1fd709eee6bd43a5adf15627c9e1556b_Out_2);
            float3 _Add_c55b332417574d1495a47eee31203ddc_Out_2;
            Unity_Add_float3(IN.ObjectSpacePosition, _Multiply_1fd709eee6bd43a5adf15627c9e1556b_Out_2, _Add_c55b332417574d1495a47eee31203ddc_Out_2);
            description.Position = _Add_c55b332417574d1495a47eee31203ddc_Out_2;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            surface.Alpha = 1;
            surface.AlphaClipThreshold = 0.5;
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
            output.WorldSpacePosition =                         TransformObjectToWorld(input.positionOS);
            output.TimeParameters =                             _TimeParameters.xyz;
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
            
        
        
        
        
        
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        void BuildAppDataFull(Attributes attributes, VertexDescription vertexDescription, inout appdata_full result)
        {
            result.vertex     = float4(attributes.positionOS, 1);
            result.tangent    = attributes.tangentOS;
            result.normal     = attributes.normalOS;
            result.vertex     = float4(vertexDescription.Position, 1);
            result.normal     = vertexDescription.Normal;
            result.tangent    = float4(vertexDescription.Tangent, 0);
            #if UNITY_ANY_INSTANCING_ENABLED
            #endif
        }
        
        void VaryingsToSurfaceVertex(Varyings varyings, inout v2f_surf result)
        {
            result.pos = varyings.positionCS;
            // World Tangent isn't an available input on v2f_surf
        
        
            #if UNITY_ANY_INSTANCING_ENABLED
            #endif
            #if !defined(LIGHTMAP_ON)
            #if UNITY_SHOULD_SAMPLE_SH
            #endif
            #endif
            #if defined(LIGHTMAP_ON)
            #endif
            #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
                result.fogCoord = varyings.fogFactorAndVertexLight.x;
                COPY_TO_LIGHT_COORDS(result, varyings.fogFactorAndVertexLight.yzw);
            #endif
        
            DEFAULT_UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(varyings, result);
        }
        
        void SurfaceVertexToVaryings(v2f_surf surfVertex, inout Varyings result)
        {
            result.positionCS = surfVertex.pos;
            // viewDirectionWS is never filled out in the legacy pass' function. Always use the value computed by SRP
            // World Tangent isn't an available input on v2f_surf
        
            #if UNITY_ANY_INSTANCING_ENABLED
            #endif
            #if !defined(LIGHTMAP_ON)
            #if UNITY_SHOULD_SAMPLE_SH
            #endif
            #endif
            #if defined(LIGHTMAP_ON)
            #endif
            #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
                result.fogFactorAndVertexLight.x = surfVertex.fogCoord;
                COPY_FROM_LIGHT_COORDS(result.fogFactorAndVertexLight.yzw, surfVertex);
            #endif
        
            DEFAULT_UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(surfVertex, result);
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/DepthOnlyPass.hlsl"
        
        ENDHLSL
        }
        Pass
        {
            Name "ScenePickingPass"
            Tags
            {
                "LightMode" = "Picking"
            }
        
        // Render State
        Cull [_BUILTIN_CullMode]
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 3.0
        #pragma multi_compile_instancing
        #pragma vertex vert
        #pragma fragment frag
        
        // DotsInstancingOptions: <None>
        // HybridV1InjectedBuiltinProperties: <None>
        
        // Keywords
        #pragma shader_feature_local_fragment _ _BUILTIN_SURFACE_TYPE_TRANSPARENT
        #pragma shader_feature_local_fragment _ _BUILTIN_AlphaClip
        #pragma shader_feature_local_fragment _ _BUILTIN_ALPHATEST_ON
        // GraphKeywords: <None>
        
        // Defines
        #define _NORMALMAP 1
        #define _NORMAL_DROPOFF_WS 1
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS ScenePickingPass
        #define BUILTIN_TARGET_API 1
        #define SCENEPICKINGPASS 1
        /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
        #ifdef _BUILTIN_SURFACE_TYPE_TRANSPARENT
        #define _SURFACE_TYPE_TRANSPARENT _BUILTIN_SURFACE_TYPE_TRANSPARENT
        #endif
        #ifdef _BUILTIN_ALPHATEST_ON
        #define _ALPHATEST_ON _BUILTIN_ALPHATEST_ON
        #endif
        #ifdef _BUILTIN_AlphaClip
        #define _AlphaClip _BUILTIN_AlphaClip
        #endif
        #ifdef _BUILTIN_ALPHAPREMULTIPLY_ON
        #define _ALPHAPREMULTIPLY_ON _BUILTIN_ALPHAPREMULTIPLY_ON
        #endif
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Shim/Shims.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/LegacySurfaceVertex.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/ShaderGraphFunctions.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
             float3 WorldSpacePosition;
             float3 TimeParameters;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _Depth;
        float _DepthFalloff;
        float4 _ShoreColor;
        float4 _Color;
        float _FoamShoreWidth;
        float4 _FoamColor;
        float _FoamDepth;
        float _FoamFalloff;
        float _WaveIntensity;
        float _WaveSpeed;
        float _Float;
        float _Metal;
        float4 _NormalTexture_TexelSize;
        float4 _NormalTexture_ST;
        float _NormalStrenght;
        CBUFFER_END
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_NormalTexture);
        SAMPLER(sampler_NormalTexture);
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Includes
        // GraphIncludes: <None>
        
        // Graph Functions
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float(float A, float B, out float Out)
        {
            Out = A + B;
        }
        
        void Unity_Sine_float(float In, out float Out)
        {
            Out = sin(In);
        }
        
        void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A + B;
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            float _Property_3e04952c468843ab8933b2692cf6bacd_Out_0 = _WaveIntensity;
            float3 _Vector3_a18246eb0d944cbe92ba8ab4df244f74_Out_0 = float3(0, _Property_3e04952c468843ab8933b2692cf6bacd_Out_0, 0);
            float _Property_e189c9961bde4d4a80a0c20b6a92503b_Out_0 = _WaveSpeed;
            float _Multiply_c13c13771f8e4aa3a00a44ac4ff11002_Out_2;
            Unity_Multiply_float_float(_Property_e189c9961bde4d4a80a0c20b6a92503b_Out_0, IN.TimeParameters.x, _Multiply_c13c13771f8e4aa3a00a44ac4ff11002_Out_2);
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_R_1 = IN.WorldSpacePosition[0];
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_G_2 = IN.WorldSpacePosition[1];
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_B_3 = IN.WorldSpacePosition[2];
            float _Split_9172b35d396b4f6da3213c0bcd4ecb96_A_4 = 0;
            float _Add_f6078872ccf44f37bfbcc10e7e23224f_Out_2;
            Unity_Add_float(_Split_9172b35d396b4f6da3213c0bcd4ecb96_R_1, _Split_9172b35d396b4f6da3213c0bcd4ecb96_B_3, _Add_f6078872ccf44f37bfbcc10e7e23224f_Out_2);
            float _Add_6e66df41b5444def8731ecb95ab6afe3_Out_2;
            Unity_Add_float(_Multiply_c13c13771f8e4aa3a00a44ac4ff11002_Out_2, _Add_f6078872ccf44f37bfbcc10e7e23224f_Out_2, _Add_6e66df41b5444def8731ecb95ab6afe3_Out_2);
            float _Sine_1dfa4e4673a14844bdcb53c2ff481fe7_Out_1;
            Unity_Sine_float(_Add_6e66df41b5444def8731ecb95ab6afe3_Out_2, _Sine_1dfa4e4673a14844bdcb53c2ff481fe7_Out_1);
            float3 _Multiply_1fd709eee6bd43a5adf15627c9e1556b_Out_2;
            Unity_Multiply_float3_float3(_Vector3_a18246eb0d944cbe92ba8ab4df244f74_Out_0, (_Sine_1dfa4e4673a14844bdcb53c2ff481fe7_Out_1.xxx), _Multiply_1fd709eee6bd43a5adf15627c9e1556b_Out_2);
            float3 _Add_c55b332417574d1495a47eee31203ddc_Out_2;
            Unity_Add_float3(IN.ObjectSpacePosition, _Multiply_1fd709eee6bd43a5adf15627c9e1556b_Out_2, _Add_c55b332417574d1495a47eee31203ddc_Out_2);
            description.Position = _Add_c55b332417574d1495a47eee31203ddc_Out_2;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            surface.Alpha = 1;
            surface.AlphaClipThreshold = 0.5;
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
            output.WorldSpacePosition =                         TransformObjectToWorld(input.positionOS);
            output.TimeParameters =                             _TimeParameters.xyz;
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
            
        
        
        
        
        
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        void BuildAppDataFull(Attributes attributes, VertexDescription vertexDescription, inout appdata_full result)
        {
            result.vertex     = float4(attributes.positionOS, 1);
            result.tangent    = attributes.tangentOS;
            result.normal     = attributes.normalOS;
            result.vertex     = float4(vertexDescription.Position, 1);
            result.normal     = vertexDescription.Normal;
            result.tangent    = float4(vertexDescription.Tangent, 0);
            #if UNITY_ANY_INSTANCING_ENABLED
            #endif
        }
        
        void VaryingsToSurfaceVertex(Varyings varyings, inout v2f_surf result)
        {
            result.pos = varyings.positionCS;
            // World Tangent isn't an available input on v2f_surf
        
        
            #if UNITY_ANY_INSTANCING_ENABLED
            #endif
            #if !defined(LIGHTMAP_ON)
            #if UNITY_SHOULD_SAMPLE_SH
            #endif
            #endif
            #if defined(LIGHTMAP_ON)
            #endif
            #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
                result.fogCoord = varyings.fogFactorAndVertexLight.x;
                COPY_TO_LIGHT_COORDS(result, varyings.fogFactorAndVertexLight.yzw);
            #endif
        
            DEFAULT_UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(varyings, result);
        }
        
        void SurfaceVertexToVaryings(v2f_surf surfVertex, inout Varyings result)
        {
            result.positionCS = surfVertex.pos;
            // viewDirectionWS is never filled out in the legacy pass' function. Always use the value computed by SRP
            // World Tangent isn't an available input on v2f_surf
        
            #if UNITY_ANY_INSTANCING_ENABLED
            #endif
            #if !defined(LIGHTMAP_ON)
            #if UNITY_SHOULD_SAMPLE_SH
            #endif
            #endif
            #if defined(LIGHTMAP_ON)
            #endif
            #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
                result.fogFactorAndVertexLight.x = surfVertex.fogCoord;
                COPY_FROM_LIGHT_COORDS(result.fogFactorAndVertexLight.yzw, surfVertex);
            #endif
        
            DEFAULT_UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(surfVertex, result);
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/DepthOnlyPass.hlsl"
        
        ENDHLSL
        }
    }
    CustomEditorForRenderPipeline "UnityEditor.Rendering.BuiltIn.ShaderGraph.BuiltInLitGUI" ""
    CustomEditor "UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI"
    FallBack "Hidden/Shader Graph/FallbackError"
}