using System;
using System.IO;
using System.Media;
using System.Security.Cryptography;
using System.Windows.Forms;
using SystemStuff.Properties;
using Microsoft.Win32;

namespace SystemStuff
{
    internal class EventListener
    {
        public delegate void LogEntryResponse(String data);

        private Boolean _bound;
        private Boolean _runningOnBattery;

        public EventListener()
        {
            _bound = false;
        }

        public event LogEntryResponse EventHappened;

        ~EventListener()
        {
            if (_bound)
            {
                SystemEvents.PowerModeChanged -= SystemEvents_PowerModeChanged;
                _bound = false;
            }
        }

        public void Start()
        {
            StartPower();
            StartSession();
        }

        private void StartSession()
        {
            SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;
        }
        private void StartPower()
        {

            var status = SystemInformation.PowerStatus.BatteryChargeStatus;

            // No battery? No operations here
            if (status == BatteryChargeStatus.NoSystemBattery)
            {
                Log("Not a valid computer for power watch");
                return;
            }

            Log("Now monitoring power supply stuff");

            _runningOnBattery = IsRunningOnBattery();

            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;
            _bound = true;
        }

        private static bool IsRunningOnBattery()
        {
            return SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Offline;
        }

        private void PlaySound(Stream data)
        {
            try
            {
                var sndPlayer = new SoundPlayer(data);
                sndPlayer.Play();
            }
            catch
            {
                // Do nothing on failure
            }
        }

        private void Log(String data)
        {
            if (EventHappened != null)
                EventHappened(data);
        }

        void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if (e.Reason != SessionSwitchReason.SessionUnlock)
                return;

            var random = new Random();
            Stream stream;
            switch (random.Next(1, 6))
            {
                case 1:
                    stream = Resources.hello_1;
                    break;
                case 2:
                    stream = Resources.hello_2;
                    break;
                case 3:
                    stream = Resources.hello_3;
                    break;
                case 4:
                    stream = Resources.hello_4;
                    break;
                default:
                    stream = Resources.hello_5;
                    break;
            }
            PlaySound(stream);
        }

        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            Log("Mode change: " + e.Mode);

            if (e.Mode != PowerModes.StatusChange)
            {
                Log("Not a status change");
                return;
            }

            if (_runningOnBattery == IsRunningOnBattery())
            {
                Log("No change in battery");
                return;
            }

            var zero = _runningOnBattery ? "powerline" : "battery";
            var one = !_runningOnBattery ? "powerline" : "battery";

            Log(String.Format("Changed from {0} to {1}", zero, one));

            _runningOnBattery = IsRunningOnBattery();

            if (_runningOnBattery)
            {
                // Play unhook sound
                PlaySound(Resources.offline);
            }
            else
            {
                // Play hook sound
                PlaySound(Resources.online);
            }
        }
    }
}