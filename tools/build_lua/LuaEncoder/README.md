不同平台使用的编码器不同

|encoder|platform|luajit version|tolua version| source |
|------|-------|-------|------|
|luajit|win32, android| [luajit2.1-beta3](http://luajit.org/) | 1.0.7.349| [tolua](https://github.com/topameng/tolua/tree/master/Luajit)
|luajit64|win64, android, ios| [luajit2.1-beta3](http://luajit.org/) | 1.0.7.349| [tolua](https://github.com/topameng/tolua/tree/master/Luajit)
|luavm|macos|luac(for u5.x) | 1.0.5.203| [LuaFramework_UGUI](https://github.com/jarjin/LuaFramework_UGUI/tree/master/LuaEncoder)|
|luajit_ios| ios, android | [luajit2.1-beta2](http://luajit.org/) | 1.0.6.311| `cd luajit2.1-beta2 && make` for x64 `make CC="gcc -m32"` for x86|
