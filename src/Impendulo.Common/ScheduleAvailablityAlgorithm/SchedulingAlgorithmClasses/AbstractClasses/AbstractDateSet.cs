using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Impendulo.Common.ScheduleAvailablityAlgorithm
{
    public abstract class AbstractPeriod : IPeriod
    {
        public AbstractPeriod()
        {

        }
        private DateTime _EndDate;
        private DateTime _StartDate;
        private int _PeriodID;
        private string _Description;

        public virtual DateTime EndDate
        {
            get
            {
                return this._EndDate;
            }
            
        }

        public virtual DateTime StartDate
        {
            get
            {
                return this._StartDate;
            }
            
        }

        public virtual int PeriodID
        {
            get
            {
                return this._PeriodID;
            }
            
        }
        public virtual string Description
        {
            get
            {
                return this._Description;
            }
            
        }

        public virtual int GetAmountOfDaysInPeriod()
        {
            int Rtn;
            Rtn = EndDate.Date.Subtract(StartDate.Date).Days;
            return Rtn;
        }

        public AbstractPeriod(DateTime StartDate, DateTime EndDate, int PeriodID, string Description)
        {
            this._StartDate = StartDate;
            this._EndDate = EndDate;
            this._PeriodID = PeriodID;
            this._Description = Description;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~AbstractPeriod() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}