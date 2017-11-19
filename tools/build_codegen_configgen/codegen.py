#-*- coding: utf-8
import sys, os, yaml, shutil, pycsharpmake
from excelconverter import convertJson2Xlsx
__script_path__ = os.path.dirname(os.path.abspath(__file__))
__project_path__ = os.path.dirname(os.path.dirname(__script_path__))
__assets_path__ = os.path.join(__project_path__, 'Assets')
autoGenFolder=os.path.join(__assets_path__, 'Configuration/AutoGen')
luaExportDir = os.path.join(__project_path__, 'lua/main/Config')
codegenMakefileName = os.path.join(__script_path__, 'codegen_makefile.yml')
configgenMakefileName = os.path.join(__script_path__, 'configgen_makefile.yml')
pathConfigName = os.path.join(__script_path__, '../pathvars.yml')

with open(pathConfigName, 'r') as stream:
	content = yaml.load(stream)
exampleConfigFolder=os.path.join(__project_path__, content['exampleConfigDir'])

def printStep(name):
	print('\n')
	print(name)
	print('--------------------------------')

# clear generated code folder
# and clear example config folder
def makeAutoGenFolder():
	if os.path.exists(autoGenFolder):
		for f in os.listdir(autoGenFolder):
			if f.endswith('.cs'):
				os.remove(os.path.join(autoGenFolder, f))
	else:
		os.mkdir(autoGenFolder)
	if os.path.exists(exampleConfigFolder):
		shutil.rmtree(exampleConfigFolder)
	os.mkdir(exampleConfigFolder)

# convert json to excel
def __convertJson2Xlsx():
	for parent, dirs, files in os.walk(exampleConfigFolder):
		for filename in files:
			pathname = os.path.join(parent, filename)
			if os.path.isfile(pathname) and pathname.endswith('.json'):
				convertJson2Xlsx(pathname)

if __name__ == "__main__":
	# 编译codegen
	printStep('compile codegen')
	makefile = pycsharpmake.Makefile()
	makefile.make(codegenMakefileName, __script_path__, __assets_path__)

	printStep('clear generated code')
	makeAutoGenFolder()
	# gen code and json example
	printStep('gen code and json example')
	makefile.run(autoGenFolder, exampleConfigFolder, luaExportDir, debug=True)
	# printStep('convert json to excel')
	__convertJson2Xlsx()

	printStep('compile configgen')
	makefile.make(configgenMakefileName, __script_path__, __assets_path__)
