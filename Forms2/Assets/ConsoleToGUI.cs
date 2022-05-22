using System;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConsoleToGUI : MonoBehaviour
{
    //#if !UNITY_EDITOR
    static string myLog = "";
    private string output;
    private string stack;
    private Vector2 scrollPosition;
    [Tooltip("Show/hide Stack Trace in Log")]
    public bool ShowStack = true;
    [Tooltip("Show/hide FPS Count")]
    public bool ShowFPS = true;
    [Tooltip("Number of characters that can be displayed")]
    public int LogBuffer = 5000;
    [Tooltip("Log Height on Buttom Screen")]
    public int LogHeight = 250;
    [Tooltip("No show on build version")]
    public bool OnlyInEditor = false;
    [Tooltip("Save file with log")]
    public bool SaveLogFile = true;

    public bool DisplayInUi = false;
    private bool exitClicked = false;
    [Tooltip("Log textbox show on start")]
    public bool ShowLog = true;

    public string logFilaName = "";
    private float deltaTime = 0.0f;


    [Tooltip("Font log size")]
    public int FontLogSize = 25;

    public string path = "/Documents/Infinite Forms Logs";
    public string filePrefix = "Infinite Forms ";

    public void Show(InputAction.CallbackContext context)
    {
        //bool b = Mathf.Abs(Time.time - (float) context.time) > 600;
        DisplayInUi = ! DisplayInUi;
        Cursor.visible = DisplayInUi;

    }


    public String GenerateLogFileName()
    {
        string home = (Environment.OSVersion.Platform == PlatformID.Unix ||
                   Environment.OSVersion.Platform == PlatformID.MacOSX)
            ? Environment.GetEnvironmentVariable("HOME")
            : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");

        DirectoryInfo info = new DirectoryInfo(home + path);
        if (!info.Exists)
        {
            Directory.CreateDirectory(home + path);
        }
        FileInfo[] fileInfo = info.GetFiles();
        int last = 0;
        foreach (FileInfo file in fileInfo)
        {
            if (file.Name.StartsWith(filePrefix))
            {
                string numPart = file.Name.Substring(filePrefix.Length
                    , 5);
                int fileNumber = int.Parse(numPart);
                if (fileNumber > last)
                {
                    last = fileNumber;
                }
            }            
        }
        return home + path + "/" + filePrefix + (last + 1).ToString("00000") + ".log";
    }


    private void Start()
    {
        logFilaName = GenerateLogFileName();
    }

    void OnEnable()
    {
        Application.logMessageReceived += Log;
        //Application.logMessageReceivedThreaded += Log;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= Log;
        //Application.logMessageReceivedThreaded -= Log;
    }
    void Update()
    {
        if (ShowFPS && ShowLog)
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }
    public void WriteLog(string logString)
    {
        if (logFilaName != "" && SaveLogFile)
        {
            TextWriter tw = new StreamWriter(logFilaName, true);
            tw.WriteLine(logString);
            tw.Close();
        }
    }
    public void Log(string logString, string stackTrace, LogType type)
    {
        output = logString;
        stack = stackTrace; 
        myLog = output + "\n" + myLog;
        if (stack.Length > 0 && ShowStack)
        {
            myLog = stack + "\n" + myLog;
            WriteLog(System.DateTime.Now.ToString("yyyyMMddHHmmss") + ": " + output + "\n" + stack );


        }
        else
        {
            WriteLog(System.DateTime.Now.ToString("yyyyMMddHHmmss") + ": " + output);

        }
        if (myLog.Length > LogBuffer)
        {
            myLog = myLog.Substring(0, LogBuffer);

        }

        scrollPosition = new Vector2(scrollPosition.x, 0);
    }

    public float offset = 1000;

    void OnGUI()
    {
        if (DisplayInUi)
        {
            if (!Application.isEditor || !OnlyInEditor) //Do not display in editor ( or you can use the UNITY_EDITOR macro to also disable the rest)
            {
                if (GUI.Button(new Rect(new Rect(10, Screen.height - LogHeight - 40 - offset, 100, 40)), "L"))
                    exitClicked = true;

                if (ShowLog)
                {

                    if (GUI.Button(new Rect(new Rect(130, Screen.height - LogHeight - 40 - offset, 100, 40)), "S"))
                    {
                        ShowStack = !ShowStack;
                    }
                    if (GUI.Button(new Rect(new Rect(250, Screen.height - LogHeight - 40 - offset, 100, 40)), "C"))
                    {
                        myLog = "";
                    }




                    // we want to place the TextArea in a particular location - use BeginArea and provide Rect
                    GUILayout.BeginArea(new Rect(10, Screen.height - LogHeight - offset, Screen.width - 40, Screen.height - 100));
                    scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(Screen.width - 40), GUILayout.Height(LogHeight));
                    // We just add a single label to go inside the scroll view. Note how the
                    // scrollbars will work correctly with wordwrap.

                    GUIStyle textStyle = new GUIStyle(GUI.skin.textArea);
                    textStyle.fontSize = FontLogSize;
                   // GUILayout.TextArea(myLog, textStyle);
                    GUILayout.Label(myLog, textStyle);

                    // End the scrollview we began above.
                    GUILayout.EndScrollView();
                    GUILayout.EndArea();

                    if (ShowFPS)
                    {
                        string FPS = string.Format("{0:0.0} ms ({1:0.} fps)", deltaTime * 1000.0f, 1.0f / deltaTime);
                        GUI.Label(new Rect(new Rect(370, Screen.height - LogHeight - 40 - offset, 250, 40)), FPS, textStyle);
                    }


                }

                if (exitClicked)
                {
                    ShowLog = !ShowLog;
                    exitClicked = false;
                }

            }
        }
    }
    //#endif
}