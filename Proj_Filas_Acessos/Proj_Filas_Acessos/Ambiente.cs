using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj_Filas_Acessos
{
    internal class Ambiente
    {
        private int id;
        private string nome;
        private Queue<Log> logs;
        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        internal Queue<Log> Logs { get => logs; set => logs = value; }
        public Ambiente(int id, string nome) 
        { 
            Id = id;
            Nome = nome;
        }
        public Ambiente() : this(-1,"") { }
        public void registrarLog(Log log)
        {
            if (logs.Count < 100)
                logs.Enqueue(log);
            else Console.WriteLine("Limite maximo excedido!");
        }
    }
}
