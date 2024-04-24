using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPMS.Domain.Interfaces.RepositoryInterfaces.BaseInterfaces
{
    public interface IBaseRepository<TModel> : ICreateRepository<TModel>, IReadRepository<TModel>, IUpdateRepository<TModel>, IDeleteRepository<TModel> where TModel : class
    {
    }
}
