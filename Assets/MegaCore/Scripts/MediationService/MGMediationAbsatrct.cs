using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MegaCore.MediationService
{
    public class MGMediationAbsatrct : MonoBehaviour
    {
        public string _adUnitId;

        public string _adUnitIdAndroid;
        public string _adUnitIdIOS;

        protected int _retryAttempt = 0;
    }

}
