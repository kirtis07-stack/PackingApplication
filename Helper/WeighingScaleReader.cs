using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PackingApplication.Helper
{
    public class WeighingItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class WeighingScaleReader
    {
        public string ReadWeight(string portName, int scaleType, int baudRate = 9600, int timeoutMs = 0)
        {
            string weight = string.Empty;
            using (SerialPort serialPort = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One))
            {
                AutoResetEvent dataReceivedEvent = new AutoResetEvent(false);

                serialPort.DataReceived += (sender, e) =>
                {
                    try
                    {
                        string inputData = serialPort.ReadExisting();
                        if (!string.IsNullOrEmpty(inputData))
                        {
                            string formattedWeight = ParseWeight(inputData, scaleType);
                            if (!string.IsNullOrEmpty(formattedWeight))
                            {
                                weight = formattedWeight;
                                dataReceivedEvent.Set(); // signal that data is ready
                            }
                        }
                    }
                    catch
                    {
                        // Handle any parsing/reading error here
                    }
                };

                serialPort.Open();

                // Wait until weight is read or timeout
                if (timeoutMs > 0)
                {
                    dataReceivedEvent.WaitOne(timeoutMs);
                }
                else
                {
                    dataReceivedEvent.WaitOne(); // wait indefinitely
                }

                serialPort.Close();
            }

            return weight;
        }

        private static string ParseWeight(string inputData, int scaleType)
        {
            if (string.IsNullOrEmpty(inputData))
                return string.Empty;

            string result = string.Empty;

            switch (scaleType)
            {
                case 0: // Old
                    int idx0 = inputData.IndexOf(" 0");
                    if (idx0 >= 0 && idx0 + 1 + 7 <= inputData.Length)
                        result = inputData.Substring(idx0 + 1, 7);
                    break;

                case 1: // Unique Instruments
                    result = inputData.Length >= 9 ? inputData.Substring(0, 9) : inputData;
                    break;

                case 2: // JISL (9600)
                case 3: // JISL (2400)
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
    }

}
