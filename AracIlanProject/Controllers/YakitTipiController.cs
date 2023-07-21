using Business.Abstract;
using Entities.Concrete;

namespace AracIlanProject.Controllers
{
    public class YakitTipiController : OzellikController<YakitTipi>
    {
        public YakitTipiController(IOzellikService<YakitTipi> ozellikService) : base(ozellikService)
        {
        }
    }
}
