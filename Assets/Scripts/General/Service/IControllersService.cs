﻿using MyGame.General.Controller;
using MyGame.General.Data;
using UnityEngine;

namespace MyGame.General.Service
{
    public interface IControllersService
    {
        void CreateGameController(int controllersCapacity);
        void CreateUiController(IUIData uiData, IControllersMediator mediator, bool IsJoysticksUse);
        void CreateCameraController(Camera camera, ICameraData cameraData, IControllersMediator mediator);

        void CreateLogicController(ILevelStaticData levelData, IStaticData staticData, IRuntimeData runtimeData,
            ISceneSerializedData sceneSerializedData, IControllersMediator mediator);

        void StartControllers();
        void DestroyLogic();
    }
}