using System;
using System.Threading;
using WintabDN;
using System.Windows.Forms;

namespace Paint_Program
{
    class TabletInfo
    {

        private CWintabContext m_logContext = null;
        private CWintabData m_wtData = null;
        private UInt32 m_maxPkts = 1;   // max num pkts to capture/display at a time

        private Int32 m_pkX = 0;
        private Int32 m_pkY = 0;
        private UInt32 m_pressure = 0;
        private UInt32 m_pkTime = 0;
        private UInt32 m_pkTimeLast = 0;

        // These constants can be used to force Wintab X/Y data to map into a
        // a 10000 x 10000 grid, as an example of mapping tablet data to values
        // that make sense for your application.
        private const Int32 m_TABEXTX = 10000;
        private const Int32 m_TABEXTY = 10000;

        public TabletInfo(EventHandler<MessageReceivedEventArgs> handler)
        {
            InitDataCapture(handler);
        }

        public CWintabData getWintabData()
        {
            return m_wtData;
        }

        public UInt32 getMaxPackets()
        {
            return m_maxPkts;
        }

        private void Test_IsWintabAvailable()
        {
            if (CWintabInfo.IsWintabAvailable())
            {
                Console.WriteLine("Wintab was found!\n");
            }
            else
            {
                Console.WriteLine("Wintab was not found!\nCheck to see if tablet driver service is running.\n");
            }
        }

        ///////////////////////////////////////////////////////////////////////
        private void Test_GetDeviceInfo()
        {
            //Console.WriteLine("DeviceInfo: " + CWintabInfo.GetDeviceInfo() + "\n");
            string devInfo = CWintabInfo.GetDeviceInfo();
            Console.WriteLine("DeviceInfo: " + devInfo + "\n");
        }


        ///////////////////////////////////////////////////////////////////////
        private void Test_GetDefaultDigitizingContext()
        {
            CWintabContext context = CWintabInfo.GetDefaultDigitizingContext();

            Console.WriteLine("Default Digitizing Context:\n");
            Console.WriteLine("\tSysOrgX, SysOrgY, SysExtX, SysExtY\t[" +
                context.SysOrgX + "," + context.SysOrgY + "," +
                context.SysExtX + "," + context.SysExtY + "]\n");

            Console.WriteLine("\tInOrgX, InOrgY, InExtX, InExtY\t[" +
                context.InOrgX + "," + context.InOrgY + "," +
                context.InExtX + "," + context.InExtY + "]\n");

            Console.WriteLine("\tOutOrgX, OutOrgY, OutExtX, OutExt\t[" +
                context.OutOrgX + "," + context.OutOrgY + "," +
                context.OutExtX + "," + context.OutExtY + "]\n");
        }

        ///////////////////////////////////////////////////////////////////////
        private void Test_GetDefaultSystemContext()
        {
            CWintabContext context = CWintabInfo.GetDefaultSystemContext();

            Console.WriteLine("Default System Context:\n");
            Console.WriteLine("\tSysOrgX, SysOrgY, SysExtX, SysExtY\t[" +
                context.SysOrgX + "," + context.SysOrgY + "," +
                context.SysExtX + "," + context.SysExtY + "]\n");

            Console.WriteLine("\tInOrgX, InOrgY, InExtX, InExtY\t[" +
                context.InOrgX + "," + context.InOrgY + "," +
                context.InExtX + "," + context.InExtY + "]\n");

            Console.WriteLine("\tOutOrgX, OutOrgY, OutExtX, OutExt\t[" +
                context.OutOrgX + "," + context.OutOrgY + "," +
                context.OutExtX + "," + context.OutExtY + "]\n");
        }

        ///////////////////////////////////////////////////////////////////////
        private void Test_GetDefaultDeviceIndex()
        {
            Int32 devIndex = CWintabInfo.GetDefaultDeviceIndex();

            Console.WriteLine("Default device index is: " + devIndex + (devIndex == -1 ? " (virtual device)\n" : "\n"));
        }

