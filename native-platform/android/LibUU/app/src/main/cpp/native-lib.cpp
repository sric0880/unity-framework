#include <jni.h>
#include <string>

extern "C"
jstring
Java_qiong_mylib_libuu_MainActivity_stringFromJNI(
        JNIEnv *env,
        jobject /* this */) {
    std::string hello = "Hello from C++";
    return env->NewStringUTF(hello.c_str());
}
