using Common.Lib.Core;
using Common.Lib.Core.Context;
using Common.Lib.Infrastructure;
using System;
using System.Linq;

namespace Empleats.Lib.Models
{
    public class Empleat : Entity
    {
        public string Dni { get; set; }
        public string Name { get; set; }
        public string SurNames { get; set; }
        public string Job { get; set; }
        public string Email { get; set; }
        public double Salary { get; set; }
        public DateTime Antiquity { get; set; }

        public Empleat()
        {

        }

        public SaveValidation<Empleat> Save()
        {
            return base.Save<Empleat>();
        }

        public DeleteValidation<Empleat> Delete()
        {
            var deleteValidation = base.Delete<Empleat>();

            return deleteValidation;
        }

        public override ValidationResult Validate()
        {
            var output = base.Validate();

            ValidateDni(output);
            if (output.IsSuccess)
            {
                ValidateName(output);
                if (output.IsSuccess)
                {
                    ValidateSurNames(output);
                    if (output.IsSuccess)
                    {
                        ValidateJob(output);
                        if (output.IsSuccess)
                        {
                            ValidateEmail(output);
                            if (output.IsSuccess)
                            {
                                ValidateSalary(output);
                                if (output.IsSuccess)
                                {
                                    ValidateAntiquity(output);
                                }
                            }
                        }
                    }
                }
            }

            return output;
        }

        public Empleat Clone()
        {
            return Clone<Empleat>();
        }

        public override T Clone<T>()
        {
            var output = base.Clone<T>() as Empleat;

            output.Dni = this.Dni;
            output.Name = this.Name;
            output.SurNames = this.SurNames;
            output.Job = this.Job;
            output.Email = this.Email;
            output.Salary = this.Salary;
            output.Antiquity = this.Antiquity;

            return output as T;
        }

        #region Domain Validations
        public void ValidateDni(ValidationResult validationResult)
        {
            var vr = ValidateDni(this.Dni, this.Id);

            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(vr.Errors);
            }
        }

