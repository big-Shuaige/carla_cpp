// Copyright 1998-2017 Epic Games, Inc. All Rights Reserved.

using System;
using System.IO;
using UnrealBuildTool;

public class Carla : ModuleRules
{
  // 控制是否启用不同模块的布尔值
  bool UsingCarSim = false;
  bool UsingChrono = false;
  bool UsingPytorch = false;
  bool UsingRos2 = false;

  // 检查目标平台是否为 Windows
  private bool IsWindows(ReadOnlyTargetRules Target)
  {
    return (Target.Platform == UnrealTargetPlatform.Win64) || (Target.Platform == UnrealTargetPlatform.Win32);
  }

  // 构造函数，接受一个只读目标规则参数
  public Carla(ReadOnlyTargetRules Target) : base(Target)
  {
  
    // 设置私有PCH（预编译头文件）
    PrivatePCHHeaderFile = "Carla.h";

    // 如果目标平台是Windows，启用异常处理
    if (IsWindows(Target))
    {
      bEnableExceptions = true;
    }

    // 读取配置文件，检查是否启用各种模块
    string CarlaPluginPath = Path.GetFullPath( ModuleDirectory );
    string ConfigDir =  Path.GetFullPath(Path.Combine(CarlaPluginPath, "../../../../Config/"));
    string OptionalModulesFile = Path.Combine(ConfigDir, "OptionalModules.ini");

    // 读取配置文件中的每一行
    string[] text = System.IO.File.ReadAllLines(OptionalModulesFile);
    foreach (string line in text)
    {
      if (line.Contains("CarSim ON"))
      {
        // 检查是否启用 CarSim
        Console.WriteLine("Enabling carsim");
        UsingCarSim = true;
        PublicDefinitions.Add("WITH_CARSIM"); // 向公共定义添加WITH_CARSIM
        PrivateDefinitions.Add("WITH_CARSIM"); // 向私有定义添加WITH_CARSIM
      }
      if (line.Contains("Chrono ON"))
      {
        Console.WriteLine("Enabling chrono");
        UsingChrono = true;
        PublicDefinitions.Add("WITH_CHRONO"); // 向公共定义添加WITH_CHRONO
        PrivateDefinitions.Add("WITH_CHRONO"); // 向私有定义添加WITH_CHRONO
      }
      if (line.Contains("Pytorch ON"))
      {
        Console.WriteLine("Enabling pytorch");
        UsingPytorch = true;
        PublicDefinitions.Add("WITH_PYTORCH"); // 向公共定义添加WITH_PYTORCH
        PrivateDefinitions.Add("WITH_PYTORCH"); // 向私有定义添加WITH_PYTORCH
      }

      if (line.Contains("Ros2 ON"))
      {
        Console.WriteLine("Enabling ros2");
        UsingRos2 = true;
        PublicDefinitions.Add("WITH_ROS2"); // 向公共定义添加WITH_ROS2
        PrivateDefinitions.Add("WITH_ROS2"); // 向私有定义添加WITH_ROS2
      }
    }

    // 添加公共包含路径
    PublicIncludePaths.AddRange(
      new string[] {
        // ... add public include paths required here ...
      }
      );

    // 添加私有包含路径
    PrivateIncludePaths.AddRange(
      new string[] {
        // ... 添加所需的其他私有包含路径 ...
      }
      );

    PublicDependencyModuleNames.AddRange(
      new string[]
      {
        "Core",
        "RenderCore",
        "RHI",
        "Renderer",
        "ProceduralMeshComponent",
        "MeshDescription"
        // ... add other public dependencies that you statically link with here ...
      }
      );
      
    // 如果启用了 CarSim，则添加相关依赖
    if (UsingCarSim)
    {
      PublicDependencyModuleNames.AddRange(new string[] { "CarSim" });
    }
    
	// 如果目标类型是编辑器，则添加 UnrealEd 依赖
	 if (Target.Type == TargetType.Editor)
	 {
		PublicDependencyModuleNames.AddRange(new string[] { "UnrealEd" });
	 }

    // 添加私有依赖模块
    PrivateDependencyModuleNames.AddRange(
      new string[]
      {
        "AIModule",
        "AssetRegistry",
        "CoreUObject",
        "Engine",
        "Foliage",
        "HTTP",
        "StaticMeshDescription",
        "ImageWriteQueue",
        "Json",
        "JsonUtilities",
        "Landscape",
        "PhysX",
        "PhysXVehicles",
        "PhysXVehicleLib",
        "Slate",
        "SlateCore",
        "PhysicsCore"
        // ... add private dependencies that you statically link with here ...
      }
      );
    if (UsingCarSim)
    {
      PrivateDependencyModuleNames.AddRange(new string[] { "CarSim" });
      PrivateIncludePathModuleNames.AddRange(new string[] { "CarSim" });
    }

    // 添加动态加载的模块
    DynamicallyLoadedModuleNames.AddRange(
      new string[]
      {
        // ... add any modules that your module loads dynamically here ...
      }
      );

    AddCarlaServerDependency(Target);
  }