        ///////////////////////////////////////////////////////////////////////
        private void Test_GetDeviceAxis()
        {
            WintabAxis axis;

            // Get virtual device axis for X, Y and Z.
            axis = CWintabInfo.GetDeviceAxis(-1, EAxisDimension.AXIS_X);

            Console.WriteLine("Device axis X for virtual device:\n");
            Console.WriteLine("\taxMin, axMax, axUnits, axResolution: " + axis.axMin + "," + axis.axMax + "," + axis.axUnits + "," + axis.axResolution.ToString() + "\n");

            axis = CWintabInfo.GetDeviceAxis(-1, EAxisDimension.AXIS_Y);
            Console.WriteLine("Device axis Y for virtual device:\n");
            Console.WriteLine("\taxMin, axMax, axUnits, axResolution: " + axis.axMin + "," + axis.axMax + "," + axis.axUnits + "," + axis.axResolution.ToString() + "\n");

            axis = CWintabInfo.GetDeviceAxis(-1, EAxisDimension.AXIS_Z);
            Console.WriteLine("Device axis Z for virtual device:\n");
            Console.WriteLine("\taxMin, axMax, axUnits, axResolution: " + axis.axMin + "," + axis.axMax + "," + axis.axUnits + "," + axis.axResolution.ToString() + "\n");
        }

