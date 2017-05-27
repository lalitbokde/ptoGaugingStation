using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCD_Measurment_of_SSSA
{
    public partial class SplineShaftCCDmeasurementTabDesing : Form
    {
        SQLHelper _helper = new SQLHelper();
        BAL _bal = new BAL();
        Validation _objValidation = new Validation();
        LoginForm _login = new LoginForm();

        double shim;
        string master_value,master_value1, CCD_Value , shim_verification_tolerence;
        bool verifyAll = false;
        public SplineShaftCCDmeasurementTabDesing()
        {
            InitializeComponent();
        }

        private void PTOMesurementTabDesing_Load(object sender, EventArgs e)
        {
            dgv_PTOMesurement.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12F, FontStyle.Bold);
            this.Hide();

        login:
            _login.ShowDialog();
            if (_login.result == "loginSuccess")
            {
                this.Show();
                if (_bal.checkAdmin(_login.txt_username.Text))
                {
                    bttn_Menu.Visible = true;
                }
                txt_username.Text = _login.txt_username.Text;
                txt_shift.Text = _login.txt_shift.Text;

                _login.txt_username.Text = "";
                _login.txt_shift.Text = "";
                _login.txt_password.Text = "";
            }
            else if (_login.result == "loginFail")
            {
                goto login;
            }
            else
            {
                Application.Exit();
            }
            //lbl_Time.Text = DateTime.Now.ToString("dd/MM/yyyy ");
            txt_time.Text = DateTime.Now.ToString("hh:mm tt");

            FillGrid();
            getmodel();
            txt_id_page1.Text = _bal.getid();
            if (txt_id_page1.Text == "")
            {
                txt_id_page1.Text = "1";
            }

            
            cmb_selectmodel.Focus();
        }

        private void bttn_checkshims_Click(object sender, EventArgs e)
        {

        }

        private void bttn_checkall_Click(object sender, EventArgs e)
        {

        }

        private void bttn_checkreset_Click(object sender, EventArgs e)
        {
            txt_dailgaugereadingDriving_page1.Text = _objValidation.Gettingdecimals(txt_dailgaugereadingDriving_page1.Text = "0");
            MasterValue_Driving_Page1.Text = _objValidation.Gettingdecimals(MasterValue_Driving_Page1.Text = master_value.ToString());
        }

        private void MasterTab_Click(object sender, EventArgs e)
        {

        }

        private void bttn_checkreset_driving_Click(object sender, EventArgs e)
        {
            //txt_dialgaugereading_driving_page1.Text = _objValidation.Gettingdecimals(txt_dialgaugereading_driving_page1.Text = "0");
            //txt_masterValueofY.Text = _objValidation.Gettingdecimals(txt_masterValueofY.Text = master_value_of_X.ToString());
        }



        private void bttn_DialGaugeReading_Click(object sender, EventArgs e)
        {

            shim = Math.Abs(Convert.ToDouble(txt_ShimRequiredDriving_page2.Text));
            if (((Convert.ToDouble(txt_ShimRequiredDriving_page2.Text) * 1000) - (Convert.ToDouble(txt_dailgaugereading_verification_page3.Text) * 1000)) < Convert.ToDouble(shim_verification_tolerence) * 1000)
            {
                Verified.Visible = true;
                NotVerified.Visible = false;

            }
            else
            {
                Verified.Visible = false;
                NotVerified.Visible = true;
            }
            // txt_shimsRequired.Text = txt_dialgaugereading.Text;
        }
        public void DrivenGaugereadeingCheck()
        {
            try
            {
                if (txt_ShimRequiredDriving_page2.Text != "" && txt_dailgaugereading_verification_page3.Text != "")
                {
                    shim = Math.Abs(Convert.ToDouble(txt_ShimRequiredDriving_page2.Text));
                    if (Math.Abs(((Convert.ToDouble(txt_ShimRequiredDriving_page2.Text) * 1000) - (Convert.ToDouble(txt_dailgaugereading_verification_page3.Text) * 1000))) < Convert.ToDouble(shim_verification_tolerence) * 1000)
                    {
                        Verified.Visible = true;
                        NotVerified.Visible = false;





                    }
                    else
                    {
                        Verified.Visible = false;
                        NotVerified.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Verified.Visible = false;
                NotVerified.Visible = false;
            }
        }


        public void DrivenGaugereadeingCheckDriven()
        {
            try
            {
                if (txt_shimsRequiredDriven_page2.Text != "" && txt_dailgaugereading_Driving.Text != "")
                {
                    shim = Math.Abs(Convert.ToDouble(txt_shimsRequiredDriven_page2.Text));
                    if (Math.Abs(((Convert.ToDouble(txt_shimsRequiredDriven_page2.Text) * 1000) - (Convert.ToDouble(txt_dailgaugereading_Driving.Text) * 1000))) < Convert.ToDouble(shim_verification_tolerence) * 1000)
                    {
                        VerifiedDriven.Visible = true;
                        NotVerifiedDriven.Visible = false;





                    }
                    else
                    {
                        VerifiedDriven.Visible = false;
                        NotVerifiedDriven.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Verified.Visible = false;
                NotVerifiedDriven.Visible = false;
            }
        }

        private bool VerifyAll()
        {
            verifyAll = true;

            if (String.IsNullOrEmpty(cmb_selectmodel.Text))
            {
                lbl_selectmodel.ForeColor = Color.Red;

                verifyAll = false;
            }
            else
            {
                lbl_selectmodel.ForeColor = Color.Black;
            }

            if (String.IsNullOrEmpty(txt_dailgaugereadingDriving_page1.Text) || Convert.ToDouble(txt_dailgaugereadingDriving_page1.Text) != 0)
            {
                lbl_gauge_reading.ForeColor = Color.Red;

                verifyAll = false;
            }
            else
            {
                lbl_gauge_reading.ForeColor = Color.Black;
            }


            if (String.IsNullOrEmpty(MasterValue_Driving_Page1.Text))
            {
                lbl_mastervalueofx.ForeColor = Color.Red;

                verifyAll = false;
            }
            else
            {
                lbl_mastervalueofx.ForeColor = Color.Black;
            }



            //second 

            if (String.IsNullOrEmpty(txt_dialogreadingDriven_page2.Text))
            {
                lbl_dialgaugeread1.ForeColor = Color.Red;

                verifyAll = false;
            }
            else
            {
                lbl_dialgaugeread1.ForeColor = Color.Black;
            }


            if (String.IsNullOrEmpty(txt_mastervalueDriven_page2.Text))
            {
                lbl_masterval1.ForeColor = Color.Red;

                verifyAll = false;
            }
            else
            {
                lbl_masterval1.ForeColor = Color.Black;
            }

            if (String.IsNullOrEmpty(txt_ShimRequiredDriving_page2.Text))
            {
                lbl_shimM_Required_Driven.ForeColor = Color.Red;

                verifyAll = false;
            }
            else
            {
                lbl_shimM_Required_Driven.ForeColor = Color.Black;
            }



            //third txt_dailgaugereading_driven_page3

            if (String.IsNullOrEmpty(txt_dailgaugereading_verification_page3.Text))
            {
                lbl_dialgaugereading.ForeColor = Color.Red;

                verifyAll = false;
            }
            else
            {
                lbl_dialgaugereading.ForeColor = Color.Black;
            }

            if (String.IsNullOrEmpty(txtmasterValueDrivingpage3.Text))
            {
                lbl_spline_Shaft_Etched_Value_page3.ForeColor = Color.Red;

                verifyAll = false;
            }
            else
            {
                lbl_spline_Shaft_Etched_Value_page3.ForeColor = Color.Black;
            }

            if (String.IsNullOrEmpty(txtmasterValueDrivenpage.Text))
            {
                lbl_spline_Shaft_Etched_Value_page3.ForeColor = Color.Red;

                verifyAll = false;
            }
            else
            {
                lbl_spline_Shaft_Etched_Value_page3.ForeColor = Color.Black;
            }

            if (String.IsNullOrEmpty(txt_shimsRequiredDrivingVerify.Text))
            {
                lbl_shimRequiredDriving_verify.ForeColor = Color.Red;

                verifyAll = false;
            }
            else
            {
                lbl_shimRequiredDriving_verify.ForeColor = Color.Black;
            }

            return verifyAll;

        }

        private void bttn_next_Click(object sender, EventArgs e)
        {
            if (ValidFirst())
            {
                this.PtoMeasurement.SelectedTab = tab_shimmeasurement;

                txt_masterValueDriving_page2.Text = MasterValue_Driving_Page1.Text;
                txt_dailgaugereadingDriving_page2.Select();
                txt_mastervalueDriven_page2.Text = MasterValue_Driving_Page1.Text;
                txt_dialogreadingDriven_page2.Select();
                txt_dailgaugereadingDriving_page2.Focus();

            }
        }
        private bool ValidFirst()
        {
            bool checkvalue = true;
            if (String.IsNullOrEmpty(cmb_selectmodel.Text ))
            {
                lbl_selectmodel.ForeColor = Color.Red;

                checkvalue = false;
            }
            else
            {
                lbl_selectmodel.ForeColor = Color.Black;
            }

            if (String.IsNullOrEmpty(txt_dailgaugereadingDriving_page1.Text))
            {
                lbl_gauge_reading.ForeColor = Color.Red;

                checkvalue = false;
            }
            else
            {
                lbl_gauge_reading.ForeColor = Color.Black;
            }

            if (String.IsNullOrEmpty(MasterValue_Driving_Page1.Text))
            {
                lbl_mastervalueofx.ForeColor = Color.Red;

                checkvalue = false;
            }
            else
            {
                lbl_mastervalueofx.ForeColor = Color.Black;
            }



            return checkvalue;
        }

        private void txt_dailgaugereading_deriven_Validated(object sender, EventArgs e)
        {
            txt_ShimRequiredDriving_page2.Text = _objValidation.Gettingdecimals(txt_ShimRequiredDriving_page2.Text);
        }

        private void txt_dialgaugereading_driving_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar))
         && (e.KeyChar != '.') && (e.KeyChar != '-'))
                e.Handled = true;

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;

            // only allow minus sign at the beginning
            if (e.KeyChar == '-' && (sender as TextBox).Text.Length > 0)
                e.Handled = true;
        }

        private void txt_mastervalue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar))
          && (e.KeyChar != '.') && (e.KeyChar != '-'))
                e.Handled = true;

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;

            // only allow minus sign at the beginning
            if (e.KeyChar == '-' && (sender as TextBox).Text.Length > 0)
                e.Handled = true;
        }



        private void txt_mastervalue_driving_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar))
          && (e.KeyChar != '.') && (e.KeyChar != '-'))
                e.Handled = true;

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;

            // only allow minus sign at the beginning
            if (e.KeyChar == '-' && (sender as TextBox).Text.Length > 0)
                e.Handled = true;
        }

        private void txt_shimsRequired_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar))
         && (e.KeyChar != '.') && (e.KeyChar != '-'))
                e.Handled = true;

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;

            // only allow minus sign at the beginning
            if (e.KeyChar == '-' && (sender as TextBox).Text.Length > 0)
                e.Handled = true;
        }

        private void txt_shimsreq_driving_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar))
         && (e.KeyChar != '.') && (e.KeyChar != '-'))
                e.Handled = true;

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;

            // only allow minus sign at the beginning
            if (e.KeyChar == '-' && (sender as TextBox).Text.Length > 0)
                e.Handled = true;
        }

        private void txt_dialgaugereading_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar))
        && (e.KeyChar != '.') && (e.KeyChar != '-'))
                e.Handled = true;

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;

            // only allow minus sign at the beginning
            if (e.KeyChar == '-' && (sender as TextBox).Text.Length > 0)
                e.Handled = true;
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar))
          && (e.KeyChar != '.') && (e.KeyChar != '-'))
                e.Handled = true;

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;

            // only allow minus sign at the beginning
            if (e.KeyChar == '-' && (sender as TextBox).Text.Length > 0)
                e.Handled = true;
        }


        private bool ValidSecond()
        {
            bool checkvalue = true;
            if (String.IsNullOrEmpty(txt_dialogreadingDriven_page2.Text))
            {
                lbl_dialgaugeread1.ForeColor = Color.Red;

                checkvalue = false;
            }
            else
            {
                lbl_dialgaugeread1.ForeColor = Color.Black;
            }

            if (String.IsNullOrEmpty(txt_mastervalueDriven_page2.Text))
            {
                lbl_masterval1.ForeColor = Color.Red;

                checkvalue = false;
            }
            else
            {
                lbl_masterval1.ForeColor = Color.Black;
            }

            if (String.IsNullOrEmpty(txt_ShimRequiredDriving_page2.Text))
            {
                lbl_shimM_Required_Driven.ForeColor = Color.Red;

                checkvalue = false;
            }
            else
            {
                lbl_shimM_Required_Driven.ForeColor = Color.Black;
            }

            return checkvalue;
        }

        private Dictionary<TabPage, Color> TabColors = new Dictionary<TabPage, Color>();
        private void SetTabHeader(TabPage page, Color color)
        {
            TabColors[page] = color;
            PtoMeasurement.Invalidate();
        }
        private void PtoMeasurement_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage page = PtoMeasurement.TabPages[e.Index];
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(239, 127, 26)), e.Bounds);

            Rectangle paddedBounds = e.Bounds;
            int yOffset = (e.State == DrawItemState.Selected) ? -2 : 1;
            paddedBounds.Offset(1, yOffset);
            Font f = new Font("Arial", 12, FontStyle.Bold);
            TextRenderer.DrawText(e.Graphics, page.Text, f, paddedBounds, Color.White);

            //using (Brush br = new SolidBrush(Color.SkyBlue))
            //{
            //    e.Graphics.FillRectangle(br, e.Bounds);
            //    SizeF sz = e.Graphics.MeasureString(PtoMeasurement.TabPages[e.Index].Text, e.Font);
            //    e.Graphics.DrawString(PtoMeasurement.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2, e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + 1);

            //    Rectangle rect = e.Bounds;
            //    rect.Offset(0, 1);
            //    rect.Inflate(0, -1);
            //    e.Graphics.DrawRectangle(Pens.DarkGray, rect);
            //    e.DrawFocusRectangle();
            //}
        }

        private void bttn_saveAll_Click(object sender, EventArgs e)
        {
            if (VerifyAll())
            {
                string total_shim_required = "", shim_required_driving = "";
                string total_shim_required_Driven = "", shim_required_driven = "";
                foreach (var item in LB_DrivingTotal_shims.Items)
                {
                    total_shim_required += item + "  ";
                }

                foreach (var item in lbl_Driventotal_shims.Items)
                {
                    total_shim_required_Driven += item + "  ";
                }



                _bal.AddPTOMeasurementData(cmb_selectmodel.SelectedValue.ToString(),cmb_selectmodel.Text, txt_MasterValueDriven_2.Text, MasterValue_Driving_Page1.Text, txt_DialGaugeReadingDriven_2.Text, txt_dailgaugereadingDriving_page2.Text, txt_ShimRequiredDriving_page2.Text, txt_dialogreadingDriven_page2.Text, txt_shimsRequiredDriven_page2.Text, txt_dailgaugereading_Driving.Text, txt_dailgaugereading_verification_page3.Text, txt_dailgaugereadingDriving_page1.Text,DateTime.Now);
              FillGrid();
                Masterresetall();
                MessageBox.Show("Data Saved");
                txt_id_page1.Clear();
                this.PtoMeasurement.SelectedTab = MasterTab;
                txt_id_page1.Text = _bal.getid();
                
            }
            else
            {
                MessageBox.Show("Verify All values");
            }
        }
        public void getmodel()
        {
           // DataTable dt = new DataTable();
            cmb_selectmodel.DataSource = _bal.getmodeldata();
        }
        private void Masterresetall()
        {
            MasterValue_Driving_Page1.ResetText();
            txt_dialogreadingDriven_page2.ResetText();
            txtmasterValueDrivingpage3.ResetText();
            txt_id_page1.ResetText();
            txt_DialGaugeReadingDriven_2.ResetText();
            txt_dailgaugereading_Driving.ResetText();
            txt_shimsRequiredDrivenVerify.ResetText();
            txt_mastervalueDriven_page2.ResetText();
            txt_MasterValueDriven_2.ResetText();
            txtmasterValueDrivenpage.ResetText();
            txt_dailgaugereadingDriving_page2.Text = "0";
            txt_ShimRequiredDriving_page2.ResetText();
            txt_dailgaugereadingDriving_page1.ResetText();
            txt_dailgaugereading_verification_page3.ResetText();
            txt_shimsRequiredDrivingVerify.ResetText();
            txt_ShimRequiredDriving_page2.ResetText();
            LB_DrivingTotal_shims.Items.Clear();
            Verified.Visible = false;
            NotVerified.Visible = false;
            NotVerifiedDriven.Visible = false;
            VerifiedDriven.Visible = false;
        }

        private void FillGrid()
        {
            dgv_PTOMesurement.Rows.Clear();
            DataTable dt = new DataTable();
            dt = _bal.getModel();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgv_PTOMesurement.Rows.Add();
                 dgv_PTOMesurement.Rows[i].Cells["modelid"].Value = dt.Rows[i]["modelid"].ToString();
                dgv_PTOMesurement.Rows[i].Cells["Model_Name"].Value = dt.Rows[i]["Model_Name"].ToString();
                dgv_PTOMesurement.Rows[i].Cells["MasterValDriven"].Value = dt.Rows[i]["MasterValDriven"].ToString();
                dgv_PTOMesurement.Rows[i].Cells["MasterValDriving"].Value = dt.Rows[i]["MasterValDriving"].ToString();
                dgv_PTOMesurement.Rows[i].Cells["GuageReadDriving"].Value = dt.Rows[i]["GuageReadDriving"].ToString();
                dgv_PTOMesurement.Rows[i].Cells["GaugeReadDriven"].Value = dt.Rows[i]["GaugeReadDriven"].ToString();
                dgv_PTOMesurement.Rows[i].Cells["GaugeMegDriving"].Value = dt.Rows[i]["GaugeMegDriving"].ToString();
                dgv_PTOMesurement.Rows[i].Cells["shimsMegReqDriving"].Value = dt.Rows[i]["shimsMegReqDriving"].ToString();
                dgv_PTOMesurement.Rows[i].Cells["GuagemeasurementDriven"].Value = dt.Rows[i]["GuagemeasurementDriven"].ToString();
                dgv_PTOMesurement.Rows[i].Cells["ShimsMegReqDriven"].Value = dt.Rows[i]["ShimsMegReqDriven"].ToString();
                dgv_PTOMesurement.Rows[i].Cells["GuagereadVarDriven"].Value = dt.Rows[i]["GuagereadVarDriven"].ToString();
                dgv_PTOMesurement.Rows[i].Cells["GuageReadVarDriving"].Value = dt.Rows[i]["GuageReadVarDriving"].ToString();
                dgv_PTOMesurement.Rows[i].Cells["createdate"].Value = dt.Rows[i]["createdate"].ToString();

            }
        }

        private void txt_dailgaugereading_driven_page2_TextChanged(object sender, EventArgs e)
        {
            try
            {


                if (txt_mastervalueDriven_page2.Text != "" && txt_dialogreadingDriven_page2.Text != "")
                {
                    try
                    {

                        txt_shimsRequiredDriven_page2.Text = _objValidation.Gettingdecimals((106.35 - (Convert.ToDouble(txt_mastervalueDriven_page2.Text) - Convert.ToDouble(txt_dialogreadingDriven_page2.Text))).ToString());

                        int count0point05microm = 0;
                        int count0point2microm = 0;
                        int count0point5microm = 0;



                        

                        shim = Math.Abs(Convert.ToDouble(txt_shimsRequiredDriven_page2.Text) * 1000);

                        while (shim >= 25)
                        {


                            if (shim >= 500)
                            {
                                shim = shim - 500;
                                count0point5microm++;
                                continue;
                            }
                            if (shim >= 200)
                            {
                                shim = shim - 200;
                                count0point2microm++;
                                continue;
                            }


                            if (shim >= 50 || shim >= 25)
                            {
                                shim = shim - 500;
                                count0point05microm++;
                                continue;
                            }
                        }

                        string MicroM0 = "0.5 mm :";
                        string MicroM1 = "0.2 mm :";
                        string MicroM2 = "0.05 mm :";


                        lbl_Driventotal_shims.Items.Clear();

                        if (count0point5microm != 0)
                            lbl_Driventotal_shims.Items.Add(MicroM0 + count0point5microm.ToString());
                        if (count0point2microm != 0)
                            lbl_Driventotal_shims.Items.Add(MicroM1 + count0point2microm.ToString());
                        if (count0point05microm != 0)
                            lbl_Driventotal_shims.Items.Add(MicroM2 + count0point05microm.ToString());



                    }
                    catch { }
                }
            }
            catch
            {

            }


        }

        private void tab_shimmeasurement_Enter(object sender, EventArgs e)
        {
            if (!ValidFirst())
            {
                bttn_exportexcel.Visible = false;

                this.PtoMeasurement.SelectedTab = MasterTab;
            }
            else
            {
                bttn_exportexcel.Visible = false;

                txt_dialogreadingDriven_page2.Focus();
            }
        }

        private void tab_shimVerification_Enter(object sender, EventArgs e)
        {
            if (!ValidFirst())
            {
                bttn_exportexcel.Visible = false;

                this.PtoMeasurement.SelectedTab = MasterTab;
            }
            else if (!ValidSecond())
            {
                bttn_exportexcel.Visible = false;

                this.PtoMeasurement.SelectedTab = tab_shimmeasurement;
            }
            else
            {
                bttn_exportexcel.Visible = false;
                txtmasterValueDrivenpage.Text = txt_mastervalueDriven_page2.Text;
                txt_shimsRequiredDrivingVerify.Text = txt_ShimRequiredDriving_page2.Text;
                txt_dailgaugereading_verification_page3.Focus();
            }
        }

        private void lbl_addModel_Click(object sender, EventArgs e)
        {
            ModelMaster _objShow = new ModelMaster();
            _objShow.ShowDialog();
           // cmb_selectmodel.DataSource = _bal.getModel();
        }

        private void txt_dailgaugereading_driven_page1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDouble(txt_dailgaugereadingDriving_page1.Text) == 0)
                {
                   
                    MasterValue_Driving_Page1.Text = master_value; // "4.512"
                    txt_dailgaugereadingDriving_page1.Text = "0.000";
                }
            }
            catch
            {
                if (txt_dailgaugereadingDriving_page1.Text != "")
                {
                    MessageBox.Show("Please Enter Zero Value from Dial Gauage");
                }
            }
        }

        private void txt_dialgaugereading_driving_page1_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (Convert.ToDouble(txt_dialgaugereading_driving_page1.Text) == 0)
            //    {
            //        txt_dialgaugereading_driving_page1.Text = "0.000";
            //        txt_masterValueofY.Text = master_value_of_Y.ToString();
            //    }
            //}
            //catch
            //{
            //    if (txt_dialgaugereading_driving_page1.Text != "")
            //    {
            //        MessageBox.Show("Please Enter Zero Value from Dial Gauage");
            //    }
            //}
        }

        private void groupBox_drivingshaft_Enter(object sender, EventArgs e)
        {

        }

        private void txt_dailgaugereading_driven_page3_TextChanged(object sender, EventArgs e)
        {
            DrivenGaugereadeingCheck();
        }

        private void txt_dialgaugereading_driving_page3_TextChanged(object sender, EventArgs e)
        {

        }

        private void PTOMesurementTabDesing_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.F2)
            {
                if (PtoMeasurement.SelectedTab == MasterTab)
                {
                    if (ValidFirst())
                    {
                        this.PtoMeasurement.SelectedTab = tab_shimmeasurement;

                        txt_mastervalueDriven_page2.Text = MasterValue_Driving_Page1.Text;
                        txt_dialogreadingDriven_page2.Focus();
                    }
                }
                else if (PtoMeasurement.SelectedTab == tab_shimmeasurement)
                {
                    if (ValidSecond())
                    {
                        this.PtoMeasurement.SelectedTab = tab_shimVerification;

                        txtmasterValueDrivingpage3.Text = txt_mastervalueDriven_page2.Text;
                        txt_shimsRequiredDrivingVerify.Text = txt_ShimRequiredDriving_page2.Text;
                        txt_dailgaugereading_verification_page3.Focus();
                    }
                }
                else if (PtoMeasurement.SelectedTab == tab_shimVerification)
                {
                    this.PtoMeasurement.SelectedTab = tab_report;
                    bttn_exportexcel.Visible = true;

                }
            }

            if (e.KeyData == Keys.F1)
            {
                if (PtoMeasurement.SelectedTab == MasterTab)
                {

                }
                else if (PtoMeasurement.SelectedTab == tab_shimmeasurement)
                {

                    this.PtoMeasurement.SelectedTab = MasterTab;

                }
                else if (PtoMeasurement.SelectedTab == tab_shimVerification)
                {
                    this.PtoMeasurement.SelectedTab = tab_shimmeasurement;
                }
                else if (PtoMeasurement.SelectedTab == tab_report)
                {
                    this.PtoMeasurement.SelectedTab = tab_shimVerification;

                }

            }

            if (e.KeyCode == Keys.S && e.Modifiers == Keys.Control)
            {
                bttn_saveAll_Click(this, new EventArgs());
            }

            if (e.KeyCode == Keys.M && e.Modifiers == Keys.Control)
            {
                lbl_addModel_Click(this, new EventArgs());
            }
        }

        private void txt_mastervalue_driving_page3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar))
        && (e.KeyChar != '.') && (e.KeyChar != '-'))
                e.Handled = true;

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;

            // only allow minus sign at the beginning
            if (e.KeyChar == '-' && (sender as TextBox).Text.Length > 0)
                e.Handled = true;
        }

        private void txt_dialgaugereading_driving_page1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar))
        && (e.KeyChar != '.') && (e.KeyChar != '-'))
                e.Handled = true;

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;

            // only allow minus sign at the beginning
            if (e.KeyChar == '-' && (sender as TextBox).Text.Length > 0)
                e.Handled = true;
        }

        private void txt_mastervalue_driven_page2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar))
        && (e.KeyChar != '.') && (e.KeyChar != '-'))
                e.Handled = true;

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;

            // only allow minus sign at the beginning
            if (e.KeyChar == '-' && (sender as TextBox).Text.Length > 0)
                e.Handled = true;
        }

        private void txt_mastervalue_driven_page2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar))
        && (e.KeyChar != '.') && (e.KeyChar != '-'))
                e.Handled = true;

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;

            // only allow minus sign at the beginning
            if (e.KeyChar == '-' && (sender as TextBox).Text.Length > 0)
                e.Handled = true;
        }

        private void lbl_F1_Click(object sender, EventArgs e)
        {

        }

        private void txt_dailgaugereading_driven_page2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar))
        && (e.KeyChar != '.') && (e.KeyChar != '-'))
                e.Handled = true;

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;

            // only allow minus sign at the beginning
            if (e.KeyChar == '-' && (sender as TextBox).Text.Length > 0)
                e.Handled = true;
        }

        private void txt_dialgaugereading_driving_page2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar))
        && (e.KeyChar != '.') && (e.KeyChar != '-'))
                e.Handled = true;

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;

            // only allow minus sign at the beginning
            if (e.KeyChar == '-' && (sender as TextBox).Text.Length > 0)
                e.Handled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void bttn_Menu_Click(object sender, EventArgs e)
        {
            Frm_MenuList _objMenu = new Frm_MenuList();
            _objMenu.ShowDialog();
        }

        private void txt_splineshaftmodelno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_dailgaugereadingDriving_page1.Focus();
            }
        }

        private void MasterTab_Enter(object sender, EventArgs e)
        {
            bttn_exportexcel.Visible = false;

            txt_dailgaugereadingDriving_page1.Focus();
        }

      
        private void txt_dailgaugereading_driven_page3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                System.Threading.Thread.Sleep(5000);
                if (Verified.Visible == true)
                {
                    if (VerifyAll())
                    {
                        string total_shim_required = "", shim_required_driving = "";
                        foreach (var item in lbl_Driventotal_shims.Items)
                        {
                            total_shim_required += item + "  ";
                        }

                        FillGrid();
                        Masterresetall();
                        MessageBox.Show("Data Saved");
                        this.PtoMeasurement.SelectedTab = MasterTab;
                        bttn_saveAll.Focus();
                        getmodel();
                    }
                    else
                    {
                        MessageBox.Show("Verify All values");
                    }
                }
            }
        }

        private void txt_dailgaugereading_driven_page1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txt_dailgaugereadingDriving_page1.Text == "")
                {
                    return;
                }
                else
                {
                    txt_DialGaugeReadingDriven_2.Focus();
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            PTOMesurementTabDesing_Load(this, new EventArgs());
        }

        private void cmb_selectmodel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmb_selectmodel_SelectedValueChanged(object sender, EventArgs e)
        {
            DataTable dtmodel = _bal.getModelDetailsById(cmb_selectmodel.SelectedValue.ToString());
            master_value = _objValidation.Gettingdecimals(dtmodel.Rows[0]["master_value"].ToString());
            master_value1 = _objValidation.Gettingdecimals(dtmodel.Rows[0]["master_value1"].ToString());

            shim_verification_tolerence = _objValidation.Gettingdecimals(dtmodel.Rows[0]["shim_verification_tolerence"].ToString());

        }

        private void bttn_exportexcel_Click(object sender, EventArgs e)
        {
            if (dgv_PTOMesurement.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application xlApp;
                Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                //Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet1;
                object misValue = System.Reflection.Missing.Value;

                xlApp = new Microsoft.Office.Interop.Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                //xlWorkSheet1 = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(2);
                int i = 0;
                int j = 0;
                for (i = 0; i < 1; i++)
                {
                    for (j = 0; j <= dgv_PTOMesurement.ColumnCount - 1; j++)
                    {
                        string header = dgv_PTOMesurement.Columns[j].HeaderText;
                        xlWorkSheet.Cells[i + 1, j + 1] = header;
                    }
                }

                for (i = 0; i <= dgv_PTOMesurement.RowCount - 1; i++)
                {
                    for (j = 0; j <= dgv_PTOMesurement.ColumnCount - 1; j++)
                    {
                        DataGridViewCell cell = dgv_PTOMesurement[j, i];
                        xlWorkSheet.Cells[i + 2, j + 1] = cell.Value;
                    }
                }
                // xlWorkSheet1.Cells[dgv_problem_master.RowCount, dgv_problem_master.ColumnCount] = "http://csharp.net-informations.com";
                //xlWorkSheet1.Cells[dgv_problem_master.RowCount, dgv_problem_master.ColumnCount] = "Chart Image";

                //xlWorkSheet1.Shapes.AddPicture(@"F:\vaishali\LearningMOBI\WebProject\img1.jpg", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 50, 50 + dgv_problem_master.RowCount, 300, 45); 
                string time = DateTime.Now.ToString("HH:mm").Replace(':', '_');
                string excelsheet_name = Environment.CurrentDirectory + "\\ExcelFiles\\" + "CCD_Measurment_of_SSSA" + "_" + DateTime.Now.ToString("dd-MM-yyyy") + "_" + time + ".xls";
                xlWorkBook.SaveAs(excelsheet_name, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                releaseObject(xlWorkSheet);
                // releaseObject(xlWorkSheet1);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);

                MessageBox.Show("Excel file created , you can find the file " + excelsheet_name);
            }
            else
            {
                MessageBox.Show("There is no data to export");

            }
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void tab_report_Enter(object sender, EventArgs e)
        {
            bttn_exportexcel.Visible = true;

        }


        private void txt_dialgaugereading_driving_page1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bttn_next.Focus();
            }
        }

        private void txt_Dial_Gauge_Reading_of_Y_page2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_mastervalueDriven_page2.Text != "" && txt_dialogreadingDriven_page2.Text != "")
                {
                    txt_ShimRequiredDriving_page2.Text = _objValidation.Gettingdecimals(((Convert.ToDouble(txt_mastervalueDriven_page2.Text) - Convert.ToDouble(txt_dialogreadingDriven_page2.Text)) - (0.2)).ToString());


                    int count0point1microm = 0;
                    int count0point2microm = 0;
                    int count0point5microm = 0;

                    shim = Math.Abs(Convert.ToDouble(txt_ShimRequiredDriving_page2.Text) * 1000);

                    while (shim >= 50)
                    {

                        if (shim >= 500)
                        {
                            shim = shim - 500;
                            count0point5microm++;
                            continue;
                        }

                        if (shim >= 200)
                        {
                            shim = shim - 200;
                            count0point2microm++;
                            continue;
                        }


                        if (shim >= 100)
                        {
                            shim = shim - 100;
                            count0point1microm++;
                            continue;
                        }


                    }

                    string MicroM1 = "0.1 mm :";
                    string MicroM2 = "0.2 mm :";
                    string MicroM3 = "0.5 mm :";

                    LB_DrivingTotal_shims.Items.Clear();

                    if (count0point1microm != 0)
                        LB_DrivingTotal_shims.Items.Add(MicroM1 + count0point1microm.ToString());
                    if (count0point2microm != 0)
                        LB_DrivingTotal_shims.Items.Add(MicroM2 + count0point2microm.ToString());
                    if (count0point5microm != 0)
                        LB_DrivingTotal_shims.Items.Add(MicroM3 + count0point5microm.ToString());

                }
            }
            catch { }
        }

        private void txt_Dial_Gauge_Reading_of_X_page2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txt_dialogreadingDriven_page2.Text == "")
                {
                    return;
                }
                else
                {
                    bttn_next_page2.Focus();
                }
            }
        }

        private void txt_Dial_Gauge_Reading_of_Y_page2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bttn_next_page2.Focus();
            }
        }

        private void bttn_next_page2_Click(object sender, EventArgs e)
        {
            if (ValidSecond())
            {
                this.PtoMeasurement.SelectedTab = tab_shimVerification;

                txtmasterValueDrivingpage3.Text = txt_dailgaugereadingDriving_page2.Text;
                txtmasterValueDrivingpage3.Text = txt_masterValueDriving_page2.Text;
                txt_shimsRequiredDrivingVerify.Text = txt_ShimRequiredDriving_page2.Text;
                txt_dailgaugereading_verification_page3.Select();

                txtmasterValueDrivenpage.Text = txt_dialogreadingDriven_page2.Text;
                txtmasterValueDrivenpage.Text = txt_mastervalueDriven_page2.Text;
                txt_shimsRequiredDrivenVerify.Text = txt_shimsRequiredDriven_page2.Text;
                txt_dailgaugereading_Driving.Select();
            }
            txt_dailgaugereading_verification_page3.Focus();
        }

        private void txt_dailgaugereading_page2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                

                if (txt_masterValueDriving_page2.Text != "" && txt_dailgaugereadingDriving_page2.Text != "")
                {
                    try
                    {
                       
                            txt_ShimRequiredDriving_page2.Text = _objValidation.Gettingdecimals((106.35-(Convert.ToDouble(txt_masterValueDriving_page2.Text) - Convert.ToDouble(txt_dailgaugereadingDriving_page2.Text))).ToString());

                            int count0point05microm = 0;
                            int count0point2microm = 0;
                            int count0point5microm = 0;
                           
                         
                            
                          

                            shim = Math.Abs(Convert.ToDouble(txt_ShimRequiredDriving_page2.Text) * 1000);

                            while (shim >= 25)
                            {

                              
                                if (shim >= 500)
                                {
                                    shim = shim - 500;
                                    count0point5microm++;
                                    continue;
                                }
                                if (shim >= 200)
                                {
                                    shim = shim - 200;
                                    count0point2microm++;
                                    continue;
                                }


                                if (shim >= 50 || shim >= 25)
                                {
                                    shim = shim - 500;
                                    count0point05microm++;
                                    continue;
                                }
                            }

                            string MicroM0 = "0.5 mm :";
                            string MicroM1 = "0.2 mm :";
                            string MicroM2 = "0.05 mm :";
                           

                            LB_DrivingTotal_shims.Items.Clear();

                            if (count0point5microm != 0)
                                LB_DrivingTotal_shims.Items.Add(MicroM0 + count0point5microm.ToString());
                            if (count0point2microm != 0)
                                LB_DrivingTotal_shims.Items.Add(MicroM1 + count0point2microm.ToString());
                            if (count0point05microm != 0)
                                LB_DrivingTotal_shims.Items.Add(MicroM2 + count0point05microm.ToString());
                            

                        
                    }
                    catch { }
                }
            }
            catch
            {

            }
        }

        private void txt_dailgaugereading_page2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txt_dailgaugereadingDriving_page2.Text == "")
                {
                    return;
                }
                else
                {
                    txt_dialogreadingDriven_page2.Focus();
                }
            }
        }

        private void txt_dailgaugereading_page2_Validated(object sender, EventArgs e)
        {

        }

        private void txt_dailgaugereading_page2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar))
  && (e.KeyChar != '.') && (e.KeyChar != '-'))
                e.Handled = true;

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;

            // only allow minus sign at the beginning
            if (e.KeyChar == '-' && (sender as TextBox).Text.Length > 0)
                e.Handled = true;
        }

        private void grpb_checkmaster_Enter(object sender, EventArgs e)
        {

        }

        private void txt_ShimRequiredDriving_page2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_shimsRequiredDriven_page2_TextChanged(object sender, EventArgs e)
        {

        }

       

        private void txt_DialGaugeReadingDriven_2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bttn_next.Focus();
            }
        }

        private void txt_masterValueDriving_page2_TextChanged(object sender, EventArgs e)
        {

        }

        private void LB_DrivingTotal_shims_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lbl_Driventotal_shims_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox_shimMeasurementDriven_Enter(object sender, EventArgs e)
        {

        }

        private void txt_MasterValueDriven_2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_masterValueDriving_page1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void txt_shimsRequiredDrivenVerify_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbl_dialgaugereading_Click(object sender, EventArgs e)
        {

        }

        private void txt_dailgaugereading_Driving_TextChanged(object sender, EventArgs e)
        {
            DrivenGaugereadeingCheckDriven();
        }

        private void bttn_DialGaugeReadingDriving_Click(object sender, EventArgs e)
        {

            shim = Math.Abs(Convert.ToDouble(txt_shimsRequiredDriven_page2.Text));
            if (((Convert.ToDouble(txt_shimsRequiredDriven_page2.Text) * 1000) - (Convert.ToDouble(txt_dailgaugereading_Driving.Text) * 1000)) < Convert.ToDouble(shim_verification_tolerence) * 1000)
            {
                Verified.Visible = true;
                NotVerifiedDriven.Visible = false;

            }
            else
            {
                Verified.Visible = false;
                NotVerifiedDriven.Visible = true;
            }
        }

        private void lbl_VerifyResultDriving_Click(object sender, EventArgs e)
        {

        }

        private void NotVerified_Click(object sender, EventArgs e)
        {

        }

        private void txt_dailgaugereading_driving_page3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                System.Threading.Thread.Sleep(5000);
                if (Verified.Visible == true)
                {
                    if (VerifyAll())
                    {
                        string total_shim_required = "", shim_required_driving = "";
                        foreach (var item in LB_DrivingTotal_shims.Items)
                        {
                            total_shim_required += item + "  ";
                        }
                        FillGrid();
                        Masterresetall();
                        MessageBox.Show("Data Saved");
                        this.PtoMeasurement.SelectedTab = MasterTab;
                        txt_dailgaugereading_Driving.Focus();
                        getmodel();
                    }
                    else
                    {
                        MessageBox.Show("Verify All values");
                    }
                }
            }









        }

        private void bttn_checkall_Click_1(object sender, EventArgs e)
        {

        }

        private void Verified_Click(object sender, EventArgs e)
        {

        }

        private void dgv_PTOMesurement_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void NotVerifiedDriven_Click(object sender, EventArgs e)
        {

        }

        private void txt_DialGaugeReading2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDouble(txt_DialGaugeReadingDriven_2.Text) == 0)
                {
                    
                    txt_MasterValueDriven_2.Text = master_value1; // "4.512"
                    txt_DialGaugeReadingDriven_2.Text = "0.000";
                }
               
            }
            catch
            {
                if (txt_dailgaugereadingDriving_page1.Text != "")
                {
                    MessageBox.Show("Please Enter Zero Value from Dial Gauage");
                }
            }
        }


        private void cmb_selectmodel_KeyDown(object sender, KeyEventArgs e)

        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_dailgaugereadingDriving_page1.Focus();
            }
        }

    }
}
