﻿using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Domain.Repositories
{
    public interface IOrderRepository
    {
       // void Save(IOrderRepository order);
        void Save(Order order);
    }
}
