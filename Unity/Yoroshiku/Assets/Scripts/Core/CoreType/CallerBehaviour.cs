using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZCore {

    public class CallerBehaviour : MonoBehaviour {

        protected void Call<TCommand>(TCommand cmd) where TCommand : ICommand {
            Core.Call<TCommand>(cmd);
        }

        protected void Call(string cmdFullName) {
            Core.Call(cmdFullName);
        }

        protected object Post(Command cmd) {
            return Core.Post(cmd);
        }

        protected TResult Post<TCommand, TResult>(TCommand cmd) where TCommand : ICommand {
            return Core.Post<TCommand, TResult>(cmd);
        }

    }

}