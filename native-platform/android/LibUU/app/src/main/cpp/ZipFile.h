#ifndef __SUPPORT_ZIPUTILS_H__
#define __SUPPORT_ZIPUTILS_H__

#include "minizip/unzip.h"
#include <string>
#include <unordered_map>

typedef struct ZipEntryInfo_S
{
    unz_file_pos pos;
    uLong uncompressed_size;
} ZipEntryInfo;

/**
* Zip file - reader helper class.
*
* It will cache the file list of a particular zip file with positions inside an archive,
* so it would be much faster to read some particular files or to check their existence.
*
*/
class ZipFile
{
public:
    ZipFile() = delete;
    /**
    * Constructor, open zip file and store file list.
    *
    * @param zipFile Zip file name
    * @param filter The first part of file names, which should be accessible.
    *               For example, "assets/". Other files will be missed.
    *
    */
    ZipFile(const char* zipFile);
    ZipFile(const void* buffer, unsigned long size);
    virtual ~ZipFile();

    /**
    * Check does a file exists or not in zip file
    *
    * @param fileName File to be checked on existence
    * @return true whenever file exists, false otherwise
    *
    */
    bool fileExists(const std::string &fileName) const;

    /**
     * Check does a dir exists or not in zip file
     *
     * @param fileName File to be checked on existence
     * @return true whenever file exists, false otherwise
     */
    bool directoryExists(const std::string &dirName) const;

    /**
    * Get resource file data from a zip file.
    * @param fileName File name
    * @param[out] size If the file read operation succeeds, it will be the data size, otherwise 0.
    * @return Upon success, a pointer to the data is returned, otherwise nullptr.
    * @warning Recall: you are responsible for calling free() on any Non-nullptr pointer returned.
    *
    */
    unsigned char *getFileData(const std::string &fileName, unsigned long *size);

    unsigned long getFileSize(const std::string &fileName) const;

private:

    unzFile zipFile;

    // std::unordered_map is faster if available on the platform
    std::unordered_map<std::string, ZipEntryInfo> fileList;

    bool setFilter();
};
#endif // __SUPPORT_ZIPUTILS_H__
