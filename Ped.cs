﻿using Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandTheftAuto.Models
{
	internal class Ped : Entity
	{
		public Ped(IntPtr address) : base(address)
		{
		}

		public Vector2D GetWaypoint()
		{
			var radar = new IntPtr(BitConverter.ToInt64(readData, 0xD0));
			return Memory.Read<Vector2D>(radar + 0x478);
		}

		public float Health
		{
			get
			{
				return BitConverter.ToSingle(readData, 0x280);
			}
			set
			{
				Memory.Write(address + 0x280, value);
			}
		}

		public float MaxHealth
		{
			get
			{
				return BitConverter.ToSingle(readData, 0x2A0);
			}
		}

		public List<Ped> GetAttackers()
		{
			var retList = new List<Ped>();
			var attackerBase = new IntPtr(BitConverter.ToInt64(readData, 0x2A8));
			for (var i = 0; i < 3; i++)
			{
				var npc = new Ped(Memory.Read<IntPtr>(attackerBase + (i * 0x18)));
				if (npc.address == IntPtr.Zero) continue;
				if (npc.address == address) continue;
				if (npc.Health < 1.0f) continue;
				retList.Add(npc);
			}
			return retList;
		}

		public PlayerInfo GetPlayerInfo()
		{
			return new PlayerInfo(new IntPtr(BitConverter.ToInt64(readData, 0x10B8)));
		}

		public Weapon GetWeapon()
		{
			var weaponManager = new IntPtr(BitConverter.ToInt64(readData, 0x10C8));
			return new Weapon(Memory.Read<IntPtr>(weaponManager + 0x20));
		}

		public Vehicle GetVehicle()
		{
			return new Vehicle(new IntPtr(BitConverter.ToInt64(readData, 0xD28)));
		}

		public bool CanBeRagdolled
		{
			get
			{
				var btRead = readData[0x10A8];
				return (btRead & 0x20) == 0x20;
			}
			set
			{
				var btRead = readData[0x10A8];
				if (value)
				{
					if ((btRead & 0x20) != 0x20) btRead |= 0x20;
				}
				else
				{
					if ((btRead & 0x20) == 0x20) btRead ^= 0x20;
				}

				Memory.Write(address + 0x10A8, btRead);
			}
		}

		public bool HasSeatBelt
		{
			get
			{
				var btRead = readData[0x13EC];
				return (btRead & 0x1) != 0x1;
			}
			set
			{
				var btRead = readData[0x13EC];
				if (!value)
				{
					if ((btRead & 0x1) != 0x1) btRead |= 0x1;
				}
				else
				{
					if ((btRead & 0x1) == 0x1) btRead ^= 0x1;
				}

				Memory.Write(address + 0x13EC, btRead);
			}
		}

		public bool IsInVehicle
		{
			get
			{
				var btRead = readData[0x146B];
				return ((btRead >> 4) & 1) == 0;
			}
		}
	}
}
