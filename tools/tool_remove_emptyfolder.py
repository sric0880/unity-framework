import os, traceback

def ignore_folder(folder):
    return folder.startswith('.') or folder in ["Library", "Temp", "obj"]

remove = []
def walk_folder_recurse(path):
    filecount = 0
    for filename in os.listdir(path):
        filepath = os.path.join(path, filename)
        if os.path.isdir(filepath):
            if not ignore_folder(filename):
                filecount += walk_folder_recurse(filepath)
        else:
            if filename.endswith('.meta'):
                if not os.path.exists(filepath[:-5]):
                    remove.append(filepath)
            else:
                filecount += 1

    if filecount == 0:
        remove.append(path)
        meta_file = path + '.meta'
        if os.path.isfile(meta_file):
            remove.append(meta_file)
    return filecount

try:
    walk_folder_recurse(os.getcwd())

    if len(remove) > 0:
        for path in remove:
            print "will remove %s" % path

        print 'delete %s files and folders? (Y/N)' % len(remove)
        input = raw_input()
        if input == 'y' or input == "Y":
            for path in remove:
                print "remove %s" % path
                if os.path.isdir(path):
                    os.rmdir(path)
                elif os.path.isfile(path):
                    os.remove(path)
            print 'delete complete'

        else:
            print 'canceled.'

    else:
        print 'nothing to delete'

except:
    traceback.print_exc()

os.system('pause')