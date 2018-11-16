using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBetterTeams
{
    class UserDTO
    {

        #region User Properties
        public string firstname { get; set; }
        public string lastname { get; set; }
        public DateTime dateofbirth { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string usertype = "Student";
        #endregion
    }
}
