using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MySql.Data.MySqlClient;
//FEITO POR KARDREAN SHINODA.
namespace memoria_dbz
{
    public partial class Form1 : Form
    {   
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();
        //variaveis
        int movimentos, cliques, cartasencontradas, tagIndex;
        //vetor de 9 espaços
        Image[] img = new Image[9];

        //lista para concatenar posições
        List<string> lista = new List<string>();

        //vetor que verifica se as imagens são iguais ou diferentes
        int[] tags = new int[2];

        public Form1()
        {
            InitializeComponent();
            player.SoundLocation = "tema.wav";
            inicio();
            player.Play();
        }
        private void inicio()
        {
            //puxa a imagem verso para parte de tras dos quadrados.
            foreach (PictureBox item in Controls.OfType<PictureBox>())
            {

                //convertendo tag para inteiro
                tagIndex = int.Parse(String.Format("{0}", item.Tag));
                img[tagIndex] = item.Image;
                item.Image = memoria.Properties.Resources.verso;
                item.Enabled = true;

            }

            Posicoes();
        }
        private void Posicoes()
        {

            foreach (PictureBox item in Controls.OfType<PictureBox>())
            {

                Random rdn = new Random();

                //posiçoes das picturebox
                int[] xP = { 26, 179, 332, 485, 638, 791 };
                int[] yP = { 25, 156, 287 };

                //etiqueta repete para sorteio
            Repete:

                var X = xP[rdn.Next(0, xP.Length)];
                var Y = yP[rdn.Next(0, yP.Length)];

                string verificacao = X.ToString() + Y.ToString();

                if (lista.Contains(verificacao))
                {

                    goto Repete;

                }
                else
                {

                    item.Location = new Point(X, Y);
                    lista.Add(verificacao);

                }
            }
        }

        //metodo de clique e sorteio
        private void ImagensClick_Click(object sender, EventArgs e)
        {

            bool parEncontrado = false;


            //referencia picturebox que foi clicada
            PictureBox pic = (PictureBox)sender;
            cliques++;
            tagIndex = int.Parse(String.Format("{0}", pic.Tag));
            pic.Image = img[tagIndex];
            pic.Refresh();

            //verificação de cliques
            if (cliques == 1)
            {

                tags[0] = int.Parse(String.Format("{0}", pic.Tag));

            }
            else if (cliques == 2)
            {

                movimentos++;
                lblMovimentos.Text = "Movimentos:" + movimentos.ToString();
                tags[1] = int.Parse(String.Format("{0}", pic.Tag));
                parEncontrado = checagemPares();
                Desvirar(parEncontrado);
            }
        }

        private bool checagemPares()
        {
            //reseta os cliques
            cliques = 0;

            if (tags[0] == tags[1]) { return true; } else { return false; }

        }

        //metodo para verificar se a carta vai continuar virada para cima caso a pessoa acerte.
        private void Desvirar(bool check)
        {
            //tepo que a carta fica virada para cima
            Thread.Sleep(500);

            foreach (PictureBox item in Controls.OfType<PictureBox>())
            {
                if (int.Parse(String.Format("{0}", item.Tag)) == tags[0] ||
                    int.Parse(String.Format("{0}", item.Tag)) == tags[1])
                {

                    if (check == true)
                    {

                        item.Enabled = false;
                        cartasencontradas++;
                    }
                    else
                    {
                        item.Image = memoria.Properties.Resources.verso;
                        item.Refresh();

                    }

                }

            }
            FinalJogo();
        }

        private void FinalJogo()
        {

            if (cartasencontradas == img.Length * 2)
            {


                tabControl1.SelectTab(0);
                MySqlConnection conn = new MySqlConnection

            ("User id=root;Host=localhost;Database=bancodbz;");
                try
                {
                    conn.Open();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "INSERT into memodbz (nome, movimentos) values('" + textBox2.Text + "','" + movimentos + "')";

                try
                {
                    int result = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                   

                MessageBox.Show("PARABENS VOCÊ ENCONTROU OS GUERREIROS Z COM " + movimentos.ToString() + " MOVIMENTOS");
                DialogResult msg = MessageBox.Show("DESEJA CONTINUAR O JOGO", "CAIXA DE PERGUNTA", MessageBoxButtons.YesNo);

                if (msg == DialogResult.Yes)
                {
                    cliques = 0; movimentos = 0; cartasencontradas = 0;
                    lista.Clear();
                    inicio();

                }
                else if (msg == DialogResult.No)
                {
                    
                    MessageBox.Show("SHENLONG REALIZOU SEU DESEJO!!!");
                    Application.Exit();
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            

             listBox1.Items.Clear();

            MySqlConnection conn = new MySqlConnection(
                "User Id=root;Host=localhost;Database=bancodbz;"
                );

            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            MySqlCommand command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM memodbz ORDER BY movimentos ";
                

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                listBox1.Items.Add(reader["nome"].ToString()  +" "+   reader["movimentos"].ToString());
            }

            conn.Close();
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
               
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


    }
}
