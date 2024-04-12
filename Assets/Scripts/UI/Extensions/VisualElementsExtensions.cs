using UnityEngine.UIElements;

namespace MyGame.Extensions
{
    internal static class VisualElementsExtensions
    {
        public static void ShowElement<T>(this T element, bool isShow) where T : VisualElement =>
            element.style.visibility = isShow ? Visibility.Visible : Visibility.Hidden;
        
        public static void FlexElement<T>(this T element, bool isShow) where T : VisualElement =>
            element.style.display = isShow ? DisplayStyle.Flex : DisplayStyle.None;
    }
}