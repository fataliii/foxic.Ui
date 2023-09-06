﻿using System.ComponentModel.DataAnnotations;

namespace FoxicUI.ViewModels.AuthVM
{
	public class RegisterVM
	{
		[Required]
		public string FirstName { get; set; } = null!;

		[Required] 
		public string LastName { get; set; } = null!;

		[Required, DataType(DataType.EmailAddress)]
		public string Email { get; set; } = null!;

		[Required, DataType(DataType.Password)]
		public string Password { get; set; } = null!;

		[Required, DataType(DataType.Password), Compare(nameof(Password))]
		public string ConfirmPasword { get; set; } = null!;
	}
}