  private bool UseDebugLibs(ReadOnlyTargetRules Target)
  {
    if (IsWindows(Target))
    {
      // 在Windows上，即使在调试模式下，Unreal也使用Release C++运行时（CRT），
      // 所以除非我们重新编译引擎，否则无法链接调试库。
      return false;
    }
    else
    {
      return false;
    }
  }
  
   // 添加动态库
  private void AddDynamicLibrary(string library)
  {
    PublicAdditionalLibraries.Add(library);
    RuntimeDependencies.Add(library);
    PublicDelayLoadDLLs.Add(library);
  }
  private void AddDllDependency(string PathToFolder, string DllName)
  {
    string Source = Path.Combine(PathToFolder, DllName);
    string Destination = Path.Combine("$(BinaryOutputDir)", DllName);
    RuntimeDependencies.Add(Destination, Source);
  }

  delegate string ADelegate(string s);

  private void AddBoostLibs(string LibPath)
  {
    string [] files = Directory.GetFiles(LibPath, "*boost*.lib");
    foreach (string file in files) PublicAdditionalLibraries.Add(file);
  }

  // 添加 Carla 服务器依赖
  private void AddCarlaServerDependency(ReadOnlyTargetRules Target)
  {
    string LibCarlaInstallPath = Path.GetFullPath(Path.Combine(ModuleDirectory, "../../CarlaDependencies"));

    // 委托，用于获取库名称
    ADelegate GetLibName = (string BaseName) => {
      if (IsWindows(Target))
      {
        return BaseName + ".lib";
      }
      else
      {
        return "lib" + BaseName + ".a";
      }
    };

    // 根据目标平台链接依赖库
    if (IsWindows(Target))
    {
    
      // 从指定路径添加Boost库
      AddBoostLibs(Path.Combine(LibCarlaInstallPath, "lib"));
      PublicAdditionalLibraries.Add(Path.Combine(LibCarlaInstallPath, "lib", GetLibName("rpc")));

      if (UseDebugLibs(Target))
      {
        PublicAdditionalLibraries.Add(Path.Combine(LibCarlaInstallPath, "lib", GetLibName("carla_server_debug")));
      }
      else
      {
        PublicAdditionalLibraries.Add(Path.Combine(LibCarlaInstallPath, "lib", GetLibName("carla_server")));
      }
      
      // 如果使用Chrono库，则添加其依赖项
      if (UsingChrono)
      {
        PublicAdditionalLibraries.Add(Path.Combine(LibCarlaInstallPath, "lib", GetLibName("ChronoEngine")));
        PublicAdditionalLibraries.Add(Path.Combine(LibCarlaInstallPath, "lib", GetLibName("ChronoEngine_vehicle")));
        PublicAdditionalLibraries.Add(Path.Combine(LibCarlaInstallPath, "lib", GetLibName("ChronoModels_vehicle")));
        PublicAdditionalLibraries.Add(Path.Combine(LibCarlaInstallPath, "lib", GetLibName("ChronoModels_robot")));
        AddDllDependency(Path.Combine(LibCarlaInstallPath, "dll"), "ChronoEngine.dll");
        AddDllDependency(Path.Combine(LibCarlaInstallPath, "dll"), "ChronoEngine_vehicle.dll");
        AddDllDependency(Path.Combine(LibCarlaInstallPath, "dll"), "ChronoModels_vehicle.dll");
        AddDllDependency(Path.Combine(LibCarlaInstallPath, "dll"), "ChronoModels_robot.dll");
        bUseRTTI = true;// 启用运行时类型信息
      }

      // OsmToODR集成的依赖项
      PublicAdditionalLibraries.Add(Path.Combine(LibCarlaInstallPath, "lib", "sqlite3.lib"));
      PublicAdditionalLibraries.Add(Path.Combine(LibCarlaInstallPath, "lib", "xerces-c_3.lib"));
      PublicAdditionalLibraries.Add(Path.Combine(LibCarlaInstallPath, "lib", "proj.lib"));
      PublicAdditionalLibraries.Add(Path.Combine(LibCarlaInstallPath, "lib", "osm2odr.lib"));
      PublicAdditionalLibraries.Add(Path.Combine(LibCarlaInstallPath, "lib", "zlibstatic.lib"));
    }
    else
    {
      // 对于非Windows平台的类似库链接过程
      PublicAdditionalLibraries.Add(Path.Combine(LibCarlaInstallPath, "lib", GetLibName("rpc")));
      if (UseDebugLibs(Target))
      {
        PublicAdditionalLibraries.Add(Path.Combine(LibCarlaInstallPath, "lib", GetLibName("carla_server_debug")));
      }
      else
      {
        PublicAdditionalLibraries.Add(Path.Combine(LibCarlaInstallPath, "lib", GetLibName("carla_server")));
      }
      if (UsingChrono)
      {
        AddDynamicLibrary(Path.Combine(LibCarlaInstallPath, "lib", "libChronoEngine.so"));
        AddDynamicLibrary(Path.Combine(LibCarlaInstallPath, "lib", "libChronoEngine_vehicle.so"));
        AddDynamicLibrary(Path.Combine(LibCarlaInstallPath, "lib", "libChronoModels_vehicle.so"));
        AddDynamicLibrary(Path.Combine(LibCarlaInstallPath, "lib", "libChronoModels_robot.so"));

        bUseRTTI = true;
      }

      // 检查是否使用PyTorch并添加其库
      if (UsingPytorch)
      {
        PublicAdditionalLibraries.Add(Path.Combine(LibCarlaInstallPath, "lib", GetLibName("carla_pytorch")));

        string LibTorchPath = LibCarlaInstallPath;
	
	// 添加各种需要的PyTorch库
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libonnx_proto.a"));
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libfbgemm.a"));
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libgloo.a"));
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libXNNPACK.a"));
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libprotobuf-lite.a"));
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libprotobuf.a"));
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libasmjit.a"));
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libcpuinfo_internals.a"));
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libclog.a"));
        // PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libbreakpad_common.a"));
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libbenchmark.a"));
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libtensorpipe.a"));
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libpytorch_qnnpack.a"));
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libtensorpipe_cuda.a"));
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libnnpack_reference_layers.a"));
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libgmock.a"));
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libdnnl.a"));
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libpthreadpool.a"));
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libcpuinfo.a"));
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libqnnpack.a"));
        // PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libbreakpad.a"));
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libkineto.a"));
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libprotoc.a"));
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libgtest.a"));
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libgmock_main.a"));
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libgtest_main.a"));
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libbenchmark_main.a"));
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libfmt.a"));
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libtensorpipe_uv.a"));
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libfoxi_loader.a"));
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libgloo_cuda.a"));
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libnnpack.a"));
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libcaffe2_protos.a"));
        PublicAdditionalLibraries.Add(Path.Combine(LibTorchPath, "lib", "libonnx.a"));
        AddDynamicLibrary(Path.Combine(LibTorchPath, "lib", "libtorch.so"));
        AddDynamicLibrary(Path.Combine(LibTorchPath, "lib", "libnnapi_backend.so"));
        AddDynamicLibrary(Path.Combine(LibTorchPath, "lib", "libbackend_with_compiler.so"));
        AddDynamicLibrary(Path.Combine(LibTorchPath, "lib", "libcaffe2_nvrtc.so"));
        AddDynamicLibrary(Path.Combine(LibTorchPath, "lib", "libtorch_cuda_cpp.so"));
        AddDynamicLibrary(Path.Combine(LibTorchPath, "lib", "libc10_cuda.so"));
        AddDynamicLibrary(Path.Combine(LibTorchPath, "lib", "libtorchbind_test.so"));
        AddDynamicLibrary(Path.Combine(LibTorchPath, "lib", "libjitbackend_test.so"));
        AddDynamicLibrary(Path.Combine(LibTorchPath, "lib", "libc10.so"));
        AddDynamicLibrary(Path.Combine(LibTorchPath, "lib", "libtorch_cuda.so"));
        AddDynamicLibrary(Path.Combine(LibTorchPath, "lib", "libtorch_global_deps.so"));
        AddDynamicLibrary(Path.Combine(LibTorchPath, "lib", "libtorch_cpu.so"));
        AddDynamicLibrary(Path.Combine(LibTorchPath, "lib", "libshm.so"));
        AddDynamicLibrary(Path.Combine(LibTorchPath, "lib", "libtorch_cuda_cu.so"));
        AddDynamicLibrary(Path.Combine(LibTorchPath, "lib", "libtorchscatter.so"));
        AddDynamicLibrary(Path.Combine(LibTorchPath, "lib", "libtorchcluster.so"));
        // AddDynamicLibrary("/usr/local/cuda/lib64/stubs/libcuda.so");
        // AddDynamicLibrary("/usr/local/cuda/lib64/libnvrtc.so");
        // AddDynamicLibrary("/usr/local/cuda/lib64/libnvToolsExt.so");
        // AddDynamicLibrary("/usr/local/cuda/lib64/libcudart.so");
        // AddDynamicLibrary("/usr/lib/llvm-10/lib/libgomp.so");
        PublicAdditionalLibraries.Add("/usr/local/cuda/lib64/stubs/libcuda.so");
        PublicAdditionalLibraries.Add("/usr/local/cuda/lib64/libnvrtc.so");
        PublicAdditionalLibraries.Add("/usr/local/cuda/lib64/libnvToolsExt.so");
        PublicAdditionalLibraries.Add("/usr/local/cuda/lib64/libcudart.so");
        PublicAdditionalLibraries.Add("/usr/lib/llvm-10/lib/libgomp.so");
        RuntimeDependencies.Add(Path.Combine(LibTorchPath, "lib", "libcudart-a7b20f20.so.11.0"));
        RuntimeDependencies.Add(Path.Combine(LibTorchPath, "lib", "libgomp-a34b3233.so.1"));
        RuntimeDependencies.Add(Path.Combine(LibTorchPath, "lib", "libnvrtc-builtins-4730a239.so.11.3"));
        RuntimeDependencies.Add(Path.Combine(LibTorchPath, "lib", "libnvrtc-1ea278b5.so.11.2"));
        RuntimeDependencies.Add(Path.Combine(LibTorchPath, "lib", "libnvToolsExt-24de1d56.so.1"));
        PublicAdditionalLibraries.Add("stdc++");
        PublicAdditionalLibraries.Add("/usr/lib/x86_64-linux-gnu/libpython3.9.so");
      }

      if (UsingRos2)
      {
        PublicAdditionalLibraries.Add(Path.Combine(LibCarlaInstallPath, "lib", GetLibName("carla_fastdds")));

        string LibFastDDSPath = LibCarlaInstallPath;
        AddDynamicLibrary(Path.Combine(LibFastDDSPath, "lib", "libfoonathan_memory-0.7.3.so"));
        AddDynamicLibrary(Path.Combine(LibFastDDSPath, "lib", "libfastcdr.so"));
        AddDynamicLibrary(Path.Combine(LibFastDDSPath, "lib", "libfastrtps.so"));
        PublicAdditionalLibraries.Add("stdc++");//确保程序能正确使用C++标准库相关功能
      }


      // 非Windows平台的OsmToODR集成依赖项
      PublicAdditionalLibraries.Add("/usr/lib/x86_64-linux-gnu/libc.so");
      PublicAdditionalLibraries.Add(Path.Combine(LibCarlaInstallPath, "lib", "libsqlite3.so"));
      PublicAdditionalLibraries.Add(Path.Combine(LibCarlaInstallPath, "lib", "libxerces-c.a"));
      PublicAdditionalLibraries.Add(Path.Combine(LibCarlaInstallPath, "lib", "libproj.a"));
      PublicAdditionalLibraries.Add(Path.Combine(LibCarlaInstallPath, "lib", "libosm2odr.a"));

    }
    bEnableExceptions = true;// 启用代码中的异常处理

    // 包含路径
    string LibCarlaIncludePath = Path.Combine(LibCarlaInstallPath, "include");

    PublicIncludePaths.Add(LibCarlaIncludePath);
    PrivateIncludePaths.Add(LibCarlaIncludePath);

    PublicDefinitions.Add("ASIO_NO_EXCEPTIONS");
    PublicDefinitions.Add("BOOST_NO_EXCEPTIONS");
    PublicDefinitions.Add("LIBCARLA_NO_EXCEPTIONS");
    PublicDefinitions.Add("PUGIXML_NO_EXCEPTIONS");
    PublicDefinitions.Add("BOOST_DISABLE_ABI_HEADERS");
    PublicDefinitions.Add("BOOST_TYPE_INDEX_FORCE_NO_RTTI_COMPATIBILITY");
  }
}
