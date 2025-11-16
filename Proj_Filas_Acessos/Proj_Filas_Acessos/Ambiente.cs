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
        private Queue<Log> logs = new Queue<Log>();
        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        internal Queue<Log> Logs { get => logs; }
        public Ambiente(int id, string nome) 
        { 
            Id = id;
            Nome = nome;
        }
        public Ambiente() : this(-1,"") { }
        public Ambiente(int id) : this(id, "") { }
        public void registrarLog(Log log)
        {
            if (logs.Count < 100)
                logs.Enqueue(log);
            else
            {
                logs.Dequeue();
                logs.Enqueue(log);
            } 
        }
        public override bool Equals(object obj)
        {
            if (obj is Ambiente a)
                return this.Id == a.Id;
            return false;
        }

    }
}
