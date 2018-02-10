#-*- coding: utf-8
import sys, os, yaml, shutil, pycsharpmake
from excelconverter import convertJson2Xlsx
__script_path__ = os.path.dirname(os.path.abspath(__file__))
__project_path__ = os.path.dirname(os.path.dirname(__script_path__))
__assets_path__ = os.path.join(__project_path__, 'Assets')
__plugins_path__ = os.path.join(__assets_path__, 'Plugins')
autoGenFolder=os.path.join(__script_path__, 'ConfigGen/AutoGen')
luaExportDir = os.path.join(__project_path__, 'lua/main/Config')
configdllMakefileName = os.path.join(__script_path__, 'makefile_config_dll.yml')
exampleMakefileName = os.path.join(__script_path__, 'makefile_examples.yml')
configgenMakefileName = os.path.join(__script_path__, 'makefile_configgen.yml')
pathConfigName = os.path.join(__script_path__, '../pathvars.yml')

confFilesForCS = ['conf/client.conf', 'conf/launch.conf']
confFilesForServer = ['conf/server.conf']
confFilesForLua = ['conf/lua.conf']

with open(pathConfigName, 'r') as stream:
	content = yaml.load(stream)
exampleConfigFolder=os.path.join(__project_path__, content['exampleConfigDir'])

makefile = pycsharpmake.Makefile()

def printStep(name):
	print('\n')
	print(name)
	print('--------------------------------')

# convert json to excel
def __convertJson2Xlsx():
	for parent, dirs, files in os.walk(exampleConfigFolder):
		for filename in files:
			pathname = os.path.join(parent, filename)
			if os.path.isfile(pathname) and pathname.endswith('.json'):
				convertJson2Xlsx(pathname)

def codegen(confFiles):
	printStep('clear generated code')
	if os.path.exists(autoGenFolder):
		shutil.rmtree(autoGenFolder)
	os.mkdir(autoGenFolder)
	# gen code
	printStep('gen code')
	for file in confFiles:
		if os.system('java -jar bin/confparser.jar %s %s' % (file, autoGenFolder)) != 0:
			raise Exception('gen code error!')
	# 编译config dll
	printStep('compile config dll')
	makefile.make(configdllMakefileName, __script_path__, __script_path__)

	# json examples
	printStep('gen json examples')
	makefile.make(exampleMakefileName, __script_path__, __script_path__)
	makefile.run(exampleConfigFolder, debug=True)

	printStep('convert json to excel')
	__convertJson2Xlsx()


if __name__ == "__main__":
	if os.path.exists(exampleConfigFolder):
		shutil.rmtree(exampleConfigFolder)
	os.mkdir(exampleConfigFolder)

	codegen(confFilesForLua)

	codegen(confFilesForServer)

	codegen(confFilesForCS)
	shutil.copy('bin/Config.dll', __plugins_path__)

	printStep('compile configgen')
	makefile.make(configgenMakefileName, __script_path__, __script_path__)

	shutil.rmtree(autoGenFolder)
