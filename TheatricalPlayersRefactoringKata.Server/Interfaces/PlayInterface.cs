using TheatricalPlayersRefactoringKata.Server.Entities;

namespace TheatricalPlayersRefactoringKata.Server.Interfaces
{
    public interface PlayInterface
    {
        Task<IEnumerable<Play>> GetAllPlay();
        void DeletePlay(Play play);
        void UpdatePlay(Play play);
        void NewPlay(Play play);
        Task<bool> SavePlay();
    }
}
