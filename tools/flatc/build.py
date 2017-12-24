#-*- coding: utf-8
import sys, os, platform, pycsharpmake
__script_path__ = os.path.dirname(os.path.abspath(__file__))
__project_path__ = os.path.dirname(os.path.dirname(__script_path__))
__assets_path__ = os.path.join(__project_path__, 'Assets')
__plugins_path__ = os.path.join(__assets_path__, 'Plugins')

fbsFileName = os.path.join(__script_path__, 'gamesave.fbs')
makeFileName = os.path.join(__script_path__, 'makefile.yml')

windows = platform.system().startswith("Windows")

if windows:
	exe = '%s/flatc.exe --csharp --gen-mutable -o src %s' % (__script_path__, fbsFileName)
else:
	exe = '%s/flatc --csharp --gen-mutable -o src %s' % (__script_path__, fbsFileName)

if os.system(exe) != 0:
	raise Exception('run flatc failed')

makefile = pycsharpmake.Makefile()
makefile.make(makeFileName, __plugins_path__, __script_path__)
