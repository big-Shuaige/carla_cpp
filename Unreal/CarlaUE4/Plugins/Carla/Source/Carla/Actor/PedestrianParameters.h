// 版权信息，指出这段代码属于2017年巴塞罗那自治大学计算机视觉中心(CVC)的作品，
// 并按照MIT许可证授权。MIT许可证的副本可以在<https://opensource.org/licenses/MIT>找到。
 
#pragma once // 防止头文件被重复包含

#include "GameFramework/Character.h" // 包含Unreal Engine的Character类，用于定义游戏中的角色
#include "PedestrianParameters.generated.h" // 自动生成的头文件，通常用于Unreal Engine的反射系统

// 定义一个枚举类，用于表示行人的性别
UENUM(BlueprintType) // 标记这个枚举可以在蓝图(Unreal Engine的可视化脚本系统)中使用
enum class EPedestrianGender : uint8
{
  Other       UMETA(DisplayName = "Other"), // 其他性别
  Female      UMETA(DisplayName = "Female"), // 女性
  Male        UMETA(DisplayName = "Male"), // 男性
  
  SIZE        UMETA(Hidden), // 隐藏属性，用于枚举大小
  INVALID     UMETA(Hidden) // 隐藏属性，表示无效值
};

// 定义一个枚举类，用于表示行人的年龄
UENUM(BlueprintType)
enum class EPedestrianAge : uint8
{
  Child       UMETA(DisplayName = "Child"), // 儿童
  Teenager    UMETA(DisplayName = "Teenager"), // 青少年
  Adult       UMETA(DisplayName = "Adult"), // 成人
  Elderly     UMETA(DisplayName = "Elderly"), // 老年人
  SIZE        UMETA(Hidden), // 隐藏属性，用于枚举大小
  INVALID     UMETA(Hidden) // 隐藏属性，表示无效值
};

// 定义一个结构体，用于表示行人的参数
USTRUCT(BlueprintType)
struct CARLA_API FPedestrianParameters
{
  GENERATED_BODY() // 宏，用于自动生成代码以支持Unreal Engine的反射系统

  // 行人的唯一标识符
  UPROPERTY(EditAnywhere, BlueprintReadWrite)
  FString Id; // 字符串类型的成员变量，用于存储行人ID

  // 行人所属的类，必须是ACharacter的子类
  UPROPERTY(EditAnywhere, BlueprintReadWrite)
  TSubclassOf<ACharacter> Class; // UClass类型的成员变量，用于存储行人类

  // 行人的性别
  UPROPERTY(EditAnywhere, BlueprintReadWrite)
  EPedestrianGender Gender = EPedestrianGender::Other; // 枚举类型的成员变量，默认值为Other

  // 行人的年龄
  UPROPERTY(EditAnywhere, BlueprintReadWrite)
  EPedestrianAge Age = EPedestrianAge::Adult; // 枚举类型的成员变量，默认值为Adult

  // 行人的速度数组，可能用于表示不同情境下的速度
  UPROPERTY(EditAnywhere, BlueprintReadWrite)
  TArray<float> Speed; // 浮点数数组，用于存储速度值

  // 行人的生成代，可能用于版本控制或区分不同的行人实例
  UPROPERTY(EditAnywhere, BlueprintReadWrite)
  int32 Generation = 0; // 整型成员变量，默认值为0

  // 行人是否可以使用轮椅
  UPROPERTY(EditAnywhere, BlueprintReadWrite)
  bool bCanUseWheelChair = false; // 布尔类型的成员变量，默认值为false
};
