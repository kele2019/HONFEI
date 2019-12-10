using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.Leave.Entity
{
	public class UserEntity
	{
		public UserEntity() { }
		public UserEntity(string EXT04) { this.EXT04 = EXT04; }
		public string EXT04 { get; set; }
	}
}