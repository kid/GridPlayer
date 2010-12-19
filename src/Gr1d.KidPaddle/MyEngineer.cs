using System.Linq;
using Gr1d.Api.Agent;
using Gr1d.Api.Deck;
using Gr1d.Api.Skill;

namespace Gr1d.KidPaddle
{
    public class MyEngineer : IEngineer1
    {
        private IDeck deck;

        public void Initialise(IDeck deck)
        {
            this.deck = deck;
            this.UnitTest();
        }

        public void OnArrived(IAgentInfo arriver, IAgentUpdateInfo agentUpdate)
        {
            this.Attack(arriver);
        }

        public void OnAttacked(IAgentInfo attacker, IAgentUpdateInfo agentUpdate)
        {
            this.Attack(attacker);
        }

        public void Tick(IAgentUpdateInfo agentUpdate)
        {
            if (!agentUpdate.Has(AgentEffect.UnitTest))
            {
                this.UnitTest();
            }

            var currentNode = agentUpdate.Node;
            if (currentNode.IsClaimable)
            {
                this.Claim(currentNode);
            }

            var opponent = currentNode.OpposingAgents.FirstOrDefault();
            if (opponent != null)
            {
                this.Attack(opponent);
            }
            var nodes = agentUpdate.Node.Exits
                .Select(_ => _.Value)
                .Where(_ => _ != agentUpdate.NodePrevious)
                .Randomize()
                .GetEnumerator();
            while (nodes.MoveNext() && this.Move(nodes.Current).Result != NodeResultType.Success) ;
        }
    }
}
