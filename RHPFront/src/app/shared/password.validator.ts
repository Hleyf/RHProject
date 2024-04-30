import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export const passwordValidator : ValidatorFn = (control: AbstractControl): ValidationErrors | null  => {
    return control.value.password === control.value.confirmPassword ? null : { NoMatch: true };
}
;