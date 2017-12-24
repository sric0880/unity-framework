#include "ZipFile.h"

// FIXME: Other platforms should use upstream minizip like mingw-w64  
#ifdef MINIZIP_FROM_SYSTEM
#define unzGoToFirstFile64(A,B,C,D) unzGoToFirstFile2(A,B,C,D, NULL, 0, NULL, 0)
#define unzGoToNextFile64(A,B,C,D) unzGoToNextFile2(A,B,C,D, NULL, 0, NULL, 0)
#endif

// --------------------- ZipFile ---------------------
// from unzip.cpp
#define UNZ_MAXFILENAMEINZIP 256
const static const char* rootDir = "assets/";

ZipFile::ZipFile(const char* filename)
{
    zipFile = unzOpen(filename);
    setFilter();
}

ZipFile::ZipFile(const void* buffer, uLong size)
{
    if (!buffer || size == 0)
        zipFile = nullptr;
    else
        zipFile = unzOpenBuffer(buffer, size);
    setFilter();
}

ZipFile::~ZipFile()
{
    if (zipFile)
    {
        unzClose(zipFile);
    }
}

bool startsWith(const char *pre, const char *str)
{
    size_t len_pre = strlen(pre),
            len_str = strlen(str);
    return len_str > len_pre ? strncmp(pre, str, len_pre) == 0 : false;
}

bool ZipFile::setFilter()
{
    if (!zipFile) return false;

    // clear existing file list
    fileList.clear();

    // UNZ_MAXFILENAMEINZIP + 1 - it is done so in unzLocateFile
    char szCurrentFileName[UNZ_MAXFILENAMEINZIP + 1];
    unz_file_info64 fileInfo;
    unz_file_pos posInfo;

    // go through all files and store position information about the required files
    int err = unzGoToFirstFile64(zipFile, &fileInfo,
                                 szCurrentFileName, sizeof(szCurrentFileName) - 1);
    while (err == UNZ_OK)
    {
        if (startsWith(rootDir, szCurrentFileName))
        {
            int posErr = unzGetFilePos(zipFile, &posInfo);
            if (posErr == UNZ_OK)
            {
                ZipEntryInfo entry;
                entry.pos = posInfo;
                entry.uncompressed_size = (uLong)fileInfo.uncompressed_size;
                fileList[std::string(szCurrentFileName + strlen(rootDir))] = entry;
            }
        }
        // next file - also get the information about it
        err = unzGoToNextFile64(zipFile, &fileInfo,
                                szCurrentFileName, sizeof(szCurrentFileName) - 1);
    }
    return true;
}

bool ZipFile::fileExists(const std::string &fileName) const
{
    if (fileName.empty()) return false;
    if (fileName.at(fileName.length()-1) == '/')
    {
        return false;
    }
    return fileList.find(fileName) != fileList.end();
}

bool ZipFile::directoryExists(const std::string &dirName) const
{
    if (dirName.empty()) return true;
    if (dirName.at(dirName.length()-1) != '/')
    {
        return fileList.find(dirName + '/') != fileList.end();
    }
    return fileList.find(dirName) != fileList.end();
}

unsigned char *ZipFile::getFileData(const std::string &fileName, unsigned long *size)
{
    if (size)
        *size = 0;

    if (!zipFile) return nullptr;
    if (fileName.empty()) return nullptr;

    auto it = fileList.find(fileName);
    if (it == fileList.end()) return nullptr;

    ZipEntryInfo fileInfo = it->second;

    int nRet = unzGoToFilePos(zipFile, &fileInfo.pos);
    if (UNZ_OK != nRet) return nullptr;

    nRet = unzOpenCurrentFile(zipFile);
    if (UNZ_OK != nRet) return nullptr;

    unsigned char * buffer = (unsigned char*)malloc(fileInfo.uncompressed_size);
    int nSize = unzReadCurrentFile(zipFile, buffer, static_cast<unsigned int>(fileInfo.uncompressed_size));
    unzCloseCurrentFile(zipFile);
    if (nSize > 0 && nSize == (int)fileInfo.uncompressed_size)
    {
        if (size)
            *size = fileInfo.uncompressed_size;
        return buffer;
    }
    return nullptr;
}

unsigned long ZipFile::getFileSize(const std::string &fileName) const
{
    if (!zipFile) return 0;
    if (fileName.empty()) return 0;

    auto it = fileList.find(fileName);
    if (it == fileList.end()) return 0;

    ZipEntryInfo fileInfo = it->second;
    return fileInfo.uncompressed_size;
}