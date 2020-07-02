using Common.Lib.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Lib.Infrastructure
{
    public class SaveValidation<T> where T : Entity
    {
		public ValidationResult Validation { get; set; } = new ValidationResult();

		public bool IsSuccess
		{
			get
			{
				return Validation.IsSuccess;
			}
			set
			{
				Validation.IsSuccess = value;
			}
		}
		public string AllErrors
		{
			get
			{
				return Validation.AllErrors;
			}
		}

		public T Entity { get; set; }

		//constructor para que sea true por defecto
		public SaveValidation()
		{

		}
		public SaveValidation(bool initTrue)
		{
			this.IsSuccess = initTrue;
		}

		public SaveValidation<TOut> Cast<TOut>() where TOut : Entity
		{
			var output = new SaveValidation<TOut>
			{
				Entity = this.Entity as TOut,
				Validation = this.Validation
			};

			return output;
		}
	}
}
