// Copyright (c) 2023 Computer Vision Center (CVC) at the Universitat Autonoma
// de Barcelona (UAB).
//
// This work is licensed under the terms of the MIT license.
// For a copy, see <https://opensource.org/licenses/MIT>.

#include "Containers/UnrealString.h" 
#pragma once

#if WITH_EDITORONLY_DATA

// 注意：从地图 FBX 导入的资产将进行语义分类
// 根据资产名称进行分割，分为道路（ROAD）、道路标线（ROADLINES）和地形（TERRAIN）
// 在RoadRunner中定义。这些标签将用于移动网格
// 并在在世界中生成这些网格时指定它们的路径
namespace SSTags {
  // carla语义分割文件夹标签
  static const FString ROAD       = TEXT("Road");
  static const FString ROADLINE   = TEXT("RoadLine");
  static const FString TERRAIN    = TEXT("Terrain");
  static const FString GRASS      = TEXT("Terrain");
  static const FString SIDEWALK   = TEXT("SideWalk");
  static const FString CURB       = TEXT("SideWalk");
  static const FString GUTTER     = TEXT("SideWalk");
//
  //RoadRunner 标签
  //
  //RoadRunner 的网格命名约定：
  // mapName_meshType_meshSubtype_layerNumberNode
  //
  // meshType 是一个较大的地理标签（例如，“道路”、“地形”）
  // meshSubType 是标签的一种名称（例如：“道路”、“排水沟”、“地面”）
  static const FString R_ROAD1     = TEXT("Road_Road");
  static const FString R_ROAD2     = TEXT("Roads_Road");
  static const FString R_GRASS1    = TEXT("Road_Grass");
  static const FString R_GRASS2    = TEXT("Roads_Grass");
  static const FString R_MARKING1  = TEXT("Road_Marking");
  static const FString R_MARKING2  = TEXT("Roads_Marking");
  static const FString R_SIDEWALK1 = TEXT("Road_Sidewalk");
  static const FString R_SIDEWALK2 = TEXT("Roads_Sidewalk");
  static const FString R_CURB1     = TEXT("Road_Curb");
  static const FString R_CURB2     = TEXT("Roads_Curb");
  static const FString R_GUTTER1   = TEXT("Road_Gutter");
  static const FString R_GUTTER2   = TEXT("Roads_Gutter");
  static const FString R_TERRAIN   = TEXT("Terrain");
}

#endif
