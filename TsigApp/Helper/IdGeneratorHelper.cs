using System;
using System.Threading;

namespace TsigApp.Helper
{
    internal class IdGeneratorHelper
    {
        private static readonly Lazy<IdGeneratorHelper> Lazy =
               new Lazy<IdGeneratorHelper>(() => new IdGeneratorHelper());

        public static IdGeneratorHelper Instance => Lazy.Value;

        private IdGeneratorHelper()
        {
        }

        #region THIS REGION IS VERY VERY IMPORTANT, PLEASE TAKE CARE OF IMMEDIATELY !!!

        private static IdGen.IdGenerator _gen = new IdGen.IdGenerator(AppSettings.BaseSettings.IdGenGeneratorId, AppSettings.BaseSettings.IdGenEposhUtc, AppSettings.BaseSettings.IdGenMaskConfig);

        #endregion

        //private static long _tempId;

        //public long CreateId()
        //{
        //    var id = _gen.CreateId();
        //    while (true)
        //    {
        //        if (_tempId == 0)
        //        {
        //            _tempId = id;
        //            id = _gen.CreateId();
        //        }
        //        else
        //        {
        //            if (id > _tempId)
        //            {
        //                _tempId = id;
        //                return id;
        //            }

        //            id = _gen.CreateId();
        //        }
        //    }
        //}


        private static long _tempId;

        public long CreateId()
        {            
            long oldValue, newValue;
            do
            {
                if (_tempId == 0)
                {
                    _tempId = _gen.CreateId();
                }

                oldValue = _tempId;
                newValue = _gen.CreateId();
            } while (Interlocked.CompareExchange(ref _tempId, newValue, oldValue) != oldValue);

            return newValue;
        }
    }
}
