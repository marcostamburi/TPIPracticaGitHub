using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Data
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly ApplicationContext _context;

        public UsuarioRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public Usuario? GetByUsername(string userName)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Username == userName);
        }
    }
}