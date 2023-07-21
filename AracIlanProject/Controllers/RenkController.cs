using Business.Abstract;
using Business.Concrete;
using Entities.Concrete;

namespace AracIlanProject.Controllers
{
    public class RenkController : OzellikController<Renk>
    {
        public RenkController(IOzellikService<Renk> renkService) : base(renkService)
        {
        }
    }
}