        ///////////////////////////////////////////////////////////////////////
        private void Test_GetDeviceOrientation()
        {
            bool tiltSupported = false;
            WintabAxisArray axisArray = CWintabInfo.GetDeviceOrientation(out tiltSupported);
            Console.WriteLine("Device orientation:\n");
            Console.WriteLine("\ttilt supported for current tablet: " + (tiltSupported ? "YES\n" : "NO\n"));

            if (tiltSupported)
            {
                for (int idx = 0; idx < axisArray.array.Length; idx++)
                {
                    Console.WriteLine("\t[" + idx + "] axMin, axMax, axResolution, axUnits: " +
                        axisArray.array[idx].axMin + "," +
                        axisArray.array[idx].axMax + "," +
                        axisArray.array[idx].axResolution + "," +
                        axisArray.array[idx].axUnits + "\n");
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////
        private void Test_GetDeviceRotation()
        {
            bool rotationSupported = false;
            WintabAxisArray axisArray = CWintabInfo.GetDeviceRotation(out rotationSupported);
            Console.WriteLine("Device rotation:\n");
            Console.WriteLine("\trotation supported for current tablet: " + (rotationSupported ? "YES\n" : "NO\n"));

            if (rotationSupported)
            {
                for (int idx = 0; idx < axisArray.array.Length; idx++)
                {
                    Console.WriteLine("\t[" + idx + "] axMin, axMax, axResolution, axUnits: " +
                        axisArray.array[idx].axMin + "," +
                        axisArray.array[idx].axMax + "," +
                        axisArray.array[idx].axResolution + "," +
                        axisArray.array[idx].axUnits + "\n");
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////
        private UInt32 Test_GetNumberOfDevices()
        {
            UInt32 numDevices = CWintabInfo.GetNumberOfDevices();
            Console.WriteLine("Number of tablets connected: " + numDevices + "\n");
            return numDevices;
        }


        ///////////////////////////////////////////////////////////////////////
        private void Test_IsStylusActive()
        {
            bool isStylusActive = CWintabInfo.IsStylusActive();
            Console.WriteLine("Is stylus active: " + (isStylusActive ? "YES\n" : "NO\n"));
        }


        ///////////////////////////////////////////////////////////////////////
        private void Test_GetStylusName()
        {
            Console.WriteLine("Stylus name (puck):   " + CWintabInfo.GetStylusName(EWTICursorNameIndex.CSR_NAME_PUCK) + "\n");
            Console.WriteLine("Stylus name (pen):    " + CWintabInfo.GetStylusName(EWTICursorNameIndex.CSR_NAME_PRESSURE_STYLUS) + "\n");
            Console.WriteLine("Stylus name (eraser): " + CWintabInfo.GetStylusName(EWTICursorNameIndex.CSR_NAME_ERASER) + "\n");
        }

        ///////////////////////////////////////////////////////////////////////
        private void Test_GetExtensionMask()
        {
            Console.WriteLine("Extension touchring mask:   0x" + CWintabExtensions.GetWTExtensionMask(EWTXExtensionTag.WTX_TOUCHRING).ToString("x") + "\n");
            Console.WriteLine("Extension touchstring mask: 0x" + CWintabExtensions.GetWTExtensionMask(EWTXExtensionTag.WTX_TOUCHSTRIP).ToString("x") + "\n");
            Console.WriteLine("Extension express key mask: 0x" + CWintabExtensions.GetWTExtensionMask(EWTXExtensionTag.WTX_EXPKEYS2).ToString("x") + "\n");
        }
        
        ///////////////////////////////////////////////////////////////////////
        private void Test_MaxPressure()
        {
            Console.WriteLine("Max normal pressure is: " + CWintabInfo.GetMaxPressure() + "\n");
            Console.WriteLine("Max tangential pressure is: " + CWintabInfo.GetMaxPressure(false) + "\n");
        }

        private void InitDataCapture(EventHandler<MessageReceivedEventArgs> handler, int ctxWidth_I = m_TABEXTX, int ctxHeight_I = m_TABEXTY, bool ctrlSysCursor_I = true)
        {
            try
            {
                // Close context from any previous test.
                CloseCurrentContext();

                Console.WriteLine("Opening context...\n");

                m_logContext = OpenTestSystemContext(ctxWidth_I, ctxHeight_I, ctrlSysCursor_I);

                if (m_logContext == null)
                {
                    Console.WriteLine("Test_DataPacketQueueSize: FAILED OpenTestSystemContext - bailing out...\n");
                    return;
                }

                // Create a data object and set its WT_PACKET handler.
                m_wtData = new CWintabData(m_logContext);
                m_wtData.SetWTPacketEventHandler(handler);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(new Form() { WindowState = FormWindowState.Maximized, TopMost = true }, ex.ToString());
            }
        }

        private CWintabContext OpenTestSystemContext(int width_I = m_TABEXTX, int height_I = m_TABEXTY, bool ctrlSysCursor = true)
        {
            bool status = false;
            CWintabContext logContext = null;

            try
            {
                // Get the default system context.
                // Default is to receive data events.
                //logContext = CWintabInfo.GetDefaultDigitizingContext(ECTXOptionValues.CXO_MESSAGES);
                logContext = CWintabInfo.GetDefaultSystemContext(ECTXOptionValues.CXO_MESSAGES);

                // Set system cursor if caller wants it.
                if (ctrlSysCursor)
                {
                    logContext.Options |= (uint)ECTXOptionValues.CXO_SYSTEM;
                }
                else
                {
                    logContext.Options &= ~(uint)ECTXOptionValues.CXO_SYSTEM;
                }

                if (logContext == null)
                {
                    Console.WriteLine("FAILED to get default digitizing context.\n");
                    return null;
                }

                // Modify the digitizing region.
                logContext.Name = "WintabDN Event Data Context";

                WintabAxis tabletX = CWintabInfo.GetTabletAxis(EAxisDimension.AXIS_X);
                WintabAxis tabletY = CWintabInfo.GetTabletAxis(EAxisDimension.AXIS_Y);

                logContext.InOrgX = 0;
                logContext.InOrgY = 0;
                logContext.InExtX = tabletX.axMax;
                logContext.InExtY = tabletY.axMax;

                // SetSystemExtents() is (almost) a NO-OP redundant if you opened a system context.
                SetSystemExtents(ref logContext);

                // Open the context, which will also tell Wintab to send data packets.
                status = logContext.Open();

                Console.WriteLine("Context Open: " + (status ? "PASSED [ctx=" + logContext.HCtx + "]" : "FAILED") + "\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("OpenTestDigitizerContext ERROR: " + ex.ToString());
            }

            return logContext;
        }

        private void SetSystemExtents(ref CWintabContext logContext)
        {
            //TODO Rectangle rect = new Rectangle(int.MaxValue, int.MaxValue, int.MinValue, int.MinValue);

            //TODO foreach (Screen screen in Screen.AllScreens)
            //TODO    rect = Rectangle.Union(rect, screen.Bounds);

            //TODO logContext.OutOrgX = rect.Left;
            //TODO logContext.OutOrgY = rect.Top;
            //TODO logContext.OutExtX = rect.Width;
            //TODO logContext.OutExtY = rect.Height;

            // In Wintab, the tablet origin is lower left.  Move origin to upper left
            // so that it coincides with screen origin.
            logContext.OutExtY = -logContext.OutExtY;
        }

        private void CloseCurrentContext()
        {
            try
            {
                Console.WriteLine("Closing context...\n");
                if (m_logContext != null)
                {
                    m_logContext.Close();
                    m_logContext = null;
                    m_wtData = null;
                }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(new Form() { WindowState = FormWindowState.Maximized, TopMost = true }, ex.ToString());
            }
        }
    }
}
