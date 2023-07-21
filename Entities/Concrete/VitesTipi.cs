﻿using Core.Entities.Abstract;
using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class VitesTipi : IEntity, IOzellik
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
