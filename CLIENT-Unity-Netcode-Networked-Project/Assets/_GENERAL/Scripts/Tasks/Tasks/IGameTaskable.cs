using System.Threading.Tasks;

namespace Game.Tasks
{
    public interface IGameTaskable
    {
        public string id { get; }
        public Task ExecuteInternal();
    }

}
