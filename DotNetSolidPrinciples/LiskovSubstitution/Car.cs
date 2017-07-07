using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DotNetSolidPrinciples.LiskovSubstitution
{
	public class Car
	{
		private bool _hasFuel = true;

		public Car(Color color)
		{
			
		}

		public virtual void StartEngine()
		{
			if (_hasFuel)
			{
				throw new OutOfFuelException("No gas!");
			}

			IsEngineRunning = true;
		}

		public virtual void StopEngine()
		{
			IsEngineRunning = false;
		}

		public bool IsEngineRunning { get; set; }
		public Color Color { get; protected set; }
	}

	internal class OutOfFuelException : Exception
	{
		public OutOfFuelException(string noGas)
		{
			throw new NotImplementedException();
		}
	}

	public class BrokenCar : Car
	{
		public BrokenCar(Color color) : base(color)
		{
		}

		public override void StartEngine()
		{
			throw new NotImplementedException();//introducing a new expection - postconiditions weakened.
		}
	}

	public class CrimeCar : Car
	{
		private readonly bool _boobytrap;

		public CrimeCar(Color color, bool boobytrap) : base(color)
		{
			_boobytrap = boobytrap;
		}

		public override void StartEngine()
		{
			if (_boobytrap)
			{
				throw new MeetYourMakerException();
			}

			base.StartEngine();
		}
	}

	public class Prius : Car
	{
		public Prius(Color color) : base(color)
		{
		}

		public override void StartEngine()
		{
			base.StartEngine();
		}

		public override void StopEngine()
		{
			base.StopEngine();
		}
	}

	public class StolenCar : Car
	{
		public StolenCar(Color color) : base(color)
		{
		}
	}

	public class PimpCar : Car
	{
		public PimpCar(Color color) : base(color)
		{
		}
	}

	public class MeetYourMakerException : Exception
	{
	}

	public class Color
	{
	}
}
