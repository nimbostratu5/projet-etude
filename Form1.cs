using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;



namespace DemarrageNoor
{
    public partial class Form1 : Form
    {
        int rowCount;
        int colCount;
        DataTable travail;
        int iteration;
        List<Point> bornes;
        List<Point> bornes2;
        Graphics feuille;
        int v = 1;
        bool fini = false;
             
        public Form1()
        {
            
            bornes = new List<Point>();
            bornes2 = new List<Point>();
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = true;
            dataGridView2.AutoGenerateColumns = true;
            dataGridView3.AutoGenerateColumns = true;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            button3GotClicked = true;
                                   
            if (comboBox1.Text == "Toggle Vx & Vy")
            {
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                button8.Enabled = false;
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
            }

            
        }
        private bool button1GotClicked;
        private void button1_Click(object sender, EventArgs e)
        {

            comboBox1.Text = Convert.ToString("Vx");
            comboBox2.Text = Convert.ToString("Potentiel de Vitesse");
            button1GotClicked = true;
            if (button1GotClicked == true)
            {
                button5.Enabled = true;
                button1.Enabled = false;
                textBox1.Enabled = false;
                button8.Enabled = true;
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
            }
            
            if (Convert.ToInt32(textBox1.Text) < 3)
            {
               MessageBox.Show("Insérez une valeur plus grand que 3.");
               return;
            }

            if (textBox1.Text == "")
            {
                textBox1.Enabled = false;
                MessageBox.Show("Insérez une valeur plus grand que 3.");
                return;
            }


            rowCount = colCount = Convert.ToInt32(textBox1.Text);
            
            for (int k = 0; k < colCount - 2; k++)
            {
                dataTable2.Columns.Add();
                dataTable6.Columns.Add();
            }

            for (int i = 0; i < rowCount; i++)
            {
                dataTable2.Rows.Add();
                dataTable6.Rows.Add();
            }
            
            
            dataGridView1[0, 0].ReadOnly = true;
            dataGridView1[Convert.ToInt32(textBox1.Text) - 1, 0].ReadOnly = true;
            dataGridView1[Convert.ToInt32(textBox1.Text) - 1, Convert.ToInt32(textBox1.Text) - 1].ReadOnly = true;
            dataGridView1[0, Convert.ToInt32(textBox1.Text) - 1].ReadOnly = true;

            dataGridView1[0, 0].Style.BackColor = Color.Black;
            dataGridView1[Convert.ToInt32(textBox1.Text) - 1, 0].Style.BackColor = Color.Black;
            dataGridView1[Convert.ToInt32(textBox1.Text) - 1, Convert.ToInt32(textBox1.Text) - 1].Style.BackColor = Color.Black;
            dataGridView1[0, Convert.ToInt32(textBox1.Text) - 1].Style.BackColor = Color.Black;
                     
            
      for (int j = 1; j < Convert.ToInt32(textBox1.Text) - 1; j++)
            {
                dataGridView1[j, 0].Style.BackColor = Color.LawnGreen;
                dataGridView1[j, Convert.ToInt32(textBox1.Text) - 1].Style.BackColor = Color.LawnGreen;
                                          
                for (int k = 1; k < Convert.ToInt32(textBox1.Text) - 1; k++)
                {
                    dataGridView1[0, k].Style.BackColor = Color.LawnGreen;
                    dataGridView1[Convert.ToInt32(textBox1.Text) - 1, k].Style.BackColor = Color.LawnGreen;
                    
                }
            }
            

            for (int j = 1; j < Convert.ToInt32(textBox1.Text) - 1; j++)
            {
                for (int k = 1; k < Convert.ToInt32(textBox1.Text) - 1; k++)
                {
                    dataGridView1[j, k].Style.BackColor = Color.LightGray;
                    dataGridView1[j, k].ReadOnly = true;
                }

            }

            dataGridView3.DataSource = dataTable5;
            for (int i = 0; i < Convert.ToInt32(textBox1.Text); i++)
            {
                dataTable5.Rows.Add();
            }

            for (int k = 0; k < Convert.ToInt32(textBox1.Text) - 2; k++)
            {
                dataTable5.Columns.Add();
            }

            MessageBox.Show("1. Insérez les valeurs pour Vx et Vy dans les cellules vertes uniquement."+ "\n" + "\n"  + "2. Cliquez sur <<Pret>> lorsque terminé.");

        }

