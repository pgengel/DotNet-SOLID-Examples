﻿using System;
using System.Runtime.Serialization;

namespace DotNetSolidPrinciples.SingleResponsibility
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