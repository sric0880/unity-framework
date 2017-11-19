#-*- coding: utf-8
import sys, os, yaml, pycsharpmake, shutil
from excelconverter import convertXlsx2Json, convertXlsx2Lua
__script_path__ = os.path.dirname(os.path.abspath(__file__))
__project_path__ = os.path.dirname(os.path.dirname(__script_path__))
__assets_path__ = os.path.join(__project_path__, 'Assets')
pathConfigName = os.path.join(__script_path__, '../pathvars.yml')
luaExportDir = os.path.join(__project_path__, 'lua/main/Config')

with open(pathConfigName, 'r') as stream:
	content = yaml.load(stream)
xlsxConfigFolder=os.path.join(__project_path__, content['xlsxConfigDir'])
binaryConfigFolder=os.path.join(__project_path__, content['binaryConfigDir'])

def printStep(name):
	print('\n')
	print(name)
	print('--------------------------------')

# convert excel to json or lua
def __convertXlsx2JsonOrLua():
	for parent, dirs, files in os.walk(xlsxConfigFolder):
		if 'locale' in parent: continue
		for filename in files:
			pathname = os.path.join(parent, filename)
			if os.path.isfile(pathname) and pathname.endswith('.xlsx') and '~' not in pathname:
				if filename.startswith('[lua]'):
					print('convert %s to lua file' % filename)
					convertXlsx2Lua(pathname, filename[5:-5])
					luafilename = filename.replace('.xlsx', '.lua')
					srcluapathname = pathname.replace('.xlsx', '.lua')
					desluapathname = os.path.join(luaExportDir, luafilename)
					desluapathname = desluapathname.replace('[lua]', '')
					destdir = os.path.dirname(desluapathname)
					if not os.path.exists(destdir):
						os.mkdir(destdir)
					shutil.move(srcluapathname, desluapathname)
				else:
					print('convert %s to json file' % filename)
					convertXlsx2Json(pathname)

def removeJsonFiles():
	for parent, dirs, files in os.walk(xlsxConfigFolder):
		for filename in files:
			pathname = os.path.join(parent, filename)
			if os.path.isfile(pathname) and pathname.endswith('.json'):
				os.remove(pathname)

if __name__ == "__main__":
	# 编译codegen
	printStep('convert excel to json or lua')
	__convertXlsx2JsonOrLua();

	printStep('config gen')
	makefile = pycsharpmake.Makefile(os.path.join(__script_path__, 'bin/configgen.exe'))
	makefile.run(xlsxConfigFolder, binaryConfigFolder, debug=True)

	printStep('remove json files')
	removeJsonFiles()
