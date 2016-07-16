using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpModBusExample
{
    internal class ModBusSerialPortWrapper : ModBusWrapper, IDisposable
    {
        public override void Connect()
        {
            throw new NotImplementedException();
        }

        public override bool CoilSend(short registerAdd)
        {
            throw new NotImplementedException();
        }

        public override bool CoilWrite(short registerAdd, bool open)
        {
            throw new NotImplementedException();
        }

        public override bool CoilWriteMulit(short registerAdd, byte num, string data)
        {
            throw new NotImplementedException();
        }

        public override bool HoldRegisterRead(short registerAdd, short num)
        {
            throw new NotImplementedException();
        }

        public override bool HoldRegisterWriteMulit(short registerAdd, byte num, short data)
        {
            throw new NotImplementedException();
        }

        public override bool HoldRegisterWrite(short registerAdd, short data)
        {
            throw new NotImplementedException();
        }

        #region IDisposable 成员
        public override void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
