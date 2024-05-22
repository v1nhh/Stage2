using System.Text.Json.Serialization;

namespace ItemCabinetModule.ApplicationCore.Utilities
{
    public class CabinetConfigurationConfigFile
    {
        [JsonPropertyName("bladeport")]
        public string BladePort
        {
            get; set;
        }

        [JsonPropertyName("nrblades")]
        public int NrBlades
        {
            get; set;
        }

        [JsonPropertyName("cabinettype")]
        public string CabinetType
        {
            get; set;
        }

        [JsonPropertyName("eventtype")]
        public string EventType
        {
            get; set;
        }

        [JsonPropertyName("cabinetversion")]
        public string CabinetVersion
        {
            get; set;
        }

        [JsonPropertyName("invertedlockcontrol")]
        public bool? InvertedLockControl
        {
            get; set;
        }

        [JsonPropertyName("buzzertype")]
        public string BuzzerType
        {
            get; set;
        }

        [JsonPropertyName("positions")]
        public PositionConfigFile[] Positions
        {
            get; set;
        }

        [JsonPropertyName("doors")]
        public DoorConfigFile[] Doors
        {
            get; set;
        }

        [JsonPropertyName("barcodeinp")]
        public BarcodeReaderConfigFile BarcodeInp
        {
            get; set;
        }

        [JsonPropertyName("rfidinp")]
        public RFIDReaderConfigFile RFIDInp
        {
            get; set;
        }

        [JsonPropertyName("hwbuzzer")]
        public HwBuzzerConfigFile HwBuzzer
        {
            get; set;
        }

        [JsonPropertyName("specialgpio")]
        public SpecialGPIOConfigFile SpecialGPIO
        {
            get; set;
        }
    }

    public class DoorConfigFile
    {
        //"id": "1",
        //"alias": "Main door",
        //"gpioportdoorstate": "1",
        //"closedlevel": "L",
        //"gpioportdoorcontrol": "2",
        //"unlocklevel": "H",
        //"unlockduration": "1000",

        [JsonPropertyName("id")]
        public string Id
        {
            get; set;
        }

        [JsonPropertyName("alias")]
        public string Alias
        {
            get; set;
        }

        [JsonPropertyName("gpioportdoorstate")]
        public string GPIOPortDoorState
        {
            get; set;
        }

        [JsonPropertyName("closedlevel")]
        public string ClosedLevel
        {
            get; set;
        }
        [JsonPropertyName("gpioportdoorcontrol")]
        public string GPIOPortDoorControl
        {
            get; set;
        }
        [JsonPropertyName("unlocklevel")]
        public string UnlockLevel
        {
            get; set;
        }
        [JsonPropertyName("unlockduration")]
        public string UnlockDuration
        {
            get; set;
        }
    }

    public class BarcodeReaderConfigFile
    {
        [JsonPropertyName("port")]
        public string Port
        {
            get; set;
        }

        [JsonPropertyName("commparms")]
        public string CommParms
        {
            get; set;
        }

        [JsonPropertyName("startofmsg")]
        public string StartOfMessage
        {
            get; set;
        }

        [JsonPropertyName("endofmsg")]
        public string EndOfMessage
        {
            get; set;
        }

        [JsonPropertyName("encoding")]
        public string Encoding
        {
            get; set;
        }

    }

    public class RFIDReaderConfigFile
    {
        [JsonPropertyName("port")]
        public string Port
        {
            get; set;
        }

        [JsonPropertyName("commparms")]
        public string CommParms
        {
            get; set;
        }

        [JsonPropertyName("startofmsg")]
        public string StartOfMessage
        {
            get; set;
        }

        [JsonPropertyName("endofmsg")]
        public string EndOfMessage
        {
            get; set;
        }

        [JsonPropertyName("encoding")]
        public string Encoding
        {
            get; set;
        }

    }

    public class HwBuzzerConfigFile
    {
        // "gpioport": "3",
        // "buzzlevel": "H"

        [JsonPropertyName("gpioport")]
        public string GPIOport
        {
            get; set;
        }
        [JsonPropertyName("buzzlevel")]
        public string BuzzLevel
        {
            get; set;
        }

    }


    public class WavBuzzerConfigFile
    {
        [JsonPropertyName("alarmwav")]
        public string Alarm
        {
            get; set;
        }
    }

    /*
    * "allkeyreleaseport": "25",
    "releaselevel": "H",
    "mountedport": "26",
    "mountedlevel": "H",
    "powerlossport": "27",
    "powerlosslevel": "H",
    "poweralarmoutport": "18",
    "poweralarnoutlevel": "H"
     */
    public class SpecialGPIOConfigFile
    {
        [JsonPropertyName("allkeyreleaseport")]
        public string AllKeyReleasePort
        {
            get; set;
        }

        [JsonPropertyName("releaselevel")]
        public string ReleaseLevel
        {
            get; set;
        }

        [JsonPropertyName("mountedport")]
        public string MountedPort
        {
            get; set;
        }

        [JsonPropertyName("mountedlevel")]
        public string MountedLevel
        {
            get; set;
        }

        [JsonPropertyName("powerlossport")]
        public string PowerLossPort
        {
            get; set;
        }

        [JsonPropertyName("powerlosslevel")]
        public string PowerLossLevel
        {
            get; set;
        }

        [JsonPropertyName("poweralarmoutport")]
        public string PowerAlarmOutPort
        {
            get; set;
        }

        [JsonPropertyName("poweralarmoutlevel")]
        public string PowerAlarmOutLevel
        {
            get; set;
        }

    }

    public class PositionConfigFile
    {
        [JsonPropertyName("id")]
        public string Id
        {
            get; set;
        }

        [JsonPropertyName("bladeaddr")]
        public string BladeAddr
        {
            get; set;
        }

        [JsonPropertyName("bladeposno")]
        public string BladePosNo
        {
            get; set;
        }

        [JsonPropertyName("alias")]
        public string Alias
        {
            get; set;
        }

        [JsonPropertyName("type")]
        public string Type
        {
            get; set;
        }

        [JsonPropertyName("fakesensor")]
        public string FakeSensorLevel
        {
            get; set;
        }

        [JsonPropertyName("doorid")]
        public string DoorId
        {
            get; set;
        }

        [JsonPropertyName("ledcolor")]
        public string LEDColor
        {
            get; set;
        }
    }
}