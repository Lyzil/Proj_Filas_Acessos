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
        private string nome;
        private List<Ambiente> ambientes = new List<Ambiente>();
        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        internal List<Ambiente> Ambientes { get => ambientes; set => ambientes = value; }
        public Usuario(int id, string name)
        {
            this.Id = id;
            this.Nome = name;
        }
        public Usuario() : this(-1,"") { }
        public Usuario(int id) : this(id, "") { }
        public bool concederPermissao(Ambiente ambiente)
        {
            if (!ambientes.Any(a => a.Id == ambiente.Id))
            {
                ambientes.Add(ambiente);
                return true;
            }
            return false;
        }
        public bool revogarPermissao(Ambiente ambiente)
        {
            return Ambientes.RemoveAll(a => a.Id == ambiente.Id) > 0;
        }
        public override bool Equals(object obj)
        {
            if (obj is Usuario u)
                return this.Id == u.Id;
            return false;
        }
    }
}
