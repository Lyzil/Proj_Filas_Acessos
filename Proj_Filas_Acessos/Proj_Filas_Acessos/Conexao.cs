using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Proj_Filas_Acessos
{
    internal class Conexao
    {
        private readonly string connectionString;
        internal string ConnectionString => connectionString;

        public Conexao()
        {
            connectionString =
                "Server=localhost\\MSSQLSERVER01;" +
                "Database=db_filaAcesso;" +
                "User Id=user;" +
                "Password=Senha@123;" +
                "TrustServerCertificate=True;";
        }
        //  AMBIENTE
        public void InsertAmbiente(Ambiente a)
        {
            string query = "INSERT INTO Ambiente (Id, Nome) VALUES (@id, @nome)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", a.Id);
                cmd.Parameters.AddWithValue("@nome", a.Nome);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Ambiente SelectAmbiente(int id)
        {
            string query = "SELECT Id, Nome FROM Ambiente WHERE Id = @id";
            Ambiente retorno = new Ambiente(-1, "null");

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();

                SqlDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
                    retorno.Id = (int)r["Id"];
                    retorno.Nome = (string)r["Nome"];
                }
            }
            return retorno;
        }

        public void DeleteAmbiente(int id)
        {
            string query = "DELETE FROM Ambiente WHERE Id = @id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // =====================================================
        //  USUARIO
        // =====================================================

        public void InsertUsuario(Usuario u)
        {
            string query = "INSERT INTO Usuario (Id, Nome) VALUES (@id, @nome)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", u.Id);
                cmd.Parameters.AddWithValue("@nome", u.Nome);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Usuario SelectUsuario(int id)
        {
            string query = "SELECT Id, Nome FROM Usuario WHERE Id = @id";
            Usuario retorno = new Usuario(-1, "null");

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();

                SqlDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
                    retorno.Id = (int)r["Id"];
                    retorno.Nome = (string)r["Nome"];
                }
            }
            return retorno;
        }

        public void DeleteUsuario(int id)
        {
            string query = "DELETE FROM Usuario WHERE Id = @id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // =====================================================
        //  USUARIO x AMBIENTE (Permissões)
        // =====================================================

        public void InsertUsuarioAmbiente(int usuarioId, int ambienteId)
        {
            string query =
              @"IF NOT EXISTS (SELECT * FROM UsuarioAmbiente WHERE UsuarioId = @usuario AND AmbienteId = @ambiente)
                INSERT INTO UsuarioAmbiente (UsuarioId, AmbienteId) VALUES (@usuario, @ambiente)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@usuario", usuarioId);
                cmd.Parameters.AddWithValue("@ambiente", ambienteId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

        }

        public void DeleteUsuarioAmbiente(int usuarioId, int ambienteId)
        {
            string query =
                "DELETE FROM UsuarioAmbiente WHERE UsuarioId = @usuario AND AmbienteId = @ambiente";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@usuario", usuarioId);
                cmd.Parameters.AddWithValue("@ambiente", ambienteId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // =====================================================
        //  LOG
        // =====================================================

        public void InsertLog(Log l)
        {
            string query =
                "INSERT INTO LogAcesso (DataAcesso, UsuarioId, AmbienteId, TipoAcesso) " +
                "VALUES (@data, @usuario, @ambiente, @tipo)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@data", l.DtAcesso);
                cmd.Parameters.AddWithValue("@usuario", l.Usuario.Id);
                cmd.Parameters.AddWithValue("@ambiente", l.Ambiente.Id);
                cmd.Parameters.AddWithValue("@tipo", l.TipoAcesso);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Log> SelectLogsPorAmbiente(int ambienteId)
        {
            List<Log> lista = new List<Log>();
            string query =
                "SELECT DataAcesso, UsuarioId, AmbienteId, TipoAcesso " +
                "FROM LogAcesso WHERE AmbienteId = @id ORDER BY DataAcesso DESC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", ambienteId);

                conn.Open();
                SqlDataReader r = cmd.ExecuteReader();

                
                while (r.Read())
                {
                    DateTime data = (DateTime)r["DataAcesso"];
                    int idUsuario = (int)r["UsuarioId"];
                    int idAmb = (int)r["AmbienteId"];
                    bool tipo = (bool)r["TipoAcesso"];

                    Usuario u = SelectUsuario(idUsuario);
                    Ambiente a = SelectAmbiente(idAmb);

                    Log log = new Log(data, u, a, tipo);
                    lista.Add(log);
                }

                }

                return lista;
        }
    }
}
