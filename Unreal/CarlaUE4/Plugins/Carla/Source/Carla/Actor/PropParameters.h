// Copyright (c) 2017 Computer Vision Center (CVC) at the Universitat Autonoma
// de Barcelona (UAB).
//
// This work is licensed under the terms of the MIT license.
// For a copy, see <https://opensource.org/licenses/MIT>.

#pragma once // 指示编译器这个头文件应该只被包含一次，防止在多个文件中重复包含

#include "Engine/StaticMesh.h" // 包含Unreal Engine中静态网格的类定义，用于处理3D模型

#include "PropParameters.generated.h" // 包含Unreal Engine自动生成的代码，用于支持UStruct的反射和蓝图系统

// 定义一个枚举类EPropSize，用于表示道具的大小。这个枚举类在蓝图系统中可用。
UENUM(BlueprintType)
enum class EPropSize : uint8
{
  // 枚举值表示道具的大小类别
  Tiny        UMETA(DisplayName = "Tiny",   ToolTip = "Smaller than a mailbox"), // 极小，比邮筒还小
  Small       UMETA(DisplayName = "Small",  ToolTip = "Size of a mailbox"),  // 小型，邮筒大小
  Medium      UMETA(DisplayName = "Medium", ToolTip = "Size of a human"),   // 中型，人类大小
  Big         UMETA(DisplayName = "Big",    ToolTip = "Size of a bus stop"), // 大型，公交站大小
  Huge        UMETA(DisplayName = "Huge",   ToolTip = "Size of a house or bigger"), // 巨大，房屋大小或更大

  SIZE        UMETA(Hidden), // 隐藏属性，用于获取枚举项的数量，不显示在蓝图编辑器中
  INVALID     UMETA(DisplayName = "INVALID") // 表示无效或未设置的道具大小
};

// 定义一个结构体FPropParameters，用于存储道具的参数。这个结构体在蓝图系统中可用。
USTRUCT(BlueprintType)
struct CARLA_API FPropParameters
{
  GENERATED_BODY() // 指示Unreal Engine为这个结构体生成必要的代码，以支持反射和蓝图系统

  // 道具的名称，可以在关卡编辑器或蓝图中设置和获取
  UPROPERTY(EditAnywhere, BlueprintReadWrite)
  FString Name;

  // 道具使用的静态网格，用于定义道具的3D模型
  UPROPERTY(EditAnywhere, BlueprintReadWrite)
  UStaticMesh *Mesh;

  // 道具的大小，影响其在游戏中的显示和交互
  UPROPERTY(EditAnywhere, BlueprintReadWrite)
  EPropSize Size = EPropSize::INVALID; // 默认值为INVALID，表示大小尚未设置
};
