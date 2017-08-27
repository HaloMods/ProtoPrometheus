using System;

namespace Prometheus.Core
{
	/// <summary>
	/// The worlds slowest factor operation.
	/// </summary>
	public class Factorer {
		public Factorer() {
		}

		/// <summary>
		/// Find the all the possible factors for a value
		/// </summary>
		/// <param name="ValueToFactor">The integer value to be factored</param>
		/// <returns>Comma delimeted list of factors</returns>
		public static string Factor(ulong ValueToFactor) {
			if (ValueToFactor <= 0)
				throw new Exception("Only values greater than zero can be factored");

			string strRet = "";
			// The absolute slowest method I can write
			for (ulong uIndex = 1; uIndex < ValueToFactor; uIndex++) {
				double dFloatCalc = (double)ValueToFactor / (double)uIndex;
				ulong uUlongCalc = ValueToFactor / uIndex;
				if (dFloatCalc == (double)uUlongCalc) {
					if (strRet.Length == 0)
						strRet = uIndex.ToString();
					else
						strRet += ", " + uIndex.ToString();
				}
			}

			return strRet;
		}

	}
}