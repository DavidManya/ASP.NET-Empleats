using Common.Lib.Core.Context.Interfaces;
using Common.Lib.Infrastructure;
using Common.Lib.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Lib.Core
{
    public abstract class Entity
    {
        #region Statics

        public static IDependencyContainer DepCon { get; set; }

        #endregion

        public Guid Id { get; set; }

        public ValidationResult CurrentValidation { get; private set; }

        public virtual SaveValidation<T> Save<T>() where T : Entity
        {
            var output = new SaveValidation<T>();

            CurrentValidation = Validate();

            if (CurrentValidation.ValidationSuccesful)
            {
                var repo = DepCon.Resolve<IRepository<T>>();

                if (this.Id == Guid.Empty)
                {
                    output = repo.Add(this as T);
                }
                else
                {
                    output = repo.Update(this as T);
                }
            }

            output.Validation = CurrentValidation;

            return output;
        }

        public virtual DeleteValidation<T> Delete<T>() where T : Entity
        {
            var output = new DeleteValidation<T>();

            CurrentValidation = Validate();

            if (CurrentValidation.ValidationSuccesful)
            {
                var repo = DepCon.Resolve<IRepository<T>>();

                output = repo.Delete(this as T);
            }

            output.Validation = CurrentValidation;

            return output;
        }

        public virtual ValidationResult Validate()
        {
            var output = new ValidationResult()
            {
                ValidationSuccesful = true
            };

            return output;
        }

        public virtual T Clone<T>() where T : Entity, new()
        {
            var output = new T();
            output.Id = this.Id;
            return output;
        }
    }
}
