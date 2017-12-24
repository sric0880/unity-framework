#-*- coding: utf-8
import sys, os, platform, pycsharpmake
__script_path__ = os.path.dirname(os.path.abspath(__file__))
__project_path__ = os.path.dirname(os.path.dirname(__script_path__))
__assets_path__ = os.path.join(__project_path__, 'Assets')
__plugins_path__ = os.path.join(__assets_path__, 'Plugins')

makeFileName = os.path.join(__script_path__, 'protobuf_makefile.yml')

makefile = pycsharpmake.Makefile()
makefile.make(makeFileName, __plugins_path__, __script_path__)
