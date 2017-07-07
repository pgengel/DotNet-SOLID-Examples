using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DotNetSolidPrinciples.OpenClose
{
	[Serializable]
	internal class InvalidInputFormatException : Exception
	{
		public InvalidInputFormatException()
		{
		}

		public InvalidInputFormatException(string message) : base(message)
		{
		}

		public InvalidInputFormatException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected InvalidInputFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
