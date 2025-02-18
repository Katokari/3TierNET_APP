using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ContactsMiddleLayer;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void _RefereshDataGrid()
        {
            dgvContacts.DataSource = clsContact.GetALlContacts();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            _RefereshDataGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2(-1);
            frm.ShowDialog();
            _RefereshDataGrid();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2(Convert.ToInt32(dgvContacts.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            _RefereshDataGrid();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsContact.DeleteContact(Convert.ToInt32(dgvContacts.CurrentRow.Cells[0].Value));
            _RefereshDataGrid();
        }
    }
}
