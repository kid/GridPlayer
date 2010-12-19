using System;
using System.Collections.Generic;
using System.Linq;
using Gr1d.Api.Agent;

namespace Gr1d.KidPaddle
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Randomizes the specified source.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
        {
            Random rnd = new Random();
            return source.OrderBy<T, int>((item) => rnd.Next());
        }

        /// <summary>
        /// Determines whether the specified agent as the specified effects.
        /// </summary>
        /// <param name="agent">The agent.</param>
        /// <param name="affect">The affect.</param>
        /// <returns>
        /// 	<c>true</c> if [has] [the specified agent]; otherwise, <c>false</c>.
        /// </returns>
        public static bool Has(this IAgentInfo agent, AgentEffect affect)
        {
            return agent.Effects.Contains(affect);
        }

        public static IEnumerable<IAgentInfo> With(this IEnumerable<IAgentInfo> agents, AgentEffect affect)
        {
            return agents.Where(x => x.Has(affect));
        }

        public static IEnumerable<IAgentInfo> Without(this IEnumerable<IAgentInfo> agents, AgentEffect affect)
        {
            return agents.Where(x => !x.Has(affect));
        }
    }
}
