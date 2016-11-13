package qiong.mylib.libuu;

import android.os.Bundle;
import android.util.Log;

import com.unity3d.player.UnityPlayerActivity;

public class MainActivity extends UnityPlayerActivity {

    static {
        System.loadLibrary("uu");
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        UUHelper.init(this);

        Log.d("MainActivity", stringFromJNI());
    }

    private static native String stringFromJNI();
}
