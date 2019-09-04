using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ZCore {

    internal enum CoreType {
        Module = 0,
        Controller,
        View,
        Model,
        Service,
        Command,
        Exception,
    }

    /// <summary>框架核心实现，仅框架层代码可访问</summary>
    internal static class Core {

        private static readonly Dictionary<Type, object> controllersDic; //Controller Type作为key
        private static readonly Dictionary<Type, object> modelsDic; //Model Type作为key
        private static readonly Dictionary<Type, object> viewsDic; //View Type作为key
        private static readonly Dictionary<Type, Delegate> commandDelegateDic; // Command Type作为key

        private static readonly GameObject core;
        private static readonly GameObject controllers;

        private const string viewResRootPath = "Views/";

        public static Assembly MainAssembly {
            get;
            private set;
        }

        static Core() {
            MainAssembly = Assembly.Load("Assembly-CSharp");
            controllersDic = new Dictionary<Type, object>();
            modelsDic = new Dictionary<Type, object>();
            viewsDic = new Dictionary<Type, object>();
            commandDelegateDic = new Dictionary<Type, Delegate>();
            core = new GameObject("Core");
            UnityEngine.Object.DontDestroyOnLoad(core);
            controllers = new GameObject("Controllers");
            controllers.transform.SetParent(core.transform);
        }

        private static object GetController(ICommand cmd) {
            try {
                return GetController(cmd.GetController());
            }
            catch (CoreException) {
                throw new CoreException(string.Format("[Core.GetController]The controller class ( depend on {0}) named {1} doesn't inherit from ZCore.Controller<,>", cmd.GetType().Name, cmd.GetController().Name));
            }
        }

        //全部lazyload
        public static object GetController(Type controllerType) {
            object controller = null;
            if (!controllersDic.TryGetValue(controllerType, out controller)) {
                if (!controllerType.IsSubClassOfRawGeneric(typeof(Controller<,>)) && !controllerType.IsSubClassOfRawGeneric(typeof(Controller<>))) {
                    throw new CoreException(string.Format("[Core.GetController]The controller class named {0} doesn't inherit from ZCore.Controller", controllerType.Name));
                }
                GameObject newController = new GameObject(controllerType.Name, controllerType);
                newController.transform.SetParent(controllers.transform);
                controller = newController.GetComponent(controllerType);
                controllersDic.Add(controllerType, controller);
            }
            return controller;
        }

        public static TModel GetModel<TModel>() where TModel : Model, new() {
            object model = null;
            if (!modelsDic.TryGetValue(typeof(TModel), out model)) {
                model = new TModel();
                modelsDic.Add(typeof(TModel), model);
            }
            return model as TModel;
        }

        public static TView GetView<TView>() where TView : View, new() {
            object view = null;
            if (!viewsDic.TryGetValue(typeof(TView), out view) || view as TView == null) {
                GameObject viewGoPrefeb = null;
                viewGoPrefeb = Resources.Load<GameObject>(viewResRootPath + typeof(TView).Name);
                if (viewGoPrefeb == null) {
                    throw new CoreException(string.Format("[Core.GetView]Couldn't find the view asset named {0} at the path : {1}", typeof(TView).Name, viewResRootPath + typeof(TView).Name));
                }
                if (viewGoPrefeb.GetComponent<TView>() == null) {
                    throw new CoreException(string.Format("[Core.GetView]The view asset named {0} isn't attached view script", typeof(TView).Name));
                }
                GameObject viewGo = GameObject.Instantiate<GameObject>(viewGoPrefeb);
                view = viewGo.GetComponent<TView>();
                viewsDic[typeof(TView)] = view;
            }
            return view as TView;
        }

        //指定强类型直接进行委托调用,调用速度更快(推荐) 
        public static void Call<TCommand>(TCommand cmd) where TCommand : ICommand {
            Type commandType = typeof(TCommand);
            Delegate action = null;
            if (!commandDelegateDic.TryGetValue(commandType, out action)) {
                Type controllerType = cmd.GetController();
                object controller = GetController(cmd);
                MethodInfo methodInfo = controllerType.GetMethod(string.Format("On{0}", cmd.GetType().Name), BindingFlags.Public | BindingFlags.Instance);
                if (methodInfo == null) {
                    throw new CoreException(string.Format("[Core.Call]Unhandled Command : {0} for {1}", cmd.GetType().Name, controllerType.Name));
                }
                action = methodInfo.CreateDelegate(typeof(Action<TCommand>), controller);
                commandDelegateDic.Add(commandType, action);
            }
            (action as Action<TCommand>) (cmd);
        }

        public static TResult Post<TCommand, TResult>(TCommand cmd) where TCommand : ICommand {
            Type commandType = typeof(TCommand);
            Delegate func = null;
            if (!commandDelegateDic.TryGetValue(commandType, out func)) {
                Type controllerType = cmd.GetController();
                object controller = GetController(cmd);
                MethodInfo methodInfo = controllerType.GetMethod(string.Format("On{0}", cmd.GetType().Name), BindingFlags.Public | BindingFlags.Instance);
                if (methodInfo == null) {
                    throw new CoreException(string.Format("[Core.Post]Unhandled Command : {0} for {1}", cmd.GetType().Name, controllerType.Name));
                }
                func = methodInfo.CreateDelegate(typeof(Func<TCommand, TResult>), controller);
                commandDelegateDic.Add(commandType, func);
            }
            return (func as Func<TCommand, TResult>) (cmd);
        }

        //call不带参字符串cmd
        public static void Call(string cmdFullName) {
            Type commandType = MainAssembly.GetType(cmdFullName);
            Command cmd = Activator.CreateInstance(commandType) as Command;
            Delegate action = null;
            if (!commandDelegateDic.TryGetValue(commandType, out action)) {
                Type controllerType = cmd.GetController();
                object controller = GetController(cmd);
                MethodInfo methodInfo = controllerType.GetMethod(string.Format("On{0}", cmd.GetType().Name), BindingFlags.Public | BindingFlags.Instance);
                if (methodInfo == null) {
                    throw new CoreException(string.Format("[Core.Call]Unhandled Command : {0} for {1}", cmd.GetType().Name, controllerType.Name));
                }
                methodInfo.Invoke(controller, new object[] { cmd });
            }
            else action.DynamicInvoke(cmd);
        }

        //未指定强类型 使用MethodInfo.Invoke调用方法或使用后期绑定的方式调用委托
        public static object Post(ICommand cmd) {
            Type commandType = cmd.GetType();
            Delegate func = null;
            if (!commandDelegateDic.TryGetValue(commandType, out func)) {
                Type controllerType = cmd.GetController();
                object controller = GetController(cmd);
                MethodInfo methodInfo = controllerType.GetMethod(string.Format("On{0}", cmd.GetType().Name), BindingFlags.Public | BindingFlags.Instance);
                if (methodInfo == null) {
                    throw new CoreException(string.Format("[Core.Post]Unhandled Command : {0} for {1}", cmd.GetType().Name, controllerType.Name));
                }
                return methodInfo.Invoke(controller, new object[] { cmd });
            }
            else return func.DynamicInvoke(cmd);
        }

    }

}