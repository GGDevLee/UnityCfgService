# CfgService

**联系作者：419731519（QQ）**

### ============CfgService介绍============
#### 日常的游戏开发过程中，经常需要用到各种的游戏配置，既要满足开发者的程序需求，又要满足策划的文案需求
#### 所以笔者这里完全封装了一套完整的配置服务，以满足开发者跟策划的全部配置需求
#### 兼容：Json，Xml，Csv，Excel，ProtoBuf，二进制等格式
#### 编辑器直接通过类生成的配置文件，也可直接将批量的配置文件转换成二进制文件，以提升更高的读取效率
#### 游戏运行时，仅一行代码，即可以读取各种类型的配置文件，配合[LeeFramework](https://gitee.com/GameDevLee/LeeFramework)使用，可更加方便！
#### 觉得我的插件能帮助到你，麻烦帮我点个Star支持一下❤️

### =================使用方法=================
- manifest.json中添加插件路径
```json
{
  "dependencies": {
	"com.leeframework.cfgservice":"https://e.coding.net/ggdevlee/leeframework/CfgService.git#1.0.0"
  }
}
```

- 引入命名空间
```csharp
using LeeFramework.Cfg;
```

<br />

####  所有服务：
#### 【[Csv服务](https://github.com/GGDevLee/UnityCfgService/blob/main/Document/Csv.md)】
#### 【[Excel服务](https://github.com/GGDevLee/UnityCfgService/blob/main/Document/Excel.md)】
#### 【[Json服务](https://github.com/GGDevLee/UnityCfgService/blob/main/Document/Json.md)】
#### 【[Xml服务](https://github.com/GGDevLee/UnityCfgService/blob/main/Document/Xml.md)】
#### 【[Binary服务](https://github.com/GGDevLee/UnityCfgService/blob/main/Document/Binary.md)】
#### 【[ProtoBuf服务](https://github.com/GGDevLee/UnityCfgService/blob/main/Document/ProtoBuf.md)】
