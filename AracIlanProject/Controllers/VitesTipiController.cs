using Business.Abstract;
using Entities.Concrete;

namespace AracIlanProject.Controllers
{
    public class VitesTipiController : OzellikController<VitesTipi>
    {
        public VitesTipiController(IOzellikService<VitesTipi> ozellikService) : base(ozellikService)
        {
        }
    }
}
