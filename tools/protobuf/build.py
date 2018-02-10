#-*- coding: utf-8
import sys, os, platform, pycsharpmake
__script_path__ = os.path.dirname(os.path.abspath(__file__))
__project_path__ = os.path.dirname(os.path.dirname(__script_path__))
__assets_path__ = os.path.join(__project_path__, 'Assets')
__plugins_path__ = os.path.join(__assets_path__, 'Plugins')

## Fixme
allProtoFiles = { 'client/*.proto', 'common/*.proto' }

protoDir = os.path.join(__script_path__, 'protos')
protoFileName = ' '.join({os.path.join(protoDir, x) for x in allProtoFiles})
makeFileName = os.path.join(__script_path__, 'protos_makefile.yml')

windows = platform.system().startswith("Windows")

if windows:
	exe = '%s/bin/protoc.exe --proto_path=%s --csharp_out=src/Protos %s' % (__script_path__, protoDir, protoFileName)
else:
	exe = '%s/bin/protoc --proto_path=%s --csharp_out=src/Protos %s' % (__script_path__, protoDir, protoFileName)

if os.system(exe) != 0:
	raise Exception('run protoc failed')

makefile = pycsharpmake.Makefile()
makefile.make(makeFileName, __plugins_path__, __script_path__)