        public void ValidateExist(ValidationResult validationResult)
        {
            var vr = ValidateExist(this.Dni);

            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(vr.Errors);
            }
        }

        public void ValidateName(ValidationResult validationResult)
        {
            var validateNameResult = ValidateName(this.Name);
            if (!validateNameResult.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(validateNameResult.Errors);
            }
        }

        public void ValidateSurNames(ValidationResult validationResult)
        {
            var validateSurNamesResult = ValidateSurNames(this.SurNames);
            if (!validateSurNamesResult.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(validateSurNamesResult.Errors);
            }
        }

        public void ValidateJob(ValidationResult validationResult)
        {
            var vr = ValidateJob(this.Job);

            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(vr.Errors);
            }
        }

        public void ValidateEmail(ValidationResult validationResult)
        {
            var vr = ValidateEmail(this.Email);

            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(vr.Errors);
            }
        }

        public void ValidateSalary(ValidationResult validationResult)
        {
            var vr = ValidateSalary(this.Salary.ToString());

            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(vr.Errors);
            }
        }

        public void ValidateAntiquity(ValidationResult validationResult)
        {
            var vr = ValidateAntiquity(this.Antiquity.ToString());

            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(vr.Errors);
            }
        }
        #endregion

        #region Static Validations
        public static ValidationResult<string> ValidateDni(string dni, Guid currentId = default)
        {
            var output = new ValidationResult<string>()
            {
                IsSuccess = true
            };

            if (string.IsNullOrEmpty(dni))
            {
                output.IsSuccess = false;
                output.Errors.Add("Debe informar DNI emplead@");
            }
            else if (dni.Length < 9)
            {
                output.IsSuccess = false;
                output.Errors.Add("El DNI de emplead@ no tiene un formato correcto");
            }

            #region check duplication
            var repo = DepCon.Resolve<IRepository<Empleat>>();
            var entityWithDni = repo.QueryAll().FirstOrDefault(s => s.Dni == dni);

            if (currentId == default && entityWithDni != null)
            {
                // on create
                output.IsSuccess = false;
                output.Errors.Add("Ya existe emplead@ con este DNI");
            }
            else if (currentId != default && entityWithDni != null && entityWithDni.Id != currentId)
            {
                if (entityWithDni.Dni == dni)
                {
                    // on update
                    output.IsSuccess = false;
                    output.Errors.Add("Ya existe emplead@ con este DNI");
                }
            }
            #endregion

            if (output.IsSuccess)
                output.ValidatedResult = dni;

            return output;
        }

        public static ValidationResult<string> ValidateExist(string dni)
        {
            var output = new ValidationResult<string>()
            {
                IsSuccess = true
            };

            if (string.IsNullOrEmpty(dni))
            {
                output.IsSuccess = false;
                output.Errors.Add("El DNI no puede estar vacío");
            }
            else if (dni.Length < 9)
            {
                output.IsSuccess = false;
                output.Errors.Add("El DNI no tiene un formato correcto");
            }

            #region check existence
            var repo = DepCon.Resolve<IRepository<Empleat>>();
            var entityWithDni = repo.QueryAll().FirstOrDefault(s => s.Dni == dni);

            if (entityWithDni == null)
            {
                output.IsSuccess = false;
                output.Errors.Add($"No existe empled@ con ese DNI {dni}");
            }
            #endregion

            if (output.IsSuccess)
            {
                output.ValidatedResult = dni;
            }
            return output;
        }

        public static ValidationResult<string> ValidateName(string name)
        {
            var output = new ValidationResult<string>()
            {
                IsSuccess = true
            };

            if (string.IsNullOrEmpty(name))
            {
                output.IsSuccess = false;
                output.Errors.Add("Debe informar el nombre");
            }

            if (output.IsSuccess)
                output.ValidatedResult = name;

            return output;
        }

        public static ValidationResult<string> ValidateSurNames(string surnames)
        {
            var output = new ValidationResult<string>()
            {
                IsSuccess = true
            };

            if (string.IsNullOrEmpty(surnames))
            {
                output.IsSuccess = false;
                output.Errors.Add("Debe informar los apellidos");
            }

            if (output.IsSuccess)
                output.ValidatedResult = surnames;

            return output;
        }

        public static ValidationResult<string> ValidateJob(string job)
        {
            var output = new ValidationResult<string>()
            {
                IsSuccess = true
            };

            if (string.IsNullOrEmpty(job))
            {
                output.IsSuccess = false;
                output.Errors.Add("Debe informar el puesto de trabajo");
            }

            if (output.IsSuccess)
                output.ValidatedResult = job;

            return output;
        }

        public static ValidationResult<string> ValidateEmail(string email)
        {
            var output = new ValidationResult<string>()
            {
                IsSuccess = true
            };

            if (string.IsNullOrEmpty(email))
            {
                output.IsSuccess = false;
                output.Errors.Add("No ha introducido el correo electrónico");
            }
            else
            {
                string[] words = email.Split('@');
                if (words.Length == 1)
                {
                    output.IsSuccess = false;
                    output.Errors.Add("No ha introducido el correo correctamente");
                }

                if (!words[1].Contains('.') || !email.Contains('@'))
                {
                    output.IsSuccess = false;
                    output.Errors.Add("No ha introducido el correo correctamente");
                }
            }

            if (output.IsSuccess)
            {
                output.ValidatedResult = email;
            }
            return output;
        }

        public static ValidationResult<double> ValidateSalary(string salaryText)
        {
            var output = new ValidationResult<double>()
            {
                IsSuccess = true
            };

            var salaryNumber = 0.0;
            var isConversionOk = false;

            if (string.IsNullOrEmpty(salaryText))
            {
                output.IsSuccess = false;
                output.Errors.Add("Debe informar un salario");
            }
            else
            {
                // check format conversion
                isConversionOk = Double.TryParse(salaryText, out salaryNumber);

                if (!isConversionOk)
                {
                    output.IsSuccess = false;
                    output.Errors.Add($"No se puede convertir {salaryText} en número");
                }
            }

            if (output.IsSuccess)
                output.ValidatedResult = salaryNumber;

            return output;
        }

        public static ValidationResult<DateTime> ValidateAntiquity(string dateText)
        {
            var output = new ValidationResult<DateTime>()
            {
                IsSuccess = true
            };

            var datetime = new DateTime();
            var isConversionOk = false;

            if (string.IsNullOrEmpty(dateText))
            {
                output.IsSuccess = false;
                output.Errors.Add("Debe informar una fecha");
            }
            else
            {
                // check format conversion
                isConversionOk = DateTime.TryParse(dateText, out datetime);

                if (!isConversionOk)
                {
                    output.IsSuccess = false;
                    output.Errors.Add($"No se puede convertir {dateText} en fecha");
                }
            }

            if (output.IsSuccess)
                output.ValidatedResult = datetime;

            return output;
        }
        #endregion
    }
}
