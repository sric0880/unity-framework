#!/usr/bin/env python
import os, yaml, re, argparse, platform, shutil, struct
__script_path__ = os.path.dirname(os.path.abspath(__file__))
__project_path__ = os.path.dirname(os.path.dirname(__script_path__))
__assets_path__ = os.path.join(__project_path__, 'Assets')

windows = platform.system().startswith("Windows")
encoders = {
'win32': ('LuaEncoder/luajit', 'luajit32.exe -b {src} {dst}c'),
'win64': ('LuaEncoder/luajit64', 'luajit64.exe -b {src} {dst}c'),
'mac': ('LuaEncoder/luavm', './luac -o {dst}c {src}'),
'ios': ('LuaEncoder/luajit_ios/'+ platform.machine(), './luajit -b {src} {dst}c'),
}
if platform.architecture()[0] == '32bit':
	encoders['win'] = encoders['win32']
else:
	encoders['win'] = encoders['win64']
encoders['android'] = encoders['win'] if windows else encoders['ios']
if windows:
	choices = ['android', 'windows']
else:
	choices = ['ios', 'android', 'mac']
parser = argparse.ArgumentParser(description='compile lua files to bytecode')
parser.add_argument('-c', '--clear', help='clear generated luac files', action='store_true')
parser.add_argument('platform', help='system platform', choices=choices)
args = parser.parse_args()

encoder = encoders[args.platform]

pathConfigName = os.path.join(__script_path__, '../pathvars.yml')
with open(pathConfigName, 'r') as stream:
	content = yaml.load(stream)
luaDir = os.path.join(__project_path__, content['luaDir'])
binaryConfigDir = os.path.join(__project_path__, content['binaryConfigDir'])
luaSrcDir = [os.path.join(__project_path__, 'lua/main'), os.path.join(__project_path__, 'lua/tolua')]

# Main logic
def encodeLuaFile(exe, src, dst):
	exe = exe.replace('{src}', src)
	exe = exe.replace('{dst}', dst)
	print(exe)
	if os.system(exe) != 0:
		raise Exception('compile lua failed, cause by ' + src)

def filePairs():
	for srcDir in luaSrcDir:
		for parent, dirs, files in os.walk(srcDir):
			for filename in files:
				if not filename.endswith('.lua'): continue
				srcPathname = os.path.join(parent, filename)
				dstPathname = os.path.join(luaDir, srcPathname[len(srcDir)+1:])
				dstPath = os.path.dirname(dstPathname)
				if not os.path.exists(dstPath):
					os.makedirs(dstPath)
				yield (srcPathname, dstPathname)

if not os.path.exists(luaDir):
	os.mkdir(luaDir)
else:
	shutil.rmtree(luaDir)

workingDir = encoder[0]
os.chdir(workingDir)
print(workingDir)
for src, dst in filePairs():
	encodeLuaFile(encoder[1], src, dst)

luacbinary = open(os.path.join(binaryConfigDir, 'cb'), 'wb')
for parent, dirs, files in os.walk(luaDir):
	for filename in files:
		if not filename.endswith('.luac'):
			print('Warning: filename not endswith luac')
			continue
		path = os.path.join(parent, filename)
		pathname = path[len(luaDir)+1:-5]
		with open(path, 'rb') as onebin:
			bytecode = onebin.read()
			pathnameLen = len(pathname)
			if pathnameLen > 127:
				raise Exception('pathname %s len cannot larger than bx01111111(127)')
			luacbinary.write(struct.pack('b', pathnameLen)) # 8bit size
			luacbinary.write(pathname.encode('ascii'))
			luacbinary.write(struct.pack('I', len(bytecode))) # unsigned int
			luacbinary.write(bytecode)
luacbinary.close()

if args.clear:
	shutil.rmtree(luaDir)
