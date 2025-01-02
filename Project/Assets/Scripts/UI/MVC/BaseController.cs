using System;

namespace Factura.UI.MVC
{
    public abstract class BaseController : IDisposable
    {
        public BaseView DerivedView { get; private set; }
        protected IModel DerivedModel { get; private set; }

        public virtual void Initialize(BaseView derivedView, IModel derivedModel)
        {
            DerivedView = derivedView;
            DerivedModel = derivedModel;
        }

        public void Dispose()
        {
            Hide();
        }

        protected virtual void Hide()
        {
        }
    }

    public abstract class BaseController<TModel> : BaseController where TModel : IModel
    {
        protected TModel Model { get; private set; }

        public override void Initialize(BaseView derivedView, IModel derivedModel)
        {
            base.Initialize(derivedView, derivedModel);

            if (derivedModel is not TModel model)
            {
                throw new InvalidCastException();
            }

            Model = model;
        }
    }

    public abstract class BaseController<TModel, TView> : BaseController<TModel>
        where TModel : IModel
        where TView : BaseView
    {
        protected TView View { get; private set; }

        public sealed override void Initialize(BaseView derivedView, IModel derivedModel)
        {
            base.Initialize(derivedView, derivedModel);

            if (derivedView is not TView view)
            {
                throw new InvalidCastException();
            }

            View = view;
            Show(Model, View);
        }

        protected virtual void Show(TModel model, TView view)
        {
            View = view;
        }
    }
}