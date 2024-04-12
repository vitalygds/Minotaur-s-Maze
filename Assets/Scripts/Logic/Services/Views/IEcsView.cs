using Leopotam.EcsLite;
using MyGame.General.View;

namespace MyGame.Logic.Services.Views
{
    public interface IEcsView : IView
    {
        void InitializeView(EcsWorld defaultWorld, EcsWorld eventWorld, int entity);
    }
}