# Carla 的 C++ 文档
注释Carla的 [LibCarla](https://github.com/OpenHUTB/carla_cpp/tree/dev/LibCarla/source/carla) 、[CarlaUE4](https://github.com/OpenHUTB/carla_cpp/tree/dev/Unreal/CarlaUE4/Plugins/Carla/Source/Carla) 模块，包括实现方式、实现原理、流程图等。做出贡献之前请查看 [指南](https://github.com/OpenHUTB/PFC/blob/main/CONTRIBUTING.md) 。

## 生成文档
1.下载并安装[git](https://git-scm.com/downloads)  、[doxygen](https://www.doxygen.nl/download.html) ，可选软件：[GitTortoiseGit](https://tortoisegit.org/download/) 、VS2019社区版；

2.克隆仓库：
```shell
git clone https://github.com/OpenHUTB/carla_cpp.git
```

3.在项目主目录下运行以下命令，根据 C++ 代码生成对应的 html 文档：
```shell script
doxygen
```
进入`carla_cpp\Doxygen\html`目录下，双击或使用浏览器打开`dir_b14cdd661f9a7048a44f1771cd402401.html`、`dir_8fc34afb5f07a67966c78bf5319f94ae.html`或`index.html`，HTML仅仅用于查看，需要改的是.cpp文件并提交.cpp和.h文件。包括的文档：
- LibCarla/source/carla：[Doxygen/html/dir_b14cdd661f9a7048a44f1771cd402401.html](https://openhutb.github.io/carla_cpp/dir_b14cdd661f9a7048a44f1771cd402401.html) 

- CarlaUE4：[Doxygen/html/dir_8fc34afb5f07a67966c78bf5319f94ae.html](https://openhutb.github.io/carla_cpp/dir_8fc34afb5f07a67966c78bf5319f94ae.html) 

4.修改并本地测试没问题后，提交代码：
```shell script
git add .
git commit -m update
git push
```

## 修改方法
### 修改类的注释
1.比如对于`UOpenDriveMap`，修改之前：
![](./img/class_annotated.jpg)

2.找到需要修改的文件：
![](./img/header_path.jpg)

3.翻译代码中的注释或添加新注释：
![](./img/update_annotated.jpg)

4.运行`doxygen`重新生成文档，查看修改后的文档：
![](./img/result.jpg)

详细注释方法参见[注释规范](./annotation_specification.md) 。

## 新建开发分支
直接创建一个空白的分支，而是像普通的分支一样把原分支的内容拷贝过来：
```shell
git checkout --orphan dev
```
还需要手动删除孤立分支中的内容：
```shell
git rm -rf .
```

## 参考

- [Carla中文文档](https://openhutb.github.io/carla_doc/)
- [从源代码构建 Carla](https://openhutb.github.io/carla_doc/build_carla/)
- [在 Windows 上进行 Carla 的调试](https://openhutb.github.io/carla_doc/tuto_D_windows_debug/#cpp_client_debug)
- [dev分支提交历史](https://github.com/carla-simulator/carla/commits/dev/)
- [注释图片在文档里面查看](https://blog.csdn.net/qq_33154343/article/details/102809510)


## 贡献者列表
[nongfugengxia](https://github.com/nongfugengxia) 、[donghaiwang](https://github.com/donghaiwang) 、
[QEYY060506](https://github.com/QEYY060506) 、[Xiedao](https://github.com/Xiedao) 、[zzz479](https://github.com/zzz479) 、[ifthen865](https://github.com/ifthen865) 、[zuo664](https://github.com/zuo664)、[Allen-Tang0014](https://github.com/Allen-Tang0014)、[BcyWind](https://github.com/BcyWind) 、
[Nananxuan](https://github.com/Nananxuan) 、[xiazhimingchen](https://github.com/xiazhimingchen)、[Muyunshui](https://github.com/Muyunshui)、[Wen-511](https://github.com/Wen-511)、[juanexuan](https://github.com/juanexuan)
[jun-72](https://github.com/jun-72)
[xiushuowang](https://github.com/xiushuowang)
[gongguixuan](https://github.com/gongguixuan)[Hyz12345678](https://github.com/Hyz12345678)
[M-3399](https://github.com/M-3399)
[Candela-best](https://github.com/Candela-best)
[karry-tang](https://github.com/karry-tang)
[wenli-xiong](https://github.com/wenli-xiong)、[wwwwu1207](https://github.com/wwwwu1207)
[leixing1](https://github.com/leixing1)
[IHateTheWorld-Zhou](https://github.com/IHateTheWorld-Zhou)
[Mamba321857](https://github.com/Mamba321857)
[gyqss](https://github.com/gyqss)
[liujue0](https://github.com/liujue0)
[yyyyy1026](https://github.com/yyyyy1026)
[sematic9527](https://github.com/sematic9527)
[yuxiyu666](https://github.com/yuxiyu666)
[xjc548](https://github.com/xjc548)
[Yuuydz](https://github.com/Yuuydz)
[zreo141271](https://github.com/zero141271)、[zxy-125](https://github.com/zxy-125)、[fx-hj](https://github.com/fx-hj)
[A-0-A-0-A](https://github.com/A-0-A-0-A) 。
[xuuu-1](https://github.com/xuuu-1) 
[minghao-lee](https://github.com/minghao-lee) 。
[Lhb106](https://github.com/Lhb106)、
[52Herze](https://github.com/52Herze) 
[HuBigmouse](https://github.com/HuBigmouse)
[lujianjjjj](https://github.com/lujianjjjj)
[chen-18](https://github.com/18-chen).
[smile947](https://github.com/smile947).
[cm1106-ai](https://github.com/cm1106-ai).
[pengwenzhuo6](https://github.com/pengwenzhuo6).
[789waa](https://github.com/789waa)
[Rinco520](https://github.com/Rinco520)、[yanpeng0902](https://github.com/yanpeng0902)
[zxxzyy](https://github.com/zxxzyy)
[huangjj114](https://github.com/huangjj114)
[fengchuanyin](https://github.com/fengchuanyin)
[cby2838](https://github.com/cby2838)
[Ekisanhinn](https://github.com/ekisannhinn)
[Seavey0402](https://github.com/Seavey0402)
[yyyyy12306](https://github.com/yyyyy12306)、
[lzh8215](https://github.com/lzh8215)。
[Rita0621](https://github.com/Rita0621)
[zxy953](https://github.com/zxy953)
[MengruChen0723](https://github.com/MengruChen0723)
[lwwwwwwwww9](https://github.com/lwwwwwwwww9).
[bobo339](https://github.com/bobo339)
[hannah-oyeys](https://github.com/hannah-oyeys)
[kthalo](https://github.com/kthalo)
[chenzhentao-chen](https://github.com/chenzhentao-chen)
[jiangyaqin6](https://github.com/jiangyaqin6)
[zhouxiayu698](https://github.com/zhouxiayu698)
[feng569150](https://github.com/feng569150)
[Dipmil](https://github.com/Dipmil)
[li-guanhua](https://github.com/li-guanhua)
[yangh05](https://github.com/yangh05)
[tushuguanL](https://github.com/tushuguanL)
[18692223518](https://github.com/18692223518)
[tellwhat](https://https://github.com/tellwhat)
[Mikecbk](https://https://github.com/Mikecbk)、[ZLW523523](https://github.com/ZLW523523)
[ovsehun](https://https://github.com/ovsehun)
[lyj510](https://github.com/lyj510/carla_cpp)
[YPPBTC](https://github.com/YPPBTC)、[mjyy22](https://github.com/mjyy22)
[DanandVVa](https://github.com/DanandVVa/carla_cpp.git)
[yuanshen6666666](https://github.com/yuanshen6666666/carla_cpp.git)
[big-Shuaige]（https://github.com/big-Shuaige）
