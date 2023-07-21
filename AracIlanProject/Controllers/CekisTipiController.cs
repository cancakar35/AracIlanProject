using Business.Abstract;
using Entities.Concrete;

namespace AracIlanProject.Controllers
{
    public class CekisTipiController : OzellikController<CekisTipi>
    {
        public CekisTipiController(IOzellikService<CekisTipi> ozellikService) : base(ozellikService)
        {
        }
    }
}
