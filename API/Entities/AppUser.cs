using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class AppUser
    {
        //Creating Id for the database and it will also work as the primary key
        public int Id { get; set; }

        //UserName - string for database
        public string UserName { get; set; }
    }
}