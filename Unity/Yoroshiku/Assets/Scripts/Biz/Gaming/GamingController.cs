using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Input;
using Biz.Map;
using Biz.Player;
using Cinemachine;
using UnityEngine;
using ZCore;

namespace Biz.Gaming {
    public sealed class GamingController : Controller<GamingModel, GamingView> {

        public void OnEnterCommand(EnterCommand cmd) {
            StartCoroutine(CoroutineExtension.Wait(null, () => {
                EnterMap(cmd.MapIndex);
            }));
        }

        public void OnExitCommand(ExitCommand cmd) {
            Model.GameStatus = GameStatus.None;
            View.Destroy();
        }

        public void OnPauseCommand(PauseCommand cmd) {
            Model.GameStatus = GameStatus.Pause;
        }

        public void OnResumeCommand(ResumeCommand cmd) {
            Model.GameStatus = GameStatus.Gaming;
        }

        public void OnCameraAnimCommand(CameraAnimCommand cmd) {
            CinemachineVirtualCamera camera = Model.Map.VirtualCamera;
            Animation animation = camera.GetComponent<Animation>();
            animation.clip = cmd.Clip;
            animation.Play();
        }

        private void EnterMap(int mapIndex) {
            Model.MapIndex = mapIndex;
            Model.GameStatus = GameStatus.Gaming;
            //先加载地图 获取地图数据(必须先执行该Command)
            Call(new Biz.Map.LoadCommand());
            //初始化Player
            Call(new Biz.Player.InitCommand());
            //初始化UI开始接收输入
            Call(new Biz.Input.InitCommand());
            // 存档，记录最后玩的关卡和解锁进度
            Call(new Biz.Storage.LastPlayCommand ());
        }

    }
}