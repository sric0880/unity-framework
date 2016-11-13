//
// Created by qiong on 16/10/22.
//

#include "android/asset_manager.h"
#include "android/asset_manager_jni.h"
#include "android/log.h"
#include "JniHelper.h"
#include "ZipFile.h"
#include <sys/stat.h>

#ifdef __cplusplus
extern "C" {
#endif

#define  LOG_TAG    "UUAssetManager"
#define  LOGD(...)  __android_log_print(ANDROID_LOG_DEBUG,LOG_TAG,__VA_ARGS__)
#define  LOGE(...)  __android_log_print(ANDROID_LOG_ERROR,LOG_TAG,__VA_ARGS__)

typedef enum{
    OK,
    NotExists,
    NotInitialized,
    OpenFailed,
    ReadFailed,
} FileStatus;

static AAssetManager* _assetManager;
static std::string _apkPath;
static ZipFile* _obbfile;

JNIEXPORT void JNICALL Java_qiong_mylib_libuu_UUHelper_nativeSetAssetManager(
        JNIEnv*  env,
        jobject /*this*/,
        jobject assetManager) {
    _assetManager = AAssetManager_fromJava(env, assetManager);
    if (_assetManager == NULL){
        LOGE("qiong.mylib.libuu.UUHelper.nativeSetAssetManager : null pointer parameter");
    }
}

JNIEXPORT void JNICALL Java_qiong_mylib_libuu_UUHelper_nativeSetApkPath(
        JNIEnv*  env,
        jobject /*thiz*/,
        jstring apkPath) {
    _apkPath = JniHelper::jstring2string(env, apkPath);
    LOGD("qiong.mylib.libuu.UUHelper.nativeSetApkPath: %s", _apkPath.c_str());
    if (_apkPath.find("/obb/") != std::string::npos)
    {
        _obbfile = new ZipFile(_apkPath);
    }
}

/*
 * filename is begins with "assets/"
 * buffer should be freed outside of method if not nullptr
 */
FileStatus getContents(const std::string& filename, unsigned char * &buffer, ssize_t* len)
{
    if (filename.empty())
        return NotExists;

    if (_obbfile)
    {
        buffer = _obbfile->getFileData(filename, len);
        if(buffer != NULL && *len != 0) return OK;
    }

    if (nullptr == _assetManager) {
        return NotInitialized;
    }

    AAsset* asset = AAssetManager_open(_assetManager, filename.c_str(), AASSET_MODE_UNKNOWN);
    if (nullptr == asset) {
        LOGD("asset is nullptr");
        return OpenFailed;
    }

    off_t size = AAsset_getLength(asset);
    if (size > 0)
    {
        buffer = (unsigned char*)malloc(size);
        int readsize = AAsset_read(asset, buffer, size);
        AAsset_close(asset);
        if (readsize != size) {
            free(buffer);
            return ReadFailed;
        }
        return OK;
    }
    return ReadFailed;
}

bool isFileExistExternal(const std::string& filepath)
{
    struct stat buffer;
    return (stat (filepath.c_str(), &buffer) == 0);
}

bool isFileExistInternal(const std::string& filename)
{
    if (filename.empty()) return false;

    if (filename[0] == '/') return false;

    if (_obbfile && _obbfile->fileExists(filename.c_str()))
    {
        return true;
    }
    else if (NULL != _assetManager)
    {
        AAsset* aa = AAssetManager_open(_assetManager, filename.c_str(), AASSET_MODE_UNKNOWN);
        if (aa)
        {
            AAsset_close(aa);
            return true;
        } else {
            LOGD("[AssetManager] ... in APK %s, found = false!", filename.c_str());
        }
    }
    return  false;
}

bool isDirectoryExistExternal(const std::string& dirPath)
{
    LOGD("find in flash memory dirPath(%s)", dirPath.c_str());
    struct stat st;
    if (stat(dirPath.c_str(), &st) == 0)
    {
        return S_ISDIR(st.st_mode);
    }
    return false;
}

bool isDirectoryExistInternal(const std::string& dirPath)
{
    LOGD("find in apk dirPath(%s)", dirPath.c_str());
    if (dirPath.empty()) return false;
    if (dirPath[0] == '/') return false;

    // find it in apk's assets dir
    if (NULL != _assetManager)
    {
        AAssetDir* aa = AAssetManager_openDir(_assetManager, dirPath.c_str());
        if (aa && AAssetDir_getNextFileName(aa))
        {
            AAssetDir_close(aa);
            return true;
        }
    }
    return false;
}

#ifdef __cplusplus
}
#endif