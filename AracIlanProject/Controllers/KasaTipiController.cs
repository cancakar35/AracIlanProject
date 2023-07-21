using Business.Abstract;
using Entities.Concrete;

namespace AracIlanProject.Controllers
{
    public class KasaTipiController : OzellikController<KasaTipi>
    {
        public KasaTipiController(IOzellikService<KasaTipi> ozellikService) : base(ozellikService)
        {
        }
    }
}
