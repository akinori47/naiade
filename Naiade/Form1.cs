using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Naiade
{
    
    public partial class NAIADE : Form
    {
        public double[,] semantic_difference = new double[100, 100];
        public double[,] semantic_difference1 = new double[100, 100];
        public double[,] mas = new double[10, 10];

        public double[,] mas_otn_pr = new double[100, 6];
        public double[,] mas_otn_pr1 = new double[100, 6];
        public double[,] mas_sov_otn_pr = new double[100, 6];
        public double[,] mas_sov_otn_pr1 = new double[100, 6];
        public double[,] mas_entrop = new double[100, 6];
        public double[,] mas_entrop1 = new double[100, 6];
        public double[,] mas_potoki = new double[100, 2];
        public double[] mas_max = new double[100];
        

        public int count_pair; // количество пар
        public int count_pair1;
        public int count_pair11;
        public double mera_konserv; //мера консервативности

        public NAIADE()
        {
            
            InitializeComponent();
            textBox4.Text = "0,5";
        }


        public void S_D()
        {
            // зануляем массивы
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    semantic_difference[i,j] = 0;
                    mas[i,j] = 0;

                }
            for (int i = 0; i < 100; i++)
                for (int j = 0; j < 100; j++)
                {
                    semantic_difference[i, j] = 0;
                 
                }
              // заполняем массив значений 
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                for (int j = 1; j < dataGridView1.ColumnCount; j++)
                  {
                      if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "Прекрасно")
                          mas[i, j - 1] = 1;
                      else if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "Очень хорошо")
                          mas[i, j - 1] = 0.95;
                      else if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "Хорошо")
                          mas[i, j - 1] = 0.87;
                      else if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "Более или менее хорошо")
                          mas[i, j - 1] = 0.67;
                      else if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "Нормально")
                          mas[i, j - 1] = 0.5;
                      else if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "Более или менее плохо")
                          mas[i, j - 1] = 0.33;
                      else if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "Плохо")
                          mas[i, j - 1] = 0.16;
                      else if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "Очень плохо")
                          mas[i, j - 1] = 0.04;
                      else if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "Совсем плохо")
                          mas[i, j - 1] = 0;

                      else
                          mas[i, j - 1] = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value); 
                  }
        
            //for(int i = 1; i < dataGridView1.ColumnCount; i++)
            //    for (int j = 0; j < dataGridView1.Rows.Count; j++)
            //    { }

            for (int kol = 0; kol < dataGridView1.ColumnCount - 1; kol++)
            {
                int iii = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    for (int j = 0; j < dataGridView1.Rows.Count - i; j++)
                        if (mas[i, kol] != mas[i + j, kol])
                        {
                            semantic_difference[iii, kol] = mas[i, kol] - mas[i + j, kol];
                            iii++;
                        }
            }

            int iii1 = 0;
            for (int j1 = 0; j1 < dataGridView1.ColumnCount - 1; j1++)
            {
                iii1 = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    for (int j = 0; j < dataGridView1.Rows.Count; j++)
                            if(i != j)
                            {
                                semantic_difference1[iii1, j1] = mas[i, j1] - mas[j, j1];
                                iii1++;
                            }

                      
            }
            count_pair11 = iii1;

            /*
             int iii1 = 0;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        for (int j = 0; j < dataGridView1.Rows.Count - i; j++)
                        if (mas[i, j1] != mas[i+j, j1])
                        {
                            semantic_difference1[iii1, j1] = mas[i, j1] - mas[i + j, j1];
                            iii1++;
                        }
             */




        }



        public void otn_pr()
        {

            
            //зануляем элементы массива
            for (int i = 0; i < 100; i++)
                for (int j = 0; j < 6; j++)
                {
                    mas_otn_pr[i, j] = 0;
                    mas_otn_pr1[i, j] = 0;
                }


            int kkk = 0;
            for (int i = 0; i < dataGridView1.ColumnCount - 1; i++)
                for (int j = 0; j < count_pair; j++)
                {
                    
                    //намного лучше/намного хуже
                    if (semantic_difference[j, i] >= 0 && Convert.ToString(dataGridView2.Rows[i].Cells[1].Value) == "Max")
                    {
                    double t1 = Convert.ToDouble(dataGridView2.Rows[i].Cells[2].Value);
                    mas_otn_pr[kkk, 0] = 1 / Math.Pow((1 + ((Math.Pow(t1, 2) * (Math.Sqrt(2) - 1)) / Math.Pow(semantic_difference[j, i], 2))), 2);
                    mas_otn_pr[kkk, 5] = 0;


                    }
                    else if (semantic_difference[j, i] < 0 && Convert.ToString(dataGridView2.Rows[i].Cells[1].Value) == "Max")
                    {
                    double t1 = Convert.ToDouble(dataGridView2.Rows[i].Cells[2].Value);
                    mas_otn_pr[kkk, 0] = 0;
                    mas_otn_pr[kkk, 5] = 1 / Math.Pow((1 + ((Math.Pow(t1, 2) * (Math.Sqrt(2) - 1)) / Math.Pow(semantic_difference[j, i], 2))), 2);

                    }

                    else if (semantic_difference[j, i] >= 0 && Convert.ToString(dataGridView2.Rows[i].Cells[1].Value) == "Min")
                    {
                        double t1 = Convert.ToDouble(dataGridView2.Rows[i].Cells[2].Value);
                        mas_otn_pr[kkk, 0] = 0;
                        mas_otn_pr[kkk, 5] = 1 / Math.Pow((1 + ((Math.Pow(t1, 2) * (Math.Sqrt(2) - 1)) / Math.Pow(semantic_difference[j, i], 2))), 2);

                    }

                    else if (semantic_difference[j, i] < 0 && Convert.ToString(dataGridView2.Rows[i].Cells[1].Value) == "Min")
                    {
                        double t1 = Convert.ToDouble(dataGridView2.Rows[i].Cells[2].Value);
                        mas_otn_pr[kkk, 0] = 1 / Math.Pow((1 + ((Math.Pow(t1, 2) * (Math.Sqrt(2) - 1)) / Math.Pow(semantic_difference[j, i], 2))), 2);
                        mas_otn_pr[kkk, 5] = 0;

                    }    
                        
                        
                        // лучше/хуже
                    if (semantic_difference[j, i] >= 0 && Convert.ToString(dataGridView2.Rows[i].Cells[1].Value) == "Max")
                    {
                    double t2 = Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value);
                    mas_otn_pr[kkk, 1] = 1 / (1 + Math.Pow(t2, 2) / Math.Pow(semantic_difference[j, i], 2));
                    mas_otn_pr[kkk, 4] = 0;

                    }
                    else if (semantic_difference[j, i] < 0 && Convert.ToString(dataGridView2.Rows[i].Cells[1].Value) == "Max")
                    {
                    double t2 = Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value);
                    mas_otn_pr[kkk, 1] = 0;
                    mas_otn_pr[kkk, 4] = 1 / (1 + Math.Pow(t2, 2) / Math.Pow(semantic_difference[j, i], 2));

                    }

                    else if (semantic_difference[j, i] >= 0 && Convert.ToString(dataGridView2.Rows[i].Cells[1].Value) == "Min")
                     {
                         double t2 = Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value);
                         mas_otn_pr[kkk, 1] = 0;
                         mas_otn_pr[kkk, 4] = 1 / (1 + Math.Pow(t2, 2) / Math.Pow(semantic_difference[j, i], 2));

                     }

                     else if (semantic_difference[j, i] < 0 && Convert.ToString(dataGridView2.Rows[i].Cells[1].Value) == "Min")
                     {
                         double t2 = Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value);
                         mas_otn_pr[kkk, 1] = 1 / (1 + Math.Pow(t2, 2) / Math.Pow(semantic_difference[j, i], 2));
                         mas_otn_pr[kkk, 4] = 0;

                     }



                    //приблизительно равны
                    double t3 = Convert.ToDouble(dataGridView2.Rows[i].Cells[4].Value);
                    mas_otn_pr[kkk, 2] = Math.Exp(-(Math.Log(2, Math.E) / t3) * Math.Abs(semantic_difference[j, i]));


                    //равны
                    double t4 = Convert.ToDouble(dataGridView2.Rows[i].Cells[5].Value);
                    mas_otn_pr[kkk, 3] = Math.Exp(-(Math.Log(2, Math.E) / Math.Pow(t4, 2)) * Math.Pow(semantic_difference[j, i], 2));


                    kkk++;
                }



            int kk = 0;
            for (int i = 0; i < dataGridView1.ColumnCount - 1; i++)
                for (int j = 0; j < count_pair11; j++)
                {

                    //намного лучше/намного хуже
                    if (semantic_difference1[j, i] >= 0 && Convert.ToString(dataGridView2.Rows[i].Cells[1].Value) == "Max")
                    {
                        double t1 = Convert.ToDouble(dataGridView2.Rows[i].Cells[2].Value);
                        mas_otn_pr1[kk, 0] = 1 / Math.Pow((1 + ((Math.Pow(t1, 2) * (Math.Sqrt(2) - 1)) / Math.Pow(semantic_difference1[j, i], 2))), 2);
                        mas_otn_pr1[kk, 5] = 0;


                    }
                    else if (semantic_difference1[j, i] < 0 && Convert.ToString(dataGridView2.Rows[i].Cells[1].Value) == "Max")
                    {
                        double t1 = Convert.ToDouble(dataGridView2.Rows[i].Cells[2].Value);
                        mas_otn_pr1[kk, 0] = 0;
                        mas_otn_pr1[kk, 5] = 1 / Math.Pow((1 + ((Math.Pow(t1, 2) * (Math.Sqrt(2) - 1)) / Math.Pow(semantic_difference1[j, i], 2))), 2);

                    }

                    else if (semantic_difference1[j, i] >= 0 && Convert.ToString(dataGridView2.Rows[i].Cells[1].Value) == "Min")
                    {
                        double t1 = Convert.ToDouble(dataGridView2.Rows[i].Cells[2].Value);
                        mas_otn_pr1[kk, 0] = 0;
                        mas_otn_pr1[kk, 5] = 1 / Math.Pow((1 + ((Math.Pow(t1, 2) * (Math.Sqrt(2) - 1)) / Math.Pow(semantic_difference1[j, i], 2))), 2);

                    }

                    else if (semantic_difference1[j, i] < 0 && Convert.ToString(dataGridView2.Rows[i].Cells[1].Value) == "Min")
                    {
                        double t1 = Convert.ToDouble(dataGridView2.Rows[i].Cells[2].Value);
                        mas_otn_pr1[kk, 0] = 1 / Math.Pow((1 + ((Math.Pow(t1, 2) * (Math.Sqrt(2) - 1)) / Math.Pow(semantic_difference1[j, i], 2))), 2);
                        mas_otn_pr1[kk, 5] = 0;

                    }


                    // лучше/хуже
                    if (semantic_difference1[j, i] >= 0 && Convert.ToString(dataGridView2.Rows[i].Cells[1].Value) == "Max")
                    {
                        double t2 = Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value);
                        mas_otn_pr1[kk, 1] = 1 / (1 + Math.Pow(t2, 2) / Math.Pow(semantic_difference1[j, i], 2));
                        mas_otn_pr1[kk, 4] = 0;

                    }
                    else if (semantic_difference1[j, i] < 0 && Convert.ToString(dataGridView2.Rows[i].Cells[1].Value) == "Max")
                    {
                        double t2 = Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value);
                        mas_otn_pr1[kk, 1] = 0;
                        mas_otn_pr1[kk, 4] = 1 / (1 + Math.Pow(t2, 2) / Math.Pow(semantic_difference1[j, i], 2));

                    }

                    else if (semantic_difference1[j, i] >= 0 && Convert.ToString(dataGridView2.Rows[i].Cells[1].Value) == "Min")
                    {
                        double t2 = Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value);
                        mas_otn_pr1[kk, 1] = 0;
                        mas_otn_pr1[kk, 4] = 1 / (1 + Math.Pow(t2, 2) / Math.Pow(semantic_difference1[j, i], 2));

                    }

                    else if (semantic_difference1[j, i] < 0 && Convert.ToString(dataGridView2.Rows[i].Cells[1].Value) == "Min")
                    {
                        double t2 = Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value);
                        mas_otn_pr1[kk, 1] = 1 / (1 + Math.Pow(t2, 2) / Math.Pow(semantic_difference1[j, i], 2));
                        mas_otn_pr1[kk, 4] = 0;

                    }



                    //приблизительно равны
                    double t3 = Convert.ToDouble(dataGridView2.Rows[i].Cells[4].Value);
                    mas_otn_pr1[kk, 2] = Math.Exp(-(Math.Log(2, Math.E) / t3) * Math.Abs(semantic_difference1[j, i]));


                    //равны
                    double t4 = Convert.ToDouble(dataGridView2.Rows[i].Cells[5].Value);
                    mas_otn_pr1[kk, 3] = Math.Exp(-(Math.Log(2, Math.E) / Math.Pow(t4, 2)) * Math.Pow(semantic_difference1[j, i], 2));


                    kk++;
                }





        }


        public void sov_otn_pr()
        {

            mera_konserv = Convert.ToDouble(textBox4.Text);
            double sum1 = 0;
            double sum2 = 0;
            int count = 0;

            for (int i = 0; i < 100; i++)
                for (int j = 0; j < 6; j++)
                {
                    mas_sov_otn_pr[i, j] = 0;
                    mas_sov_otn_pr1[i, j] = 0;
                }


            for (int l = 0; l < 6; l++)
            {
                count_pair1 = 0;
                count = 0;

                for (int j = count_pair1; j < count_pair; j++)
                {
                    for (int k = count_pair1; k < count_pair1 + dataGridView1.ColumnCount - 1; k++)
                    {
                        if (mas_otn_pr[k, l] - mera_konserv > 0)
                            sum1 += mas_otn_pr[k, l] - mera_konserv;
                        else
                            sum1 += 0;

                            sum2 += Math.Abs(mas_otn_pr[k, l] - mera_konserv);

                    }
                    count_pair1 += dataGridView1.ColumnCount - 1;

                    mas_sov_otn_pr[count, l] = sum1 / sum2;
                    sum1 = 0;
                    sum2 = 0;
                    count++;
                }
            }



            for (int l = 0; l < 6; l++)
            {
                count_pair1 = 0;
                count = 0;

                for (int j = count_pair1; j < count_pair11; j++)
                {
                    for (int k = count_pair1; k < count_pair1 + dataGridView1.ColumnCount - 1; k++)
                    {
                        if (mas_otn_pr1[k, l] - mera_konserv > 0)
                            sum1 += mas_otn_pr1[k, l] - mera_konserv;
                        else
                            sum1 += 0;

                        sum2 += Math.Abs(mas_otn_pr1[k, l] - mera_konserv);

                    }
                    count_pair1 += dataGridView1.ColumnCount - 1;

                    mas_sov_otn_pr1[count, l] = sum1 / sum2;
                    sum1 = 0;
                    sum2 = 0;
                    count++;
                }
            }



        }



        public void entrop()
        {
            mera_konserv = Convert.ToDouble(textBox4.Text);
            double sum1 = 0;

            int count1 = 0;


            for (int i = 0; i < 100; i++)
                for (int j = 0; j < 6; j++)
                {
                    mas_entrop[i, j] = 0;
                    mas_entrop1[i, j] = 0;
                }

            for (int i = 0; i < 6; i++)
            {
                count_pair1 = 0;
                count1 = 0;
                

                for (int j = count_pair1; j < count_pair; j++) 
                {
                    if (mas_sov_otn_pr[j, i] - mera_konserv > 0)
                    {
                        for (int k = j * (dataGridView1.ColumnCount - 1); k < j * (dataGridView1.ColumnCount - 1) + dataGridView1.ColumnCount - 1; k++)
                        {
                            //vsp = mas_otn_pr[k, i] - mera_konserv;
                            if (mas_otn_pr[k, i] != 0)
                            sum1 += mas_otn_pr[k, i] * Math.Log(mas_otn_pr[k, i], 2) + (1 - mas_otn_pr[k, i]) * Math.Log((1 - mas_otn_pr[k, i]), 2);
                            // формула
                        }
                        mas_entrop[count1, i] = (Convert.ToDouble(-1) / (dataGridView1.ColumnCount - 1)) * sum1;
                        sum1 = 0;
                        count1++;
                    }
                    else
                    {
                        mas_entrop[count1, i] = 0;
                        sum1 = 0;
                        count1++;
                    }

                }



                for (int j = count_pair1; j < count_pair11; j++)
                {
                    if (mas_sov_otn_pr1[j, i] - mera_konserv > 0)
                    {
                        for (int k = j * (dataGridView1.ColumnCount - 1); k < j * (dataGridView1.ColumnCount - 1) + dataGridView1.ColumnCount - 1; k++)
                        {
                            //vsp = mas_otn_pr[k, i] - mera_konserv;
                            if (mas_otn_pr1[k, i] != 0)
                                sum1 += mas_otn_pr1[k, i] * Math.Log(mas_otn_pr1[k, i], 2) + (1 - mas_otn_pr1[k, i]) * Math.Log((1 - mas_otn_pr1[k, i]), 2);
                            // формула
                        }
                        mas_entrop1[count1, i] = (Convert.ToDouble(-1) / (dataGridView1.ColumnCount - 1)) * sum1;
                        sum1 = 0;
                        count1++;
                    }
                    else
                    {
                        mas_entrop1[count1, i] = 0;
                        sum1 = 0;
                        count1++;
                    }

                }


            }        
            
            
        }



        public void potoki()
        {

            double sum1 = 0;
            double sum2 = 0;
            double sum3 = 0;
            double sum4 = 0;


            for (int i = 0; i < 100; i++)
                for (int j = 0; j < 2; j++)
                {
                    mas_potoki[i, j] = 0;
                    
                }

                    int ccoo = 0;
                    int i11 = 0;

                    for (int j = count_pair1; j < count_pair11; j++)
                    {
                        for (int k = ccoo; k < ccoo + dataGridView1.Rows.Count - 1; k++)
                        {
                            if (mas_otn_pr1[k, 0] > 1 - mas_entrop1[k, 0])
                                sum1 += 1 - mas_entrop1[k, 0];
                            else sum1 += mas_otn_pr1[k, 0];

                            if (mas_otn_pr1[k, 1] > 1 - mas_entrop1[k, 1])
                                sum1 += 1 - mas_entrop1[k, 1];
                            else sum1 += mas_otn_pr1[k, 1];

                            sum2 += 2 - mas_entrop1[k, 0] + mas_entrop1[k, 1];


                            if (mas_otn_pr1[k, 5] > 1 - mas_entrop1[k, 5])
                                sum3 += 1 - mas_entrop1[k, 5];
                            else sum3 += mas_otn_pr1[k, 5];

                            if (mas_otn_pr1[k, 4] > 1 - mas_entrop1[k, 4])
                                sum3 += 1 - mas_entrop1[k, 4];
                            else sum3 += mas_otn_pr1[k, 4];

                            sum4 += 2 - mas_entrop1[k, 5] + mas_entrop1[k, 4];
                        }
                        ccoo += dataGridView1.Rows.Count - 1;


                        mas_potoki[i11, 0] = sum1 / sum2;
                        mas_potoki[i11, 1] = sum3 / sum4;
                        sum1 = sum2 = sum3 = sum4 = 0;
                        i11++;
                    }
                       
                
                    
                    
          

       }



        private void button2_Click(object sender, EventArgs e)
        {
            string p1 = comboBox2.Text;
            if (p1 == "Лингвистический")
            {
                DataGridViewComboBoxColumn ccc = new DataGridViewComboBoxColumn();
                DataGridViewColumnHeaderCell n = new DataGridViewColumnHeaderCell();
                ccc.Items.AddRange(new string[] { "Прекрасно", "Очень хорошо", "Хорошо", "Более или менее хорошо", "Нормально", "Более или менее плохо", "Плохо", "Очень плохо", "Совсем плохо" });

                dataGridView1.Columns.Add(ccc);
                int k = dataGridView1.ColumnCount;
                dataGridView1.Columns[k-1].HeaderCell.Value = this.textBox3.Text.ToString();

                dataGridView2.Rows.Add(this.textBox3.Text);
            }
            else
            {
                dataGridView1.Columns.Add("", this.textBox3.Text.ToString());
                dataGridView2.Rows.Add(this.textBox3.Text);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        dataGridView1.Rows.Add(this.textBox1.Text);
           
        }
       
        //PictureBox pbox = new PictureBox();
        private void draw(PictureBox _Box)
        {
            PictureBox pbox = new PictureBox();
            int w = pictureBox1.Width;
            int h = pictureBox1.Height;
            pbox = _Box;
            Graphics gP = pbox.CreateGraphics();
            gP.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            gP.Clear(Color.White);
            Pen pen = new Pen(Color.Black, 1);

            for (int i = 0; i < 100; i++)
            {mas_max[i] = 0;
              
            }
            
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            mas_max[i] = mas_potoki[i, 0];


// по возрастанию
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Rows.Count - 1; j++)
                {
                    if (mas_max[j] < mas_max[j + 1])
                    {
                        double z = mas_max[j];
                        mas_max[j] = mas_max[j + 1];
                        mas_max[j + 1] = z;
                    }
                }
            }
            


            string[] povt = new string[100];

            for (int i = 0; i < 100; i++)
                povt[i] = "";

            string qqqqq = "";
            int k = 1;
            int jjj = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                bool yes = false;
                bool yes1 = false;
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                    
                    if (mas_max[i] == mas_potoki[j, 0] && i == 0)
                    {gP.DrawString(dataGridView1.Rows[j].Cells[0].Value.ToString(), new System.Drawing.Font("Arial", 10F, FontStyle.Bold), Brushes.Red, 50, 50 + i * 50);
                    yes = true;
                    qqqqq = dataGridView1.Rows[j].Cells[0].Value.ToString();
                    povt[i] = qqqqq;
                    jjj++;
                    }
                    
                    
                    else if (mas_max[i] == mas_potoki[j, 0] && mas_max[i] != mas_max[i - 1] && yes == false)
                    {
           gP.DrawString(dataGridView1.Rows[j].Cells[0].Value.ToString(), new System.Drawing.Font("Arial", 10F, FontStyle.Bold), Brushes.Red, 50, 50 + i * 50);
           gP.DrawLine(pen, 50, 50 + (i - 1) * 30, 50, 50 + i * 50); 
                   yes = true;
                   qqqqq = dataGridView1.Rows[j].Cells[0].Value.ToString();
                   povt[i] = qqqqq;
                   jjj++;
                    }
                    else if (mas_max[i] == mas_potoki[j, 0] && yes == false && dataGridView1.Rows[j].Cells[0].Value.ToString() != qqqqq)
                    {
                        for (int jj = 0; jj < jjj; jj++)
                            if (dataGridView1.Rows[j].Cells[0].Value.ToString() == povt[jj])
                            {
                                yes1 = true;
                            }   

                        if (!yes1)
                        {
                            gP.DrawString(dataGridView1.Rows[j].Cells[0].Value.ToString(), new System.Drawing.Font("Arial", 10F, FontStyle.Bold), Brushes.Red, 50 + k * 30, 50 + (i - k) * 50);
                            gP.DrawLine(pen, 50, 50 + (i - k) * 30, 50 + k * 30, 50 + (i - k) * 50);

                            qqqqq = dataGridView1.Rows[j].Cells[0].Value.ToString();
                            povt[i] = qqqqq;


                            jjj++;
                            k++;
                            yes = true;
                            
                        }
                        yes1 = false;
                    }
            
                       
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            S_D();// находим семантические расстояния
            richTextBox1.Text = "Семантическое расстояние:" + "\n\n\n";

            richTextBox1.Text += "Пары             ";
            for (int i = 1; i < dataGridView1.ColumnCount; i++)
                richTextBox1.Text += dataGridView1.Columns[i].HeaderCell.Value + "               ";


            richTextBox1.Text += "\n";

            count_pair = 0;// количество пар
            for (int kol = 0; kol < dataGridView1.Rows.Count; kol++)
                for (int i = 0; i < dataGridView1.Rows.Count - kol; i++)
                    if (dataGridView1.Rows[kol].Cells[0].Value != dataGridView1.Rows[kol + i].Cells[0].Value)
                    {
                        richTextBox1.Text += dataGridView1.Rows[kol].Cells[0].Value + " - " + dataGridView1.Rows[kol + i].Cells[0].Value;


                        for (int x = 0; x < dataGridView1.ColumnCount - 1; x++)
                             richTextBox1.Text += "             " + semantic_difference[count_pair, x];

                             count_pair++;                      
                             richTextBox1.Text += "\n";
                           
                    
                    }

            otn_pr(); // расчет индексов интенсивности предпочтений
            richTextBox1.Text += "\n\n\n";
            richTextBox1.Text += "Расчет индексов интенсивности предпочтений:" + "\n\n";
            richTextBox1.Text += "Пары         ";
            richTextBox1.Text += "             " + "Намн. лучше       "
                                                 + "Лучше             "
                                                 + "Прим. равны       "
                                                 + "Равны             "
                                                 + "Хуже              "
                                                 + "Намн. хуже        "
                                                 + "\n";

            count_pair1 = 0;
            for (int kol = 0; kol < dataGridView1.Rows.Count; kol++)
                for (int i = 0; i < dataGridView1.Rows.Count - kol; i++)
                    if (dataGridView1.Rows[kol].Cells[0].Value != dataGridView1.Rows[kol + i].Cells[0].Value)
                    {
                        richTextBox1.Text += dataGridView1.Rows[kol].Cells[0].Value + " - " + dataGridView1.Rows[kol + i].Cells[0].Value + "              \n";
                        for (int i1 = 1; i1 < dataGridView1.ColumnCount; i1++)
                        {
                            richTextBox1.Text += dataGridView1.Columns[i1].HeaderCell.Value + "                          ";
                            for (int q = 0; q < 6; q++)
                                richTextBox1.Text += mas_otn_pr[count_pair1, q].ToString("F") + "                          ";
                                
                            count_pair1++;
                            richTextBox1.Text += "\n";
                            
                            
                        }
                    }



            sov_otn_pr();

            richTextBox1.Text += "\n\n\n";
            richTextBox1.Text += "Расчет совокупного отношения предпочтения:" + "\n\n";
            richTextBox1.Text += "Пары         ";
            richTextBox1.Text += "             " + "Намн. лучше       "
                                                 + "Лучше                          "
                                                 + "Прим. равны                      "
                                                 + "Равны                    "
                                                 + "Хуже                            "
                                                 + "Намн. хуже        "
                                                 + "\n";


            count_pair1 = 0;// количество пар
            for (int kol = 0; kol < dataGridView1.Rows.Count; kol++)
                for (int i = 0; i < dataGridView1.Rows.Count - kol; i++)

                    if (dataGridView1.Rows[kol].Cells[0].Value != dataGridView1.Rows[kol + i].Cells[0].Value)
                    {
                        richTextBox1.Text += dataGridView1.Rows[kol].Cells[0].Value + " - " + dataGridView1.Rows[kol + i].Cells[0].Value + "                  ";
                       
                        for (int q = 0; q < 6; q++)
                            richTextBox1.Text += mas_sov_otn_pr[count_pair1, q].ToString("F") + "                            ";
                           
                            count_pair1++;
                            richTextBox1.Text += "\n";
                    }


            entrop();

            richTextBox1.Text += "\n\n\n";
            richTextBox1.Text += "Расчет энтропии:" + "\n\n";
            richTextBox1.Text += "Пары         ";
            richTextBox1.Text += "             " + "Намн. лучше       "
                                                 + "Лучше                          "
                                                 + "Прим. равны                      "
                                                 + "Равны                    "
                                                 + "Хуже                            "
                                                 + "Намн. хуже        "
                                                 + "\n";


            count_pair1 = 0;// количество пар
            for (int kol = 0; kol < dataGridView1.Rows.Count; kol++)
                for (int i = 0; i < dataGridView1.Rows.Count - kol; i++)

                    if (dataGridView1.Rows[kol].Cells[0].Value != dataGridView1.Rows[kol + i].Cells[0].Value)
                    {
                        richTextBox1.Text += dataGridView1.Rows[kol].Cells[0].Value + " - " + dataGridView1.Rows[kol + i].Cells[0].Value + "                  ";

                        for (int q = 0; q < 6; q++)
                            richTextBox1.Text += mas_entrop[count_pair1, q].ToString("F") + "                            ";

                        count_pair1++;
                        richTextBox1.Text += "\n";
                    }






            potoki();

            richTextBox1.Text += "\n\n\n";
            richTextBox1.Text += "Потоки:" + "\n\n";
            richTextBox1.Text += "Альтернативы         ";
            richTextBox1.Text += "Ф(+)                  Ф(-)";
            richTextBox1.Text += "\n";

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                richTextBox1.Text += dataGridView1.Rows[i].Cells[0].Value + "                  ";
                richTextBox1.Text += mas_potoki[i, 0].ToString("F") + "                  " + mas_potoki[i, 1].ToString("F") + "\n";
            }


            draw(pictureBox1);
                
        }



        private void button1_Click(object sender, EventArgs e)
        {


            
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            draw(pictureBox1);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        

       



       

       

        
    }
}
