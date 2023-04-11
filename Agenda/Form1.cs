using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Agenda
{
    public partial class Form1 : Form
    {
        MySqlConnection conexao;
        MySqlCommand comando;
        MySqlDataAdapter da;
        MySqlDataReader dr;
        string strSQL;
        public Form1()
        {
            InitializeComponent();
            ExibirDados();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            if (txtNome.Text != "" && txtEndereco.Text != "" && txtCelular.Text != "" && txtEmail.Text != "")
            {
                try
                {
                    conexao = new MySqlConnection("Server = localhost; Database = cad_agenda; Uid = root; Pwd = Gags@3215;");
                    strSQL = "INSERT INTO cad_agenda (nome, endereco, telefone, email) VALUES (@NOME, @ENDERECO, @TELEFONE, @EMAIL)";
                    comando = new MySqlCommand(strSQL, conexao);
                    comando.Parameters.AddWithValue("@NOME", txtNome.Text);
                    comando.Parameters.AddWithValue("@ENDERECO", txtEndereco.Text);
                    comando.Parameters.AddWithValue("@TELEFONE", txtCelular.Text);
                    comando.Parameters.AddWithValue("@EMAIL", txtEmail.Text);

                    conexao.Open();
                    comando.ExecuteNonQuery();
                    ExibirDados();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conexao.Close();
                    conexao = null;
                    comando = null;
                    txtNome.Text = "";
                    txtEndereco.Text = "";
                    txtCelular.Text = "";
                    txtEmail.Text = "";
                    txtNome.Focus();
                }
            }
            else
            {
                MessageBox.Show("Informe todos os campos para a inserção!");
            }
        }
        private void ExibirDados()
        {
            try
            {
                conexao = new MySqlConnection("Server = localhost; Database = cad_agenda; Uid = root; Pwd = Gags@3215;");
                conexao.Open();
                DataTable dt = new DataTable();
                da = new MySqlDataAdapter("SELECT nome, endereco, telefone, email FROM cad_agenda WHERE id IS NOT NULL ORDER BY nome", conexao);
                da.Fill(dt);
                dgvAgenda.DataSource = dt;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
