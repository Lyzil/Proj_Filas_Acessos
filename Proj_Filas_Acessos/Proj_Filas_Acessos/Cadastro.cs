using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj_Filas_Acessos
{
    internal class Cadastro
    {
        private List<Usuario> usuarios;
        private List<Ambiente> ambientes;
        private Conexao conexao = new Conexao();
        internal List<Usuario> Usuarios { get => usuarios; set => usuarios = value; }
        internal List<Ambiente> Ambientes { get => ambientes; set => ambientes = value; }
        internal Conexao Conexao { get => conexao; set => conexao = value; }
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
            conexao.InsertUsuario(usuario);
            Console.WriteLine("Usuario cadastrado com sucesso!");
        }
        public bool removerUsuario(Usuario usuario)
        {
            Usuario usuarioRemover = pesquisarUsuario(usuario);
            if (usuarioRemover.Id == -1) return false;
            if (usuarioRemover.Ambientes.Count == 0) { 
                this.usuarios.Remove(usuarioRemover);
                conexao.DeleteUsuario(usuarioRemover.Id);
                return true;
            }
            return false;
        }
        public Usuario pesquisarUsuario(Usuario usuario)
        {
            foreach (Usuario user in usuarios)
                if (user.Id == usuario.Id)
                    return user;
            //pesquisa no banco de dados
            Usuario doBanco = conexao.SelectUsuario(usuario.Id);

            if (doBanco.Id != -1)
            {
                usuarios.Add(doBanco); // salva na memória depois de achar
                return doBanco;
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
            conexao.InsertAmbiente(ambiente);
            Console.WriteLine("Ambiente cadastrado com sucesso!");
        }
        public bool removerAmbiente(Ambiente ambiente)
        {
            Ambiente ambienteRemover = pesquisarAmbiente(ambiente);
            if (ambienteRemover.Id == -1) return false;
               
            this.ambientes.Remove(ambienteRemover);
            conexao.DeleteAmbiente(ambienteRemover.Id);
            return true;
        }
        public Ambiente pesquisarAmbiente(Ambiente ambiente)
        {
            foreach (Ambiente amb in ambientes)
                if (amb.Id == ambiente.Id)
                    return amb;
            //pesquisa no banco de dados
            Ambiente doBanco = conexao.SelectAmbiente(ambiente.Id);
            if (doBanco.Id != -1)
            {
                ambientes.Add(doBanco);
                return doBanco;
            }
            return new Ambiente();
        }
        public void upload()
        {
            foreach (Ambiente amb in ambientes)
            {
                try
                {
                    conexao.InsertAmbiente(amb);
                }
                catch (SqlException ex)
                {
                    if (!ex.Message.Contains("PRIMARY KEY"))
                        Console.WriteLine($"Erro ao inserir ambiente {amb.Id}: {ex.Message}");
                }
            }
            foreach (Usuario u in usuarios)
            {
                try
                {
                    conexao.InsertUsuario(u);
                }
                catch (SqlException ex)
                {
                    if (!ex.Message.Contains("PRIMARY KEY"))
                        Console.WriteLine($"Erro ao inserir usuario {u.Id}: {ex.Message}");
                }
                foreach (Ambiente amb in u.Ambientes)
                {
                    try
                    {
                        conexao.InsertUsuarioAmbiente(u.Id, amb.Id);
                    }
                    catch (SqlException ex)
                    {
                        if (!ex.Message.Contains("PRIMARY KEY"))
                            Console.WriteLine($"Erro ao inserir permissão Usuario {u.Id} - Ambiente {amb.Id}: {ex.Message}");
                    }
                }
            }

            Console.WriteLine("Upload concluído!");
        }
        public void download()
        {
            this.usuarios.Clear();
            this.ambientes.Clear();

            using (SqlConnection conn = new SqlConnection(conexao.ConnectionString))
            {
                conn.Open();
                string queryAmb = "SELECT Id, Nome FROM Ambiente";
                using (SqlCommand cmd = new SqlCommand(queryAmb, conn))
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        Ambiente amb = new Ambiente((int)r["Id"], (string)r["Nome"]);
                        this.ambientes.Add(amb);
                    }
                }
                string queryUser = "SELECT Id, Nome FROM Usuario";
                using (SqlCommand cmd = new SqlCommand(queryUser, conn))
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        Usuario u = new Usuario((int)r["Id"], (string)r["Nome"]);
                        this.usuarios.Add(u);
                    }
                }
                string queryPerm = "SELECT UsuarioId, AmbienteId FROM UsuarioAmbiente";
                using (SqlCommand cmd = new SqlCommand(queryPerm, conn))
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        int idUsuario = (int)r["UsuarioId"];
                        int idAmbiente = (int)r["AmbienteId"];

                        Usuario u = this.usuarios.FirstOrDefault(x => x.Id == idUsuario);
                        Ambiente a = this.ambientes.FirstOrDefault(x => x.Id == idAmbiente);

                        if (u != null && a != null)
                        {
                            u.concederPermissao(a, Conexao); // só precisa conceder na memória
                        }
                    }
                }
            }

            Console.WriteLine("Download concluído! Dados carregados do SQL Server.");
        }

    }
}
