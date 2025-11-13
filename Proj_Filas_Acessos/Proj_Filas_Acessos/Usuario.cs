using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj_Filas_Acessos
{
    internal class Usuario
    {
        private int id;
        private string name;
        private List<Ambiente> ambientes;
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        internal List<Ambiente> Ambientes { get => ambientes; set => ambientes = value; }
        public Usuario(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        public Usuario() : this(-1,"") { }

        public bool concederPermissao(Ambiente ambiente)
        {
            return false;
        }
        public bool RevogarPermisao(Ambiente ambiente)
        {
            return false;
        }
    }
}
