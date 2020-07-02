using Common.Lib.Core;
using System.Collections.Generic;

namespace Common.Lib.Infrastructure
{
    public class DeleteValidation<T> where T : Entity
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
		public DeleteValidation()
		{

		}
		public DeleteValidation(bool initTrue)
		{
			this.IsSuccess = initTrue;
		}

		public DeleteValidation<TOut> Cast<TOut>() where TOut : Entity
		{
			var output = new DeleteValidation<TOut>
			{
				Entity = this.Entity as TOut,
				Validation = this.Validation
			};

			return output;
		}
	}
}
