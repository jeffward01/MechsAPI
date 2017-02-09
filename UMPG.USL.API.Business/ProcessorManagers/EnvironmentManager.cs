using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.ProcessorModel;

namespace UMPG.USL.API.Business.ProcessorManagers
{
    public class EnvironmentManager : IEnvironmentManager
    {
        public void SetEnvironmentInformation()
        {
            
            var machineName = Environment.MachineName;
            if (_isUATEnvironment(machineName))
            {
                //Set Environment Info
            }
            else if(_isPRODEnvironment(machineName))
            {
                //Set Environment Info
            }
            else if (_isQAEnvironment(machineName))
            {   //Set Environment Info

            }
            else if (_isDEVEnvironment(machineName))
            {
                //Set Environment Info
            }
            else
            {
                EnvironmentInformation.EnvironmentName = machineName;
                EnvironmentInformation.EnvironmentType = "Local";
                EnvironmentInformation.SisterEnvironmentName = "N/A";
            }
        }

        private bool _isDEVEnvironment(string machineName)
        {
            if (machineName == DevEnvironmentInformation.Server01)
            {
                EnvironmentInformation.EnvironmentType = "DEV";
                EnvironmentInformation.EnvironmentName = machineName;
                EnvironmentInformation.SisterEnvironmentName = "N/A";
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool _isQAEnvironment(string machineName)
        {
            if (machineName == QaEnvironmentInformation.Server01)
            {
                EnvironmentInformation.EnvironmentType = "QA";
                EnvironmentInformation.EnvironmentName = machineName;
                EnvironmentInformation.SisterEnvironmentName = "N/A";
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool _isPRODEnvironment(string machineName)
        {
            if (machineName == ProdEnvironmentInformation.Server01)
            {
                EnvironmentInformation.EnvironmentType = "PROD";
                EnvironmentInformation.EnvironmentName = machineName;
                EnvironmentInformation.SisterEnvironmentName = ProdEnvironmentInformation.Server02;
                return true;
            }

            if (machineName == ProdEnvironmentInformation.Server02)
            {
                {
                    EnvironmentInformation.EnvironmentType = "PROD";
                    EnvironmentInformation.EnvironmentName = machineName;
                    EnvironmentInformation.SisterEnvironmentName = ProdEnvironmentInformation.Server01;
                    return true;
                }
            }

            else
            {
                return false;
            }
        }
        private bool _isUATEnvironment(string machineName)
        {
            if (machineName == UatEnvironmentInformation.Server01)
            {
                EnvironmentInformation.EnvironmentType = "UAT";
                EnvironmentInformation.EnvironmentName = machineName;
                EnvironmentInformation.SisterEnvironmentName = UatEnvironmentInformation.Server02;
                return true;
            }
            if (machineName == UatEnvironmentInformation.Server02)
            {
                EnvironmentInformation.EnvironmentType = "UAT";
                EnvironmentInformation.EnvironmentName = machineName;
                EnvironmentInformation.SisterEnvironmentName = UatEnvironmentInformation.Server01;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
