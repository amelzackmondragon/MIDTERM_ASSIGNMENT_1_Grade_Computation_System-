using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MIDTERM_ASSIGNMENT_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AttachEvents(this);//para bawal letters only numbers
        }
        private void AttachEvents(Control container)
        {
            foreach (Control c in container.Controls)
            {
                if (c is TextBox)
                {
                    c.KeyPress += OnlyNumbers_KeyPress;
                }
                if (c.HasChildren)
                {
                    AttachEvents(c);
                }
            }
        }
        private void OnlyNumbers_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true; 
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        private double GetGrade(TextBox scoreBox, TextBox totalBox, TextBox gradeBox)
        {
            if (string.IsNullOrWhiteSpace(scoreBox.Text) && string.IsNullOrWhiteSpace(totalBox.Text))
            {
                gradeBox.Text = "";
                return 0;
            }
            if (string.IsNullOrWhiteSpace(scoreBox.Text) || string.IsNullOrWhiteSpace(totalBox.Text))
            {
                throw new Exception(" Incomplete make sure each Score must have a Total");
            }
            try
            {
                double s = double.Parse(scoreBox.Text);
                double t = double.Parse(totalBox.Text);

                if (s > t) throw new Exception("Score cannot be greater than Total");
                if (t == 0) throw new Exception("Total cannot be zero");

                double result = (s / t) * 60 + 40;
                gradeBox.Text = result.ToString("0.0");
                return result;
            }
            catch (FormatException)
            {
                throw new Exception("Please enter valid numbers only");
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void btncompute_Click(object sender, EventArgs e)
        {
            try
            {
                //prelim
                double p_cp = (GetGrade(ps1, pt1, pg1) + GetGrade(ps2, pt2, pg2) + GetGrade(ps3, pt3, pg3) +
                               GetGrade(ps4, pt4, pg4) + GetGrade(ps5, pt5, pg5) + GetGrade(ps6, pt6, pg6)) / 6;
                pav1.Text = p_cp.ToString("0.0");

                double p_lab = (GetGrade(pls1, plt1, plg1) + GetGrade(pls2, plt2, plg2) +
                                GetGrade(pls3, plt3, plg3) + GetGrade(pls4, plt4, plg4)) / 4;
                pav2.Text = p_lab.ToString("0.0");

                double p_qz = (GetGrade(pqs1, pqt1, pqg1) + GetGrade(pqs2, pqt2, pqg2) + GetGrade(pqs3, pqt3, pqg3)) / 3;
                pav3.Text = p_qz.ToString("0.0");

                double p_lab_ex = GetGrade(ples1, plet1, pleg1); 
                double p_wri_ex = GetGrade(pes1, pet1, peg1);    

                double preTotal = (p_cp * 0.1) + (p_lab * 0.1) + (p_qz * 0.2) + (p_lab_ex * 0.2) + (p_wri_ex * 0.4);
                txtp.Text = preTotal.ToString("0.00");

                //midterm
                double m_cp = (GetGrade(ms1, mt1, mg1) + GetGrade(ms2, mt2, mg2) + GetGrade(ms3, mt3, mg3) +
                               GetGrade(ms4, mt4, mg4) + GetGrade(ms5, mt5, mg5) + GetGrade(ms6, mt6, mg6)) / 6;
                mav1.Text = m_cp.ToString("0.0");

                double m_lab = (GetGrade(mls1, mlt1, mlg1) + GetGrade(mls2, mlt2, mlg2) +
                                GetGrade(mls3, mlt3, mlg3) + GetGrade(mls4, mlt4, mlg4)) / 4;
                mav2.Text = m_lab.ToString("0.0");

                double m_qz = (GetGrade(mqs1, mqt1, mqg1) + GetGrade(mqs2, mqt2, mqg2) + GetGrade(mqs3, mqt3, mqg3)) / 3;
                mav3.Text = m_qz.ToString("0.0");

                double m_lab_ex = GetGrade(mles1, mlet1, mleg1); 
                double m_wri_ex = GetGrade(mes1, met1, meg1);    

                double midTotal = (m_cp * 0.1) + (m_lab * 0.1) + (m_qz * 0.2) + (m_lab_ex * 0.2) + (m_wri_ex * 0.4);
                txtm.Text = midTotal.ToString("0.00");

                //finals
                double f_cp = (GetGrade(fs1, ft1, fg1) + GetGrade(fs2, ft2, fg2) + GetGrade(fs3, ft3, fg3) +
                               GetGrade(fs4, ft4, fg4) + GetGrade(fs5, ft5, fg5) + GetGrade(fs6, ft6, fg6)) / 6;
                fav1.Text = f_cp.ToString("0.0");

                double f_lab = (GetGrade(fls1, flt1, flg1) + GetGrade(fls2, flt2, flg2) +
                                GetGrade(fls3, flt3, flg3) + GetGrade(fls4, flt4, flg4)) / 4;
                fav2.Text = f_lab.ToString("0.0");

                double f_qz = (GetGrade(fqs1, fqt1, fqg1) + GetGrade(fqs2, fqt2, fqg2) + GetGrade(fqs3, fqt3, fqg3)) / 3;
                fav3.Text = f_qz.ToString("0.0");

                double f_proj = (GetGrade(fps1, fpt1, fpg1) + GetGrade(fps2, fpt2, fpg2)) / 2;
                double f_wri_ex = GetGrade(fes1, fet1, feg1); 

                double finTotal = (f_cp * 0.05) + (f_lab * 0.1) + (f_qz * 0.2) + (f_proj * 0.25) + (f_wri_ex * 0.40);
                txtf.Text = finTotal.ToString("0.00");

                //SUMMARY 
                double grandTotal = (preTotal * 0.33) + (midTotal * 0.33) + (finTotal * 0.34);
                txttotal.Text = grandTotal.ToString("0.00");

                if (grandTotal >= 75)
                {
                    lblstatus.Text = "PASSED";
                    lblstatus.BackColor = Color.Green;
                    lblstatus.ForeColor = Color.White;
                }
                else
                {
                    lblstatus.Text = "FAILED";
                    lblstatus.BackColor = Color.Red;
                    lblstatus.ForeColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnclear_Click(object sender, EventArgs e)
        {
            ClearAllTextBoxes(this);
            lblstatus.Text = "";
            lblstatus.BackColor = Color.Maroon;
        }
        private void ClearAllTextBoxes(Control container)
        {
            foreach (Control c in container.Controls)
            {
                if (c is TextBox) ((TextBox)c).Clear();
                if (c.HasChildren) ClearAllTextBoxes(c);
            }
        }
        private void btnexit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
