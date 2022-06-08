﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoGreenService.Models
{
    public class Settings
    {
		public DatabaseSettings Database { get; set; }
		public string ClientUrl { get; set; }

		public class DatabaseSettings
		{
			public string[] Urls { get; set; }
			public string DatabaseName { get; set; }
		}
	}
}
