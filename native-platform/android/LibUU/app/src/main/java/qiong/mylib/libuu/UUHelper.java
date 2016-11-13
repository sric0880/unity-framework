package qiong.mylib.libuu;

import android.app.Activity;
import android.content.Context;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageManager;
import android.content.res.AssetManager;
import android.os.Environment;

import java.io.File;

/**
 * Created by qiong on 16/10/22.
 */

public class UUHelper {

    public static String sAssetsPath = "";
    public static Activity sActivity;
    public static String sPackageName;

    public static void init(final Activity activity)
    {
        sActivity = activity;
        final ApplicationInfo applicationInfo = activity.getApplicationInfo();
        sPackageName = applicationInfo.packageName;
        nativeSetAssetManager(activity, activity.getAssets());
        nativeSetApkPath(getAssetsPath());
    }

    // This function returns the absolute path to the OBB if it exists,
    // else it returns the absolute path to the APK.
    public static String getAssetsPath()
    {
        if (UUHelper.sAssetsPath == "") {
            int versionCode = 1;
            try {
                versionCode = UUHelper.sActivity.getPackageManager().getPackageInfo(UUHelper.sPackageName, 0).versionCode;
            } catch (PackageManager.NameNotFoundException e) {
                e.printStackTrace();
            }
            String pathToOBB = Environment.getExternalStorageDirectory().getAbsolutePath() + "/Android/obb/" + UUHelper.sPackageName + "/main." + versionCode + "." + UUHelper.sPackageName + ".obb";
            File obbFile = new File(pathToOBB);
            if (obbFile.exists())
                UUHelper.sAssetsPath = pathToOBB;
            else
                UUHelper.sAssetsPath = UUHelper.sActivity.getApplicationInfo().sourceDir;
        }
        return UUHelper.sAssetsPath;
    }

    private static native void nativeSetAssetManager(final Context context, final AssetManager assetManager);
    private static native void nativeSetApkPath(final String pApkPath);
}
