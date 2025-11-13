using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj_Filas_Acessos
{
    internal class Cadastro
    {
        private List<Usuario> usuarios;
        private List<Ambiente> ambientes;
        internal List<Usuario> Usuarios { get => usuarios; set => usuarios = value; }
        internal List<Ambiente> Ambientes { get => ambientes; set => ambientes = value; }
        public void adicionarUsuario(Usuario usuarios)
        {

        }
        public bool removerUsuario(Usuario usuario) 
        {
            if (usuario == pesquisarUsuario(usuario))
            {
                return true;
            }
        }
        public Usuario pesquisarUsuario(Usuario usuario)
        {
            foreach()
        }
        public void adicionarAmbiente(Ambiente ambiente)
        {

        }
        public bool removerAmbiente(Ambiente ambiente)
        {
            if (ambiente == pesquisarAmbiente(ambiente))
            {
                return true;
            }
        }
        public Ambiente pesquisarAmbiente(Ambiente ambiente)
        {
            foreach() 
            {
                
            }
        }
        public void upload()
        {

        }
        public void download()
        {

        }
    }
}
