using HelpdeskDAL;
using System;
using System.Diagnostics;

namespace HelpdeskViewModels
{
    public class ViewModelUtils
    {
        public static void ErrorRoutine(Exception e, string obj, string method)
        {
            if (e.InnerException != null)
            {
                Trace.WriteLine("Error in ViewModels, object=" + obj +
                    ", method =" + method + ", inner exceptipon message="
                    + e.InnerException.Message);
                throw e.InnerException;
            }
            else
            {
                Trace.WriteLine("Error in ViewModels, object=" + obj +
                    ", method =" + method + ", inner exceptipon message="
                    + e.Message);
                throw e;
            }
        }

        public bool LoadCollection()
        {
            bool createOk = false;

            try
            {
                DALUtils dalUtil = new DALUtils();
                createOk = dalUtil.LoadCollections();
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "ViewModelUtils", "LoadCollections");
            }
            return createOk;
        }
    }
}
