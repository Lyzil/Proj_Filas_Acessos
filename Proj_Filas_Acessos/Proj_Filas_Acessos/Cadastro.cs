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
        public Cadastro()
        {
            this.usuarios = new List<Usuario>();
            this.ambientes = new List<Ambiente>();
        }
        public void adicionarUsuario(Usuario usuario)
        {
            if(pesquisarUsuario(usuario).Id != -1)
            {
                Console.WriteLine("Usuario ja cadastrado!");
                return;
            }
            this.usuarios.Add(usuario);
            Console.WriteLine("Usuario cadastrado com sucesso!");
        }
        public bool removerUsuario(Usuario usuario)
        {
            Usuario usuarioRemover = pesquisarUsuario(usuario);
            if (usuarioRemover.Id == -1) return false;
            if (usuarioRemover.Ambientes.Count == 0) { 
                this.usuarios.Remove(usuarioRemover);
                return true;
            }
            return false;
        }
        public Usuario pesquisarUsuario(Usuario usuario)
        {
            foreach (Usuario user in usuarios)
            {
                if (user.Id == usuario.Id)
                {
                    return user;
                }
            }
            return new Usuario();
        }
        public void adicionarAmbiente(Ambiente ambiente)
        {
            if (pesquisarAmbiente(ambiente).Id != -1)
            {
                Console.WriteLine("Ambiente ja cadastrado!");
                return;
            }
            this.ambientes.Add(ambiente);
            Console.WriteLine("Ambiente cadastrado com sucesso!");
        }
        public bool removerAmbiente(Ambiente ambiente)
        {
            Ambiente ambienteRemover = pesquisarAmbiente(ambiente);
            if (ambienteRemover.Id == -1) return false;
            this.ambientes.Remove(ambienteRemover);
            return true;
        }
        public Ambiente pesquisarAmbiente(Ambiente ambiente)
        {
            foreach (Ambiente amb in ambientes)
            {
                if (amb.Id == ambiente.Id)
                {
                    return amb;
                }
            }
            return new Ambiente();
        }
        public void upload()
        {
            // Implementar persistência no SQL Server.
        }
        public void download()
        {
            // Implementar carregamento do SQL Server.
        }
    }
}
