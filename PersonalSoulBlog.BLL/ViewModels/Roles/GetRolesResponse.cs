using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalSoulBlog.BLL.ViewModels.Roles
{
    /// <summary>
    /// Модель для получения ответа о ролях
    /// </summary>
    public class GetRolesResponse
    {
        public int RolesAmount { get; set; }
        public List<RoleView> Roles { get; set; }
    }
}
