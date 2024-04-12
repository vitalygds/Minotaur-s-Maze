using System;
using System.Collections.Generic;

namespace MyGame.General.Controller
{
    public sealed class Controllers
    {
        private readonly Dictionary<Type, IController> _controllersMap;
        private readonly List<IStart> _startControllers;
        private readonly List<IUpdate> _updateControllers;
        private readonly List<IFixedUpdate> _fixedControllers;
        private readonly List<ILateUpdate> _lateControllers;
        private readonly List<IDestroy> _destroyControllers;

        public Controllers(int capacity = 4)
        {
            _controllersMap = new Dictionary<Type, IController>(capacity);
            _startControllers = new List<IStart>(capacity);
            _updateControllers = new List<IUpdate>(capacity);
            _fixedControllers = new List<IFixedUpdate>(capacity);
            _lateControllers = new List<ILateUpdate>(capacity);
            _destroyControllers = new List<IDestroy>(capacity);
        }

        public void Add(IController controller, Type controllerType)
        {
            _controllersMap[controllerType] = controller;
            if (controller is IStart initialize)
            {
                _startControllers.Add(initialize);
            }

            if (controller is IUpdate update)
            {
                _updateControllers.Add(update);
            }

            if (controller is IFixedUpdate fixedUpdate)
            {
                _fixedControllers.Add(fixedUpdate);
            }

            if (controller is ILateUpdate lateUpdate)
            {
                _lateControllers.Add(lateUpdate);
            }

            if (controller is IDestroy destroy)
            {
                _destroyControllers.Add(destroy);
            }
        }

        public void Remove<T>() where T : IController
        {
            if (_controllersMap.TryGetValue(typeof(T), out IController controller))
            {
                _controllersMap.Remove(typeof(T));
                if (controller is IStart initialize)
                    _startControllers.Remove(initialize);

                if (controller is IUpdate update)
                    _updateControllers.Remove(update);

                if (controller is IFixedUpdate fixedUpdate)
                    _fixedControllers.Remove(fixedUpdate);

                if (controller is ILateUpdate lateUpdate)
                    _lateControllers.Remove(lateUpdate);

                if (controller is IDestroy destroy)
                {
                    destroy.Destroy();
                    _destroyControllers.Remove(destroy);
                }
            }
        }

        public void Start()
        {
            for (var index = 0; index < _startControllers.Count; ++index)
            {
                _startControllers[index].Start();
            }
        }

        public void Update()
        {
            for (var index = 0; index < _updateControllers.Count; ++index)
            {
                _updateControllers[index].Update();
            }
        }

        public void FixedUpdate()
        {
            for (var index = 0; index < _fixedControllers.Count; ++index)
            {
                _fixedControllers[index].FixedUpdate();
            }
        }

        public void LateUpdate()
        {
            for (var index = 0; index < _lateControllers.Count; ++index)
            {
                _lateControllers[index].LateUpdate();
            }
        }

        public void Destroy()
        {
            for (var index = 0; index < _destroyControllers.Count; ++index)
            {
                _destroyControllers[index].Destroy();
            }
            _startControllers.Clear();
            _updateControllers.Clear();
            _fixedControllers.Clear();
            _lateControllers.Clear();
            _destroyControllers.Clear();
            _controllersMap.Clear();
        }
    }
}