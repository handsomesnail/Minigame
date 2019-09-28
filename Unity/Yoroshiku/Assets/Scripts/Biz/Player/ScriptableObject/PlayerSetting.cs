using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZCore;

namespace Biz.Player {

    [CreateAssetMenu(menuName = "ScriptableObject/PlayerSetting")]
    public class PlayerSetting : ScriptableObject {

        [Header("行走属性：")]
        [FieldName("移动(普通)加速度(m/s^2)")]
        public float Normal_MoveForce;

        [FieldName("最大移动(普通)速度(m/s)")]
        [Tooltip("仅指水平最大速度")]
        public float Normal_MaxMoveSpeed;

        [FieldName("摩擦(普通)减速度(m/s^2)")]
        [Tooltip("仅指水平减速度")]
        public float Normal_LinearDrag;

        [Space(5)]
        [FieldName("移动(溶入)加速度(m/s^2)")]
        public float Melted_MoveForce;

        [FieldName("最大移动(溶入)速度(m/s)")]
        [Tooltip("指水平和竖直的最大速度")]
        public float Melted_MaxMoveSpeed;

        [FieldName("摩擦(溶入)减速度(m/s^2)")]
        [Tooltip("指水平和竖直合速度的减速度")]
        public float Melted_LinearDrag;

        [Header("跳跃属性："), Space(10)]
        [FieldName("移动(空中)加速度(m/s^2)")]
        public float Air_MoveForce;

        [FieldName("最大移动(空中)速度(m/s)")]
        [Tooltip("仅指水平最大速度")]
        public float Air_MaxMoveSpeed;

        // [FieldName("最大掉落(空中)速度(m/s)")]
        // [Tooltip("仅指竖直最大速度")]
        //public float Air_MaxDropSpeed;

        [FieldName("摩擦(空中)减速度(m/s^2)")]
        [Tooltip("仅指水平减速度")]
        public float Air_LinearDrag;

        [FieldName("跳跃初始速度(m/s)")]
        public float JumpInitialSpeed;

        [FieldName("重力加速度(m/s^2)")]
        public float Gravity;

        [Header("其它手感："), Space(10)]
        [FieldName("反向移动增强系数(倍)")]
        public float InvertDirectionMultiplier;

        [FieldName("溶入判定滞后时间(s)")]
        public float MeltJudgeDuration;

        [FieldName("跳跃判定滞后时间(s)")]
        public float JumpJudgeDuration;

        [FieldName("溶入过程时间")]
        public float MeltInDuration;

        [FieldName("溶出过程时间")]
        public float MeltOutDuration;

        [FieldName("溶入推力系数")]
        public float MeltInPushMultiplier;

        [FieldName("溶出推力系数")]
        public float MeltOutPushMultiplier;

    }
}