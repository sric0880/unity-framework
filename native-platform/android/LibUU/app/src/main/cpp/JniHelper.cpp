//
// Created by qiong on 16/11/4.
//

#include "JniHelper.h"

std::string JniHelper::jstring2string(JNIEnv* env, jstring jstr) {
    if (jstr == nullptr || !env) {
        return "";
    }
    const char * utfChars = env->GetStringUTFChars(jstr, nullptr);
    std::string utf8Str(utfChars);
    env->ReleaseStringUTFChars(jstr, utfChars);
    return utf8Str;
}

jstring JniHelper::string2jstring(JNIEnv* env, const char* x) {
    if (x == nullptr)
    {
        x = "";
    }
    return env->NewStringUTF(x);
}
