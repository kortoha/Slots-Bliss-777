using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    public string screenshotPath = "Screenshots";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!System.IO.Directory.Exists(screenshotPath))
            {
                System.IO.Directory.CreateDirectory(screenshotPath);
            }

            string screenshotName = string.Format("{0}/Screenshot_{1}.png",
                                                  screenshotPath,
                                                  System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));

            ScreenCapture.CaptureScreenshot(screenshotName);
            Debug.Log("Save: " + screenshotName);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Time.timeScale = 0f;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = 1f;
        }
    }
}
