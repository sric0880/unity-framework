#!/usr/bin/python
#encoding : utf-8

import os
import os.path
import fnmatch
import re
import platform

curDir = os.path.dirname(os.path.realpath(__file__))
sourceCodeFolder = os.path.join(curDir, os.path.abspath("../Assets"))
win_line_ending = re.compile('.*\r\n$', re.MULTILINE)
linux_line_ending = re.compile('.*\n$', re.MULTILINE)
common_line_ending = re.compile('\r?\n', re.MULTILINE)

need_to_be_processed = []

def is_line_ending_unified(fn):
	eol = '\r\n'
	if not windows_or_linux(fn):
		eol = '\n'
	with open(fn, 'rb') as fread:
		data = fread.read()
		newData = common_line_ending.sub(eol, data)
		return data == newData

def replace_line_ending(fn, eol):
	with open(fn, 'rb') as fread:
		data = fread.read()
		newData = common_line_ending.sub(eol, data)
		if data != newData:
			with open(fn, 'wb') as fwrite:
				fwrite.write(newData)
				print fn

def windows_or_linux(csfile):
	crlf = 0
	cr = 0
	with open(csfile, 'rb') as fread:
		for i in range(0,3):
			line = fread.readline()
			if win_line_ending.match(line):
				crlf = crlf + 1
			elif linux_line_ending.match(line):
				cr = cr + 1
	return crlf > cr

def collect_bad_files(dir):
	csfiles = []
	for root, dirnames, filenames in os.walk(dir):
		for csfile in fnmatch.filter(filenames, '*.cs'):
			csfiles.append(os.path.join(root, csfile))
	for fn in csfiles:
		if not is_line_ending_unified(fn):
			need_to_be_processed.append(fn)

def process():
	if len(need_to_be_processed) == 0:
		print "Everything is ok!"
	else:
		print "Will process these files:\n"
		for fn in need_to_be_processed:
			print fn
		bad_file_count = len(need_to_be_processed)
		key = raw_input("Process %d files? (Y/N)" % bad_file_count)
		if key == 'y' or key == 'Y':
			for fn in need_to_be_processed:
				eol = '\r\n'
				if not windows_or_linux(fn):
					eol = '\n'
				replace_line_ending(fn, eol)
			print "Processed %d files." % bad_file_count
		else:
			print "Nothing processed."

def pause_with_prompt(prompt):
	if platform.system() == "Windows":
		os.system("echo %s & pause > nul" % prompt)
	else:
		os.system("read -rsp $'%s\n' -n1 key" % prompt)

def press_to_continue():
	pause_with_prompt('Press any key to exit...')

def main():
	collect_bad_files(sourceCodeFolder)
	process()
	press_to_continue()

if __name__ == '__main__':
	main()
