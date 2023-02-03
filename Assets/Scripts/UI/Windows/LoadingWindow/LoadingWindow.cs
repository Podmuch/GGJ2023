using PDGames.UserInterface;

namespace BoxColliders.Windows
{
    public sealed class LoadingWindow : BaseWindow<LoadingWindowView>
    {
        public void UpdateView(float value, string localeKey, string param1, string param2)
        {
            view.UpdateView(value, localeKey, param1, param2);
        }
    }
}