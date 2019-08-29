using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZCore {

    /// <summary>模块控制器，只包含行为</summary>
    public abstract class Controller<TModel, TView> : CallerBehaviour where TModel : Model, new() where TView : View, new() {

        private TModel model;
        private TView view;

        protected TModel Model {
            get {
                if (model == null)
                    model = Core.GetModel<TModel>();
                return model;
            }
        }

        protected TView View {
            get {
                if (view == null)
                    view = Core.GetView<TView>();
                return view;
            }
        }

    }

    public abstract class Controller<TModel> : CallerBehaviour where TModel : Model, new() {
        private TModel model;
        protected TModel Model {
            get {
                if (model == null)
                    model = Core.GetModel<TModel>();
                return model;
            }
        }
    }
}