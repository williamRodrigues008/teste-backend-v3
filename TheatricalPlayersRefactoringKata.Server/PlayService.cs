using TheatricalPlayersRefactoringKata.Server.ContextDb;
using TheatricalPlayersRefactoringKata.Server.Entities;
using TheatricalPlayersRefactoringKata.Server.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace TheatricalPlayersRefactoringKata.Server
{
    public class PlayService : PlayInterface
    {
        private readonly ContextDtaBase? _context;

        public PlayService(ContextDtaBase? context)
        {
            _context = context;
        }

        public void DeletePlay(Play play)
        {
            if (play == null) throw new ArgumentNullException("you have to assign a value to the play parameter");
            _context.Play.Remove(play);
        }

        public async Task<IEnumerable<Play>> GetAllPlay()
        {
            return _context.Play.ToList();
        }

        public void NewPlay(Play play)
        {
            if(play == null) throw new ArgumentNullException($"To create a new play, the parameter [play] can't be empty");
            _context.Play.Add(play);
        }

        public async Task<bool> SavePlay()
        {
                return await _context.SaveChangesAsync() > 0;
        }

        public void UpdatePlay(Play play)
        {
            string result = string.Empty;
            if (play != null)
            {
                _context.Play.Update(play);
            }
            else 
            {
                throw new ArgumentNullException($"The play don't be empty");
            }
        }
    }
}
