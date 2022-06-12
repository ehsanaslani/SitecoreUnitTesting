using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Sitecore;
using Sitecore.Diagnostics;

namespace TestingTools.Switchers
{
    public class ServiceProviderSwitcher : IDisposable
    {
        private static string _itemsKey;
        private int _disposed;

        protected event EventHandler<EventArgs> Disposed;

        public ServiceProviderSwitcher(ServiceCollection objectToSwitchTo) => ServiceProviderSwitcher.Enter(objectToSwitchTo);

        protected ServiceProviderSwitcher()
        {
        }

        public static ServiceCollection CurrentValue
        {
            get
            {
                Stack<ServiceCollection> stack = ServiceProviderSwitcher.GetStack(false);
                return stack == null || stack.Count == 0 ? null : stack.Peek();
            }
        }

        public static string ItemsKey
        {
            get
            {
                if (ServiceProviderSwitcher._itemsKey == null)
                    ServiceProviderSwitcher._itemsKey = typeof(ServiceProviderSwitcher).Name + "Switcher_State";
                return ServiceProviderSwitcher._itemsKey;
            }
        }

        public static void Enter(ServiceCollection serviceCollectionToSwitchTo)
        {
            Assert.ArgumentNotNull((object)serviceCollectionToSwitchTo, nameof(serviceCollectionToSwitchTo));
            ServiceProviderSwitcher.GetStack(true).Push(serviceCollectionToSwitchTo);
        }

        public static void Exit()
        {
            Stack<ServiceCollection> stack = ServiceProviderSwitcher.GetStack(false);
            Assert.IsTrue(stack != null && stack.Count > 0, "Stack is null or empty.");
            stack.Pop();
        }

        public static Stack<ServiceCollection> GetStack(bool createIfEmpty)
        {
            Stack<ServiceCollection> objStack = Context.Items[ServiceProviderSwitcher.ItemsKey] as Stack<ServiceCollection>;
            if (objStack == null & createIfEmpty)
            {
                objStack = new Stack<ServiceCollection>();
                Context.Items[ServiceProviderSwitcher.ItemsKey] = (object)objStack;
            }
            return objStack;
        }

        public virtual void Dispose()
        {
            if (Interlocked.CompareExchange(ref this._disposed, 1, 0) != 0)
                return;
            ServiceProviderSwitcher.Exit();
            EventHandler<EventArgs> disposed = this.Disposed;
            if (disposed == null)
                return;
            disposed((object)this, EventArgs.Empty);
        }
    }
}
