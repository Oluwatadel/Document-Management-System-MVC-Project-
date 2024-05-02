using DMSMVC.Models.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSMVC.Repository.Interface
{
    public interface IChatRepository : IBaseRepository<Chat>
    {
    }
}
