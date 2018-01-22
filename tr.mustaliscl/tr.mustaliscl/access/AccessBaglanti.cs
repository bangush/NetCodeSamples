using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Data;
using System.Collections;

namespace tr.mustaliscl.Access
{
    public sealed class AccessBaglanti
    {

        private System.Data.OleDb.OleDbConnection bglnti = null;
        private System.Data.OleDb.OleDbCommand cmd = null;
        OleDbDataReader dR = null;
        public const string Provider4 = "Provider=Microsoft.Jet.Oledb.4.0;";

        public const string Provider12="Provider=Microsoft.ACE.OLEDB.12.0;";

        private string _dataSource = null, _user_id = null, _sifre = null;

        public AccessBaglanti(String VeriKaynagi,String Kullanici, String Sifre) {
            _dataSource = VeriKaynagi;
            _user_id = Kullanici;
            _sifre = Sifre;
            Olustur();
        }

        public AccessBaglanti(String VeriKaynagi):this(VeriKaynagi,null,null) { }

        public AccessBaglanti():this(null,null,null) { }

        public string Kullanici
        {
            get { return _user_id; }
            set { _user_id = value; }
        }

        public string Sifre
        {
            get { return _sifre; }
            set { _sifre = value; }
        }

        public string VeriKaynagi
        {
            get { return _dataSource; }
            set { _dataSource = value; }
        }

        public override string ToString()
        {
            return BaglantiMetni();
        }

        public string BaglantiMetni()
        {
            StringBuilder sB = new StringBuilder();
            sB.Append(Provider12);
            if (_dataSource != null)
                sB.AppendFormat("Data Source={0};", _dataSource);
            if (_user_id != null)
                sB.AppendFormat("User Id={0};", _user_id);
            if (_sifre != null)
                sB.AppendFormat("Jet OLEDB:Database Password={0};", _sifre);
           
            return sB.ToString();
        }

      

        public void Olustur()
        {
            try
            {
                bglnti = new OleDbConnection(BaglantiMetni());
            }
            catch (OleDbException ole)
            {
                MessageBox.Show(ole.Message);
            }
            catch (Exception le)
            {
                MessageBox.Show(le.Message);
            }
            return;
        }
        public void Kontrol() { 
            try{
                if (bglnti != null)
                {
                    if (bglnti.State == ConnectionState.Closed)
                    {
                        bglnti.Open();
                    }
                }
                else { Olustur(); }
            }
            catch(OleDbException ole){
            MessageBox.Show(ole.Message);
            }catch(Exception e){
            MessageBox.Show(e.Message);
            }
        }
         
        public Object[,] SorgulaDizi(String cmdSorgu) {
            Object[,] d=null;
            try{
                DataSet dS = SorgulaDataSet(cmdSorgu);
                DataTable dT = dS.Tables[0];
                d=new Object[dT.Rows.Count,dT.Columns.Count];
                for (int i = 0; i < dT.Rows.Count; i++)
                {
                    for (int j = 0; j < dT.Columns.Count; j++)
                    {
                        d[i, j] = dT.Rows[i][j];
                    }
                    
                }
            }
            catch (OleDbException ole)
            {
                MessageBox.Show(ole.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally { bglnti.Close(); }
            return d;
        }
         
      

        public DataSet SorgulaDataSet(string cmdSorgu) {
            Kontrol();
            DataSet dS = null;
            try
            {
                dS=new DataSet();
                (new OleDbDataAdapter(cmdSorgu, bglnti)).Fill(dS);                
            }
            catch (OleDbException ole)
            {
                MessageBox.Show(ole.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally { bglnti.Close(); }
            return dS;
        }

        public String[] SutunAdlari(String cmdSorgu) {
            String[] dizi = null;
            try
            {
                Kontrol();
                cmd = new OleDbCommand(cmdSorgu, bglnti);
                dR = cmd.ExecuteReader();
                    dizi = new String[dR.FieldCount];
                        for (int i = 0; i < dR.FieldCount; i++)
                            dizi[i] = dR.GetName(i);

                dR.Close();
                dR.Dispose();
                cmd.Dispose();
            }
            catch (OleDbException ole)
            {
                MessageBox.Show(ole.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally { bglnti.Close(); }
            return dizi;
        }


        public Byte SutunSayisi(String cmdSorgu)
        {
            Byte stn=0;
            try
            {
                Kontrol();
                stn = Convert.ToByte((new OleDbCommand(cmdSorgu, bglnti)).ExecuteReader().FieldCount);
            }
            catch (OleDbException ole)
            {
                MessageBox.Show(ole.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally { bglnti.Close(); }
            return stn;
        }

        public void Sil(String tabloAdi,String SutunAd, int id) {
            try
            {
                String cmdStr = String.Format("DELETE * FROM {0} WHERE {1}=@idm", tabloAdi, SutunAd);
                Kontrol();
                cmd = new OleDbCommand(cmdStr, bglnti);
                cmd.Parameters.Add("@idm", OleDbType.Integer).Value = id;
                int i=cmd.ExecuteNonQuery();
                MessageBox.Show(String.Format("{0} kadar satır silindi.", i), "Sonuç");                
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
            finally
            {
                cmd.Parameters.Clear();
                cmd.Dispose(); 
                bglnti.Close();
            }
        }
    }
}