        private bool button2GotCliked;
        private void button2_Click(object sender, EventArgs e)
        {
            label2.BackColor = Color.LawnGreen;
            button2GotCliked = true;
            travail = dataTable5;
            if (button2GotCliked == true)
            {
                button3.Enabled = true;
            }
            // verifier que les cells contiennent les bonnes valeurs(car d'entrée manuelle)
           
                for (int i = 0; i < dataTable5.Rows.Count; i++)
                {
                    for (int j = 0; j < dataTable5.Columns.Count; j++)
                    {
                        if (dataTable5.Rows[i][j].ToString() == "")
                            dataTable5.Rows[i][j] = 0;
                        else
                            bornes.Add(new Point(i, j));
                    }
                }
           
            travail = dataTable5.Copy();
            //timer1.Enabled = true;
                        
            //traiter l'info ....
            timer3.Start();
           
            dataGridView3.Invalidate();
            
        }

        /// <summary>
        /// visuel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
           // dataTable2 = travail.Copy();

            //changer pour meilleur affichage
            for (int i = 1; i < dataTable2.Rows.Count - 1; i++)
            {
                for (int j = 1; j < dataTable2.Columns.Count - 1; j++)
                {
                    dataTable2.Rows[i][j] = Math.Round(Convert.ToDouble(dataTable2.Rows[i][j]), 4);



                    int bl = (int)Math.Round(Convert.ToDouble(dataTable2.Rows[i][j])); ;
                    //les donnees ne doivent pas sortir de rangee 1 a 255
                    if (bl <= 0 || bl > 255)
                        bl = 254;
                    Color couleur = Color.FromArgb(255, 255, bl);
                    Pen pn = new Pen(couleur);
                    feuille.FillEllipse(pn.Brush, i * 12, j * 12, 12, 12);
                }
            }



            dataGridView1.DataSource = dataTable2;
            //   dataGridView2.DataSource = dataTable4;

            label1.Text = iteration.ToString();
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            iteration++;
            Double temp;
            dataTable5 = travail.Copy();
            bool brt = false;
            for (int i = 1; i < travail.Rows.Count - 1; i++)
            {
                for (int j = 1; j < travail.Columns.Count - 1; j++)
                {
                    foreach (Point pt in bornes)
                    {
                        brt = false;
                        if (pt.X == travail.Rows.Count && pt.Y == travail.Columns.Count)
                        {
                            i = 1;
                            j = 1;
                        }
                        else
                        {
                            brt = true;
                        }
                    }
                    if (!brt)
                    {

                        temp = Convert.ToDouble((Convert.ToDouble(travail.Rows[i + 1][j]) + Convert.ToDouble(travail.Rows[i - 1][j]) + Convert.ToDouble(travail.Rows[i][j + 1]) + Convert.ToDouble(travail.Rows[i][j - 1])) / 4);
                        Math.Round(temp, 2);
                        MessageBox.Show(Convert.ToString(temp));
                        travail.Rows[i][j] = temp;
                    }
                }
            }

            //a enlever ou modifier pour accomoder fin quand pas de changement
            if (iteration > 10000)
                timer2.Stop();
        }

        private Random random = new Random();
        const int minimumNumber = 0;
        const int maximumNumber = 100;
        public int GenerateRandomNumber()
        {
            var randomNumber = random.Next(minimumNumber, maximumNumber);
            return randomNumber;
        }

        private bool button5gotclicked = false;
        private void button5_Click(object sender, EventArgs e)
        {
            button5gotclicked = true;
            if (button5gotclicked == true)
            {
                checkBox1.Enabled = true;
                button2.Enabled = true;
                button6.Enabled = true;
            }

			if (comboBox1.Text == "Vx")
			{

				foreach (DataGridViewRow row in dataGridView1.Rows)
				{
					foreach (DataGridViewCell cell in row.Cells)
					{

						cell.Value = GenerateRandomNumber();
					}
				}
			}
			if (comboBox1.Text == "Vy")
			{
				foreach (DataGridViewRow row in dataGridView2.Rows)
				{
					foreach (DataGridViewCell cell in row.Cells)
					{

						cell.Value = GenerateRandomNumber();
					}
				}
			}
        }

