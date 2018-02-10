#-*- coding: utf-8
import sys, os, yaml, shutil, pycsharpmake
# from excelconverter import convertJson2Xlsx
__script_path__ = os.path.dirname(os.path.abspath(__file__))
__project_path__ = os.path.dirname(os.path.dirname(__script_path__))
__assets_path__ = os.path.join(__project_path__, 'Assets')
codegenMakefileName = os.path.join(__script_path__, 'codegen_makefile.yml')

def printStep(name):
	print('\n')
	print(name)
	print('--------------------------------')

if __name__ == "__main__":
	# 编译codegen
	printStep('compile codegen')
	makefile = pycsharpmake.Makefile()
	makefile.make(codegenMakefileName, __script_path__, __script_path__)

	# gen code and json example
	printStep('gen code and json example')
	makefile.run("UF.Config.Config", 'dump.json', debug=True)

