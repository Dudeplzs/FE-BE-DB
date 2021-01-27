using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListWebAPI.Models
{
    public class Lista
    {
        public int Id { get; set; }

        public string Nome{ get; set; }

        public Boolean Estado { get; set; }
    }
}
