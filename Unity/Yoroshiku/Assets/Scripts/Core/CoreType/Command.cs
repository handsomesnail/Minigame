using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZCore {

    public interface ICommand {
        Type GetController();
    }

    public abstract class Command : ICommand {
        public abstract Type GetController();
    }

}