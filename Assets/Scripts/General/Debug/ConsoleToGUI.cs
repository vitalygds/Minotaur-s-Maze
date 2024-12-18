using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace General
{
    public class ConsoleToGUI : MonoBehaviour, IConsoleInputListener
    {
        private string _myLog = "LOG:";
        private string _filename = string.Empty;
        private bool _doShow;
        private IInputService _inputService;
        private const int c_kChars = 700;

        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
            _inputService.AddConsoleListener(this);
            DontDestroyOnLoad(this);
        }

        private void OnEnable() => Application.logMessageReceived += Log;

        private void OnDisable() => Application.logMessageReceived -= Log;

        private void Log(string logString, string stackTrace, LogType type)
        {
            _myLog = _myLog + "\n" + logString;
            if (_myLog.Length > c_kChars)
            {
                _myLog = _myLog[^c_kChars..];
            }

            if (_filename == string.Empty)
            {
                string d = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/LOGS";
                System.IO.Directory.CreateDirectory(d);
                string r = Random.Range(1000, 9999).ToString();
                _filename = d + "/log-" + r + ".txt";
            }

            try
            {
                System.IO.File.AppendAllText(_filename, logString + "\n");
            }
            catch
            {
                // ignored
            }
        }

        private void OnGUI()
        {
            if (!_doShow)
            {
                return;
            }

            GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity,
                new Vector3(Screen.width / 1200.0f, Screen.height / 800.0f, 1.0f));
            GUI.TextArea(new Rect(10, 10, 540, 370), _myLog);
        }

        public void OnShow(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _doShow = !_doShow;
            }
        }

        private void OnDestroy()
        {
            _inputService?.RemoveConsoleListener(this);
        }
    }
}