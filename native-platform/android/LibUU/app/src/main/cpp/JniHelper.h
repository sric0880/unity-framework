//
// Created by qiong on 16/11/4.
//

#ifndef LIBUU_JNIHELPER_H
#define LIBUU_JNIHELPER_H

#include <jni.h>
#include <string>

class JniHelper {
public:
    static std::string jstring2string(JNIEnv* env, jstring str);
    static jstring string2jstring(JNIEnv* env, const char* x);
};

#endif //LIBUU_JNIHELPER_H
