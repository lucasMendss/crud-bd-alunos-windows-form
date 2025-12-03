using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CadAluno
{
    internal class Aluno
    {
        private string _prontuario;
        private string _nome;
        private string _cpf;
        private string _rg;
        private string _email;

        public string prontuario
        {
            get { return _prontuario; }
            set
            {
                if (!Validacoes.ValidarProntuario(value)) { throw new Exception("Prontuário Inválido."); }
                else { _prontuario = value; }
            }
        }
        public string nome
        {
            get { return _nome; }
            set
            {
                if (!Validacoes.ValidarNome(value)) { throw new Exception("Nome inválido."); }
                else { _nome = value; }
            }
        }
        public string cpf
        {
            get { return _cpf; }
            set
            {
                if (!Validacoes.ValidarCPF(value)) { throw new Exception("CPF inválido."); }
                else { _cpf = value; }
            }
        }
        public string rg
        {
            get { return _rg; }
            set
            {
                if (!Validacoes.ValidarRGDeSP(value)) { throw new Exception("RG inválido."); }
                else { _rg = value; }
            }
        }
        public string email
        {
            get { return _email; }
            set
            {
                if (!Validacoes.ValidarEmail(value)) { throw new Exception("E-mail inválido."); }
                else { _email = value; }
            }
        }
        public bool ConsultarAluno()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=LUCASW;Initial Catalog=dbAcademico;Integrated Security=True";
            SqlCommand comando = new SqlCommand();
            comando.Connection = conn;
            comando.CommandType = CommandType.Text;
            comando.CommandText = "select prontuario, nome, cpf, rg, email from alunos where prontuario = @prontuario";
            comando.Parameters.AddWithValue("@prontuario", this.prontuario);

            conn.Open();
            SqlDataReader dataReader = comando.ExecuteReader();
            if (!dataReader.Read())
            {
                return false;
            }
            this.nome = dataReader["nome"].ToString();
            this.cpf = dataReader["cpf"].ToString();
            this.rg = dataReader["rg"].ToString();
            this.email = dataReader["email"].ToString();
            conn.Close();
            return true;
        }
        public void CadastrarAluno()
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = "Data Source=LUCASW;Initial Catalog=dbAcademico;Integrated Security=True";
                SqlCommand comando = new SqlCommand();
                comando.Connection = conn;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "insert into alunos (prontuario, nome, cpf, rg, email) values (@prontuario, @nome, @cpf, @rg, @email)";
                comando.Parameters.AddWithValue("@prontuario", this.prontuario);
                comando.Parameters.AddWithValue("@nome", this.nome);
                comando.Parameters.AddWithValue("@cpf", this.cpf);
                comando.Parameters.AddWithValue("@rg", this.rg);
                comando.Parameters.AddWithValue("@email", this.email);

                conn.Open();
                comando.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao se conectar com o banco de dados.", ex);
            }
            finally
            {
                conn.Close();
            }
        }
        public void AtualizarAluno()
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = "Data Source=LUCASW;Initial Catalog=dbAcademico;Integrated Security=True";
                List<string> campos = new List<string>();
                SqlCommand comando = new SqlCommand();
                comando.Connection = conn;
                comando.CommandType = CommandType.Text;

                if (!string.IsNullOrWhiteSpace(this.nome))
                {
                    campos.Add("nome = @nome");
                    comando.Parameters.AddWithValue("@nome", this.nome);
                }

                if (!string.IsNullOrWhiteSpace(this.cpf))
                {
                    campos.Add("cpf = @cpf");
                    comando.Parameters.AddWithValue("@cpf", this.cpf);
                }

                if (!string.IsNullOrWhiteSpace(this.rg))
                {
                    campos.Add("rg = @rg");
                    comando.Parameters.AddWithValue("@rg", this.rg);
                }

                if (!string.IsNullOrWhiteSpace(this.email))
                {
                    campos.Add("email = @email");
                    comando.Parameters.AddWithValue("@email", this.email);
                }

                if (campos.Count == 0)
                    throw new Exception("Nenhum campo válido para atualizar.");

                string sql =
                    "UPDATE alunos SET " + string.Join(", ", campos) +
                    " WHERE prontuario = @prontuario";

                comando.CommandType = CommandType.Text;
                comando.CommandText = sql;
                comando.Parameters.AddWithValue("@prontuario", this.prontuario);

                conn.Open();
                int linhas = comando.ExecuteNonQuery();

                if (linhas == 0)
                {
                    throw new Exception("Nenhum aluno encontrado para atualizar.");                    
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao se conectar com o banco de dados.", ex);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
        public void ExcluirAluno()
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = "Data Source=LUCASW;Initial Catalog=dbAcademico;Integrated Security=True";
                SqlCommand comando = new SqlCommand();
                comando.Connection = conn;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "delete from alunos where prontuario = @prontuario";
                comando.Parameters.AddWithValue("@prontuario", this.prontuario);

                conn.Open();
                comando.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao se conectar com o banco de dados.", ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
