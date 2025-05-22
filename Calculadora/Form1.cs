using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculadora
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txbTela.Text = "0"; //Iniciaza a calculadora com zero
          
        }

        private void Numero_Click(object sender, EventArgs e)
        {
            Button botaoClicado = (Button)sender;

            // Se for o primeiro dígito ou após erro (reinicia a tela com o número clicado)
            if (txbTela.Text == "0" || txbTela.Text == "Erro" || txbTela.Text == "Erro: Div/0")
            {
                // Não permite múltiplos zeros no início
                if (botaoClicado.Text == "0") return;

                txbTela.Text = botaoClicado.Text;
            }
            else
            {   // Adiciona o número no final da tela
                txbTela.Text += botaoClicado.Text;
            }
            // Verifica se o último caractere da tela é um operador
            if (!string.IsNullOrEmpty(txbTela.Text))
            {

                char ultimoChar = txbTela.Text[txbTela.Text.Length - 1];

                // Substitui operador anterior pelo novo operador digitado
                if ("+-*÷".Contains(ultimoChar))
                {
                    txbTela.Text = txbTela.Text.Remove(txbTela.Text.Length - 1);
                }
            }

        }
        private void Operador_Click(object sender, EventArgs e)
        {   
            //Pegar as Infos do Botão que esta sendo chamando o evento:
            Button botaoClicado = (Button)sender;
            string operador = botaoClicado.Text;
           

            if ((txbTela.Text == "0" && operador != "-") ||
           (txbTela.Text.Length > 0 && "+-X÷".Contains(txbTela.Text[txbTela.Text.Length - 1].ToString())))
            {
                return;
            }
            // Adiciona o operador ná expressão
            txbTela.Text += operador;
        
        }
        private void btnIgual_Click(object sender, EventArgs e)
        {

            // Converte os símbolos da interface para os operadores válidos do C#
            string expressao = txbTela.Text.Replace("÷", "/").Replace("X", "*").Replace(",",".");

            // Validação simples para divisão por zero direta
            if (expressao.Contains("/0"))
            {
                MessageBox.Show("Não é possível dividir por zero.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbTela.Text = "0"; // Reseta a tela para zero
                return;
            }

            try
            {
                DataTable dt = new DataTable();
                var resultado = dt.Compute(expressao, "");
                txbTela.Text = resultado.ToString();//Exibe o resultado na tela 
            }
            catch
            {
                MessageBox.Show("Erro no cálculo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txbTela.Text = "0"; // Reseta a tela para zero em caso de erro genérico
            }
        }



        private void btnLimpar_Click(object sender, EventArgs e)
        {
            //Volta a tela para zero
            txbTela.Text = "0";

        }
    }
}
