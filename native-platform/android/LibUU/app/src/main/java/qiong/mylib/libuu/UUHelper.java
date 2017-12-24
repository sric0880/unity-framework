package qiong.mylib.libuu;

import android.content.Context;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageManager;
import android.content.res.AssetManager;

import java.io.File;

/**
 * Created by qiong on 16/10/22.
 */

public class UUHelper {

    static {
        System.loadLibrary("uu");
    }

    private static String sAssetsPath;
    private static Context mContext;

    public static void init(final Context context)
    {
        mContext = context;
        final ApplicationInfo applicationInfo = context.getApplicationInfo();
        setAssetManager(context.getAssets());

        int versionCode = 1;
        try {
            versionCode = UUHelper.mContext.getPackageManager().getPackageInfo(applicationInfo.packageName, 0).versionCode;
        } catch (PackageManager.NameNotFoundException e) {
            e.printStackTrace();
        }
        String pathToOBB = context.getObbDir() + "/main." + versionCode + ".obb";
        File obbFile = new File(pathToOBB);
        if (obbFile.exists()) {
            UUHelper.sAssetsPath = pathToOBB;
            newObbFile(pathToOBB);
        }
        else {
            UUHelper.sAssetsPath = applicationInfo.sourceDir;
        }
    }

    // This function returns the absolute path to the OBB if it exists,
    // else it returns the absolute path to the APK.
    public static String getAssetsPath()
    {
        return UUHelper.sAssetsPath;
    }

    private static native void setAssetManager(final AssetManager assetManager);
    private static native void newObbFile(final String assetPath);
}
