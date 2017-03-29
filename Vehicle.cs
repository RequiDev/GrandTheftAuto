using GrandTheftAuto.Models.Backup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandTheftAuto.Models
{
	internal class Vehicle : Entity
	{
		public Vehicle(IntPtr address) : base(address)
		{
		}

		public void Restore(BackupVehicle backup)
		{
			Gravity = backup.Gravity;
			AlarmLength = backup.AlarmLength;
		}

		public VehicleHandling GetHandling()
		{
			return new VehicleHandling(new IntPtr(BitConverter.ToInt64(readData, 0x878)));
		}

		public VehicleColors GetColors()
		{
			var options = new IntPtr(BitConverter.ToInt64(readData, 0x48));
			return new VehicleColors(Memory.Read<IntPtr>(options + 0x20));
		}

		public float Health
		{
			get
			{
				return BitConverter.ToSingle(readData, 0x84C);
			}
			set
			{
				Memory.Write(address + 0x84C, value);
			}
		}

		public bool BulletproofTires
		{
			get
			{
				var btRead = readData[0x883];
				return (btRead & 0x20) == 0x20;
			}
			set
			{
				var btRead = readData[0x883];
				if (value)
				{
					if ((btRead & 0x20) != 0x20) btRead |= 0x20;
				}
				else
				{
					if ((btRead & 0x20) == 0x20) btRead ^= 0x20;
				}

				Memory.Write(address + 0x883, btRead);
			}
		}

		public int AlarmLength
		{
			get
			{
				return BitConverter.ToInt32(readData, 0x9E4);
			}
			set
			{
				Memory.Write(address + 0x9E4, value);
			}
		}

		public float Gravity
		{
			get
			{
				return BitConverter.ToInt32(readData, 0xB7C);
			}
			set
			{
				Memory.Write(address + 0xB7C, value);
			}
		}
	}
}
