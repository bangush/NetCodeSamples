using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace tr.mustaliscl.data
{
    public class OledbManager : Manager
    {
        
        public DataSet GetDataSetWithGivenQuery2(string ConnStr, string query, params Param[] parameters)
        {
            try
            {

                return dBManager.GetDataSetWithGivenQuery(
                    ConnectionTypes.Access,
                    ConnStr, query, parameters);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public DataSet GiveDataSetWithGivenProcedureAndParameters(
            string ConnStr, string procedurename, params Param[] parameters)
        {
            try
            {
                return dBManager.GiveDataSetWithGivenProcedureAndParameters(
                    ConnectionTypes.Access, ConnStr, procedurename, parameters);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}