        private void button6_Click(object sender, EventArgs e)
        {

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    int i = Convert.ToInt32(cell.Value);

                    if (dataGridView1[cell.ColumnIndex, cell.RowIndex] == dataGridView1[0, 0] || dataGridView1[cell.ColumnIndex, cell.RowIndex] == dataGridView1[0, Convert.ToInt32(textBox1.Text)])
                    {
                        //dataGridView1[cell.ColumnIndex, cell.RowIndex] = dataGridView1[1, 1];


                    }
                    else
                    {
                        i = Convert.ToInt32(dataGridView1[cell.ColumnIndex + 1, cell.RowIndex].Value) + Convert.ToInt32(dataGridView1[cell.ColumnIndex - 1, cell.RowIndex].Value) + Convert.ToInt32(dataGridView1[cell.ColumnIndex, cell.RowIndex + 1].Value) + Convert.ToInt32(dataGridView1[cell.ColumnIndex, cell.RowIndex - 1].Value);
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.Text == "Vx")
            {
                if ( v == 1)
                {
                comboBox1.Items.Add("Vy");
                v++;
                }
                dataGridView2.Visible = false;
                dataGridView1.Visible = true;
            }
            else if (comboBox1.Text == "Vy")
            {
                dataGridView1.Visible = false;
                dataGridView2.Visible = true;

                dataGridView2[0, 0].ReadOnly = true;
                dataGridView2[Convert.ToInt32(textBox1.Text) - 1, 0].ReadOnly = true;
                dataGridView2[Convert.ToInt32(textBox1.Text) - 1, Convert.ToInt32(textBox1.Text) - 1].ReadOnly = true;
                dataGridView2[0, Convert.ToInt32(textBox1.Text) - 1].ReadOnly = true;

                dataGridView2[0, 0].Style.BackColor = Color.Black;
                dataGridView2[Convert.ToInt32(textBox1.Text) - 1, 0].Style.BackColor = Color.Black;
                dataGridView2[Convert.ToInt32(textBox1.Text) - 1, Convert.ToInt32(textBox1.Text) - 1].Style.BackColor = Color.Black;
                dataGridView2[0, Convert.ToInt32(textBox1.Text) - 1].Style.BackColor = Color.Black;

                for (int j = 1; j < Convert.ToInt32(textBox1.Text) - 1; j++)
                {
                    dataGridView2[j, 0].Style.BackColor = Color.LawnGreen;
                    dataGridView2[j, Convert.ToInt32(textBox1.Text) - 1].Style.BackColor = Color.LawnGreen;

                    for (int k = 1; k < Convert.ToInt32(textBox1.Text) - 1; k++)
                    {

                        dataGridView2[0, k].Style.BackColor = Color.LawnGreen;
                        dataGridView2[Convert.ToInt32(textBox1.Text) - 1, k].Style.BackColor = Color.LawnGreen;

                    }
                }


                for (int j = 1; j < Convert.ToInt32(textBox1.Text) - 1; j++)
                {
                    for (int k = 1; k < Convert.ToInt32(textBox1.Text) - 1; k++)
                    {
                        dataGridView2[j, k].Style.BackColor = Color.LightGray;
                        dataGridView2[j, k].ReadOnly = true;
                    }

                }
            }

            if (comboBox1.Text == "Vy")
            {
                dataGridView1.Visible = false;
                dataGridView2.Visible = true;
            }
            else if (comboBox1.Text == "Vx")
            {
                dataGridView2.Visible = false;
                dataGridView1.Visible = true;

            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            int q; //row
            int w; //column
            int k = Convert.ToInt32(textBox1.Text) / 2;
            
            double min = Convert.ToDouble(dataGridView3[k, k].Value);
            double max = Convert.ToDouble(dataGridView3[k, k].Value);

            for (int i = 1; i < dataGridView3.Rows.Count - 1; i++)
            {
                for (int j = 1; j < dataGridView3.Columns.Count - 1; j++)
                {
                    if (Convert.ToDouble(dataGridView3[j,i].Value) < min)
                    {
                        min = Convert.ToDouble(dataGridView3[j, i].Value);
                    }
                    if (Convert.ToDouble(dataGridView3[j, i].Value) > max)
                    {
                        max = Convert.ToDouble(dataGridView3[j, i].Value);
                    }
                }
            }

            int milieu = Convert.ToInt32(Math.Round((max + min) / 2));
            
                               
            if (checkBox1.Checked == true)
            {
                checkBox1.Text = "Enlever le gradient";

                if (comboBox2.Text == "Potentiel de Vitesse")
                {
                    for (q = 1; q < dataGridView3.Rows.Count-1; q++)
                    {
                        for (w = 1; w < dataGridView3.Columns.Count-1; w++)
                        {
                            dataGridView3[w, q].Style.BackColor = Color.FromArgb((Convert.ToInt32(Convert.ToDouble(dataGridView3[w, q].Value) * 64 / milieu)), (Convert.ToInt32(Convert.ToDouble(dataGridView3[w, q].Value) * 128 / milieu)), (Convert.ToInt32(Convert.ToDouble(dataGridView3[w, q].Value) * 64 / milieu)));
                        }
                    }

                }
            }

            else
            {
                checkBox1.Text = " Afficher le gradient";
                for (int t = 1; t < dataGridView3.Rows.Count-1; t++)
                {
                    for (int g = 1; g < dataGridView3.Columns.Count-1; g++)
                    {
                        dataGridView3[g, t].Style.BackColor = Color.White;
                    }
                }
                
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            checkBox2.Enabled = true;

            // Creation des outils de tracage du champ de vecteurs.

            feuille = pictureBox1.CreateGraphics();
            int offset = (Convert.ToInt32(pictureBox1.Width)) / (Convert.ToInt32(textBox1.Text)-2);
            int numberP = Convert.ToInt32(textBox1.Text) - 2;

            Point pt1 = new Point();
            Point pt2 = new Point();
            Point pt3 = new Point();
            Point pt4 = new Point();

            pt3.X = 0;
            pt3.Y = 0;

            pt4.X = pictureBox1.Width;
            pt4.Y = 0;

            pt2.Y = pictureBox1.Height;
            pt2.X = 0;

            pt1.Y = 0;
            pt1.X = 0;

            Pen pencil = new Pen(Color.Black, 2);

            for (int k = 0; k < numberP; k++)
            {

                pt1.X = pt1.X + offset;
                pt2.X = pt2.X + offset;

                pt3.Y = pt3.Y + offset;
                pt4.Y = pt4.Y + offset;

                feuille.DrawLine(pencil, pt1, pt2);
                feuille.DrawLine(pencil, pt3, pt4);
            }
          
            Pen pencil2 = new Pen(Color.Black, 2);
            pencil2.StartCap = LineCap.RoundAnchor;
            pencil2.EndCap = LineCap.ArrowAnchor;
                                   
            // Creation des variables min et max pour la fonction d'echelle scale.

            double min = Convert.ToDouble(dataGridView3[1, 1].Value);
            double max = Convert.ToDouble(dataGridView3[2, 2].Value);

            //On cherche le min et le max afin de creer une fonction scale.

            for (int q = 1; q < dataGridView3.Rows.Count - 1; q++)
            {
                for (int w = 1; w < dataGridView3.Columns.Count - 1; w++)
                {

                    if (Convert.ToDouble(dataGridView3[w, q].Value) < min)
                    {
                        min = Convert.ToDouble(dataGridView3[w, q].Value);
                    }
                    if (Convert.ToDouble(dataGridView3[w, q].Value) > max)
                    {
                        max = Convert.ToDouble(dataGridView3[w, q].Value);
                    }

                }
            }

            // Calculs des variables nécessaires pour la fonction scale.

            double oldRange = (max - min);
            double newRange = (25 - 5);

            // Initialisation des points de depart pour le tracage des vecteurs. 
            // Creation des variables pour le module en x et le module en y.

            double moduleX;
            double moduleY;
            Point pTa = new Point();
            Point pTb = new Point();
            pTa.X = offset / 2;
            pTa.Y = offset / 2;
            pTb.X = 0;
            pTb.Y = 0;

            // Calculs des modules Vx et Vy à partir du potentiel de vitesses.
            // Établir les points finaux pour chaque vecteur vitesse aux points (x,y). 
            // Obtention des vecteurs vitesses V en chaque point (x,y).
            // Tracage du champ de vecteurs.

            if (comboBox2.Text == "Champ de Vecteurs Vitesse")
            {
                for (int t = 1; t < dataGridView3.Rows.Count - 1; t++)
                {
                    for (int g = 1; g < dataGridView3.Columns.Count - 1; g++)
                    {
                        moduleX = (Convert.ToDouble(dataGridView3[g - 1, t].Value) + Convert.ToDouble(dataGridView3[g + 1, t].Value)) /2;
                        moduleX = (((moduleX - min) * newRange) / oldRange) + 5;

                        moduleY = (Convert.ToDouble(dataGridView3[g, t + 1].Value) + Convert.ToDouble(dataGridView3[g, t - 1].Value)) / 2;
                        moduleY = (((moduleY - min) * newRange) / oldRange) + 5;

                       // if (pTa.X == pictureBox1.Width - (offset/2))
                        if (pTa.X > (offset*(Convert.ToInt32(textBox1.Text)-3))+(offset/2))
                        {
                            pTa.Y = pTa.Y + offset;
                            pTa.X = offset / 2;
                        }

                        pTb.X = Convert.ToInt32(pTa.X + moduleX);
                        pTb.Y = Convert.ToInt32(pTa.Y - moduleY);
                        feuille.DrawLine(pencil2, pTa, pTb);
                        pTa.X = pTa.X + offset;

                    }
                }
            }
        }

        private bool button3GotClicked;
        private void button3_Click(object sender, EventArgs e)
        {
            
            if (button3GotClicked == false)
            {
                label2.BackColor = Color.LawnGreen;
                timer3.Start();
               // timer2.Start();
               
                
                button3.Text = Convert.ToString("Arret d'iteration");
                button3GotClicked = true;
                return;
            }
            if (button3GotClicked == true)
            {
                label2.BackColor = Color.Red;
                timer3.Stop();
               // timer2.Stop();
                
                button3.Text = Convert.ToString("Resumer");
                button3GotClicked = false;
                return;
            }



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label2.Font = new Font("Arial", 10, FontStyle.Bold);
            label1.Font = new Font("Arial", 10, FontStyle.Bold);
            label3.Font = new Font("Arial", this.Font.Size, FontStyle.Bold);
            
        }

        private bool button4GotClicked;
        private void button4_Click(object sender, EventArgs e)
        {
            button4GotClicked = true;
            if (button4GotClicked == true)
            {
                button4.Enabled = false;
                button2.Enabled = true;
            }
            //Boutton servant à remplir le reste du domaine par la moyenne des potentiels donnés aux frontières.
            //Méthode séquentielle : on prend la somme des frontières du haut et du bas et ensuite la somme sur les côtés.

            int somme2 = 0;
            int somme1 = 0;
            for (int j = 1; j < Convert.ToInt32(textBox1.Text) - 1; j++)
            {
                somme1 += Convert.ToInt32(dataGridView3[j, 0].Value);
                somme2 += Convert.ToInt32(dataGridView3[j, Convert.ToInt32(textBox1.Text) - 1].Value);
            }

            int somme4 = 0;
            int somme3 = 0;
            for (int k = 1; k < Convert.ToInt32(textBox1.Text) - 1; k++)
            {
                somme3 += Convert.ToInt32(dataGridView3[0, k].Value);
                somme4 += Convert.ToInt32(dataGridView3[Convert.ToInt32(textBox1.Text) - 1, k].Value);
            }

            // Calcul de la moyenne.

            decimal moyenne = (somme1 + somme2 + somme3 + somme4) / ((Convert.ToInt32(textBox1.Text) - 2) * 4);

            // On se limite aux nombres entiers.

            Math.Round(moyenne,0);

            // Allocalisation de la valeur ''moyenne'' dans le domaine excluant les frontières.

            for (int j = 1; j < Convert.ToInt32(textBox1.Text) - 1; j++)
            {
                for (int k = 1; k < Convert.ToInt32(textBox1.Text) - 1; k++)
                {
                    dataGridView3[j, k].Value = moyenne;

                }

            }



        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Champ de Vecteurs Vitesse")
            {
                checkBox1.Enabled = false;
                pictureBox1.Visible = true;
                dataGridView3.Visible = false;
                checkBox2.Enabled = true;
            }
            else if (comboBox2.Text == "Potentiel de Vitesse")
            {
                checkBox2.Enabled = false;
                button6.Enabled = false;
                checkBox1.Enabled = true;
                dataGridView3.Visible = true;
                pictureBox1.Visible = false;
              //  MessageBox.Show("Cliquer sur <<Calculer Potentiel>> pour afficher le potentiel de vitesse.");
            }

            if ((comboBox2.Text == "Champ de Vecteurs Vitesse") && (fini == true))
            {
                button6.Enabled = true;            
            }
        }

        private bool button7GotClicked;
        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView3[0, 0].Style.BackColor = Color.Black;
            dataGridView3[Convert.ToInt32(textBox1.Text) - 1, 0].Style.BackColor = Color.Black;
            dataGridView3[Convert.ToInt32(textBox1.Text) - 1, Convert.ToInt32(textBox1.Text) - 1].Style.BackColor = Color.Black;
            dataGridView3[0, Convert.ToInt32(textBox1.Text) - 1].Style.BackColor = Color.Black;

            button7GotClicked = true;
            if (button7GotClicked == true)
            {
                button4.Enabled = true;
                button7.Enabled = false;
            }

            // u = ax + by ; {a = Vx; b = Vy}
            // Calcul du potentiel à partir des valeurs de Vx et Vy données et de la fonction proposée.
            // Allocalisation de ces valeurs dans le DataGridView3.
                        
            for (int i = 1; i < dataTable5.Columns.Count-1; i++)
            {
                dataGridView3[i, 0].Value = (Convert.ToInt32(dataGridView1[i, 0].Value) * i) + (Convert.ToInt32(dataGridView2[i, 0].Value) * i);
                dataGridView3[i, (Convert.ToInt32(textBox1.Text) - 1)].Value = Convert.ToDouble((Convert.ToInt32((dataGridView1[i, (Convert.ToInt32(textBox1.Text) - 1)].Value)) * i) + (Convert.ToInt32((dataGridView2[i, (Convert.ToInt32(textBox1.Text) - 1)].Value)) * i));
                dataGridView3[i, 0].Style.BackColor = Color.Tomato;
                dataGridView3[i, (Convert.ToInt32(textBox1.Text) - 1)].Style.BackColor = Color.Tomato;
            }

            for (int j = 1; j < dataTable5.Rows.Count-1; j++)
            {

                dataGridView3[0, j].Value = Convert.ToDouble((Convert.ToInt32(dataGridView1[0, j].Value) * j) + (Convert.ToInt32(dataGridView2[0, j].Value) * j));
                dataGridView3[Convert.ToInt32(textBox1.Text) - 1, j].Value = Convert.ToDouble((Convert.ToInt32(dataGridView1[Convert.ToInt32(textBox1.Text) - 1, j].Value) * j) + (Convert.ToInt32(dataGridView2[Convert.ToInt32(textBox1.Text) - 1, j].Value) * j));
                dataGridView3[0, j].Style.BackColor = Color.Tomato;
                dataGridView3[Convert.ToInt32(textBox1.Text) - 1, j].Style.BackColor = Color.Tomato;
            }


        }

        private void button8_Click(object sender, EventArgs e)
        {
            bool complete1 = false;
            bool complete2 = false;
            bool complete3 = false;
            bool complete4 = false;

            if (button1GotClicked == true)
            {
                for (int j = 1; j < Convert.ToInt32(textBox1.Text) - 1; j++)
                {
                    if (dataGridView1[j, 0].Value is DBNull | dataGridView2[j, 0].Value is DBNull)
                    {
                        button8.BackColor = Color.OrangeRed;
                        MessageBox.Show("Il vous manque des données dans les cellules appropriées. Indiquez par le chiffre 0 si nul.");
                        return;
                    }

                    if (((Convert.ToInt32(dataGridView1[j, 0].Value)) >= 0) && ((Convert.ToInt32(dataGridView2[j, 0].Value)) >= 0))
                    {
                        complete1 = true;
                    }
                    if ((Convert.ToInt32(dataGridView1[j, Convert.ToInt32(textBox1.Text) - 1].Value) >= 0) && (Convert.ToInt32(dataGridView2[j, Convert.ToInt32(textBox1.Text) - 1].Value) >= 0))
                    {
                        complete2 = true;
                    }
                }

                for (int k = 1; k < Convert.ToInt32(textBox1.Text) - 1; k++)
                {
                    if (dataGridView1[0, k].Value is DBNull | dataGridView2[0, k].Value is DBNull)
                    {
                        button8.BackColor = Color.OrangeRed;
                        MessageBox.Show("Il vous manque des données dans les cellules appropriées. Indiquez par le chiffre 0 si nul.");
                        return;
                    }

                    if (((Convert.ToInt32(dataGridView1[0, k].Value)) >= 0) && (Convert.ToInt32(dataGridView2[0, k].Value)) >= 0)
                    {
                        complete3 = true;
                    }
                    if ((Convert.ToInt32(dataGridView1[Convert.ToInt32(textBox1.Text) - 1, k].Value) >= 0) && (Convert.ToInt32(dataGridView1[Convert.ToInt32(textBox1.Text) - 1, k].Value) >= 0))
                    {
                        complete4 = true;
                    }

                }


                if ((complete1 == true) && (complete2 == true) && (complete3 == true) && (complete4 == true))
                {
                    button7.Enabled = true;
                    button8.Enabled = false;
                    button8.BackColor = Color.GreenYellow;
                }
                else
                {
                    button8.BackColor = Color.OrangeRed;
                    MessageBox.Show("Il vous manque des données dans une ou plusieurs cellules.");
                }
            }
                       
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            double temp;
            double valP;
            iteration++;
            for (int i = 1; i < dataTable5.Rows.Count-1; i++)
            {
                for (int j = 1; j < dataTable5.Columns.Count-1 ; j++)
                {
                    valP = Convert.ToDouble(dataTable5.Rows[i][j]);
                    if ((i == dataTable5.Rows.Count - 1) && (j == dataTable5.Columns.Count - 1))
                    {
                        i = 1;
                        j = 1;
                    }
                    else 
                    {
                        temp = Convert.ToDouble((Convert.ToDouble(dataTable5.Rows[i + 1][j]) + Convert.ToDouble(dataTable5.Rows[i - 1][j]) + Convert.ToDouble(dataTable5.Rows[i][j + 1]) + Convert.ToDouble(dataTable5.Rows[i][j - 1])) / 4);
                        dataTable5.Rows[i][j] = Math.Round(temp,2);

                        if (Math.Round(valP, 2) == Math.Round(temp, 2))
                        {
                            timer3.Stop();
                            MessageBox.Show("Términé");
                            checkBox1.Enabled = true;
                            button2.Enabled = false;
                            button3.Enabled = false;
                            fini = true;
                            label2.BackColor = Color.SkyBlue;
                            break;
                        }             
                    }
                }
            }

            label1.Text = iteration.ToString();
            dataGridView3.DataSource = dataTable5;
           // dataGridView3.Invalidate();

            if (iteration == 1000)
            {
                timer3.Stop();
                MessageBox.Show("Términé");
                checkBox1.Enabled = true;
                button6.Enabled = true;
                button2.Enabled = false;
                button3.Enabled = false;
                fini = true;
                label2.BackColor = Color.SkyBlue;
            }


        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            // Creation des outils de tracage du champ de vecteurs.


            feuille = pictureBox1.CreateGraphics();
            int offset = (Convert.ToInt32(pictureBox1.Width)) / (Convert.ToInt32(textBox1.Text) - 2);
            int numberP = Convert.ToInt32(textBox1.Text) - 2;
            Point pt1 = new Point();
            Point pt2 = new Point();
            Point pt3 = new Point();
            Point pt4 = new Point();

            pt3.X = 0;
            pt3.Y = 0;

            pt4.X = pictureBox1.Width;
            pt4.Y = 0;

            pt2.Y = pictureBox1.Height;
            pt2.X = 0;

            pt1.Y = 0;
            pt1.X = 0;

            Pen pencil = new Pen(Color.Black, 2);
            Bitmap bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            feuille = Graphics.FromImage(bm);
           
   
            // Creation d'un quadrille pour la visualisation. (optionel)
            // => Chaque case contiendra un vecteur ou son (0,0) se trouve au milieu de sa case allocalise.

            if (checkBox2.Checked == true)
            {
                pictureBox1.Image = bm;
                for (int k = 0; k < numberP; k++)
                {
                    
                    pt1.X = pt1.X + offset;
                    pt2.X = pt2.X + offset;

                    pt3.Y = pt3.Y + offset;
                    pt4.Y = pt4.Y + offset;

                    feuille.DrawLine(pencil, pt1, pt2);
                    feuille.DrawLine(pencil, pt3, pt4);
                }
                
            }
            else if (checkBox2.Checked == false)
            {
                pictureBox1.Image = bm;
            }
        }

  
    }
}

