using System;
using System.Collections.Generic;

namespace SilverlightRun.Util
{
    /// <summary>
    /// After initialisation and registration of an action, fires only on the first FireWith.
    /// </summary>
    /// <typeparam name="T">Any type as event argument.</typeparam>
    public class OneTimeEvent<T>
    {
        bool _fired;
        List<Action<T>> _actions;

        public OneTimeEvent(Action<T> action)
        {
            _fired = false;
            _actions = new List<Action<T>>() { action };
        }

        public OneTimeEvent()
            : this((t) => { })
        {
        }

        public void Register(Action<T> action)
        {
            _actions.Add(action);
        }

        public OneTimeEvent<T> When(bool condition)
        {
            if (condition)
                return this;
            else return new OneTimeEvent<T>();
        }

        public void FireWith(T t)
        {
            if (!_fired) _actions.ForEach((a) => a(t));
            _fired = true;
        }

        public void Restart()
        {
            _fired = false;
        }
    }
}
