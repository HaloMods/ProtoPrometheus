/* ---------------------------------------------------------------
 * Prometheus
 * Bungie Map-based Multi-Game Editing Kit
 * 2004-2005, Halo-Dev
 * ---------------------------------------------------------------
 * Class       : Prometheus.BetaGuardian
 * Description : Provides a means to verify that the user trying
 *             : to run the app is authorized (based on a
 *             : compiled table of authorized hashes)
 * Author      : Grenadiac
 * Co-Authors  : MonoxideC
 * ---------------------------------------------------------------
 */

using System;
using System.Management;
using Microsoft.Win32;
using System.Security.Cryptography;
using System.Collections;

namespace Prometheus
{
	/// <summary>
	/// Summary description for BetaGuardian.
	/// </summary>
	public class BetaGuardian 
	{
		private ArrayList m_MacLookupTable;
		public BetaGuardian() 
		{
			m_MacLookupTable = new ArrayList();
			m_MacLookupTable.Add("4d42fedff53d6d2abd0fdcaa5aaf71b");   // MonoxideC
			m_MacLookupTable.Add("32c8deb1aeb237e1ea86a2e127e44bf9");   // MonoxideC #2
			m_MacLookupTable.Add("e648d55e4cb8eb5b166522403a99e626");  // Grenadiac
		}
		public static string GetMac()
		{
			ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
			ManagementObjectCollection moc = mc.GetInstances();
			foreach(ManagementObject mo in moc) 
				if((bool)mo["IPEnabled"] == true) return mo["MacAddress"].ToString();
			return "NOMACSFOUND";
		}
		
		// Generate a unique key based on the user's MAC address and WinNT ProductID
		public static string GetUniqueKey()
		{
			// Get our two keys that will be hashed together
			string productID = (string)(Registry.LocalMachine.OpenSubKey(
				@"SOFTWARE\Microsoft\Windows NT\CurrentVersion").GetValue("ProductID"));
			string mac = GetMac();
			
			// Convert string data to byte array
			byte[] data = System.Text.Encoding.Default.GetBytes(productID + mac);
			
			// Create an MD5 hash
			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] hash = md5.ComputeHash(data);

			string result = String.Empty;
			for (int x=0; x<hash.Length; x++) {
				result += Convert.ToString(hash[x], 16);
			}
			return result;
		}

		// Check the lookup table for a given key
		public bool DoesUserHaveBetaAccess(string key) {
			return (m_MacLookupTable.Contains(key));
		}
	}
}
