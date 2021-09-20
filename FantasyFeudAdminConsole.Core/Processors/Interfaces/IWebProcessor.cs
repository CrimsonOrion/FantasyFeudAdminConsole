using FantasyFeudAdminConsole.Core.Models;

using System.Threading.Tasks;

namespace FantasyFeudAdminConsole.Core.Processors
{
    public interface IWebProcessor
    {
        Task PostEvent(QuestionModel questionModel);
    }
}