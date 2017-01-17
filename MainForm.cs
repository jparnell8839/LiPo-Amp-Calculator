using System;
using System.Windows.Forms;

namespace LiPo_Amp_Calculator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtCapacity.Text))
                {
                    throw new Exception("Capacity cannot be blank! Please try again!");
                }

                else if (rdoAh.Checked == false & rdomAh.Checked == false)
                {
                    throw new Exception("Please choose a unit of measurement for capacity, Ah (Amp-Hours) or mAh (milliAmp-Hours)");
                }

                else if (String.IsNullOrEmpty(txtCRatingConst.Text))
                {
                    throw new Exception("Please put in the constant C rating for your battery");
                }

                else
                {
                    double Capacity, ConstC, BurstC, ConstA, BurstA;

                    try
                    {
                        double.TryParse(txtCapacity.Text, out Capacity);
                        double.TryParse(txtCRatingConst.Text, out ConstC);
                        if (!String.IsNullOrEmpty(txtCRatingBurst.Text)) { double.TryParse(txtCRatingBurst.Text, out BurstC); }
                        else { BurstC = 0; }
                        if (rdomAh.Checked) { Capacity = Capacity / 1000; }
                        try
                        {
                            ConstA = Capacity * ConstC;
                            txtMaxContAmp.Text = ConstA.ToString();
                            if (!String.IsNullOrEmpty(txtCRatingBurst.Text))
                            {
                                BurstA = Capacity * BurstC;
                                txtMaxBurstAmp.Text = BurstA.ToString();
                            }
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error calculating values...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error parsing input...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormAbout about = new FormAbout();
            about.ShowDialog();
        }
    }
}
