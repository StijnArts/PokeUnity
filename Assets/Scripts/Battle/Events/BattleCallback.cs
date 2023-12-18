using System;
using System.Reflection;

namespace Assets.Scripts.Battle.Events
{
    public class VoidBattleCallback : DynamicInvokable
    {
        public Action Callback;
        public VoidBattleCallback(Action callback, int? order = null, int? priority = null, int? subOrder = null)
        {
            Callback = callback;
            Order = order;
            Priority = priority;
            SubOrder = subOrder;
        }

        public void InvokeVoid(params object[] args)
        {
            ParameterInfo[] parameters = Callback.GetType().GetMethod("Invoke").GetParameters();
            var dynamicParameters = GetDynamicParameters(parameters, args);
            Callback.DynamicInvoke(dynamicParameters);
        }

        public override object Invoke(params object[] args)
        {
            return null;
        }
    }

    public class BattleCallback<T> : DynamicInvokable
    {
        public Func<T> Callback;
        public BattleCallback(Func<T> callback, int? order = null, int? priority = null, int? subOrder = null)
        {
            Callback = callback;
            Order = order;
            Priority = priority;
            SubOrder = subOrder;
        }

        public override object Invoke(params object[] args)
        {
            ParameterInfo[] parameters = Callback.GetType().GetMethod("Invoke").GetParameters();
            var dynamicParameters = GetDynamicParameters(parameters, args);
            return Callback.DynamicInvoke(dynamicParameters);
        }
    }

    public class BattleCallback<T1, TResult> : DynamicInvokable
    {
        public Func<T1, TResult> Callback;
        public BattleCallback(Func<T1, TResult> callback, int? order = null, int? priority = null, int? subOrder = null)
        {
            Callback = callback;
            Order = order;
            Priority = priority;
            SubOrder = subOrder;
        }

        public override object Invoke(params object[] args)
        {
            ParameterInfo[] parameters = Callback.GetType().GetMethod("Invoke").GetParameters();
            var dynamicParameters = GetDynamicParameters(parameters, args);
            return Callback.DynamicInvoke(dynamicParameters);
        }
    }

    public class BattleCallback<T1, T2, TResult> : DynamicInvokable
    {
        public Func<T1, T2, TResult> Callback;
        public BattleCallback(Func<T1, T2, TResult> callback, int? order = null, int? priority = null, int? subOrder = null)
        {
            Callback = callback;
            Order = order;
            Priority = priority;
            SubOrder = subOrder;
        }

        public override object Invoke(params object[] args)
        {
            ParameterInfo[] parameters = Callback.GetType().GetMethod("Invoke").GetParameters();
            var dynamicParameters = GetDynamicParameters(parameters, args);
            return Callback.DynamicInvoke(dynamicParameters);
        }
    }

    public class BattleCallback<T1, T2, T3, TResult> : DynamicInvokable
    {
        public Func<T1, T2, T3, TResult> Callback;
        public BattleCallback(Func<T1, T2, T3, TResult> callback, int? order = null, int? priority = null, int? subOrder = null)
        {
            Callback = callback;
            Order = order;
            Priority = priority;
            SubOrder = subOrder;
        }

        public override object Invoke(params object[] args)
        {
            ParameterInfo[] parameters = Callback.GetType().GetMethod("Invoke").GetParameters();
            var dynamicParameters = GetDynamicParameters(parameters, args);
            return Callback.DynamicInvoke(dynamicParameters);
        }
    }

    public class BattleCallback<T1, T2, T3, T4, TResult> : DynamicInvokable
    {
        public Func<T1, T2, T3, T4, TResult> Callback;
        public BattleCallback(Func<T1, T2, T3, T4, TResult> callback, int? order = null, int? priority = null, int? subOrder = null)
        {
            Callback = callback;
            Order = order;
            Priority = priority;
            SubOrder = subOrder;
        }

        public override object Invoke(params object[] args)
        {
            ParameterInfo[] parameters = Callback.GetType().GetMethod("Invoke").GetParameters();
            var dynamicParameters = GetDynamicParameters(parameters, args);
            return Callback.DynamicInvoke(dynamicParameters);
        }
    }

    public class BattleCallback<T1, T2, T3, T4, T5, TResult> : DynamicInvokable
    {
        public Func<T1, T2, T3, T4, T5, TResult> Callback;
        public BattleCallback(Func<T1, T2, T3, T4, T5, TResult> callback, int? order = null, int? priority = null, int? subOrder = null)
        {
            Callback = callback;
            Order = order;
            Priority = priority;
            SubOrder = subOrder;
        }

        public override object Invoke(params object[] args)
        {
            ParameterInfo[] parameters = Callback.GetType().GetMethod("Invoke").GetParameters();
            var dynamicParameters = GetDynamicParameters(parameters, args);
            return Callback.DynamicInvoke(dynamicParameters);
        }
    }
}
