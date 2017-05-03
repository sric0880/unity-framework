package com.sualaction.ninjaslash;

import android.app.Activity;
import android.content.ClipData;
import android.content.ClipDescription;
import android.content.ClipboardManager;
import android.content.Context;

/**
 * Created by User on 2016/10/10.
 */
public class ClipboardTools {
    public static ClipboardManager clipboard = null;

    // 向剪贴板中添加文本
    public void copyTextToClipboard(final Context activity, final String str) throws Exception {
        clipboard = (ClipboardManager) activity.getSystemService(Activity.CLIPBOARD_SERVICE);
        ClipData textCd = ClipData.newPlainText("data", str);
        clipboard.setPrimaryClip(textCd);
    }

    // 从剪贴板中获取文本
    public String getTextFromClipboard() {
        if (clipboard != null && clipboard.hasPrimaryClip()
                && clipboard.getPrimaryClipDescription().hasMimeType(ClipDescription.MIMETYPE_TEXT_PLAIN)) {
            ClipData cdText = clipboard.getPrimaryClip();
            ClipData.Item item = cdText.getItemAt(0);
            return item.getText().toString();
        }
        return "null";
    }
}
