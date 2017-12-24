//
// Created by qiong on 16/10/22.
//

#include "android/asset_manager.h"
#include "android/asset_manager_jni.h"
#include "ZipFile.h"

#ifdef __cplusplus
extern "C" {
#endif

//typedef void (*OnBytesRead)(const char*, unsigned char*, unsigned long);

static AAssetManager* _assetManager;
static ZipFile* _obbfile;

/*
 * filename is begins with "assets/"
 * buffer should be freed outside of method if not nullptr
 */
unsigned char *getFileBytes(const char* filename, unsigned long* len)
{
    if (filename == nullptr) return nullptr;
    if (strlen(filename) == 0) return nullptr;
    if (filename[0] == '/') return nullptr;

    if (_obbfile)
    {
        return _obbfile->getFileData(filename, len);
    }

    if (len)
        *len = 0;

    if (nullptr == _assetManager) {
        return nullptr;
    }

    AAsset* asset = AAssetManager_open(_assetManager, filename, AASSET_MODE_UNKNOWN);
    if (nullptr == asset) {
        return nullptr;
    }

    off_t size = AAsset_getLength(asset);
    if (size > 0)
    {
        auto buffer = (unsigned char*)malloc(size);
        int readSize = AAsset_read(asset, buffer, size);
        AAsset_close(asset);
        if (readSize != size) {
            free(buffer);
            return nullptr;
        }
        *len = (unsigned long)size;
        return buffer;
    }
    return nullptr;
}

void freeFileBytes(void* obj) {
    free(obj);
}

/*
 * filename: relative file path
 */
bool isFileExistInternal(const char* filename)
{
    if (strlen(filename) == 0) return false;
    if (filename[0] == '/') return false;

    if (_obbfile && _obbfile->fileExists(filename))
    {
        return true;
    }
    else if (nullptr != _assetManager)
    {
        AAsset* aa = AAssetManager_open(_assetManager, filename, AASSET_MODE_UNKNOWN);
        if (aa)
        {
            AAsset_close(aa);
            return true;
        }
    }
    return  false;
}

bool isDirectoryExistInternal(const char* dirPath)
{
    if (dirPath[0] == '/') return false;

    if (_obbfile && _obbfile->directoryExists(dirPath))
    {
        return true;
    }
    else if (nullptr != _assetManager)
    {
        AAssetDir* aa = nullptr;
        auto len = strlen(dirPath) - 1;
        if (len != 0 && dirPath[len] == '/')
        {
            char newDirPath[len+1];
            strncpy(newDirPath, dirPath, len);
            newDirPath[len] = '\0';
            aa = AAssetManager_openDir(_assetManager, newDirPath);
        }
        else
        {
            aa = AAssetManager_openDir(_assetManager, dirPath);
        }
        if (AAssetDir_getNextFileName(aa) != nullptr)
        {
            AAssetDir_close(aa);
            return true;
        }
        AAssetDir_close(aa);
    }
    return false;
}

unsigned long getFileSize(const char* filename)
{
    if (strlen(filename) == 0) return 0;
    if (filename[0] == '/') return 0;

    if (_obbfile)
    {
        return _obbfile->getFileSize(filename);
    }
    else if (nullptr != _assetManager)
    {
        AAsset* aa = AAssetManager_open(_assetManager, filename, AASSET_MODE_UNKNOWN);
        if (aa)
        {
            auto len = (unsigned long)AAsset_getLength(aa);
            AAsset_close(aa);
            return len;
        }
    }
}

JNIEXPORT void JNICALL Java_qiong_mylib_libuu_UUHelper_setAssetManager
        (JNIEnv * env, jclass, jobject assetManager)
{
    _assetManager = AAssetManager_fromJava(env, assetManager);
}

JNIEXPORT void JNICALL Java_qiong_mylib_libuu_UUHelper_newObbFile
        (JNIEnv * env, jclass, jstring assetPath)
{
    const char * utfChars = env->GetStringUTFChars(assetPath, nullptr);
    _obbfile = new ZipFile(utfChars);
    env->ReleaseStringUTFChars(assetPath, utfChars);
}

//JNIEXPORT jbyteArray JNICALL Java_qiong_mylib_libuu_UUHelper_getFileBytes
//        (JNIEnv * env, jclass, jstring filename)
//{
//    unsigned long len = 0;
//    unsigned char* bytes = getFileBytes(JniHelper::jstring2string(env, filename), &len);
//    jbyteArray array = env->NewByteArray(len);
//    env->SetByteArrayRegion(array, 0, len, (jbyte*)bytes);
//    if (len > 0 && bytes != nullptr) free(bytes);
//    return array;
//}

//JNIEXPORT void JNICALL Java_qiong_mylib_libuu_UUHelper_getFileBytesAsync
//        (JNIEnv * env, jclass, jstring filename, jobject onBytesRead)
//{
//    jobject gObj = env->NewGlobalRef(onBytesRead);
//
//    OnBytesReadFunction callback = [gObj](unsigned char* bytes, unsigned long len){
//        JNIEnv* e = NULL;
//        if (gVM->AttachCurrentThread(&e, NULL) != JNI_OK) {
//            LOGE("%s: AttachCurrentThread() failed", __FUNCTION__);
//            return;
//        }
//        jclass clazz = e->GetObjectClass(gObj);
//        jmethodID mid = e->GetMethodID(clazz, "onBytesRead","([B)V");
//        jbyteArray array = e->NewByteArray(len);
//        e->SetByteArrayRegion(array, 0, len, (jbyte*)bytes);
//        e->CallVoidMethod(gObj, mid, array);
//        // detach current thread from jvm
//        if (gVM->DetachCurrentThread() != JNI_OK)
//        {
//            LOGE("%s: DetachCurrentThread() failed", __FUNCTION__);
//        }
//    };
//    getFileBytesAsync_(JniHelper::jstring2string(env, filename), callback);
//}

//JNIEXPORT jboolean JNICALL Java_qiong_mylib_libuu_UUHelper_isFileExistInternal
//        (JNIEnv * env, jclass, jstring filename)
//{
//    const char * utfChars = env->GetStringUTFChars(filename, nullptr);
//    auto ret = isFileExistInternal(utfChars) ? (jboolean)1 : (jboolean)0;
//    env->ReleaseStringUTFChars(filename, utfChars);
//    return ret;
//}
//
//JNIEXPORT jboolean JNICALL Java_qiong_mylib_libuu_UUHelper_isDirectoryExistInternal
//        (JNIEnv * env, jclass, jstring dirpath)
//{
//    const char * utfChars = env->GetStringUTFChars(dirpath, nullptr);
//    auto ret = isDirectoryExistInternal(utfChars) ? (jboolean)1 : (jboolean)0;
//    env->ReleaseStringUTFChars(dirpath, utfChars);
//    return ret;
//}

//JNIEXPORT jint JNICALL Java_qiong_mylib_libuu_UUHelper_getFileSize
//        (JNIEnv * env, jclass, jstring filename)
//{
//    return (jint)getFileSize(JniHelper::jstring2string(env, filename));
//}

#ifdef __cplusplus
}
#endif