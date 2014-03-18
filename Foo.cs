using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonNetTest
{
	public class Bar
	{
		private Baz[] myBazField;
		public Baz[] myBaz
		{
			get
			{
				return myBazField;
			}
			set { myBazField = value; }
		}

		public virtual bool ShouldSerializemyBaz()
		{
			return (myBaz != null);
		}
	}

	public class Baz
	{
		private string nameField;
		public string name
		{
			get
			{
				return nameField;
			}
			set { nameField = value; }
		}

		public virtual bool ShouldSerializename()
		{
			return (name != null);
		}
	}

	public class Foo
	{
		private Bar myBarField;
		public Bar myBar
		{
			get
			{
				return myBarField;
			}
			set { myBarField = value; }
		}

		public virtual bool ShouldSerializemyBar()
		{
			return (myBar != null);
		}
	}
}
