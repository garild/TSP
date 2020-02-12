using Elastic.Apm;
using Elastic.Apm.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Tsp.AuthService.Controllers
{
    public static class TraceApm
    {
        public static void Capture(this ITracer transaction, Expression<Action> action)
        {
            var methodCallExp = (MethodCallExpression)action.Body;
            var type = methodCallExp.Object.Type.FullName;
            transaction.CurrentTransaction.CaptureSpan(methodCallExp.Method.Name, type, () => action);
        }
    }
}
