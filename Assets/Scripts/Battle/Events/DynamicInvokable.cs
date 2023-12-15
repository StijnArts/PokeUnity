using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static UnityEditor.ShaderData;

namespace Assets.Scripts.Battle.Events
{
    public abstract class DynamicInvokable
    {
        public int? Order;
        public int? Priority;
        public int? SubOrder;
        public abstract object Invoke(params object[] args);

        public object[] GetDynamicParameters(ParameterInfo[] methodParameters, object[] input)
        {
            var parameterInput = input.ToList();
            var dynamicParameters = new object[methodParameters.Count()];
            for (int i = 0; i < methodParameters.Length; i++)
            {
                var foundArgs = Enumerable.Range(0, parameterInput.Count)
                    .Where(index => parameterInput[index].GetType() == methodParameters[i].ParameterType).ToArray();
                if (foundArgs.Length >= 1)
                {
                    var arg = parameterInput[foundArgs[0]];
                    dynamicParameters[i] = arg;
                    parameterInput.RemoveAt(foundArgs[0]);
                }
            }
            return dynamicParameters;
        }
    }
}
