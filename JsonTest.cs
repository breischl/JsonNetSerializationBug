using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;

namespace JsonNetTest
{
	[TestFixture]
	public class JsonTest
	{
		[Test]
		public void Run()
		{
			var setFoo = new Foo()
			{
				myBar = new Bar()
				{
					myBaz = new Baz[] { 
						new Baz(){
						name = Guid.NewGuid().ToString()
						},
						new Baz(){
						name = Guid.NewGuid().ToString()
						},
						new Baz(){
						name = Guid.NewGuid().ToString()
						},
					}
				}
			};

			Console.Out.WriteLine("setFoo.ShouldSerializemyBar() = {0}", setFoo.ShouldSerializemyBar());
			Console.Out.WriteLine("setFoo.myBar.ShouldSerializemyBaz() = {0}", setFoo.myBar.ShouldSerializemyBaz());
			Console.Out.WriteLine("setFoo.myBar.myBaz[0].ShouldSerializename() = {0}", setFoo.myBar.myBaz[0].ShouldSerializename());
			Console.Out.WriteLine("setFoo.myBar.myBaz[1].ShouldSerializename() = {0}", setFoo.myBar.myBaz[1].ShouldSerializename());
			Console.Out.WriteLine("setFoo.myBar.myBaz[2].ShouldSerializename() = {0}", setFoo.myBar.myBaz[2].ShouldSerializename());

			var setFooJson = Serialize(setFoo);
			var deserializedSetFoo = JsonConvert.DeserializeObject<Foo>(setFooJson);

			Assert.NotNull(deserializedSetFoo.myBar);
			Assert.NotNull(deserializedSetFoo.myBar.myBaz);
			Assert.AreEqual(setFoo.myBar.myBaz.Length, deserializedSetFoo.myBar.myBaz.Length);
			Assert.AreEqual(setFoo.myBar.myBaz[0].name, deserializedSetFoo.myBar.myBaz[0].name);
			Assert.AreEqual(setFoo.myBar.myBaz[1].name, deserializedSetFoo.myBar.myBaz[1].name);
			Assert.AreEqual(setFoo.myBar.myBaz[2].name, deserializedSetFoo.myBar.myBaz[2].name);
		}

		private string Serialize(Foo f)
		{
			//Code copied from JsonConvert.SerializeObject(), with addition of trace writing
			JsonSerializer jsonSerializer = JsonSerializer.CreateDefault();
			var traceWriter = new MemoryTraceWriter();
			jsonSerializer.TraceWriter = traceWriter;

			StringBuilder sb = new StringBuilder(256);
			StringWriter sw = new StringWriter(sb, CultureInfo.InvariantCulture);
			using (JsonTextWriter jsonWriter = new JsonTextWriter(sw))
			{
				jsonWriter.Formatting = Formatting.None;
				jsonSerializer.Serialize(jsonWriter, f, typeof(Foo));
			}

			Console.Out.WriteLine("Trace output:\n{0}", traceWriter.ToString());

			return sw.ToString();
		}
	}
}
