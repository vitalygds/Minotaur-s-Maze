using General;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public sealed class PauseMenu : MonoBehaviour
    {
        private const string c_resume = "ResumeButton", c_restart = "RestartButton", c_quit = "QuitButton", c_header = "Header";
        private const string c_pause = "PAUSE", c_gameOver = "GAME OVER";
        private IControllersMediator _mediator;
        private VisualElement _root;
        private Button _resumeButton;
        private Button _restartButton;
        private Button _quitButton;
        private Label _headerField;

        public void Construct(IControllersMediator mediator)
        {
            _mediator = mediator;
            InitializeElements();
            InitializeView();
            Show(false);
            _resumeButton.clicked += ContinueGame;
            _restartButton.clicked += RestartGame;
            _quitButton.clicked += QuitGame;
        }

        private void QuitGame()
        {
            _mediator.QuitGame();
        }

        private void RestartGame()
        {
            _mediator.RestartGame();
            Show(false);
            InitializeView();
        }

        public void SetGameOver()
        {
            _resumeButton.SetEnabled(false);
            _headerField.text = c_gameOver;
            _headerField.style.color = new StyleColor(Color.red);
        }
        
        public void Show(bool isShow)
        {
            _root.style.visibility = isShow ? Visibility.Visible : Visibility.Hidden;
        }

        private void InitializeElements()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _resumeButton = _root.Q<VisualElement>(c_resume).Q<Button>();
            _restartButton = _root.Q<VisualElement>(c_restart).Q<Button>();
            _quitButton = _root.Q<VisualElement>(c_quit).Q<Button>();
            _headerField = _root.Q<Label>(c_header);
        }

        private void InitializeView()
        {
            _headerField.text = c_pause;
            _headerField.style.color = new StyleColor(Color.white);
            _resumeButton.SetEnabled(true);
        }

        private void ContinueGame()
        {
            Show(false);
            _mediator.ContinueGame();
        }

        private void OnDestroy()
        {
            _resumeButton.clicked -= ContinueGame;
            _restartButton.clicked -= RestartGame;
            _quitButton.clicked -= QuitGame;
        }
    }
}