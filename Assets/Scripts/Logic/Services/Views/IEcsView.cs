using General;
using Leopotam.EcsLite;

namespace Logic
{
    public interface IEcsView : IView
    {
        void InitializeView(EcsWorld defaultWorld, EcsWorld eventWorld, int entity);
    }
}