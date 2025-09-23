using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingApplication.Helper
{
    public class WeighingScaleReader
    {
        private SerialPort _serialPort;
        private int _weighScaleType; // 0 = Old, 1 = Unique Instruments, 2/3 = JISL

        public WeighingScaleReader(string portName, int baudRate = 9600, Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.One)
        {
            _serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
            _serialPort.Open();
        }

        /// <summary>
        /// Reads weight data from COM port
        /// </summary>
        /// <returns>Formatted weight string</returns>
        public string ReadDataFromCOMPort()
        {
            if (_serialPort == null || !_serialPort.IsOpen)
                throw new InvalidOperationException("Serial port is not open.");

            // Clear any existing buffer
            _serialPort.DiscardInBuffer();

            // Read available data
            string inputData = _serialPort.ReadExisting();

            if (string.IsNullOrEmpty(inputData))
                return string.Empty;

            string result = string.Empty;

            switch (_weighScaleType)
            {
                case 0: // Old
                    int idx0 = inputData.IndexOf(" 0");
                    if (idx0 >= 0 && idx0 + 1 + 7 <= inputData.Length)
                        result = inputData.Substring(idx0 + 1, 7);
                    break;

                case 1: // Unique Instruments
                    result = inputData.Length >= 9 ? inputData.Substring(0, 9) : inputData;
                    break;

                case 2: // New - JISL (9600)
                case 3: // New - JISL (2400)
                    int idxPlus = inputData.IndexOf("+");
                    if (idxPlus >= 0 && idxPlus + 1 + 11 <= inputData.Length)
                        result = inputData.Substring(idxPlus + 1, 11);
                    break;

                default:
                    result = inputData;
                    break;
            }

            return result.Trim();
        }

        public void SetWeighScaleType(int type)
        {
            _weighScaleType = type;
        }

        public void Close()
        {
            if (_serialPort != null && _serialPort.IsOpen)
                _serialPort.Close();
        }
    }

    //To call this function below code is used- 
    //    var reader = new WeighingScaleReader("COM3", 9600);
    //    reader.SetWeighScaleType(0); // Old type scale

    //    string weight = reader.ReadDataFromCOMPort();
    //    Console.WriteLine("Weight: " + weight);

    //    reader.Close();
}
