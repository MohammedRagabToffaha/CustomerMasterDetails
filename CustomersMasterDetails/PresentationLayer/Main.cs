using CustomersMasterDetails.BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomersMasterDetails.PresentationLayer
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        private void Main_Load(object sender, EventArgs e)
        {
            ShowCustomers();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            new AddEditCustomer(dgvCustomer).ShowDialog();
        }
        public void ShowCustomers()
        {
            DataTable dt = Customers.SelectAllCustomers();
            DataTable dt1 = AutoNumberedTable(dt);
            dgvCustomer.DataSource = dt1;

            dgvCustomer.Columns[1].Visible = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvCustomer.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dgvCustomer.SelectedCells[1].RowIndex;
                string CustID = dgvCustomer.Rows[selectedRowIndex].Cells[1].Value.ToString();
                string Name = dgvCustomer.Rows[selectedRowIndex].Cells[2].Value.ToString();
                string Address = dgvCustomer.Rows[selectedRowIndex].Cells[3].Value.ToString();
                string Email = dgvCustomer.Rows[selectedRowIndex].Cells[4].Value.ToString();
                string Mobile = dgvCustomer.Rows[selectedRowIndex].Cells[5].Value.ToString();
                new AddEditCustomer(dgvCustomer, CustID, Name, Address, Email, Mobile).ShowDialog();
            }
            else
            {
                MessageBox.Show("اختر العميل الذي تريد تعديله");
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvCustomer.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dgvCustomer.SelectedCells[1].RowIndex;
                string c_Name = dgvCustomer.Rows[selectedRowIndex].Cells[2].Value.ToString();
                DialogResult dr =MessageBox.Show(c_Name + " هل تريد مسح العميل", "مسح عميل", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    
                    string c_id = dgvCustomer.Rows[selectedRowIndex].Cells[1].Value.ToString();
                    int CID = int.Parse(c_id);
                    int s = Customers.DeleteCustomer(CID);
                    if (s > 0)
                    {
                        ShowCustomers();
                    }
                }

            }
            else
            {
                MessageBox.Show("اختر العميل الذي ترد حزفه");
            }
        }
        private DataTable AutoNumberedTable(DataTable SourceTable)

        {

            DataTable ResultTable = new DataTable();

            DataColumn AutoNumberColumn = new DataColumn();

            AutoNumberColumn.ColumnName = "No.";

            AutoNumberColumn.DataType = typeof(int);

            AutoNumberColumn.AutoIncrement = true;

            AutoNumberColumn.AutoIncrementSeed = 1;

            AutoNumberColumn.AutoIncrementStep = 1;

            ResultTable.Columns.Add(AutoNumberColumn);

            ResultTable.Merge(SourceTable);

            return ResultTable;

        }
    }
}
