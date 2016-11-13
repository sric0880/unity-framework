/****************************************************************************
 Copyright (c) 2010-2012 cocos2d-x.org
 Copyright (c) 2013-2016 Chukong Technologies Inc.

 http://www.cocos2d-x.org
 
 Permission is hereby granted, free of charge, to any person obtaining a copy
 of this software and associated documentation files (the "Software"), to deal
 in the Software without restriction, including without limitation the rights
 to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 copies of the Software, and to permit persons to whom the Software is
 furnished to do so, subject to the following conditions:
 
 The above copyright notice and this permission notice shall be included in
 all copies or substantial portions of the Software.
 
 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 THE SOFTWARE.
 ****************************************************************************/

#include "minizip/unzip.h"
#include "ZipFile.h"
#include <assert.h>

//#include "base/CCData.h"
//#include "base/ccMacros.h"
//#include "platform/CCFileUtils.h"
#include <unordered_map>

// FIXME: Other platforms should use upstream minizip like mingw-w64  
#ifdef MINIZIP_FROM_SYSTEM
#define unzGoToFirstFile64(A,B,C,D) unzGoToFirstFile2(A,B,C,D, NULL, 0, NULL, 0)
#define unzGoToNextFile64(A,B,C,D) unzGoToNextFile2(A,B,C,D, NULL, 0, NULL, 0)
#endif

// --------------------- ZipFile ---------------------
// from unzip.cpp
#define UNZ_MAXFILENAMEINZIP 256

static const std::string emptyFilename("");

struct ZipEntryInfo
{
    unz_file_pos pos;
    uLong uncompressed_size;
};

class ZipFilePrivate
{
public:
    unzFile zipFile;
    
    // std::unordered_map is faster if available on the platform
    typedef std::unordered_map<std::string, struct ZipEntryInfo> FileListContainer;
    FileListContainer fileList;
};

ZipFile::ZipFile(const std::string &zipFile, const std::string &filter)
: _data(new ZipFilePrivate)
{
    _data->zipFile = unzOpen(zipFile.c_str());
    setFilter(filter);
}

ZipFile::ZipFile(const void* buffer, uLong size)
: _data(new ZipFilePrivate)
{
    if (!buffer || size == 0)
        _data->zipFile = nullptr;
    else
        _data->zipFile = unzOpenBuffer(buffer, size);
    setFilter(emptyFilename);
}

ZipFile::~ZipFile()
{
    if (_data && _data->zipFile)
    {
        unzClose(_data->zipFile);
    }

    delete(_data);
    _data = nullptr;
}

bool ZipFile::setFilter(const std::string &filter)
{
    if (!_data) return false;
    if (!_data->zipFile) return false;

    // clear existing file list
    _data->fileList.clear();

    // UNZ_MAXFILENAMEINZIP + 1 - it is done so in unzLocateFile
    char szCurrentFileName[UNZ_MAXFILENAMEINZIP + 1];
    unz_file_info64 fileInfo;

    // go through all files and store position information about the required files
    int err = unzGoToFirstFile64(_data->zipFile, &fileInfo,
                                 szCurrentFileName, sizeof(szCurrentFileName) - 1);
    while (err == UNZ_OK)
    {
        unz_file_pos posInfo;
        int posErr = unzGetFilePos(_data->zipFile, &posInfo);
        if (posErr == UNZ_OK)
        {
            std::string currentFileName = szCurrentFileName;
            // cache info about filtered files only (like 'assets/')
            if (filter.empty()
                || currentFileName.substr(0, filter.length()) == filter)
            {
                ZipEntryInfo entry;
                entry.pos = posInfo;
                entry.uncompressed_size = (uLong)fileInfo.uncompressed_size;
                _data->fileList[currentFileName] = entry;
            }
        }
        // next file - also get the information about it
        err = unzGoToNextFile64(_data->zipFile, &fileInfo,
                                szCurrentFileName, sizeof(szCurrentFileName) - 1);
    }
    return true;
}

bool ZipFile::fileExists(const std::string &fileName) const
{
    if (!_data) return false;
    return _data->fileList.find(fileName) != _data->fileList.end();
}

unsigned char *ZipFile::getFileData(const std::string &fileName, ssize_t *size)
{
    if (size)
        *size = 0;

    if (!_data->zipFile) return nullptr;
    if (fileName.empty()) return nullptr;

    ZipFilePrivate::FileListContainer::const_iterator it = _data->fileList.find(fileName);
    if (it == _data->fileList.end()) return nullptr;

    ZipEntryInfo fileInfo = it->second;

    int nRet = unzGoToFilePos(_data->zipFile, &fileInfo.pos);
    if (UNZ_OK != nRet) return nullptr;

    nRet = unzOpenCurrentFile(_data->zipFile);
    if (UNZ_OK != nRet) return nullptr;

    unsigned char * buffer = (unsigned char*)malloc(fileInfo.uncompressed_size);
    int nSize = unzReadCurrentFile(_data->zipFile, buffer, static_cast<unsigned int>(fileInfo.uncompressed_size));
    assert(nSize == 0 || nSize == (int)fileInfo.uncompressed_size);

    if (size)
    {
        *size = fileInfo.uncompressed_size;
    }
    unzCloseCurrentFile(_data->zipFile);

    return buffer;
}

std::string ZipFile::getFirstFilename()
{
    if (unzGoToFirstFile(_data->zipFile) != UNZ_OK) return emptyFilename;
    std::string path;
    unz_file_info info;
    getCurrentFileInfo(&path, &info);
    return path;
}

std::string ZipFile::getNextFilename()
{
    if (unzGoToNextFile(_data->zipFile) != UNZ_OK) return emptyFilename;
    std::string path;
    unz_file_info info;
    getCurrentFileInfo(&path, &info);
    return path;
}

int ZipFile::getCurrentFileInfo(std::string *filename, unz_file_info *info)
{
    char path[FILENAME_MAX + 1];
    int ret = unzGetCurrentFileInfo(_data->zipFile, info, path, sizeof(path), nullptr, 0, nullptr, 0);
    if (ret != UNZ_OK) {
        *filename = emptyFilename;
    } else {
        filename->assign(path);
    }
    return ret;
}
