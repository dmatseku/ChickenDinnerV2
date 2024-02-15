using ChickenDinnerV2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChickenDinnerV2.Core
{
    class ObserverManager
    {
        protected List<IObserver> Observers = new List<IObserver>();

        public void Register()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(IObserver).IsAssignableFrom(p) && p != typeof(IObserver));

            foreach (Type observerType in types)
            {
                IObserver instance = (IObserver)Activator.CreateInstance(observerType);
                if (instance.Register())
                {
                    Observers.Add(instance);
                }
            }
        }

        public void Unregister()
        {
            foreach (IObserver observer in Observers)
            {
                observer.Unregister();
            }
            Observers = new List<IObserver>();
        }
    }
}
