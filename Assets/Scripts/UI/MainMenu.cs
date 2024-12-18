using General;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    internal sealed class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _starterObject;
        private const string c_startButton = "StartButton",
            c_quitButton = "QuitButton",
            c_joysticksToggle = "Joystics_Toggle";
        private bool _isJoysticksUse;
        private Button _startButton;
        private Button _quitButton;
        private Toggle _joysticksToggle;

        private void Awake()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement; 
            _joysticksToggle = root.Q<Toggle>(c_joysticksToggle);
            _startButton = root.Q<VisualElement>(c_startButton).Q<Button>();
            _quitButton = root.Q<VisualElement>(c_quitButton).Q<Button>();
 #if UNITY_ANDROID || UNITY_IOS
             _joysticksToggle.ShowElement(false);
             _joysticksToggle.value = true;
#endif
        }
        
        private void QuitGame() => Application.Quit();

        private void StartGame() => _starterObject.GetComponent<IGameStarter>().Initialize(_joysticksToggle.value);

        private void OnEnable()
        {
            _startButton.clicked += StartGame;
            _quitButton.clicked += QuitGame;
        }
        
        private void OnDisable()
        {
            _startButton.clicked -= StartGame;
            _quitButton.clicked -= QuitGame;
        }
    }
}