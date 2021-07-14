# HTTP 请求敏感信息检查插件

## Fiddler 插件安装

将所有生成目录中的文件，拷贝到 Fidddler 的插件目录中。

`%USERPROFILE%\Documents\Fiddler2\Scripts`

## 插件的使用 - 快速开始

在配置界面中，定义需要匹配的 Host，添加一条规则，如

``` bash
匹配模式：字符串；pattern 值：17712341234
命中提示：出现手机号
```

开启检测，使用对应的账号登录软件并使用，当 HTTP 请求或响应中出现指定的字符串，则会给出提示。

![](img/2021-07-14-11-18-22.png)

## 插件开发

[JasonGrass/Fiddler.Plugin.SDK: Fiddler 插件开发 SDK](https://github.com/JasonGrass/Fiddler.Plugin.SDK )
[Fiddler 插件开发，使用 WPF 作为 UI 控件 - J.晒太阳的猫 - 博客园](https://www.cnblogs.com/jasongrass/p/12039575.html )

WPF UI 库  
[HandyOrg/HandyControl: Contains some simple and commonly used WPF controls](https://github.com/HandyOrg/HandyControl )
