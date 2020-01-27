using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DiskManagerApplication.DriveOperations.S.M.A.R.T
{
    public class SmartBuilder : ISmartBuilder
    {
        private ManagementObjectCollection queryCollection { get; set; }
        private Dictionary<int, DriveData> allDrivesDictionary { get; set; }
        private ConnectionService connectionService;

        private int driveIndex = 0;

        public ISmartBuilder SetScope(ManagementScope scope)
        {
            connectionService = new ConnectionService(scope);
            return this;
        }
        public ISmartBuilder SetDriveStorage()
        {
            allDrivesDictionary = new Dictionary<int, DriveData>();
            return this;
        }

        private void FindAllDrives()
        {
            driveIndex = 0;
            DriveData driveData; 

            queryCollection = connectionService.GetQueryCollectionFromWin32Class("Win32_DiskDrive");
            foreach (ManagementObject foundDrive in queryCollection)
            {
                driveData = new DriveData();
                driveData.Model = foundDrive["Model"].ToString().Trim();
                driveData.Type = foundDrive["InterfaceType"].ToString().Trim();
                if (null != foundDrive["SerialNumber"])
                    driveData.Serial = foundDrive["SerialNumber"].ToString().Trim();
                else
                    driveData.Serial = "None";
                allDrivesDictionary.Add(driveIndex, driveData);
                driveIndex++;
            }

        }

        private void SetStatusToAllDrives()
        {
            driveIndex = 0;
            queryCollection = connectionService.GetQueryCollectionFromDiskDrive("MSStorageDriver_FailurePredictStatus");
            foreach (ManagementObject foundDrive in queryCollection)
            {
                if ((bool)foundDrive.Properties["PredictFailure"].Value == false)
                        allDrivesDictionary[driveIndex].IsOK = true;
                else
                    allDrivesDictionary[driveIndex].IsOK = false;
                driveIndex++;
            }

        }

        private void SetConditionDataToDrives()
        {
            //Ściąga językowa - vendor specific jako dostawca 
            SmartObjectHelper helper;
            driveIndex = 0;
            queryCollection = connectionService.GetQueryCollectionFromDiskDrive("MSStorageDriver_FailurePredictData");

            foreach(ManagementObject drivePredictData in queryCollection)
            {
                //Wszystkie dane zakodowane są na poszczególnych pozycjach pozyskanej tablicy bitów
                Byte[] byteArray = (Byte[])drivePredictData.Properties["VendorSpecific"].Value;
                for (int i = 0; i < 30; ++i) {
                    try
                    {
                        helper = new SmartObjectHelper();

                        //Co 12 bitów zmienia się argument, a jego id znajduje się na pozycji 2
                        helper.id = byteArray[i * 12 + 2];
                        if (helper.id == 0) continue;

                        helper.flags = byteArray[i * 12 + 4]; //określa najmłodszy bit statusu, pozostała reszta jest ignorowana
                        helper.failureIsComing = (helper.flags & 0x1) == 0x1;
                        helper.value = byteArray[i * 12 + 5];
                        helper.worst = byteArray[i * 12 + 6];
                        helper.vendorData = BitConverter.ToInt32(byteArray, i * 12 + 7);

                        var currentAttribute = allDrivesDictionary[driveIndex].Attributes[helper.id];

                        currentAttribute.Current = helper.value;
                        currentAttribute.Worst = helper.worst;
                        currentAttribute.Data = helper.vendorData;
                        if (helper.failureIsComing == false)
                            currentAttribute.Status = true;
                        else
                            currentAttribute.Status = false;
                    }
                    catch
                    {
                        //Podane id nie zostało uwzględnione pośród wymienionych atrybutów (DriveData)
                    }
                }
                driveIndex++;
            }

        }

        private void SetThresholdsToDrives()
        {
            SmartObjectHelper helper;
            driveIndex = 0;
            queryCollection = connectionService.GetQueryCollectionFromDiskDrive("MSStorageDriver_FailurePredictThresholds");

            foreach (ManagementObject drivePredictData in queryCollection)
            {
                //Wszystkie dane zakodowane są na poszczególnych pozycjach pozyskanej tablicy bitów
                Byte[] byteArray = (Byte[])drivePredictData.Properties["VendorSpecific"].Value;
                for (int i = 0; i < 30; ++i)
                {
                    try
                    {
                        helper = new SmartObjectHelper();
                        helper.id = byteArray[i * 12 + 2];
                        if (helper.id == 0) continue;

                        helper.threshold = byteArray[i * 12 + 3];

                        var currentAttribute = allDrivesDictionary[driveIndex].Attributes[helper.id];

                        currentAttribute.Threshold = helper.threshold;
                    }
                    catch
                    {
                        //Podane id nie zostało uwzględnione pośród wymienionych atrybutów (DriveData)
                    }
                    
                }

                driveIndex++;
            }
        }
        private void PrintInfomation()
        {
            string[] boolAnswerDrive = { "BAD", "OK" };
            string[] boolAnswerData = { "", "OK" };
            string spaces = "";
            string tabs = "";
            int select;

            foreach (var drive in allDrivesDictionary)
            {
                select = drive.Value.IsOK ? 1 : 0;
                Console.WriteLine("-----------------------------------------------------");
                Console.WriteLine($" Dysk ({boolAnswerDrive[select]}): {drive.Value.Serial} - {drive.Value.Model} - {drive.Value.Type}");
                Console.WriteLine("-----------------------------------------------------");
                Console.WriteLine("");

                Console.WriteLine("ID                                      Current  Worst   Threshold Data   Status");
                foreach (var attr in drive.Value.Attributes)
                {
                    if (attr.Value.CheckIfHasData)
                    {
                        spaces = "";
                        for (int i = 0; i < 40 - attr.Value.Attribute.Length; i++)
                            spaces += " ";
                        select = attr.Value.Status ? 1 : 0;
                        Console.WriteLine($"{attr.Value.Attribute}{spaces}{attr.Value.Current}\t {attr.Value.Worst}\t {attr.Value.Threshold}\t   {attr.Value.Data}    {boolAnswerData[select]}");
                    }
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
            }
        }
        public void Build()
        {
            FindAllDrives();
            SetStatusToAllDrives();
            SetConditionDataToDrives();
            SetThresholdsToDrives();
            PrintInfomation();
        }


    }
  
}
