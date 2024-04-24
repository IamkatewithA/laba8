using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.RegularExpressions;

namespace laba8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ReadTheFile_Click(object sender, EventArgs e)
        {
            string[] fileLines = File.ReadAllLines("datas.dat.txt");
            int rowCount = fileLines.Length;
            int columnCount = fileLines[0].Split(' ').Length;



            label5.Text = $"Размерность исходной матрицы {rowCount} ✕ {columnCount}";

        }

        private void ApplyChanges_Click(object sender, EventArgs e)
        {
            try
            {
                string[] fileLines = File.ReadAllLines("datas.dat.txt");
                int userRowCount = Convert.ToInt32(textBox1.Text);
                int userColumnCount = Convert.ToInt32(textBox2.Text);

                string choise_1 = comboBox1.SelectedItem.ToString();
                string choise_2 = comboBox2.SelectedItem.ToString();

                string[,] subMatrix = new string[userRowCount, userColumnCount];
                string[,] originalMatrix = new string[userRowCount, userColumnCount];


                for (int i = 0; i < userRowCount; i++)
                {
                    string[] rowValues = fileLines[i].Split(' ');
                    for (int j = 0; j < userColumnCount; j++)
                    {
                        subMatrix[i, j] = rowValues[j];
                        originalMatrix[i, j] = rowValues[j];
                    }

                }

                switch (choise_1)
                {
                    case "К верхнему регистру":
                        for (int i = 0; i < subMatrix.GetLength(0); i++)
                        {
                            for (int j = 0; j < subMatrix.GetLength(1); j++)
                            {
                                subMatrix[i, j] = subMatrix[i, j].ToUpper(); // Преобразование к нижнему регистру
                            }
                        }
                        break;

                    case "К нижнему регистру":
                        for (int i = 0; i < subMatrix.GetLength(0); i++)
                        {
                            for (int j = 0; j < subMatrix.GetLength(1); j++)
                            {
                                subMatrix[i, j] = subMatrix[i, j].ToLower(); // Преобразование к нижнему регистру
                            }
                        }
                        break;

                    case "1й символ к верхнему регистру":
                        for (int i = 0; i < subMatrix.GetLength(0); i++)
                        {
                            for (int j = 0; j < subMatrix.GetLength(1); j++)
                            {
                                if (!string.IsNullOrEmpty(subMatrix[i, j]))
                                {
                                    subMatrix[i, j] = subMatrix[i, j][0].ToString().ToUpper() + subMatrix[i, j].Substring(1); // Преобразование 1го символа к верхнему регистру

                                }
                            }
                        }
                        break;

                    case "Не преобразовывать":
                        for (int i = 0; i < subMatrix.GetLength(0); i++)
                        {
                            for (int j = 0; j < subMatrix.GetLength(1); j++)
                            {
                                subMatrix[i, j] = originalMatrix[i, j];
                            }
                        }
                        break;
                }
                switch (choise_2)
                {
                    case "Русские символы":
                        for (int i = 0; i < subMatrix.GetLength(0); i++)
                        {
                            for (int j = 0; j < subMatrix.GetLength(1); j++)
                            {
                                if (Regex.IsMatch(subMatrix[i, j], @"^[\p{IsCyrillic}]+$"))
                                {
                                }
                                else
                                {
                                    subMatrix[i, j] = originalMatrix[i, j];
                                }
                            }
                        }
                        break;

                    case "Латинские символы":
                        for (int i = 0; i < subMatrix.GetLength(0); i++)
                        {
                            for (int j = 0; j < subMatrix.GetLength(1); j++)
                            {
                                if (Regex.IsMatch(subMatrix[i, j], @"^[a-zA-z]+$"))
                                {
                                }
                                else
                                {
                                    subMatrix[i, j] = originalMatrix[i, j];
                                }
                            }
                        }
                        break;

                    case "Русские и латинские символы":
                        for (int i = 0; i < subMatrix.GetLength(0); i++)
                        {
                            for (int j = 0; j < subMatrix.GetLength(1); j++)
                            {
                                if (Regex.IsMatch(subMatrix[i, j], @"[a-zA-Z]") & Regex.IsMatch(subMatrix[i, j], @"[\p{IsCyrillic}]"))
                                {
                                }
                                else
                                {
                                    subMatrix[i, j] = originalMatrix[i, j];
                                }
                            }
                        }
                        break;

                }

                dataGridView1.ColumnCount = userColumnCount;
                dataGridView1.RowCount = userRowCount;
                for (int i = 0; i < userRowCount; i++)
                {
                    for (int j = 0; j < userColumnCount; j++)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = subMatrix[i, j];
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка\nПроверьте введенную размерность матрицы");
            }
        }

    }
}
