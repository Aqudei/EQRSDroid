using System;

namespace EQRSDroid.Model
{
    public interface IDataService
    {
        void GetData(Action<DataItem, Exception> callback);
    }
}