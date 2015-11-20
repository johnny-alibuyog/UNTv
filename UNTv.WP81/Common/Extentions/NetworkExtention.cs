using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;

namespace UNTv.WP81.Common.Extentions
{
    public static class NetworkExtention
    {
        public static bool HasInternetConnection()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
                return false;

            var connection = NetworkInformation.GetInternetConnectionProfile().GetNetworkConnectivityLevel();
            if (connection == NetworkConnectivityLevel.LocalAccess)
                return false;

            if (connection == NetworkConnectivityLevel.None)
                return false;

            return true;
        }
    }
}
