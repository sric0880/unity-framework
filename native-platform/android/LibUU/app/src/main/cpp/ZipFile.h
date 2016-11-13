#ifndef __SUPPORT_ZIPUTILS_H__
#define __SUPPORT_ZIPUTILS_H__

#include <string>

#ifndef _unz64_H
typedef struct unz_file_info_s unz_file_info;
#endif

class ZipFilePrivate;
struct unz_file_info_s;

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
    ZipFile(const std::string &zipFile, const std::string &filter = std::string());
    ZipFile(const void* buffer, unsigned long size);
    virtual ~ZipFile();

    /**
    * Regenerate accessible file list based on a new filter string.
    *
    * @param filter New filter string (first part of files names)
    * @return true whenever zip file is open successfully and it is possible to locate
    *              at least the first file, false otherwise
    *
    */
    bool setFilter(const std::string &filter);

    /**
    * Check does a file exists or not in zip file
    *
    * @param fileName File to be checked on existence
    * @return true whenever file exists, false otherwise
    *
    */
    bool fileExists(const std::string &fileName) const;

    /**
    * Get resource file data from a zip file.
    * @param fileName File name
    * @param[out] size If the file read operation succeeds, it will be the data size, otherwise 0.
    * @return Upon success, a pointer to the data is returned, otherwise nullptr.
    * @warning Recall: you are responsible for calling free() on any Non-nullptr pointer returned.
    *
    */
    unsigned char *getFileData(const std::string &fileName, ssize_t *size);

    std::string getFirstFilename();
    std::string getNextFilename();

private:

    int getCurrentFileInfo(std::string *filename, unz_file_info *info);

    /** Internal data like zip file pointer / file list array and so on */
    ZipFilePrivate *_data;
};
#endif // __SUPPORT_ZIPUTILS_H__
