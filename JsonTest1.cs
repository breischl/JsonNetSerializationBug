using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace JsonNetTest
{
	[TestFixture]
	public class JsonTest1
	{
		[Test]
		public void Run()
		{
			var foo = new Foo()
			{
				myBar = new Bar()
				{
					myBaz = null
				}
			};

			var json = JsonConvert.SerializeObject(foo);
			Assert.IsNotNull(json);

			foo.myBar.myBaz = new Baz[] { null };
			json = JsonConvert.SerializeObject(foo);
			Assert.IsNotNull(json);

			foo.myBar.myBaz = new Baz[] { new Baz() { name = null } };
			json = JsonConvert.SerializeObject(foo);
			Assert.IsNotNull(json);
		}
	}
}
