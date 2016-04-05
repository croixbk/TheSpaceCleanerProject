package com.Arysi.SpaceCleaner;

/**
 * Created by Yuri on 05/04/2016.
 */
import android.content.Intent;
import com.unity3d.player.UnityPlayerActivity;

public class ShareText extends UnityPlayerActivity {

    public void shareText(String subject, String body) {
        Intent sharingIntent = new Intent(android.content.Intent.ACTION_SEND);
        sharingIntent.setType("text/plain");
        sharingIntent.putExtra(android.content.Intent.EXTRA_SUBJECT, subject);
        sharingIntent.putExtra(android.content.Intent.EXTRA_TEXT, body);
        startActivity(Intent.createChooser(sharingIntent, "Share via"));
    }

}