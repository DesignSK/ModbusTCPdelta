using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModbusTCPdelta_Library.Tests
{
	[TestClass()]
	public class ModbusTCPTests
	{
		[TestMethod()]
		public void GetSignIntTest()
		{
			ModbusTCP Hard = new ModbusTCP("192.168.1.1", 502, 500);

			Assert.AreEqual(Hard.GetSignInt("FFCE"), "-50");
			Assert.AreEqual(Hard.GetSignInt("0032"), "50");
			Assert.AreEqual(Hard.GetSignInt("5BA1"), "23457");
			Assert.AreEqual(Hard.GetSignInt("A45F"), "-23457");
		}

		[TestMethod()]
		public void GetHexFromIntTest()
		{
			ModbusTCP Hard = new ModbusTCP("192.168.1.1", 502, 500);
			Assert.AreEqual(Hard.GetHexFromInt(-50), 65486);
		}
	}
}