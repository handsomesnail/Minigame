using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Biz.Map {
    public interface IAttachable {

        /// <summary>当前fixUpdate移动Offset</summary>
        Vector3 CurrentMoveOffset { get; }

        /// <summary>移动时每帧调用</summary>
        void OnPlayerMove(Vector2 moveForce);

        /// <summary>依附瞬间时调用</summary>
        void OnStartAttached(Rigidbody2D playerRigidbod);

        /// <summary>是否锁死(像飞机那样)</summary>
        bool IsLock();

    }
}