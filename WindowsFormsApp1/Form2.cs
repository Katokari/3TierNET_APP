using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ContactsMiddleLayer;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        enum enMode { AddNew = 0, Update = 1 }
        private clsContact _contact;
        private enMode _mode;
        private int _ID;

        public Form2(int ID)
        {
            InitializeComponent();

            _ID = ID;

            if (ID == -1)
                _mode = enMode.AddNew;
            else
                _mode = enMode.Update;
        }

        private void _FillComboBoxWithCountries()
        {
            DataTable dt = clsCountry.GetALlCountries();

            foreach (DataRow dataRow in dt.Rows)
            {
                comboBox1.Items.Add(dataRow["CountryName"].ToString());
            }
        }

        private void _LoadData()
        {
            _FillComboBoxWithCountries();
            comboBox1.SelectedIndex = 0;

            if (_mode == enMode.AddNew)
            {
                label1.Text = "Add New Contact";
                _contact = new clsContact();
                return;
            }

            _contact = clsContact.Find(_ID);
            label1.Text = "Edit Contact with ID = " + _ID;

            if (_contact == null)
            {
                MessageBox.Show("Contact was deleted so form will close", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            label10.Text = _contact.ID.ToString();
            textBox1.Text = _contact.FirstName;
            textBox2.Text = _contact.LastName;
            textBox3.Text = _contact.Email;
            textBox4.Text = _contact.Phone;
            textBox5.Text = _contact.Address;
            dateTimePicker1.Value = _contact.DateOfBirth;
            
            if (_contact.ImagePath != "")
            {
                pictureBox1.Load(_contact.ImagePath);
            }

            linkLabel2.Visible = (_contact.ImagePath != "");

            comboBox1.SelectedIndex = comboBox1.FindString(clsCountry.Find(_contact.CountryID).CountryName);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            _contact.FirstName = textBox1.Text;
            _contact.LastName = textBox2.Text;
            _contact.Email = textBox3.Text;
            _contact.Phone = textBox4.Text;
            _contact.Address = textBox5.Text;
            _contact.CountryID = clsCountry.Find(comboBox1.Text).ID;
            _contact.DateOfBirth = dateTimePicker1.Value;
            if (pictureBox1.ImageLocation != null)
            {
                _contact.ImagePath = pictureBox1.ImageLocation.ToString();
            } 
            else
            {
                _contact.ImagePath = "";
            }

            if (_contact.Save())
            {
                MessageBox.Show("Data Saved Successfully");
            }
            else
            {
                MessageBox.Show("Something Went Wrong");
            }

            _mode = enMode.Update;
            label10.Text = _contact.ID.ToString();
            label1.Text = "Edit Contact with ID = " + _ID;

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.tiff;*.webp;*.svg";
            openFileDialog.InitialDirectory = @"C:\Users\xevel\Pictures";
            openFileDialog.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog.FileName;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pictureBox1.ImageLocation = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
