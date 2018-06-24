using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Extensions
{
    public interface IJoinEntity<TEntity>
    {
        TEntity Navigation { get; set; }
    }
}
