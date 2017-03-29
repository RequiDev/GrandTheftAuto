using BlyadTheftAuto.GrandTheftAuto.Models.Backup;
using BlyadTheftAuto.MemorySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlyadTheftAuto.GrandTheftAuto.Models
{
	internal class Weapon
	{
		private static ProcessMemory Memory => BlyadTheftAuto.Memory;
		private byte[] _readData;
		private IntPtr _address;

		public Weapon(IntPtr address)
		{
			this._address = address;
			Update();
		}

		public void Update()
		{
			_readData = Memory.ReadByteArray(_address, 0x2A8);
		}

		public void Restore(BackupWeapon backup)
		{
			Damage = backup.Damage;
			BulletBatch = backup.BulletBatch;
			ReloadTime = backup.ReloadTime;
			Spread = backup.Spread;
			Range = backup.Range;
			Recoil = backup.Recoil;
			BatchSpread = backup.BatchSpread;
			SpinUp = backup.SpinUp;
			Spin = backup.Spin;
			MuzzleVelocity = backup.MuzzleVelocity;
		}

		public int NameHash
		{
			get
			{
				return BitConverter.ToInt32(_readData, 0x10);
			}
		}

		public float Damage
		{
			get
			{
				return BitConverter.ToSingle(_readData, 0x98);
			}
			set
			{
				Memory.Write(_address + 0x98, value);
			}
		}

		public int BulletBatch
		{
			get
			{
				return BitConverter.ToInt32(_readData, 0x100);
			}
			set
			{
				Memory.Write(_address + 0x100, value);
			}
		}

		public float ReloadTime
		{
			get
			{
				return BitConverter.ToSingle(_readData, 0x114);
			}
			set
			{
				Memory.Write(_address + 0x114, value);
			}
		}

		public float Spread
		{
			get
			{
				return BitConverter.ToSingle(_readData, 0x5C);
			}
			set
			{
				Memory.Write(_address + 0x5C, value);
			}
		}

		public float BatchSpread
		{
			get
			{
				return BitConverter.ToSingle(_readData, 0x104);
			}
			set
			{
				Memory.Write(_address + 0x104, value);
			}
		}

		public float Range
		{
			get
			{
				return BitConverter.ToSingle(_readData, 0x25C);
			}
			set
			{
				Memory.Write(_address + 0x25C, value);
			}
		}

		public float Recoil
		{
			get
			{
				return BitConverter.ToSingle(_readData, 0x2A4);
			}
			set
			{
				Memory.Write(_address + 0x2A4, value);
			}
		}

		public float SpinUp
		{
			get
			{
				return BitConverter.ToSingle(_readData, 0x124);
			}
			set
			{
				Memory.Write(_address + 0x124, value);
			}
		}

		public float Spin
		{
			get
			{
				return BitConverter.ToSingle(_readData, 0x128);
			}
			set
			{
				Memory.Write(_address + 0x128, value);
			}
		}

		public float MuzzleVelocity
		{
			get
			{
				return BitConverter.ToSingle(_readData, 0xFC);
			}
			set
			{
				Memory.Write(_address + 0xFC, value);
			}
		}
	}
}
