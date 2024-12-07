import { Injectable } from '@angular/core';
import { FormGroup, ValidatorFn, FormBuilder, AsyncValidatorFn, FormControl, Validators } from '@angular/forms';

export interface ConfigFormControl {
  id: string;
  name: string;
  formState: any;
  validatorOrOpts?: ConfigValidatorFn[];
  asyncValidator?: ConfigAsyncValidatorFn[];
  configFormControl?: ConfigFormControl[];
}

export interface ConfigValidatorFn {
  validationKey?: string;
  validatorOpt?: ValidatorFn;
  validationMessage?: string;
  required?: boolean;
  maxLength?: number;
  minLength?: number;
  max?: number;
  min?: number;
  isEmail?: boolean;
  requiredTrue?: boolean;
  isNotNull?: boolean;
  pattern?: string;
}

export interface ConfigAsyncValidatorFn {
  validatorOpt: AsyncValidatorFn;
  validationMessage: string;
}

export interface FormError {
  [key: string]: string
}

export interface ValidationMessage {
  [key: string]: { [key: string]: string }
}

@Injectable({
  providedIn: 'root'
})
export class FormBuilderService {
  formGroup: FormGroup = new FormGroup({});
  formErrors: FormError = {}; // {[key: string]: string}[];
  validationMessages: ValidationMessage = {};
  formControls!: ConfigFormControl[];
  validatorFormGroup: ValidatorFn[] = [];

  messageErrorGenerics: { [key: string]: string } = {
    required: `{0} es requerido`,
    requiredtrue: `{0} es requerido`,
    email: `{0} no tiene un formato valido`,
    min: `{0} requiere un valor mínimo de {1}`,
    max: `{0} requiere un valor máximo de {1}`,
    minlength: `{0} debe tener un mínimo de {1} caracteres`,
    maxlength: `{0} debe tener un máximo de {1} caracteres`,
    nullvalidator: `{0} es requerido`,
    pattern: `{0} no tiene el formato correcto`
  };

  constructor(private formBuilder: FormBuilder) { }

  initFormGroup() {
    this.formGroup = this.loadFormGroup(this.formControls);
  }

  loadFormGroup(formControls: ConfigFormControl[]): FormGroup {
    const formGroupTemp = this.formBuilder.group({});

    formControls.forEach(item => {

      const validators: ValidatorFn[] = [];

      this.formErrors[item.id] = '';

      this.validationMessages[item.id] = {};

      if (item.validatorOrOpts) {
        item.validatorOrOpts.forEach(validatorOpt => {

          if (validatorOpt.validatorOpt) {
            validators.push(validatorOpt.validatorOpt);
          }

          if (validatorOpt.required) {
            validators.push(Validators.required);
            this.validationMessages[item.id][`required`]
              = this.messageErrorGenerics[`required`]
                .replace('{0}', !item.name ? item.id : item.name);
          }

          if (validatorOpt.maxLength) {
            validators.push(Validators.maxLength(validatorOpt.maxLength));
            this.validationMessages[item.id][`maxlength`]
              = this.messageErrorGenerics[`maxlength`]
                .replace('{0}', !item.name ? item.id : item.name).replace('{1}', validatorOpt.maxLength.toString());
          }

          if (validatorOpt.minLength) {
            validators.push(Validators.minLength(validatorOpt.minLength));
            this.validationMessages[item.id][`minlength`]
              = this.messageErrorGenerics[`minlength`]
                .replace('{0}', !item.name ? item.id : item.name).replace('{1}', validatorOpt.minLength.toString());
          }
          if (validatorOpt.max) {
            validators.push(Validators.max(validatorOpt.max));
            this.validationMessages[item.id][`max`]
              = this.messageErrorGenerics[`max`]
              .replace('{0}', !item.name ? item.id : item.name).replace('{1}', validatorOpt.max.toString());
          }
          if (validatorOpt.min) {
            validators.push(Validators.min(validatorOpt.min));
            this.validationMessages[item.id][`min`]
              = this.messageErrorGenerics[`min`]
              .replace('{0}', !item.name ? item.id : item.name).replace('{1}', validatorOpt.min.toString());
          }
          if (validatorOpt.isEmail) {
            validators.push(Validators.email);
            this.validationMessages[item.id][`email`]
              = this.messageErrorGenerics[`email`].replace('{0}', !item.name ? item.id : item.name);
          }
          if (validatorOpt.requiredTrue) {
            validators.push(Validators.requiredTrue);
            this.validationMessages[item.id][`requiredtrue`]
              = this.messageErrorGenerics[`requiredtrue`].replace('{0}', !item.name ? item.id : item.name);
          }
          if (validatorOpt.isNotNull) {
            validators.push(Validators.nullValidator);
            this.validationMessages[item.id][`nullvalidator`]
              = this.messageErrorGenerics[`nullvalidator`].replace('{0}', !item.name ? item.id : item.name);
          }
          if (validatorOpt.pattern) {
            validators.push(Validators.pattern(validatorOpt.pattern));
            this.validationMessages[item.id][`pattern`]
              = this.messageErrorGenerics[`pattern`].replace('{0}', !item.name ? item.id : item.name);
          }

          if (validatorOpt.validationKey)
            this.validationMessages[item.id][validatorOpt.validationKey] = validatorOpt.validationMessage ?? '';
        });
      }

      formGroupTemp.addControl(item.id, new FormControl(item.formState, validators));
    });

    formGroupTemp.setValidators(this.validatorFormGroup);

    formGroupTemp.valueChanges.subscribe(() => this.onValueChanged(formGroupTemp));

    this.onValueChanged(formGroupTemp);

    return formGroupTemp;
  }



  onValueChanged(form: FormGroup) {

    if (!form) { return; }

    // for (const field in this.formErrors) {
    for (const field of Object.keys(this.formErrors)) {

      this.formErrors[field] = '';

      const control = form.get(field);

      if (control && control.dirty && !control.valid) {

        console.log(`Field: Invalid ` + field);

        const messages = this.validationMessages[field];

        for (const key of Object.keys(control.errors!)) {

          if (!messages[key]) {
            console.log(`Mensaje no parametrizado. Field: ` + field + `. Key: ` + key);
          }
          this.formErrors[field] += messages[key] + ' ';
        }
      }
    }
  }
}

